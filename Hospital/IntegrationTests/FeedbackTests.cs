using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Service;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;

namespace IntegrationAppTests
{
    public class FeedbackTests
    {
        private FeedbackService service;

        [Fact]
        public void Get_feedbacks()
        {
            var stubRepository = new Mock<IFeedbackRepository>();
            service = new FeedbackService(stubRepository.Object);
            List<Feedback> feedbacks = new List<Feedback>();

            Feedback f = new Feedback { Id = 1, Content = "Feedback1", FeedbackDate = "22.11.2021.", PharmacyName = "Pharmacy1" };
            feedbacks.Add(f);

            stubRepository.Setup(m => m.GetAll()).Returns(feedbacks);

            feedbacks = service.GetFeedbacks();

            feedbacks.ShouldNotBeEmpty();
        }
    }
}
