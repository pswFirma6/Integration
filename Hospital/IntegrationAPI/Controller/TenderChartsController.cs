using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Tendering.DTO;
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
        private readonly TenderChartsService chartsService = new TenderChartsService();

        [HttpGet]
        [Route("getTenderParticipants")]
        public List<TenderParticipantDto> GetTenderParticipants()
        {
            return chartsService.TenderParticipants();
        }

    }
}
