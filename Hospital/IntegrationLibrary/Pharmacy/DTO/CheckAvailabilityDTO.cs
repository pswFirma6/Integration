using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.DTO
{
    public class CheckAvailabilityDto
    {
        public string PharmacyName { get; set; }
        public MedicineDto Medicine { get; set; }

        public CheckAvailabilityDto() {  }

        public CheckAvailabilityDto(string pharmacyName, MedicineDto medicine)
        {
            PharmacyName = pharmacyName;
            Medicine = medicine;
        }
    }
}
