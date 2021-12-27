using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Tendering.DTO
{
    public class TenderEarningDto
    {
        public string Name { get; set; }
        public double Earning { get; set; }

        public TenderEarningDto() { }

        public TenderEarningDto(string name, double earning)
        {
            Name = name;
            Earning = earning;
        }
    }
}
