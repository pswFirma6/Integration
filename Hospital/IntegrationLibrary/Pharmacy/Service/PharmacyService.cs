using IntegrationLibrary.Pharmacy.DTO;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Pharmacy.Service
{
    public class PharmacyService
    {
        private const int APIKEYLENGTH = 16;
        private string server = "https://localhost:44377";
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

        public List<PharmacyMedicineAvailabilityDTO> CheckPharmacyMedicines(MedicineDTO medicine)
        {
            List<PharmacyMedicineAvailabilityDTO> pharmacies = new List<PharmacyMedicineAvailabilityDTO>();
            foreach(Model.Pharmacy pharmacy in repository.GetAll())
            {
                bool isAvailable = PostRequest(server, medicine);
                pharmacies.Add(new PharmacyMedicineAvailabilityDTO { PharmacyName = pharmacy.PharmacyName, IsAvailable = isAvailable });
            }
            return pharmacies;
        }

        public bool CheckMedicineOfCertainPharmacy(CheckAvailabilityDTO availability)
        {
            foreach(Model.Pharmacy pharmacy in repository.GetAll())
            {
                if (pharmacy.PharmacyName.Equals(availability.PharmacyName))
                {
                    return PostRequest(server, availability.Medicine);
                }
            }
            return false;
        }

        private bool PostRequest(string url, MedicineDTO medicine)
        {
            var client = new RestClient(url);
            var request = new RestRequest("/checkMedicine");
            request.AddJsonBody(medicine);
            var response = client.Post(request);
            return Boolean.Parse(response.Content);
        }

        public List<Model.Pharmacy> GetPharmacies()
        {
            return repository.GetAll();
        }

        public void OrderFromCertainPharmacy(CheckAvailabilityDTO order)
        {
            foreach(Model.Pharmacy pharmacy in GetPharmacies())
            {
                if(pharmacy.PharmacyName == order.PharmacyName)
                {
                    OrderMedicine(server, order.Medicine);
                }
            }
        }

        private void OrderMedicine(string url, MedicineDTO medicine)
        {
            var client = new RestClient(url);
            var request = new RestRequest("/orderMedicine");
            request.AddJsonBody(medicine);
            var response = client.Post(request);
        }


    }
}
