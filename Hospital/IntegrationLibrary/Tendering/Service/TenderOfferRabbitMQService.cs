using IntegrationLibrary.Exceptions;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Repository;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tendering.Service
{
    public class TenderOfferRabbitMQService : BackgroundService
    {
        private readonly DatabaseContext databaseContext = new DatabaseContext();

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            IConnection connection;
            IModel channel;
            IPharmacyRepository pharmacyRepository = new PharmacyRepository(databaseContext);
            ITenderOfferRepository tenderOfferRepository = new TenderOfferRepository(databaseContext);
            PharmacyService pharmacyService = new PharmacyService(pharmacyRepository);
            TenderOfferService offerService = new TenderOfferService(tenderOfferRepository);
            List<Pharmacy.Model.Pharmacy> pharmacies = pharmacyService.GetPharmacies();

            foreach(string apiKey in pharmacies.Select(pharmacy => pharmacy.PharmacyConnectionInfo.ApiKey))
            {
                if (apiKey == null)
                {
                    throw new DomainNotFoundException("No connected pharmacy!");
                }
                var factory = new ConnectionFactory
                {
                    HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                    UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest",
                    Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest",
                };

                connection = factory.CreateConnection();
                channel = connection.CreateModel();

                channel.ExchangeDeclare("tender-offer-exchange-" + apiKey, type: ExchangeType.Fanout);
                channel.QueueDeclare("tender-offer-queue-" + apiKey,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                channel.QueueBind("tender-offer-queue-" + apiKey, "tender-offer-exchange-" + apiKey, string.Empty);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, e) =>
                {
                    byte[] body = e.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                    if(jsonMessage == null)
                    {
                        throw new DomainNotFoundException("RabbitMQ server isn't receiving valid messages");
                    }
                    TenderOfferDto message;
                    message = JsonConvert.DeserializeObject<TenderOfferDto>(jsonMessage);
                    offerService.AddTenderOffer(message);
                };

                channel.BasicConsume(queue: "tender-offer-queue-" + apiKey,
                                        autoAck: true,
                                        consumer: consumer);
            }

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
