using IntegrationLibrary.Partnership.IRepo;
using IntegrationLibrary.Partnership.Model;
using IntegrationLibrary.Partnership.Repository;
using IntegrationLibrary.Partnership.Service;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.Controller
{
    [ApiController]
    public class OfferController : ControllerBase
    {
        private OfferService service;
        private IOfferRepository repository;

        public OfferController(DatabaseContext dcontext)
        {
            repository = new OfferRepository(dcontext);
            service = new OfferService(repository);
        }

        [HttpGet]
        [Route("getOffers")]
        public List<Offer> GetOffers()
        {
            return service.GetOffers();
        }

        [HttpPost]
        [Route("postOffer")]
        public void PostOffer(Offer offer)
        {
            service.PostOffer(offer);
        }
    }

}
