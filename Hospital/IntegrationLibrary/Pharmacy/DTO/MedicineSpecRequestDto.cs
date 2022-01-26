using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.DTO
{
    public class MedicineSpecRequestDto
    {
        public string MedicationName { get; set; }
        public string PharmacyName { get; set; }

        public MedicineSpecRequestDto() { }

        public MedicineSpecRequestDto(string medicationName, string pharmacyName)
        {
            MedicationName = medicationName;
            PharmacyName = pharmacyName;
        }
    }
}