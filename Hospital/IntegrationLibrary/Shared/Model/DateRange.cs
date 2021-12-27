using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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
            if (EndDate < StartDate)
                throw new DateException("The end date cannot be before start date!");
        }

        public bool CheckIfEndDateIsBeforeToday()
        {
            return DateTime.Compare(EndDate, DateTime.Now) > 0;
        }

        public bool CheckIfStartDateIsBeforeToday()
        {
            return DateTime.Compare(StartDate, DateTime.Now) > 0;
        }

    }
    public class DateException: Exception, ISerializable
    {
        public DateException(string message) : base(message) { }

    }

}
