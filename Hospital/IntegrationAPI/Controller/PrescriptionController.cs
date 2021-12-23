using AutoMapper;
using IntegrationAPI.DTO;
using IntegrationLibrary.Pharmacy.DTO;
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
using static IntegrationAPI.Mapper.Mapper;

namespace IntegrationAPI.Controller
{
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly PrescriptionService service = new PrescriptionService();
        private readonly MappingProfile mapper = new MappingProfile();

        [HttpPost]
        [Route("sendPrescription")]
        public String GeneratePrescriptionFile(PrescriptionDTO dto)
        {
            var prescription = mapper.MapPharmacyPrescription(dto);
            service.GenerateReport(prescription);
            return "OK";
        }
    }
}
