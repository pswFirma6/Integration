using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace Integration_library.Pharmacy.Service
{
    public class OfferService
    {
        private IOfferRepository offerRepository;

        public OfferService(IOfferRepository IRepository)
        {
            offerRepository = IRepository;
        }

        public List<Offer> GetOffers()
        {
            List<Offer> offers = new List<Offer>();
            List<Offer> all = offerRepository.GetAll();
            foreach (Offer offer in all)
            {
                if(CheckEndDate(offer))
                {
                    offers.Add(offer);
                }
            }
            return offers;
        }

        public bool CheckEndDate(Offer offer)
        {
            return DateTime.Compare(offer.EndDate, DateTime.Now) > 0;
        }

        public void PostOffer(Offer offer)
        {
            foreach(Offer o in offerRepository.GetAll())
            {
                if(o.Id == offer.Id)
                {
                    o.Posted = true;
                    offerRepository.Save();
                    break;
                }
            }
        }

        public void AddOffer(Offer offer)
        {
            offerRepository.Add(offer);
            offerRepository.Save();
        }

        public void ReceiveOffer()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare("offer-exchange", type: ExchangeType.Fanout);
            channel.QueueDeclare("offer-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueBind("offer-queue", "offer-exchange", string.Empty);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            };

            channel.BasicConsume("offer-queue", true, consumer);

        }
    }
}
