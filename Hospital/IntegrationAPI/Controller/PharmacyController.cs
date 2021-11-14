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

namespace IntegrationAPI.Controller
{
    //[Route("api/[controller]")]

    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private PharmacyService service;

        public PharmacyController(PharmacyDbContext c)
        {
            service = new PharmacyService(c);
        }

        [HttpGet]
        [Route("pharmacyNames")]
        public List<string> getPharmacyNames()
        {  
            return service.GetPharmacyNames();
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
