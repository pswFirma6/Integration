using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.DTO
{
    public class TenderForPharmacyDto
    {
        public int Id { get; set; }
        public string CreationDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<TenderItemDto> TenderItems { get; set; }
        public string HospitalApiKey { get; set; }
        public int HospitalTenderId { get; set; }
        public bool Opened { get; set; }

        public TenderForPharmacyDto() { }

        public TenderForPharmacyDto(int id, string creationDate, string startDate, string endDate, List<TenderItemDto> tenderItems, string hospitalApiKey, int hospitalTenderId, bool opened)
        {
            Id = id;
            CreationDate = creationDate;
            StartDate = startDate;
            EndDate = endDate;
            TenderItems = tenderItems;
            HospitalApiKey = hospitalApiKey;
            HospitalTenderId = hospitalTenderId;
            Opened = opened;
        }
    }
}
