using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.DTO
{
    public class EmailDto
    {
        public string PharmacyEmail { get; set; }
        public string PharmacyPassword { get; set; }

        public EmailDto(){ }

        public EmailDto(string pharmacyEmail, string pharmacyPassword)
        {
            PharmacyEmail = pharmacyEmail;
            PharmacyPassword = pharmacyPassword;
        }
    }
}
