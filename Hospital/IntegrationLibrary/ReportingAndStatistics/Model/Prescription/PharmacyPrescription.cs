using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.ReportingAndStatistics.Model
{
    public class PharmacyPrescription
    {
        public string PharmacyName { get; set; }
        public Prescription Prescription { get; set; }

        public PharmacyPrescription() { }

        public PharmacyPrescription(string pharmacyName, Prescription prescription)
        {
            PharmacyName = pharmacyName;
            Prescription = prescription;
        }
    }
}
