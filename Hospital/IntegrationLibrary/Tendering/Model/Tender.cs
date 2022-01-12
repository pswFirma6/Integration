using Abp.Domain.Entities;
using Abp.Events.Bus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.Model
{
    public class Tender : Entity, IAggregateRoot
    {
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Opened { get; set; }

        private readonly List<TenderItem> _tenderItems;
        public IReadOnlyCollection<TenderItem> TenderItems => _tenderItems;

        public Tender() { }

        public Tender(int id, DateTime creationDate, DateTime startDate, DateTime endDate)
        {
            Id = id;
            CreationDate = creationDate;
            StartDate = startDate;
            EndDate = endDate;
        }

        public ICollection<IEventData> DomainEvents => throw new NotImplementedException();

        public void AddTenderItem(string name, int quantity)
        {
            var tenderItem = new TenderItem(name, quantity);
            _tenderItems.Add(tenderItem);
        }
    }
}
