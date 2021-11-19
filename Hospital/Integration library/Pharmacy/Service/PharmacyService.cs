using Integration_library.Pharmacy.IRepository;
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

        private IPharmacyRepository repository;
    
        public PharmacyService(IPharmacyRepository iRepository)
        {
            repository = iRepository;
        }
        public PharmacyService()
        {
            
        }
        public List<string> GetPharmacyNames()
        {
            List<string> pharmacyNames = new List<string>();
            repository.GetAll().ForEach(pharmacy => pharmacyNames.Add(pharmacy.PharmacyName));
            return pharmacyNames;
        }

        public void AddPharmacy(Model.Pharmacy pharmacy)
        {
            pharmacy.ApiKey = GenerateApiKey();
            repository.Add(pharmacy);
            repository.Save();
        }

        private String GenerateApiKey()
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

        public String GetPharmacyApiKey(String pharmacyName)
        {
            Model.Pharmacy pharmacy = repository.GetAll().Find(pharmacy => pharmacyName == pharmacy.PharmacyName);
            return pharmacy.ApiKey;
        }

        public List<Model.Pharmacy> GetPharmacies()
        {
            return repository.GetAll();
        }


    }
}
