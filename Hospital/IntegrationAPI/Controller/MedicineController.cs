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

namespace IntegrationAPI.Controller
{

    [ApiController]
    public class MedicineController : ControllerBase
    {
        public MedicineController(){}

        [HttpPost]
        [Route("orderMedicine")]
        public IActionResult OrderMedicine(OrderMedicineDto medicine)
        {
            OrderMedicineViaGrpc(medicine);
            return Ok();
        }

        private void OrderMedicineViaGrpc(OrderMedicineDto medicine)
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


    }
}
