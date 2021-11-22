using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Repository
{
    public class OfferRepository : Repo<Model.Offer>, IOfferRepository
    {
        public OfferRepository(DatabaseContext context) : base(context)
        {
        }
    }
    }
