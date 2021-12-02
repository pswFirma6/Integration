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

namespace IntegrationAPI.Controller
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly MedicineConsumptionService consumptionService;
        private readonly MedicineSpecificationService specificationService;
        private readonly PrescriptionService prescriptionService;

        public ReportsController(DatabaseContext context)
        {
            IMedicationConsumptionRepository repository = new MedicationConsumptionRepository(context);
            consumptionService = new MedicineConsumptionService(repository);
            specificationService = new MedicineSpecificationService();
            prescriptionService = new PrescriptionService();
        }

        [HttpPost]
        [Route("generateReport")]
        public void MakeReport(TimePeriodStringDTO period)
        {
            DateTime startDate = DateTime.ParseExact(period.startDate, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(period.endDate, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

            TimePeriodDTO timePeriod = new TimePeriodDTO(startDate, endDate);
            consumptionService.GenerateReport(timePeriod);
        }

        [HttpPost]
        [Route("requestReport")]
        public String RequestReport(ReportRequestDTO request)
        {
            return specificationService.RequestReport(request);
        }

        [HttpPost]
        [Route("medicationNames")]
        public String RequestMedicationNames(String pharmacyName)
        {
            return specificationService.RequestMedicationNames(pharmacyName);
        }

        [HttpPost]
        [Route("sendPrescription")]
        public String GeneratePrescriptionFile(PrescriptionReportDto prescription)
        {
            prescriptionService.GenerateReport(prescription.Prescription, prescription.Method);
            return "OK";
        }

    }
}
