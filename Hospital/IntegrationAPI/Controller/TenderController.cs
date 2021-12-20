using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Model;
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
        private readonly TenderOfferService tenderOfferService;
        private readonly IConfiguration _config;

        public TenderController(DatabaseContext context, IConfiguration config)
        {
            ITenderRepository tenderRepository = new TenderRepository(context);
            ITenderOfferRepository tenderOfferRepository = new TenderOfferRepository(context);
            tenderService = new TenderService(tenderRepository);
            tenderOfferService = new TenderOfferService(tenderOfferRepository);
            _config = config;
        }

        [HttpGet]
        [Route("getTenders")]
        public List<TenderDto> GetTenders()
        {
            return tenderService.GetTendersWithItems();
        }

        [HttpGet]
        [Route("getTenderOffers")]
        public List<TenderOfferDto> GetTenderOffers()
        {
            return tenderOfferService.GetTendersWithItems();
        }

        [HttpPost]
        [Route("addTender")]
        public void AddTender(TenderDto tender)
        {
            var apiKey = _config.GetValue<string>("ApiKey");
            tenderService.AddTender(tender, apiKey);

        }

        [HttpPost]
        [Route("closeTender")]
        public void CloseTender(TenderOffer offer)
        {
            tenderService.CloseTender(offer.TenderId);

        }

    }
}
