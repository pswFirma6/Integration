using IntegrationLibrary.Partnership.IRepo;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Service;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests
{
    public class NotificationTests
    {
        private NotificationService service;
        
        [Fact]
        public void Add_notification()
        {
            var stubRepository = new Mock<INotificationRepository>();
            service = new NotificationService(stubRepository.Object);
            List<Notification> notifications = new List<Notification>();
            Notification notification = new Notification { Content = "novo", Date = new DateTime(), FileName = "noviFajl.pdf", Id = 1, Read = false, Title = "Naslov" };
            stubRepository.Setup(m => m.Add(notification)).Callback((Notification n) => notifications.Add(n));

            service.AddNotification(notification);

            notifications.ShouldNotBeEmpty();
        }

        [Fact]
        public void Read_notification()
        {
            var stubRepository = new Mock<INotificationRepository>();
            service = new NotificationService(stubRepository.Object);
            List<Notification> notifications = new List<Notification>();
            Notification notification = new Notification { Content = "novo", Date = new DateTime(), FileName = "noviFajl.pdf", Id = 1, Read = false, Title = "Naslov" };
            notifications.Add(notification);
            stubRepository.Setup(m => m.GetAll()).Returns(notifications);
            service.ReadNotification(notification);
            notification.Read.ShouldBeTrue();
        }
    }
}
