using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.Repository
{
    public class TenderOfferItemRepository : Repo<TenderOfferItem>, ITenderOfferItemRepository
    {
        public TenderOfferItemRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
