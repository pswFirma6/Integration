using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Repository
{
    public class MedicineRepository : Repo<Medicine>, IMedicineRepository
    {
        public MedicineRepository(DatabaseContext context): base(context)
        {
        }
    }
}
