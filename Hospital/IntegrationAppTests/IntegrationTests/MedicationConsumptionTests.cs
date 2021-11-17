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
    public class MedicationConsumptionTests
    {
        private MedicationConsumptionService service;
        private IMedicationConsumptionRepository repository;
        private DatabaseContext context = new DatabaseContext();

        [Fact]
        public void Get_medication_consumptions()
        {
            var stubRepository = new Mock<IMedicationConsumptionRepository>();
            service = new MedicationConsumptionService(stubRepository.Object);
            List<MedicationConsumption> consumptions = new List<MedicationConsumption>();

            MedicationConsumption c = new MedicationConsumption { Id = 1, MedicationId = 1, MedicationName = "Medication1", Date = new DateTime(), AmountConsumed = 5 };
            consumptions.Add(c);

            stubRepository.Setup(m => m.GetAll()).Returns(consumptions);

            consumptions = service.GetConsumptions();

            consumptions.ShouldNotBeNull();

        }
    }
}
