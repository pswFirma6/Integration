using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IntegrationLibrary.Pharmacy.Repository
{
    public class PharmacyRepository : Repo<Model.Pharmacy>, IPharmacyRepository
    {
        
        public PharmacyRepository(DatabaseContext context):base(context)
        {
        }
    }
}
