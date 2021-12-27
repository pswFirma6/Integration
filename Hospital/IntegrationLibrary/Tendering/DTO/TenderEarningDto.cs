using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.DTO
{
    public class TenderEarningDto
    {
        public string PharmacyName { get; set; }
        public double Earning { get; set; }

        public TenderEarningDto() { }

        public TenderEarningDto(string pharmacyName, double earning)
        {
            PharmacyName = pharmacyName;
            Earning = earning;
        }
    }
}
