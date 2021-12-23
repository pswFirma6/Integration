using IntegrationLibrary.Partnership.Model;
using IntegrationLibrary.Tendering.Model;
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
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<TenderItem> TenderItems { get; set; }
        public DbSet<TenderOffer> TenderOffers { get; set; }
        public DbSet<TenderOfferItem> TenderOfferItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TenderItem>()
                .HasOne<Tender>()
                .WithMany()
                .HasForeignKey(item => item.TenderId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(CreateConnectionStringFromEnvironment());
            base.OnConfiguring(optionsBuilder);
        }

        private static string CreateConnectionStringFromEnvironment()
        {
            var server = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "5432";
            var database = Environment.GetEnvironmentVariable("DATABASE_SCHEMA") ?? "integrationdb";
            var user = Environment.GetEnvironmentVariable("DATABASE_USERNAME") ?? "root";
            var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "root";
            var integratedSecurity = Environment.GetEnvironmentVariable("DATABASE_INTEGRATED_SECURITY") ?? "true";
            var pooling = Environment.GetEnvironmentVariable("DATABASE_POOLING") ?? "true";

            string retVal = "Server=" + server + ";Port=" + port + ";Database=" + database + ";User ID=" + user + ";Password=" + password + ";Integrated Security=" + integratedSecurity + ";Pooling=" + pooling + ";";
            return retVal;
        }
    }
}
