using Abp.Domain.Entities;
using Abp.Events.Bus;
using IntegrationLibrary.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationLibrary.Tendering.Model
{
    public class Tender : Shared.Model.Entity
    {
        public DateTime CreationDate { get; set; }
        public DateRange TenderDateRange { get; set; }
        public bool Opened { get; set; }

        private readonly List<TenderItem> _tenderItems = new List<TenderItem>();
        public IReadOnlyCollection<TenderItem> TenderItems => _tenderItems;

        public Tender() { }

        public Tender(int id, DateTime creationDate, DateRange dateRange)
        {
            Id = id;
            CreationDate = creationDate;
            TenderDateRange = dateRange;
        }


        public void AddTenderItem(Tender tender, string name, int quantity)
        {
            var tenderItem = new TenderItem(tender, name, quantity);
            _tenderItems.Add(tenderItem);
        }
    }
}
