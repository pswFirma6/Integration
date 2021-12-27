using IntegrationLibrary.Partnership.IRepo;
using IntegrationLibrary.Partnership.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Partnership.Service
{
    public class OfferService
    {
        private readonly IOfferRepository offerRepository;

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
                if (CheckEndDate(offer)) offers.Add(offer);
            }
            return offers;
        }

        public bool CheckEndDate(Offer offer)
        {
            return offer.CheckEndDate();

        }

        public void PostOffer(Offer o)
        {

            Offer offer = offerRepository.FindById(o.Id);
            offer.PostOffer();
            offerRepository.Save();
        }

        public void AddOffer(Offer offer)
        {
            offerRepository.Add(offer);
            offerRepository.Save();
        }
    }
}
