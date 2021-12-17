using IntegrationLibrary.Tendering.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.DTO
{
    public class TenderDTO
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<TenderItemDTO> TenderItems { get; set; }

        public TenderDTO() { }

        public TenderDTO(string startDate, string endDate, List<TenderItemDTO> tenderItems)
        {
            StartDate = startDate;
            EndDate = endDate;
            TenderItems = tenderItems;
        }
    }
}
