using IntegrationLibrary.Exceptions;
using IntegrationLibrary.Partnership.IRepo;
using IntegrationLibrary.Partnership.Model;
using IntegrationLibrary.Partnership.Repository;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrationLibrary.Partnership.Service
{
    public class RabbitMQService : BackgroundService
    {
          IConnection connection;
          IModel channel;
          private readonly DatabaseContext databaseContext = new DatabaseContext();
          private OfferService service;

          public override Task StartAsync(CancellationToken cancellationToken)
          {
            IOfferRepository repository = new OfferRepository(databaseContext);
            service = new OfferService(repository);

            var factory = new ConnectionFactory
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest",
                Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest",
            };
            try
            {
                connection = factory.CreateConnection();
            } catch
            {
                throw new DomainNotFoundException("RabbitMQ server refuses to connect!");
            }
            channel = connection.CreateModel();

            channel.ExchangeDeclare("offer-exchange", type: ExchangeType.Fanout);
            channel.QueueDeclare("offer-queue",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            channel.QueueBind("offer-queue", "offer-exchange", string.Empty);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, e) =>
            {
                byte[] body = e.Body.ToArray();
                var jsonMessage = Encoding.UTF8.GetString(body);
                Offer message;
                message = JsonConvert.DeserializeObject<Offer>(jsonMessage);
                service.AddOffer(message);
            };

            channel.BasicConsume(queue: "offer-queue",
                                    autoAck: true,
                                    consumer: consumer);

            return base.StartAsync(cancellationToken);
          }

          public override Task StopAsync(CancellationToken cancellationToken)
          {
              channel.Close();
              connection.Close();
              return base.StopAsync(cancellationToken);
          }

          protected override Task ExecuteAsync(CancellationToken stoppingToken)
          {
              return Task.CompletedTask;
          }
      }
    
}
