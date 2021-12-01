using IntegrationLibrary.Pharmacy.DTO;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.ReportingAndStatistics.Service;
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
        private MedicineConsumptionService service;

        [Fact]
        public void Get_consumptions_for_time_period()
        {
            
            var stubRepository = new Mock<IMedicationConsumptionRepository>();
            service = new MedicineConsumptionService(stubRepository.Object);
            List<MedicationConsumption> consumptions = new List<MedicationConsumption>();
            List<MedicationConsumption> requestedConsumptions = new List<MedicationConsumption>();

            MedicationConsumption c = new MedicationConsumption { Id = 1, MedicationId = 1, MedicationName = "Medication1", Date = new DateTime(2021, 09, 30), AmountConsumed = 5 };
            consumptions.Add(c);

            stubRepository.Setup(m => m.GetAll()).Returns(consumptions);

            TimePeriodDTO timePeriod = new TimePeriodDTO { StartDate = new DateTime(2021, 09, 28), EndDate = new DateTime(2021, 10, 31) };
            requestedConsumptions = service.GetConsumptionsForTimePeriod(timePeriod);

            requestedConsumptions.ShouldNotBeEmpty();
            
        }
    }
}
