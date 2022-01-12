using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.DTO
{
    public class OrderMedicineDto
    {
        public string PharmacyName { get; set; }
        public MedicineDto Medicine { get; set; }

        public OrderMedicineDto() {  }

        public OrderMedicineDto(string pharmacyName, MedicineDto medicine)
        {
            PharmacyName = pharmacyName;
            Medicine = medicine;
        }
    }
}
