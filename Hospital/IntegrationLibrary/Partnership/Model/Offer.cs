using IntegrationLibrary.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Partnership.Model
{
    public class Offer: Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateRange OfferDateRange { get; set; }
        public string PharmacyName { get; set; }
        public bool Posted { get; set; }
        public Offer() { }

        public Offer(int id, string title, string content, DateRange offerDateRange, string pharmacyName, bool posted)
        {
            Id = id;
            Title = title;
            Content = content;
            OfferDateRange = offerDateRange;
            PharmacyName = pharmacyName;
            Posted = posted;
        }

        public bool CheckEndDate()
        {
            return OfferDateRange.CheckIfEndDateIsBeforeToday();
        }

        public void PostOffer()
        {
            Posted = true;
        }
    }
}
