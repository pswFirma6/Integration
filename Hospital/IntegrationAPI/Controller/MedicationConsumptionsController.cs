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
using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Repository;


namespace IntegrationAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationConsumptionsController : ControllerBase
    {
        private MedicationConsumptionService service;
        private IMedicationConsumptionRepository repository;

        public MedicationConsumptionsController(DatabaseContext context)
        {
            repository = new MedicationConsumptionRepository(context);
            service = new MedicationConsumptionService(repository);
        }

        [HttpGet]
        [Route("report")]
        public void MakeReport()
        {
            service.GenerateReport(new Integration_library.Pharmacy.DTO.TimePeriodDTO(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2)));
        }

    }
}
