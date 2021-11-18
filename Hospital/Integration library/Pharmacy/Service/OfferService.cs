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
            foreach(Offer offer in offerRepository.GetAll())
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
            return offer.EndDate < DateTime.Now;
        }

        public bool PostOffer(Offer offer)
        {
            if (CheckEndDate(offer))
            {
                offer.Posted = true;
            }
            return offer.Posted;
        }
    }
}
