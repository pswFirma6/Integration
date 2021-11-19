using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Model
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Intensity { get; set; }

        public Medicine(int id, string name, int quantity, double intensity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Intensity = intensity;
        }
    }
}
