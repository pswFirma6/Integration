using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.DTO
{
    public class EmailDTO
    {
        public string PharmacyEmail { get; set; }
        public string PharmacyPassword { get; set; }

        public EmailDTO(){ }

        public EmailDTO(string pharmacyEmail, string pharmacyPassword)
        {
            PharmacyEmail = pharmacyEmail;
            PharmacyPassword = pharmacyPassword;
        }
    }
}
