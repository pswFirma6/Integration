using IntegrationLibrary.Partnership.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Model
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
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<PharmacyComment> PharmacyComments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;port=5432;database=integrationdb;username=root;password=root");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
