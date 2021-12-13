using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Repository
{
    public class NotificationRepository : Repo<Notification>, INotificationRepository
    {
        public NotificationRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
