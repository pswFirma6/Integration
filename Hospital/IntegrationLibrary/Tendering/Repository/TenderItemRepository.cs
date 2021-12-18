using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.Repository
{
    public class TenderItemRepository : Repo<TenderItem>, ITenderItemRepository
    {
        public TenderItemRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
