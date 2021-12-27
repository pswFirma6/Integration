using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Shared.Model
{
    public class DateRange : ValueObject
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateRange() { }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            ValidateDates();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartDate;
            yield return EndDate;
        }

        private void ValidateDates()
        {
            if (this.EndDate < this.StartDate)
            {
                throw new DateException(StartDate,EndDate);
            }
        }

    }

    public class DateException: Exception
    {
        public DateException(DateTime startDate, DateTime endDate) : base("End date should not be before start date") { }

    }

}
