using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.Model
{
    public class TenderOffer
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public String PharmacyName { get; set; }
        public bool IsWinner { get; set; } = false;
        public int PharmacyOfferId { get; set; }

        private readonly List<TenderOfferItem> _offerItems = new List<TenderOfferItem>();
        public IReadOnlyCollection<TenderOfferItem> OfferItems => _offerItems;

        public TenderOffer() { }

        public TenderOffer(int id, int tenderId, string pharmacyName, int pharmacyOfferId)
        {
            Id = id;
            TenderId = tenderId;
            PharmacyName = pharmacyName;
            PharmacyOfferId = pharmacyOfferId;
        }

        public void AddOfferItem(TenderOffer offer, string name, int quantity, double price)
        {
            var offerItem = new TenderOfferItem(offer, name, quantity, price);
            _offerItems.Add(offerItem);
        }

    }
}
