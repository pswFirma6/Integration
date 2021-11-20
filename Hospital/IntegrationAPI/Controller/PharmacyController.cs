using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Integration_library.Pharmacy.Model;
using RestSharp;
using System.Text.Json;
using System.Text;
using Integration_library.Pharmacy.Service;
using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Repository;

namespace IntegrationAPI.Controller
{
    //[Route("api/[controller]")]

    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private PharmacyService service;
        private IPharmacyRepository pharmacyRepository;

        public PharmacyController(DatabaseContext context)
        {
            pharmacyRepository = new PharmacyRepository(context);
            service = new PharmacyService(pharmacyRepository);
        }

        [HttpGet]
        [Route("pharmacyNames")]
        public List<string> getPharmacyNames()
        {  
            return service.GetPharmacyNames();
        }

        [HttpGet]
        [Route("pharmacies")]
        public List<Pharmacy> GetPharmacies()
        {
            return service.GetPharmacies();
        }

        [HttpPost]
        [Route("registerPharmacy")]
        public IActionResult AddPharmacy(Pharmacy pharmacy)
        {
            service.AddPharmacy(pharmacy);
            return Ok();
        }

       
    }
}
