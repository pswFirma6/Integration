using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Model
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {

        }

        public DatabaseContext()
        {

        }

        public DbSet<MedicationConsumption> MedicationConsumptions { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<PharmacyOffer> PharmacyOffers { get; set; }

    }
}
