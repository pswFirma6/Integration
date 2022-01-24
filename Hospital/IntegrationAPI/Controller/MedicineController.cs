using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegrationLibrary.Pharmacy.Model;
using RestSharp;
using System.Text.Json;
using System.Text;
using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.Pharmacy.DTO;
using System.Diagnostics;
using Grpc.Core;
using IntegrationLibrary.Exceptions;
using Microsoft.Extensions.Configuration;

namespace IntegrationAPI.Controller
{

    [ApiController]
    public class MedicineController : ControllerBase
    {
        private PharmacyService pharmacyService;
        private readonly EmailService emailService;
        private readonly IConfiguration _config;
        private IPharmacyRepository pharmacyRepository;

        public MedicineController(DatabaseContext context, IConfiguration config)
        {
            pharmacyRepository = new PharmacyRepository(context);
            pharmacyService = new PharmacyService(pharmacyRepository);
            _config = config;
            emailService = new EmailService();
        }

        [HttpPost]
        [Route("orderMedicine")]
        public IActionResult OrderMedicine(OrderMedicineDto medicineForOrder)
        {
            OrderMedicineViaGrpc(medicineForOrder);
            SendMessageToHospital(medicineForOrder);
            return Ok();
        }

        private void OrderMedicineViaGrpc(OrderMedicineDto medicine)
        {
            try
            {
                var request = new MedicineAvailabilityMessage
                {
                    MedicineName = medicine.Medicine.Name,
                    MedicineQuantity = medicine.Medicine.Quantity
                };
                var channel = new Channel("localhost:4111", ChannelCredentials.Insecure);
                var client = new MedicineService.MedicineServiceClient(channel);
                client.medicineUrgentProcurement(request);
            }
            catch
            {
                throw new DomainNotFoundException("Grpc refuses to connect!");
            }
        }

        private void SendMessageToHospital(OrderMedicineDto medicine)
        {
            string hospitalEmail = _config.GetValue<string>("Email");
            var message = new Message(new string[] {hospitalEmail }, "URGENT PROCUREMENT", "You have successfully ordered:<br> " + medicine.Medicine.Name + ", quantity:"  + medicine.Medicine.Quantity + "<br>");
            EmailDto pharmacyEmail = pharmacyService.GetPharmacyEmailByName(medicine.PharmacyName);
            emailService.SendEmail(message, pharmacyEmail);
        }

    }
}
