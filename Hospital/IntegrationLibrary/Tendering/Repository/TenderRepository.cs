using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.Repository
{
    public class TenderRepository : Repo<Tender>, ITenderRepository
    {
        public TenderRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
