using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Repository
{
    public class MedicineRepository : Repo<Medicine>, IMedicineRepository
    {
        public MedicineRepository(DatabaseContext context): base(context)
        {
        }
    }
}
