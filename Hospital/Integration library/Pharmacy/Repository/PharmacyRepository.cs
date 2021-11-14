using Integration_library.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Integration_library.Pharmacy.Repository
{
    public class PharmacyRepository
    {
        private readonly PharmacyDbContext _pharmacycontext = new PharmacyDbContext();

        public PharmacyRepository(PharmacyDbContext pc)
        {
            _pharmacycontext = pc;
        }

        public PharmacyRepository() { }

        public List<Model.Pharmacy> GetPharmacies()
        {
            return _pharmacycontext.Pharmacies.ToList();
        }

        public void AddPharmacy(Model.Pharmacy pharmacy)
        {
            _pharmacycontext.Pharmacies.Add(pharmacy);
            _pharmacycontext.SaveChanges();
        }

    }
}
