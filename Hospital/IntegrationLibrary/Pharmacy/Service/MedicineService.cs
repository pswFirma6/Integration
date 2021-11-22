using IntegrationLibrary.Pharmacy.DTO;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Service
{
    public class MedicineService
    {
        private string server = "https://localhost:44377/api";
        private string orderServer = "https://localhost:44377";
        private IMedicineRepository repository;

        public MedicineService(IMedicineRepository iRepository)
        {
            repository = iRepository;
        }

        public List<Medicine> GetMedicines()
        {
            return repository.GetAll();
        }

        public void AddMedicine(Medicine medicine)
        {
            repository.Add(medicine);
            repository.Save();
        }

        public bool CheckIfMedicineIsAvailableInPharmacy(MedicineDTO medicine)
        {
            return true;
        }

        public Medicine FindMedicine(int id)
        {
            return repository.FindById(id);
        }

        public void EditMedicine(MedicineDTO medicine)
        {
            foreach (Medicine med in GetMedicines())
            {
                if (med.Name == medicine.Name)
                {
                    med.Quantity += medicine.Quantity;
                    repository.Save();
                }
            }
        }

        public void AddExistingMedicine(Medicine medicine)
        {
            Medicine oldStateMedicine = FindMedicine(medicine.Id);
            Medicine newStateMedicine = oldStateMedicine;
            newStateMedicine.Quantity += medicine.Quantity;
            repository.Update(newStateMedicine);
        }

        public void OrderMedicine(CheckAvailabilityDTO order)
        {
            if (!CheckIfMedicineExists(order.Medicine.Name))
            {
                AddMedicine(new Medicine(order.Medicine.Name, order.Medicine.Quantity));
            }
            else
            {
                EditMedicine(order.Medicine);
            }
        }


        private bool CheckIfMedicineExists(string medicineName)
        {
            foreach (Medicine medicine in GetMedicines())
            {
                if (medicine.Name == medicineName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
