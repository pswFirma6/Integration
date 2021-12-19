using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Tendering.DTO;
using IntegrationLibrary.Tendering.IRepository;
using IntegrationLibrary.Tendering.Repository;
using IntegrationLibrary.Tendering.Service;
using Microsoft.AspNetCore.Mvc;
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

        public TenderController(DatabaseContext context)
        {
            ITenderRepository tenderRepository = new TenderRepository(context);
            tenderService = new TenderService(tenderRepository);
        }

        [HttpGet]
        [Route("getTenders")]
        public List<TenderDto> GetTenders()
        {
            return tenderService.GetTendersWithItems();
        }

        [HttpPost]
        [Route("addTender")]
        public void AddTender(TenderDto tender)
        {  
            tenderService.AddTender(tender);
        }

    }
}
