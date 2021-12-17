using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.Model
{
    public class TenderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int TenderId { get; set; }

        public TenderItem() { }

        public TenderItem(int id, string name, int quantity, int tenderId)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            TenderId = tenderId;
        }

    }
}
