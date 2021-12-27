using IntegrationLibrary.Partnership.IRepo;
using IntegrationLibrary.Partnership.Model;
using IntegrationLibrary.Partnership.Service;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.Shared.Model;
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
            DateRange dateRange = new DateRange(new DateTime(2021, 11, 11), new DateTime(2022, 11, 17));
            Offer offer = new Offer { Id = 1, Title = "Offer1", Content = "Offer1", OfferDateRange = dateRange, PharmacyName = "Pharmacy1", Posted = false };

            bool checkDates = service.CheckEndDate(offer);

            checkDates.ShouldBeTrue();
        }

        

    }
}
