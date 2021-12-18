using IntegrationLibrary.Tendering.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.DTO
{
    public class TenderDto
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<TenderItemDto> TenderItems { get; set; }

        public TenderDto() { }

        public TenderDto(string startDate, string endDate, List<TenderItemDto> tenderItems)
        {
            StartDate = startDate;
            EndDate = endDate;
            TenderItems = tenderItems;
        }
    }
}
