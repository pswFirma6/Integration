using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationLibrary.Tendering.Repository
{
    public class TenderRepository : Repo<Tender>, ITenderRepository
    {
        DatabaseContext _context = new DatabaseContext();
        private DbSet<Tender> table;

        public TenderRepository(DatabaseContext context) : base(context)
        {
        }

        public List<Tender> GetTenders()
        {
            table = _context.Set<Tender>();
            var tenders = table.Include(tender => tender.TenderItems).ToList();
            return tenders;
        }
    }
}
