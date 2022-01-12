using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.Pharmacy.DTO;
using RestSharp;
using System.Text.Json;
using System.Text;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationAPI.DTO;
using IntegrationLibrary.ReportingAndStatistics.Service;
using IntegrationLibrary.ReportingAndStatistics.Model;
using IntegrationLibrary.Shared.Model;

namespace IntegrationAPI.Controller
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly MedicineConsumptionService consumptionService;
        private readonly MedicineSpecificationService specificationService;

        public ReportsController(DatabaseContext context)
        {
            IMedicationConsumptionRepository repository = new MedicationConsumptionRepository(context);
            consumptionService = new MedicineConsumptionService(repository);
            specificationService = new MedicineSpecificationService();
        }

        [HttpPost]
        [Route("generateReport")]
        public void MakeReport(DateRangeDto dto)
        {
            DateTime startDate = DateTime.ParseExact(dto.StartDate, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(dto.EndDate, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

            DateRange dateRange = new DateRange(startDate, endDate);
            consumptionService.GenerateReport(dateRange);
        }

        [HttpPost]
        [Route("requestReport")]
        public String RequestReport(MedicineSpecRequestDto request)
        {
            return specificationService.RequestReport(request.MedicineName);
        }

        [HttpPost]
        [Route("medicationNames")]
        public String RequestMedicationNames(String pharmacyName)
        {
            return specificationService.RequestMedicationNames(pharmacyName);
        }


    }
}
