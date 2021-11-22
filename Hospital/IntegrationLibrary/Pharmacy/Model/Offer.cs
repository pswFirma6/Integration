using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Model
{
    public class Offer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PharmacyName { get; set; }
        public bool Posted { get; set; }
        public Offer() { }

        public Offer(int id, string title, string content, DateTime startDate, DateTime endDate, string pharmacyName, bool posted)
        {
            Id = id;
            Title = title;
            Content = content;
            StartDate = startDate;
            EndDate = endDate;
            PharmacyName = pharmacyName;
            Posted = posted;
        }
    }
}
