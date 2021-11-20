using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using Integration_library.Pharmacy.Repository;
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
        private IOfferRepository repository;
        private DatabaseContext context = new DatabaseContext();

        [Fact]
        public void Add_offer()
        {
            repository = new OfferRepository(context);
            service = new OfferService(repository);
            Offer offer = new Offer { Title = "Offer1", Content = "Offer1", StartDate = new DateTime(2021, 11, 11), EndDate = new DateTime(2021, 11, 30), PharmacyName = "Pharmacy1" };

            List<Offer> beforeAdding = service.GetOffers();
            service.AddOffer(offer);
            List<Offer> afterAdding = service.GetOffers();

            (afterAdding.Count - beforeAdding.Count).ShouldNotBe(0);
        }

        [Fact]
        public void Get_offers()
        {
            repository = new OfferRepository(context);
            service = new OfferService(repository);
                
            List<Offer> offers = new List<Offer>();

            offers = service.GetOffers();

            offers.ShouldNotBeEmpty();
        }

        [Fact]
        public void Post_offer()
        {
            var stubRepository = new Mock<IOfferRepository>();
            service = new OfferService(stubRepository.Object);
            Offer offer = new Offer { Id = 1, Title = "Offer1", Content = "Offer1", StartDate = new DateTime(2021, 11, 11), EndDate = new DateTime(2021, 11, 17), PharmacyName = "Pharmacy1", Posted = false };

            bool offerPosted = service.PostOffer(offer);

            offerPosted.ShouldBeTrue();
        }
    }
}
