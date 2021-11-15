using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Integration_library.Pharmacy.Model;
using Integration_library.Pharmacy.Service;
using RestSharp;
using System.Text.Json;
using System.Text;

namespace IntegrationAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationConsumptionsController : ControllerBase
    {
        private MedicationConsumptionService service;

        public MedicationConsumptionsController(DatabaseContext context)
        {
            service = new MedicationConsumptionService(context);
        }

    }
}
