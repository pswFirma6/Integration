using IntegrationAPI.DTO;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.ReportingAndStatistics.Model;
using IntegrationLibrary.ReportingAndStatistics.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntegrationAPI.Controller
{
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly PrescriptionService service = new PrescriptionService();

        [HttpPost]
        [Route("sendPrescription")]
        public String GeneratePrescriptionFile(Prescription prescription)
        {
            service.GenerateReport(prescription);
            return "OK";
        }
    }
}
