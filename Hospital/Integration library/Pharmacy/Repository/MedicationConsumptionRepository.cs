using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Repository
{
    class MedicationConsumptionRepository : Repo<Model.MedicationConsumption>, IMedicationConsumptionRepository
    {
        public MedicationConsumptionRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
