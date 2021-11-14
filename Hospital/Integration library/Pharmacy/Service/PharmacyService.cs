using Integration_library.Pharmacy.Model;
using Integration_library.Pharmacy.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Service
{
    public class PharmacyService
    {
        private const int APIKEYLENGTH = 16;

        private PharmacyRepository repository;
    
        public PharmacyService(PharmacyDbContext context)
        {
            repository = new PharmacyRepository(context);
        }

        public List<string> GetPharmacyNames()
        {
            List<string> pharmacyNames = new List<string>();
            repository.GetPharmacies().ForEach(pharmacy => pharmacyNames.Add(pharmacy.PharmacyName));
            return pharmacyNames;
        }

        public void AddPharmacy(Model.Pharmacy pharmacy)
        {
            pharmacy.ApiKey = generateApiKey();
            repository.AddPharmacy(pharmacy);
        }

        private String generateApiKey()
        {
            const string src = "abcdefghijklmnopqrstuvwxyz0123456789";
            var sb = new StringBuilder();
            Random RNG = new Random();
            for (var i = 0; i < APIKEYLENGTH; i++)
            {
                var c = src[RNG.Next(0, src.Length)];
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
