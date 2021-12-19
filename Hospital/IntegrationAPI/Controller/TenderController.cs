using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Repository;
using IntegrationLibrary.Tendering.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Controller
{
    [ApiController]
    public class TenderController
    {
        private readonly TenderService tenderService;
        private readonly IConfiguration _config;

        public TenderController(DatabaseContext context, IConfiguration config)
        {
            ITenderRepository tenderRepository = new TenderRepository(context);
            tenderService = new TenderService(tenderRepository);
            _config = config;
        }

        [HttpGet]
        [Route("getTenders")]
        public List<TenderDto> GetTenders()
        {
            return tenderService.GetTendersWithItems();
        }

        [HttpGet]
        [Route("addTender")]
        public string AddTender(TenderDto tender)
        {
            var apiKey = _config.GetValue<string>("ApiKey");
            return apiKey.ToString();
            tenderService.AddTender(tender, apiKey);

        }

    }
}
