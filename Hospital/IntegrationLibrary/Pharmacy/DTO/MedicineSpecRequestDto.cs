using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.DTO
{
    public class MedicineSpecRequestDto
    {
        public string MedicineName { get; set; }
        public string PharmacyName { get; set; }

        public MedicineSpecRequestDto() { }

        public MedicineSpecRequestDto(string medicineName, string pharmacyName)
        {
            MedicineName = medicineName;
            PharmacyName = pharmacyName;
        }
    }
}