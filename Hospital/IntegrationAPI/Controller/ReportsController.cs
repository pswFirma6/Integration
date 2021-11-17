using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Integration_library.Pharmacy.Model;
using Integration_library.Pharmacy.Service;
using Integration_library.Pharmacy.DTO;
using RestSharp;
using System.Text.Json;
using System.Text;
using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Repository;
using IntegrationAPI.DTO;

namespace IntegrationAPI.Controller
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private ReportsService service;
        private IMedicationConsumptionRepository repository;

        public ReportsController(DatabaseContext context)
        {
            repository = new MedicationConsumptionRepository(context);
            service = new ReportsService(repository);
        }

        [HttpPost]
        [Route("report")]
        public void MakeReport(TimePeriodStringDTO period)
        {
            DateTime startDate = DateTime.ParseExact(period.startDate, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(period.endDate, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

            TimePeriodDTO timePeriod = new TimePeriodDTO(startDate,endDate);
            service.GenerateReport(timePeriod);
        }

        [HttpPost]
        [Route("report")]
        public void RequestReport(String medicineName)
        {
            service.RequestReport(medicineName);
        }

    }
}
