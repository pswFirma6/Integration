using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Service;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class PharmacyProfileTests
    {
        private PharmacyService pharmacyService;

        [Fact]
        public void AddPharmacy()
        {

            var stubRepository = new Mock<IPharmacyRepository>();
            pharmacyService = new PharmacyService(stubRepository.Object);

            List<Pharmacy> pharmacies = new List<Pharmacy>();
            Address ad1 = new Address("Novi Sad", "Cankareva 15");
            ConnectionInfo c1 = new ConnectionInfo("Benu", "HTTP", "url");
            Pharmacy pharmacy = new Pharmacy(31, "Benu", "image.jpg", ad1, c1, "email.com", "12345");
            Pharmacy pharmacy1 = new Pharmacy(32, "Jankovic", "image.jpg", ad1, c1, "email.com", "12345");


            pharmacies.Add(pharmacy);
            pharmacies.Add(pharmacy1);


            PharmacyInfo pharmacyInfo = new PharmacyInfo("Jankovic", "Street", "city", "apikey", "HTTP", "url");
            Pharmacy p = new Pharmacy(pharmacyInfo);

            stubRepository.Setup(m => m.Add(p)).Callback((Pharmacy p) => pharmacies.Add(p));

            pharmacyService.AddPharmacy(pharmacyInfo);

            pharmacies.Count.ShouldBe(2);

        }
    }
}