using IntegrationLibrary.Pharmacy.DTO;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using RestSharp;
using System;
using System.Collections.Generic;


namespace IntegrationLibrary.Pharmacy.Service
{
    public class PharmacyService
    {
        private IPharmacyRepository repository;
    
        public PharmacyService(IPharmacyRepository iRepository)
        {
            repository = iRepository;
        }

        public void AddPharmacy(PharmacyInfo info)
        {
            Model.Pharmacy newPharmacy = new Model.Pharmacy(info);
            repository.Add(newPharmacy);
            repository.Save();

        }

        public List<string> GetPharmacyNames()
        {
            List<string> pharmacyNames = new List<string>();
            repository.GetAll().ForEach(pharmacy => pharmacyNames.Add(pharmacy.PharmacyName));
            return pharmacyNames;
        }

        public Model.Pharmacy GetPharmacyByName(String pharmacyName)
        {
            return repository.GetAll().Find(pharmacy => pharmacyName == pharmacy.PharmacyName);
        }

        public List<PharmacyMedicineAvailabilityDTO> CheckPharmacyMedicines(MedicineDto medicine)
        {
            List<PharmacyMedicineAvailabilityDTO> pharmacies = new List<PharmacyMedicineAvailabilityDTO>();
            foreach(Model.Pharmacy pharmacy in repository.GetAll())
            {
                bool isAvailable = PostRequest(pharmacy.PharmacyConnectionInfo.Url, medicine);
                if (isAvailable)
                {
                    pharmacies.Add(new PharmacyMedicineAvailabilityDTO { PharmacyName = pharmacy.PharmacyName, IsAvailable = isAvailable });
                }
            }
            return pharmacies;
        }

        public bool CheckMedicineOfCertainPharmacy(CheckAvailabilityDto availability)
        {
            foreach(Model.Pharmacy pharmacy in repository.GetAll())
            {
                if (pharmacy.PharmacyName.Equals(availability.PharmacyName))
                {
                    return PostRequest(pharmacy.PharmacyConnectionInfo.Url, availability.Medicine);
                }
            }
            return false;
        }

        private bool PostRequest(string url, MedicineDto medicine)
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

        public void OrderFromCertainPharmacy(CheckAvailabilityDto order)
        {
            foreach(Model.Pharmacy pharmacy in GetPharmacies())
            {
                if(pharmacy.PharmacyName == order.PharmacyName)
                {
                    OrderMedicine(pharmacy.PharmacyConnectionInfo.Url, order.Medicine);
                }
            }
        }

        private void OrderMedicine(string url, MedicineDto medicine)
        {
            var client = new RestClient(url);
            var request = new RestRequest("/orderMedicine");
            request.AddJsonBody(medicine);
            var response = client.Post(request);
        }

        public void EditPharmacy(Model.Pharmacy pharmacy)
        {
            repository.Update(pharmacy);
            repository.Save();
        }

        public void AddPictureToPharmacy(string pharmacyName, string pharmacyPicture)
        {
            Model.Pharmacy pharmacy = GetPharmacyByName(pharmacyName);
            pharmacy.SetPharmacyPicture(pharmacyPicture);
            repository.Update(pharmacy);
            repository.Save();
        }


    }
}
