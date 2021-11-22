using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Integration_library.Pharmacy.Model;
using RestSharp;
using System.Text.Json;
using System.Text;
using Integration_library.Pharmacy.Service;
using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Repository;
using Integration_library.Pharmacy.DTO;
using System.Diagnostics;

namespace IntegrationAPI.Controller
{

    [ApiController]
    public class MedicineController : ControllerBase
    {
        private MedicineService medicineService;
        private IMedicineRepository medicineRepository;
        private PharmacyService pharmacyService;
        private IPharmacyRepository pharmacyRepository;

        public MedicineController(DatabaseContext context)
        {
            medicineRepository = new MedicineRepository(context);
            medicineService = new MedicineService(medicineRepository);
            pharmacyRepository = new PharmacyRepository(context);
            pharmacyService = new PharmacyService(pharmacyRepository);
        }

        [HttpPost]
        [Route("orderMedicine")]
        public IActionResult OrderMedicine(CheckAvailabilityDTO medicineForOrder)
        {
            medicineService.OrderMedicine(medicineForOrder);
            pharmacyService.OrderFromCertainPharmacy(medicineForOrder);
            return Ok();
        }


    }
}
