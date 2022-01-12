using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.DTO
{
    public class MedicineAvailabilityDto
    {
        public string PharmacyName { get; set; }
        public bool IsAvailable { get; set; }

        public MedicineAvailabilityDto() { }

        public MedicineAvailabilityDto(string pharmacyName, bool isAvailable)
        {
            PharmacyName = pharmacyName;
            IsAvailable = isAvailable;
        }
    }
}
