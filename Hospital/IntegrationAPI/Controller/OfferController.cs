using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using Integration_library.Pharmacy.Repository;
using Integration_library.Pharmacy.Service;
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
