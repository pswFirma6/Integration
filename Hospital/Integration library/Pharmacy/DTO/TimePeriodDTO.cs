using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.DTO
{
    public class TimePeriodDTO
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public TimePeriodDTO(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

    }
}
