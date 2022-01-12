﻿using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;

namespace IntegrationAppTests.UnitTests
{
    public class UrgentProcurementTests
    {
        private PharmacyService pharmacyService;
        private MedicineService medicineService;

        [Fact]
        public void Get_connected_pharmacies()
        {
            var stubRepository = new Mock<IPharmacyRepository>();
            pharmacyService = new PharmacyService(stubRepository.Object);

            List<Pharmacy> pharmacies = new List<Pharmacy>();
            ConnectionInfo info = new ConnectionInfo("Zlatni Lav", "HTTP", "url");
            Pharmacy pharmacy = new Pharmacy { PharmacyConnectionInfo = info, PharmacyName = "Zlatni Lav" };
            pharmacies.Add(pharmacy);

            stubRepository.Setup(m => m.GetAll()).Returns(pharmacies);

            pharmacies = pharmacyService.GetPharmacies();

            pharmacies.ShouldNotBeEmpty();
        }

        [Fact]
        public void Urgent_procurement_new_medicine()
        {
            var stubRepository = new Mock<IMedicineRepository>();
            medicineService = new MedicineService(stubRepository.Object);

            List<Medicine> medicines = new List<Medicine>();
            Medicine medicine = new Medicine(1, "Panklav", 50);
            Medicine medicine1 = new Medicine(2, "Amoksicilin", 0);
            medicines.Add(medicine);
            medicines.Add(medicine1);

            Medicine medicine2 = new Medicine(3, "Nixar", 30);

            stubRepository.Setup(m => m.Add(medicine2)).Callback((Medicine m) => medicines.Add(m));

            medicineService.AddMedicine(medicine2);

            medicines.Count.ShouldBe(3);
        }

        [Fact]
        public void Urgent_procurement_existing_medicine()
        {
            var stubRepository = new Mock<IMedicineRepository>();
            medicineService = new MedicineService(stubRepository.Object);

            List<Medicine> medicines = new List<Medicine>();
            Medicine medicine = new Medicine(1, "Panklav", 50);
            Medicine medicine1 = new Medicine(2, "Amoksicilin", 10);
            medicines.Add(medicine);
            medicines.Add(medicine1);

            Medicine medicine2 = new Medicine(2, "Amoksicilin", 30);

            stubRepository.Setup(m => m.FindById(2)).Returns(medicine1);
            stubRepository.Setup(m => m.Update(medicine2)).Verifiable();

            medicineService.AddExistingMedicine(medicine2);

            medicines[1].Quantity.ShouldBe(40);
        }

        [Fact]
        public void Find_existable_medicine()
        {
            var stubRepository = new Mock<IMedicineRepository>();
            medicineService = new MedicineService(stubRepository.Object);

            List<Medicine> medicines = new List<Medicine>();
            Medicine medicine = new Medicine(1, "Panklav", 50);
            Medicine medicine1 = new Medicine(2, "Amoksicilin", 20);
            medicines.Add(medicine);
            medicines.Add(medicine1);

            stubRepository.Setup(m => m.FindById(2)).Returns(medicine1);

            Medicine medicineFound = medicineService.FindMedicine(2);

            medicineFound.Id.ShouldBe(2);
        }


    }
}