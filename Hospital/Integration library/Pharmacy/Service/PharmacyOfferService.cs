using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using Integration_library.Pharmacy.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Service
{
    public class PharmacyOfferService
    {
        private IPharmacyOfferRepository repository;

        public PharmacyOfferService(DatabaseContext context)
        {
            repository = new PharmacyOfferRepository(context);
        }

        public List<PharmacyOffer> GetPharmacyOffers()
        {
            return repository.GetAll();
        }

        public void SavePharmacyOffer(PharmacyOffer pharmacyOffer)
        {
            repository.Add(pharmacyOffer);
            repository.Save();
        }
    }
}
