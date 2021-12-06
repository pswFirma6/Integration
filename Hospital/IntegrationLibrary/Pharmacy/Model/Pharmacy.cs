using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Model
{
    public class Pharmacy
    {
        [Key]
        public string PharmacyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ApiKey { get; set; }
        public string PharmacyPicture { get; set; }
        public string FileProtocol { get; set; }

        public Pharmacy() { }
        public Pharmacy(string pharmacyName, string apiKey, string address, string city, string pharmacyPicture, string fileProtocol)
        {
            PharmacyName = pharmacyName;
            ApiKey = apiKey;
            Address = address;
            City = city;
            PharmacyPicture = pharmacyPicture;
            FileProtocol = fileProtocol;
        }
    }
}
