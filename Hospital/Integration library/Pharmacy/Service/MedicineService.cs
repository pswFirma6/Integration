using Integration_library.Pharmacy.DTO;
using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Service
{
    public class MedicineService
    {
        private string server = "https://localhost:44377/api";
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
        }

        public bool CheckIfMedicineIsAvailableInPharmacy(MedicineDTO medicine)
        {
            return true;
        }

        public Medicine FindMedicine(int id)
        {
            return repository.FindById(id);
        }

        public void EditMedicine(Medicine medicine)
        {
            repository.Update(medicine);
        }

        public void AddExistingMedicine(Medicine medicine)
        {
            Medicine oldStateMedicine = FindMedicine(medicine.Id);
            Medicine newStateMedicine = oldStateMedicine;
            newStateMedicine.Quantity += medicine.Quantity;
            repository.Update(newStateMedicine);
        }
    }
}
