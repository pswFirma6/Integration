using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.DTO
{
    public class TenderOfferDto
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public string PharmacyName { get; set; }

        public List<TenderOfferItemDto> TenderOfferItems { get; set; }
        public TenderOfferDto() { }

        public TenderOfferDto(int id, int tenderId, string pharmacyName, List<TenderOfferItemDto> tenderOfferItems)
        {
            Id = id;
            TenderId = tenderId;
            PharmacyName = pharmacyName;
            this.TenderOfferItems = tenderOfferItems;
        }
    }
}
