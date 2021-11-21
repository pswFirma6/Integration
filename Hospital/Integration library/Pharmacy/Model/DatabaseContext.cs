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
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;port=5432;database=integrationdb;username=root;password=root");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
