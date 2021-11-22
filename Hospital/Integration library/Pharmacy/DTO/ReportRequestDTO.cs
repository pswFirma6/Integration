using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.DTO
{
    public class ReportRequestDTO
    {

        public string MedicationName { get; set; }
        public string PharmacyName { get; set; }

        public ReportRequestDTO()
        {
        }

        public ReportRequestDTO(string medicationName, string pharmacyName)
        {
            MedicationName = medicationName;
            PharmacyName = pharmacyName;
        }
    }
}