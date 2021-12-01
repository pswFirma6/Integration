using IntegrationLibrary.ReportingAndStatistics.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class PrescriptionReportDto
    {
        public Prescription Prescription { get; set; }
        public String Method { get; set; }

        public PrescriptionReportDto(Prescription prescription, string method)
        {
            this.Prescription = prescription;
            this.Method = method;
        }

        public PrescriptionReportDto()
        {
        }
    }
}
