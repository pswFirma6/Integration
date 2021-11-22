using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.DTO
{
    public class PharmacyMedicineAvailabilityDTO
    {
        public string PharmacyName { get; set; }
        public bool IsAvailable { get; set; }

        public PharmacyMedicineAvailabilityDTO() { }

        public PharmacyMedicineAvailabilityDTO(string pharmacyName, bool isAvailable)
        {
            PharmacyName = pharmacyName;
            IsAvailable = isAvailable;
        }
    }
}
