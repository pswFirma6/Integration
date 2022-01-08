using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.DTO
{
    public class MedicineDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public MedicineDto() { }

        public MedicineDto(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

    }
}
