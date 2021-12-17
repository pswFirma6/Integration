using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.DTO
{
    public class TenderItemDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public TenderItemDTO() { }

        public TenderItemDTO(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
