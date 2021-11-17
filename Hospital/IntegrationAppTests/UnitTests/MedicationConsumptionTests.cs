using Integration_library.Pharmacy.DTO;
using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Service;
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
        private MedicationConsumptionService service; 

        [Fact]
        public void Is_date_within_range()
        {
            var stubRepository = new Mock<IMedicationConsumptionRepository>();
            service = new MedicationConsumptionService(stubRepository.Object);

            DateTime date = new DateTime(2021, 09, 16);
            TimePeriodDTO range = new TimePeriodDTO { StartDate = new DateTime(2021, 07, 31), EndDate = new DateTime(2021, 09, 28) };

            service.IsWithinRange(date, range).ShouldBe(true);
        }
    }
}
