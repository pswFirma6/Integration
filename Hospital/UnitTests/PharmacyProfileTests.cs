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
        public void UpdatePharmacy()
        {
            var stubRepository = new Mock<IPharmacyRepository>();
            pharmacyService = new PharmacyService(stubRepository.Object);

            List<Pharmacy> pharmacies = new List<Pharmacy>();
            Pharmacy pharmacy = new Pharmacy("Benu", "Benu", "Cankareva 15", "Novi Sad", "image.jpg");
            Pharmacy pharmacy1 = new Pharmacy("Jankovic", "Jankovic", "Branka Bajica 80", "Novi Sad", "image.jpg");
            pharmacies.Add(pharmacy);
            pharmacies.Add(pharmacy1);

            Pharmacy pharmacy2 = new Pharmacy("Benu", "Benu", "Cankareva 5", "Novi Sad", "imageNEW.jpg");

            stubRepository.Setup(m => m.Update(pharmacy2)).Verifiable();

            pharmacyService.EditPharmacy(pharmacy2);

            pharmacies[0].PharmacyPicture.ShouldBe("imageNEW.jpg");
        }

        [Fact]
        public void AddPharmacy()
        {
            var stubRepository = new Mock<IPharmacyRepository>();
            pharmacyService = new PharmacyService(stubRepository.Object);

            List<Pharmacy> pharmacies = new List<Pharmacy>();
            Pharmacy pharmacy = new Pharmacy("Benu", "Benu", "Cankareva 15", "Novi Sad", "image.jpg");
            Pharmacy pharmacy1 = new Pharmacy("Jankovic", "Jankovic", "Branka Bajica 80", "Novi Sad", "image.jpg");
            pharmacies.Add(pharmacy);
            pharmacies.Add(pharmacy1);

            Pharmacy pharmacy2 = new Pharmacy("Benu", "Benu", "Cankareva 5", "Novi Sad", "imageNEW.jpg");

            stubRepository.Setup(m => m.Add(pharmacy2)).Callback((Pharmacy p) => pharmacies.Add(p));

            pharmacyService.EditPharmacy(pharmacy2);

            pharmacies.Count.ShouldBe(3);
        }
    }
}
