using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.DTO
{
    public class CheckAvailabilityDTO
    {
        public string PharmacyName { get; set; }
        public MedicineDTO Medicine { get; set; }

        public CheckAvailabilityDTO() {  }

        public CheckAvailabilityDTO(string pharmacyName, MedicineDTO medicine)
        {
            PharmacyName = pharmacyName;
            Medicine = medicine;
        }
    }
}
