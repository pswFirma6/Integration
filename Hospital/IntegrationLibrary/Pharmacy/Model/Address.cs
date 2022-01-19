using IntegrationLibrary.Shared.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IntegrationLibrary.Pharmacy.Model
{
    public class Address: ValueObject
    {
        public string Street { get; set; }
        public string City { get; set; }

        public Address(string street, string city)
        {
            Street = street;
            City = city;
            ValidateAddress();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
        }

        private void ValidateAddress()
        {
            if (string.IsNullOrEmpty(Street) || string.IsNullOrEmpty(City))
                throw new ConnectionInfoException("Address cannot be empty");
        }
    }

    public class AddressException : Exception
    {
        public AddressException(String message) : base(message) { }
    }
}
