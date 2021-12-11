using System;
using System.Runtime;
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
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.StaticFiles;
using Grpc.Core;

namespace IntegrationAPI.Controller
{
    //[Route("api/[controller]")]

    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private PharmacyService service;
        private IPharmacyRepository pharmacyRepository;

        public PharmacyController(DatabaseContext context)
        {
            pharmacyRepository = new PharmacyRepository(context);
            service = new PharmacyService(pharmacyRepository);
        }

        [HttpGet]
        [Route("pharmacyNames")]
        public List<string> getPharmacyNames()
        {  
            return service.GetPharmacyNames();
        }

        [HttpGet]
        [Route("pharmacies")]
        public List<Pharmacy> GetPharmacies()
        {
            return service.GetPharmacies();
        }

        [HttpPost]
        [Route("registerPharmacy")]
        public IActionResult AddPharmacy(Pharmacy pharmacy)
        {
            service.AddPharmacy(pharmacy);
            return Ok();
        }

        [HttpPost]
        [Route("checkMedicine")]
        public List<PharmacyMedicineAvailabilityDTO> CheckMedicine(MedicineDTO medicine)
        {
            return service.CheckPharmacyMedicines(medicine);
        }

        [HttpPost]
        [Route("checkPharmacyMedicine")]
        public bool CheckMedicineOfCertainPharmacy(CheckAvailabilityDTO isAvailable)
        {
            return checkMedicineViaGrpc(isAvailable);
        }

        private bool checkMedicineViaGrpc(CheckAvailabilityDTO medicine)
        {
            bool response = false;
            var request = new MedicineAvailabilityMessage
            {
                MedicineName = medicine.Medicine.Name,
                MedicineQuantity = medicine.Medicine.Quantity
            };
            var channel = new Channel("localhost:4111", ChannelCredentials.Insecure);
            var client = new MedicineService.MedicineServiceClient(channel);
            var reply = client.checkMedicineAvailability(request);
            response = reply.IsAvailable;
            return response;
        }

        [HttpGet]
        [Route("pharmacyByName/{pharmacyName}")]
        public Pharmacy GetPharmacyByName([FromRoute] string pharmacyName)
        {
            return service.GetPharmacyByName(pharmacyName);
        }

        [HttpPut]
        [Route("editPharmacy")]
        public void EditPharmacy(Pharmacy pharmacy)
        {
            service.EditPharmacy(pharmacy);
        }

        [HttpPost]
        [Route("uploadImage/{pharmacyName}")]
        public IActionResult UploadImage([FromRoute] string pharmacyName)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullpath = Path.Combine(pathToSave, fileName);

                    using (var stream = new FileStream(fullpath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    service.AddPictureToPharmacy(pharmacyName, fileName);
                    return Ok();
                } else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        [Route("getImage/{imageName}")]
        public string GetImageBase64([FromRoute] string imageName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", imageName);
            var bytes = System.IO.File.ReadAllBytes(filePath);
            string file = Convert.ToBase64String(bytes);

            return JsonSerializer.Serialize(file);
        }
 
    }
}
