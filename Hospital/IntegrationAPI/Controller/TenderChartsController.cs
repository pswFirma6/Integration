using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Repository;
using IntegrationLibrary.Tendering.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Controller
{
    [ApiController]
    public class TenderChartsController
    {
        private readonly TenderChartsService chartsService;

        public TenderChartsController(DatabaseContext context)
        {
            ITenderOfferRepository offerRepository = new TenderOfferRepository(context);
            chartsService = new TenderChartsService(offerRepository);
        }

        [HttpGet]
        [Route("getTenderParticipants")]
        public List<TenderParticipantDto> GetTenderParticipants()
        {
            return chartsService.GetTendersParticipants();
        }

        [HttpGet]
        [Route("getTenderWinners")]
        public List<TenderParticipantDto> GetTenderWinners()
        {
            return chartsService.GetTenderWinners();
        }

        [HttpGet]
        [Route("getTendersWinningOffersPrices")]
        public List<double> GetTendersWinningOffersPrices()
        {
            return chartsService.GetWinningOffersPrices();
        }

        [HttpGet]
        [Route("getPharmaciesEarnings")]
        public List<TenderEarningDto> GetPharmaciesEarnings()
        {
            return chartsService.GetPharmaciesEarnings();
        }

    }
}
