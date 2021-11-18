using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.DTO
{
    public class TimePeriodDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public TimePeriodDTO() { }

        public TimePeriodDTO(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

    }
}
