using IntegrationLibrary.Exceptions;
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
            Model.Pharmacy pharmacy = repository.GetAll().Find(ph => pharmacyName == ph.PharmacyName);
            if(pharmacy == null)
            {
                throw new DomainNotFoundException("Pharmacy by name: " + pharmacyName + " doesn't exist!");
            }
            return pharmacy;
        }

        public List<MedicineAvailabilityDto> CheckPharmacyMedicines(Medicine medicine)
        {
            List<MedicineAvailabilityDto> pharmacies = new List<MedicineAvailabilityDto>();
            foreach(Model.Pharmacy pharmacy in repository.GetAll())
            {
                bool isAvailable = SendRequestToPharmacy(pharmacy.PharmacyConnectionInfo.Url, medicine);
                if (isAvailable)
                {
                    pharmacies.Add(new MedicineAvailabilityDto { PharmacyName = pharmacy.PharmacyName, IsAvailable = isAvailable });
                }
            }
            return pharmacies;
        }

        private bool SendRequestToPharmacy(string url, Medicine medicine)
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

        public EmailDto GetPharmacyEmailByName(string pharmacyName)
        {
            return new EmailDto(GetPharmacyByName(pharmacyName).PharmacyEmail, GetPharmacyByName(pharmacyName).PharmacyPassword);
        }


    }
}
