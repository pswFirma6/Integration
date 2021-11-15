using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Model
{
    public class PharmacyOffer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public PharmacyOffer(string title, string content, DateTime startDate, DateTime endDate)
        {
            Title = title;
            Content = content;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
