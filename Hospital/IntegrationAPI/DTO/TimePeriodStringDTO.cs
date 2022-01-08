using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class TimePeriodStringDTO
    {
        public string startDate { get; set; }
        public string endDate { get; set; }

        public TimePeriodStringDTO(string startDate, string endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public TimePeriodStringDTO()
        {
        }
    }
}
