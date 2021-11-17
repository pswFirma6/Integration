using Integration_library.Pharmacy.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration_library.Pharmacy.Service;
using Newtonsoft.Json;

namespace IntegrationAPI.Controller
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PharmacyOfferController : ControllerBase
    {
        private PharmacyOfferService service;

        public PharmacyOfferController(DatabaseContext context)
        {
            service = new PharmacyOfferService(context);
        }

        [HttpGet]
        [Route("notifications")]
        public List<PharmacyOffer> ReceiveNotification()
        {
            
            receiveNotification();
            return service.GetPharmacyOffers();
        }

        private void receiveNotification()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("demo-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
            };

            channel.BasicConsume("demo-queue", false, consumer);
        }
        
    }
}
