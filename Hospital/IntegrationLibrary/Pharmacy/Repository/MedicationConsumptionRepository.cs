using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Repository
{
    public class MedicationConsumptionRepository : Repo<Model.MedicationConsumption>, IMedicationConsumptionRepository
    {
        public MedicationConsumptionRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
