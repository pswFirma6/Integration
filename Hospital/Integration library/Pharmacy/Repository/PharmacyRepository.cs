using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Integration_library.Pharmacy.Repository
{
    public class PharmacyRepository : Repo<Model.Pharmacy>, IPharmacyRepository
    {
        
        public PharmacyRepository(DatabaseContext context):base(context)
        {
        }

       


    }
}
