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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tendering.Service
{
    public class TenderOfferRabbitMQService : BackgroundService
    {
        IConnection connection;
        IModel channel;
        private readonly DatabaseContext databaseContext = new DatabaseContext();
        private PharmacyService pharmacyService;
        private TenderOfferService offerService;

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            IPharmacyRepository pharmacyRepository = new PharmacyRepository(databaseContext);
            ITenderOfferRepository tenderOfferRepository = new TenderOfferRepository(databaseContext);
            pharmacyService = new PharmacyService(pharmacyRepository);
            offerService = new TenderOfferService(tenderOfferRepository);
            List<Pharmacy.Model.Pharmacy> pharmacies = pharmacyService.GetPharmacies();

            foreach(Pharmacy.Model.Pharmacy pharmacy in pharmacies)
            {
                var factory = new ConnectionFactory
                {
                    HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                    UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest",
                    Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest",
                };

                connection = factory.CreateConnection();
                channel = connection.CreateModel();

                channel.ExchangeDeclare("tender-offer-exchange-" + pharmacy.ApiKey, type: ExchangeType.Fanout);
                channel.QueueDeclare("tender-offer-queue-" + pharmacy.ApiKey,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                channel.QueueBind("tender-offer-queue-" + pharmacy.ApiKey, "tender-offer-exchange-" + pharmacy.ApiKey, string.Empty);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, e) =>
                {
                    byte[] body = e.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                    TenderOfferDto message;
                    message = JsonConvert.DeserializeObject<TenderOfferDto>(jsonMessage);
                    offerService.AddTenderOffer(message);
                };

                channel.BasicConsume(queue: "tender-offer-queue-" + pharmacy.ApiKey,
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
