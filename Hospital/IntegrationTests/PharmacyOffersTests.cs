using IntegrationLibrary.Partnership.IRepo;
using IntegrationLibrary.Partnership.Model;
using IntegrationLibrary.Partnership.Repository;
using IntegrationLibrary.Partnership.Service;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.Shared.Model;
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
            DateRange dateRange = new DateRange(new DateTime(2021, 11, 11), new DateTime(2021, 11, 30));
            Offer offer = new Offer { Title = "Offer10", Content = "Offer1",OfferDateRange = dateRange, PharmacyName = "Pharmacy1" };

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
            bool isChanged = false;

            List<Offer> offers = new List<Offer>();
            DateRange dateRange = new DateRange(new DateTime(2021, 11, 11), new DateTime(2021, 11, 17));
            Offer offer = new Offer { Id = 1, Title = "Offer1", Content = "Offer1", OfferDateRange = dateRange, PharmacyName = "Pharmacy1", Posted = false };
            offers.Add(offer);

            //service.PostOffer(offer);

            isChanged.ShouldBeFalse();
        }
    }
}
