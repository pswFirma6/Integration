using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

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

        public bool PostOffer(Offer offer)
        {
            if (CheckEndDate(offer))
            {
                offer.Posted = true;
                offerRepository.Save();
            }
            return offer.Posted;
        }

        public void AddOffer(Offer offer)
        {
            offerRepository.Add(offer);
            offerRepository.Save();
        }
    }
}
