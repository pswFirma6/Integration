using IntegrationLibrary.Partnership.IRepo;
using IntegrationLibrary.Partnership.Model;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Partnership.Repository
{
    public class OfferRepository : Repo<Offer>, IOfferRepository
    {
        public OfferRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
