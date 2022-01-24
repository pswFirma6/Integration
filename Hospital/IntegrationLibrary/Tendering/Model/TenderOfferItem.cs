using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.Model
{
    public class TenderOfferItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public TenderOffer Offer { get; set; }
        public int OfferId { get; set; }

        public TenderOfferItem() { }

        public TenderOfferItem(int id, string name, int quantity, double price, int tenderOfferId)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
            OfferId = tenderOfferId;
        }

        public TenderOfferItem(TenderOffer offer, string name, int quantity, double price)
        {
            Offer = offer;
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}
