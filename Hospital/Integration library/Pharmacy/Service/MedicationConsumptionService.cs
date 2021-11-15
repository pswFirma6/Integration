using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using Integration_library.Pharmacy.Repository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Service
{
    public class MedicationConsumptionService
    {
        private IMedicationConsumptionRepository repository;
        public MedicationConsumptionService(DatabaseContext context)
        {
            repository = new MedicationConsumptionRepository(context);
        }
    }
}
