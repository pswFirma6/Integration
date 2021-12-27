using IntegrationLibrary.Pharmacy.DTO;
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

        [HttpGet]
        [Route("pharmacyOffers/{pharmacyName}")]
        public List<TenderEarningDto> GetPharmacyOffers([FromRoute] string pharmacyName)
        {
            return chartsService.GetPharmacyOffers(pharmacyName);
        }

        [HttpGet]
        [Route("pharmacyWinningOffers/{pharmacyName}")]
        public List<TenderEarningDto> GetPharmacyWinningOffers([FromRoute] string pharmacyName)
        {
            return chartsService.GetPharmacyWinningOffers(pharmacyName);
        }

        [HttpGet]
        [Route("pharmacyWins/{pharmacyName}")]
        public int GetNumberOfPharmacyWins([FromRoute] string pharmacyName)
        {
            return chartsService.GetNumberOfPharmacyWins(pharmacyName);
        }

        [HttpGet]
        [Route("pharmacyParticipations/{pharmacyName}")]
        public int GetNumberOfPharmacyParticipations([FromRoute] string pharmacyName)
        {
            return chartsService.GetNumberOfPharmacyParticipations(pharmacyName);
        }

        [HttpGet]
        [Route("pharmacyMedicineConsumption/{pharmacyName}")]
        public List<MedicineDto> GetPharmacyMedicineConsumption([FromRoute] string pharmacyName)
        {
            return chartsService.GetPharmacyMedicineConsumption(pharmacyName);
        }

    }
}
