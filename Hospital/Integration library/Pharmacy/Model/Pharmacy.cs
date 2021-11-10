using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Integration_library.Pharmacy.Model
{
    public class Pharmacy
    {
        [Key]
        public string PharmacyName { get; set; }
        public string ApiKey { get; set; }

        public Pharmacy() { }

        public Pharmacy(string pharmacyName, string apiKey)
        {
            PharmacyName = pharmacyName;
            ApiKey = apiKey;
        }
    }
}
