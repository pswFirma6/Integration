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
    public class TenderController
    {
        private ITenderRepository tenderRepository;
        private TenderService tenderService;

        public TenderController(DatabaseContext context)
        {
            tenderRepository = new TenderRepository(context);
            tenderService = new TenderService(tenderRepository);
        }

        [HttpPost]
        [Route("addTender")]
        public void addTender(TenderDTO tender)
        {
            tenderService.addTender(tender);
        }

    }
}
