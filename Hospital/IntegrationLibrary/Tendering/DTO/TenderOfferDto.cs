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
        public string HospitalApiKey { get; set; }
        public List<TenderOfferItemDto> TenderOfferItems { get; set; }
        public string CreationDate { get; set; }

        public TenderOfferDto() { }

        public TenderOfferDto(int id, int tenderId, string pharmacyName, List<TenderOfferItemDto> tenderOfferItems, string hospitalApiKey)
        {
            Id = id;
            TenderId = tenderId;
            PharmacyName = pharmacyName;
            this.TenderOfferItems = tenderOfferItems;
            HospitalApiKey = hospitalApiKey;
        }
    }
}
