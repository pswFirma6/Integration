using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using Integration_library.Pharmacy.Service;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationAppTests.IntegrationTests
{
    public class PharmacyOffersTests
    {
        private OfferService service;

        [Fact]
        public void Get_offers()
        {
            var stubRepository = new Mock<IOfferRepository>();
            service = new OfferService(stubRepository.Object);
                
            List<Offer> offers = new List<Offer>();
            Offer offer = new Offer { Id = 1, Title = "Offer1", Content = "Offer1", StartDate = new DateTime(2021, 11, 11), EndDate = new DateTime(2021, 11, 17), PharmacyName = "Pharmacy1", Posted = false };
            offers.Add(offer);

            stubRepository.Setup(m => m.GetAll()).Returns(offers);

            offers = service.GetOffers();

            offers.ShouldNotBeEmpty();
        }
    }
}
