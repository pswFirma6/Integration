using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.DTO
{
    public class TenderItemDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public TenderItemDto() { }

        public TenderItemDto(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
