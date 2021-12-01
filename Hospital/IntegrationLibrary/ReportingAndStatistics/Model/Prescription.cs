using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.ReportingAndStatistics.Model
{
    public class Prescription
    {
        public string Id { get; set; }
        public string MedicineName { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public string RecommendedDose {get; set;}
        public string PrescriptionDate { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public string TherapyStart { get; set; }
        public string TherapyEnd { get; set; }
        public string Diagnosis { get; set; }
    }
}
