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
    public class TenderOfferRepository : Repo<TenderOffer>, ITenderOfferRepository
    {
        readonly DatabaseContext _context = new DatabaseContext();

        public TenderOfferRepository(DatabaseContext context) : base(context)
        {
        }

        public List<TenderOffer> GetTenderOffers()
        {
            DbSet<TenderOffer>  table = _context.Set<TenderOffer>();
            var offers = table.Include(offer => offer.OfferItems).ToList();
            return offers;
        }
    }
}
