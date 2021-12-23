using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Model
{
    public class PharmacyInfo
    {
        public string PharmacyName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ApiKey { get; set; }
        public string FileProtocol { get; set; }
        public string Url { get; set; }

        public PharmacyInfo(string pharmacyName, string street, string city, string apiKey, string fileProtocol, string url)
        {
            PharmacyName = pharmacyName;
            Street = street;
            City = city;
            ApiKey = apiKey;
            FileProtocol = fileProtocol;
            Url = url;
        }

        public PharmacyInfo()
        {
        }
    }
}
