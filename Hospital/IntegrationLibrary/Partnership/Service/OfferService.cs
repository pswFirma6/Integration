using IntegrationLibrary.Partnership.IRepo;
using IntegrationLibrary.Partnership.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Partnership.Service
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
                if (CheckEndDate(offer))
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
            foreach (Offer o in offerRepository.GetAll())
            {
                if (o.Id == offer.Id)
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
    }
}
