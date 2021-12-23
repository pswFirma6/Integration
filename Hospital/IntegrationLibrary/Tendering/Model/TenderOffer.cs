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
        public bool isWinner { get; set; } = false;
        public TenderOffer() { }
        public TenderOffer(int id, int tenderId, string pharmacyName)
        {
            Id = id;
            TenderId = tenderId;
            PharmacyName = pharmacyName;
        }
    }
}
