using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.DTO
{
    public class MedicineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Intensity { get; set; }
        public string PharmacyApiKey { get; set; }

        public MedicineDTO(int id, string name, int quantity, double intensity, string pharmacyApiKey)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Intensity = intensity;
            PharmacyApiKey = pharmacyApiKey;
        }
    }
}
