using IntegrationLibrary.Pharmacy.DTO;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.ReportingAndStatistics.Service;
using IntegrationLibrary.Shared.Model;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationAppTests.UnitTests
{
    
    public class MedicationConsumptionTests
    {
        private MedicineConsumptionService service;

        public MedicationConsumptionTests()
        {
            
        }

        [Fact]
        public void Is_date_within_range()
        {
            var stubRepository = new Mock<IMedicationConsumptionRepository>();
            service = new MedicineConsumptionService(stubRepository.Object);

            DateTime date = new DateTime(2021, 09, 16);
            DateRange range = new DateRange { StartDate = new DateTime(2021, 07, 31), EndDate = new DateTime(2021, 09, 28) };

            service.IsWithinRange(date, range).ShouldBe(true);
        }

        [Fact]
        public void Get_consumptions_amount_for_medication() //sigurno ih ima vise od 0, i u listi su svi lekovi isti
        {
            var stubRepository = new Mock<IMedicationConsumptionRepository>();
            service = new MedicineConsumptionService(stubRepository.Object);

            List<MedicationConsumption> consumptions = new List<MedicationConsumption>();
            MedicationConsumption m1 = new MedicationConsumption { Id = 1, MedicationId = 1, MedicationName = "Brufen", Date = new DateTime(2021, 07, 31), AmountConsumed = 5 };
            MedicationConsumption m2 = new MedicationConsumption { Id = 2, MedicationId = 1, MedicationName = "Brufen", Date = new DateTime(2021, 09, 16), AmountConsumed = 5 };
            consumptions.Add(m1);
            consumptions.Add(m2);

            int amount = service.GetConsumptionsAmountForCertainMedication(consumptions);

            amount.ShouldNotBe(0);
        }

        [Fact]
        public void Get_consumptions_for_medication()
        {
            var stubRepository = new Mock<IMedicationConsumptionRepository>();
            service = new MedicineConsumptionService(stubRepository.Object);

            List<MedicationConsumption> consumptions = new List<MedicationConsumption>();
            MedicationConsumption m1 = new MedicationConsumption { Id = 1, MedicationId = 1, MedicationName = "Brufen", Date = new DateTime(2021, 07, 31), AmountConsumed = 5 };
            MedicationConsumption m2 = new MedicationConsumption { Id = 2, MedicationId = 2, MedicationName = "Paracetamol", Date = new DateTime(2021, 09, 16), AmountConsumed = 5 };
            consumptions.Add(m1);
            consumptions.Add(m2);

            List<MedicationConsumption> medConsumptions = service.GetConsumptionsForCertainMedication("Brufen", consumptions);

            medConsumptions.ShouldNotBeEmpty();
        }

        [Fact]
        public void Is_consumption_evaluated()
        {
            var stubRepository = new Mock<IMedicationConsumptionRepository>();
            service = new MedicineConsumptionService(stubRepository.Object);

            List<string> evaluetedConsumptions = new List<string>();
            evaluetedConsumptions.Add("Brufen");
            evaluetedConsumptions.Add("Paracetamol");

            bool isEvalueted = service.IsEvaluated(evaluetedConsumptions, "Brufen");

            isEvalueted.ShouldBe(true);

        }

    }
}
