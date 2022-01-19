using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
using IntegrationLibrary.Tendering.Repository;
using IntegrationLibrary.Tendering.Service;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IntegrationTests
{
    public class TenderTests
    {
        private TenderService tenderService;

        [Fact]
        public void Get_tenders()
        {
            var stubRepository = new Mock<ITenderRepository>();
            tenderService = new TenderService(stubRepository.Object);

            List<Tender> tenders = new List<Tender>();
            Tender tender = new Tender 
            {
                Id = 1, CreationDate = DateTime.Now,
                TenderDateRange = new IntegrationLibrary.Shared.Model.DateRange
                {
                    StartDate = new DateTime(2021, 12, 18),
                    EndDate = new DateTime(2022, 3, 1)
                }
            };
            tenders.Add(tender);

            stubRepository.Setup(m => m.GetAll()).Returns(tenders);

            tenders = tenderService.GetTenders();

            tenders.ShouldNotBeEmpty();
        }

    }
}
