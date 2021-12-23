using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.ReportingAndStatistics.Model
{
    public class MedicineInfo
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string RecommendedDose { get; set; }

        public MedicineInfo() { }

        public MedicineInfo(string name, int quantity, string recommendedDose)
        {
            Name = name;
            Quantity = quantity;
            RecommendedDose = recommendedDose;
        }
    }
}
