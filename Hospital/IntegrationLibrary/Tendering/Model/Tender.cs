using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.Model
{
    public class Tender
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool Opened { get; set; }
        public Tender() { }

        public Tender(int id, DateTime creationDate, DateTime startDate, DateTime endDate)
        {
            // this.Opened = true;
            Id = id;
            CreationDate = creationDate;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
