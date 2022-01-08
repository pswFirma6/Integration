using IntegrationLibrary.Partnership.IRepo;
using IntegrationLibrary.Partnership.Model;
using IntegrationLibrary.Partnership.Service;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Service;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationAppTests.UnitTests
{
    public class PharmacyOffersTests
    {
        private OfferService service;

        [Fact]
        public void Check_dates()
        {
            var stubRepository = new Mock<IOfferRepository>();
            service = new OfferService(stubRepository.Object);
            Offer offer = new Offer { Id = 1, Title = "Offer1", Content = "Offer1", StartDate = new DateTime(2021, 11, 11), EndDate = new DateTime(2021, 12, 17), PharmacyName = "Pharmacy1", Posted = false };

            bool checkDates = service.CheckEndDate(offer);

            checkDates.ShouldBeTrue();
        }

        

    }
}
