using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Model
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }

        public Address(string street, string city)
        {
            Street = street;
            City = city;
        }
    }
}
