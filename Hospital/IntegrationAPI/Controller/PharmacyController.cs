using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.Pharmacy.DTO;
using IntegrationLibrary.Pharmacy.Model;
using System.IO;
using System.Net.Http.Headers;
using Grpc.Core;
using Microsoft.Extensions.Configuration;

namespace IntegrationAPI.Controller
{

    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly PharmacyService service;
        private readonly IConfiguration _config;

        public PharmacyController(DatabaseContext context, IConfiguration config)
        {
            IPharmacyRepository pharmacyRepository = new PharmacyRepository(context);
            service = new PharmacyService(pharmacyRepository);
            _config = config;
        }

        [HttpGet]
        [Route("pharmacyNames")]
        public List<string> getPharmacyNames()
        {  
            return service.GetPharmacyNames();
        }

        [HttpGet]
        [Route("pharmacies")]
        public List<IntegrationLibrary.Pharmacy.Model.Pharmacy> GetPharmacies()
        {
            return service.GetPharmacies();
        }

        [HttpPost]
        [Route("registerPharmacy")]
        public IActionResult RegisterPharmacy(PharmacyInfo info)
        {
            service.AddPharmacy(info);
            var apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? _config.GetValue<string>("ApiKey");
            return Ok(apiKey.ToString());
        }

        [HttpPost]
        [Route("checkMedicine")]
        public List<PharmacyMedicineAvailabilityDTO> CheckMedicine(MedicineDto medicine)
        {
            return service.CheckPharmacyMedicines(medicine);
        }

        [HttpPost]
        [Route("checkPharmacyMedicine")]
        public bool CheckMedicineOfCertainPharmacy(CheckAvailabilityDto isAvailable)
        {
            return checkMedicineViaGrpc(isAvailable);
        }

        private bool checkMedicineViaGrpc(CheckAvailabilityDto medicine)
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
        public IntegrationLibrary.Pharmacy.Model.Pharmacy GetPharmacyByName([FromRoute] string pharmacyName)
        {
            return service.GetPharmacyByName(pharmacyName);
        }

        [HttpPut]
        [Route("editPharmacy")]
        public void EditPharmacy(IntegrationLibrary.Pharmacy.Model.Pharmacy pharmacy)
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

