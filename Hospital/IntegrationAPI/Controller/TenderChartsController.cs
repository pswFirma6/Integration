using IntegrationLibrary.Pharmacy.DTO;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Shared.Model;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Repository;
using IntegrationLibrary.Tendering.Service;
using Microsoft.AspNetCore.Mvc;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

    
namespace IntegrationAPI.Controller
{
    [ApiController]
    public class TenderChartsController : ControllerBase
    {
        private readonly TenderChartsService chartsService;

        public TenderChartsController(DatabaseContext context)
        {
            ITenderOfferRepository offerRepository = new TenderOfferRepository(context);
            chartsService = new TenderChartsService(offerRepository);
        }

        [HttpPost]
        [Route("getTenderParticipants")]
        public List<TenderParticipantDto> GetTenderParticipants(DateRange dateRange)
        {
            return chartsService.GetTendersParticipants(dateRange);
        }

        [HttpPost]
        [Route("getTenderWinners")]
        public List<TenderParticipantDto> GetTenderWinners(DateRange dateRange)
        {
            return chartsService.GetTenderWinners(dateRange);
        }

        [HttpPost]
        [Route("getTendersWinningOffersPrices")]
        public List<TenderEarningDto> GetTendersWinningOffersPrices(DateRange dateRange)
        {
            return chartsService.GetWinningOffersPrices(dateRange);
        }

        [HttpPost]
        [Route("getPharmaciesEarnings")]
        public List<TenderEarningDto> GetPharmaciesEarnings(DateRange dateRange)
        {
            return chartsService.GetPharmaciesEarnings(dateRange);
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
        public int[] GetNumberOfPharmacyWins([FromRoute] string pharmacyName)
        {
            int[] wins = { chartsService.GetNumberOfPharmacyWins(pharmacyName) };
            return wins;
        }

        [HttpGet]
        [Route("pharmacyParticipations/{pharmacyName}")]
        public int[] GetNumberOfPharmacyParticipations([FromRoute] string pharmacyName)
        {
            int[] participations = { chartsService.GetNumberOfPharmacyParticipations(pharmacyName) };
            return participations;
        }

        [HttpGet]
        [Route("pharmacyMedicineConsumption/{pharmacyName}")]
        public List<MedicineDto> GetPharmacyMedicineConsumption([FromRoute] string pharmacyName)
        {
            return chartsService.GetPharmacyMedicineConsumption(pharmacyName);
        }

        [HttpPost]
        [Route("getPdf")]
        public async Task<IActionResult> GetFile(PdfDocument pdf)
        {
            Debug.WriteLine(pdf);
            chartsService.GenerateReport(pdf);
            var memory = new MemoryStream();
            Debug.WriteLine(chartsService.GetFile());
            using (var stream = new FileStream(chartsService.GetFile(), FileMode.Open))
                await stream.CopyToAsync(memory);

            memory.Position = 0;
            var contentType = "APPLICATION/octet-stream";
            return File(memory, contentType, "TenderReport.pdf");
      
        }


    }
}
