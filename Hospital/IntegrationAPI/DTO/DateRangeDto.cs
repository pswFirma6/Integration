using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class DateRangeDto
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public DateRangeDto() { }

        public DateRangeDto(string startDate, string endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
