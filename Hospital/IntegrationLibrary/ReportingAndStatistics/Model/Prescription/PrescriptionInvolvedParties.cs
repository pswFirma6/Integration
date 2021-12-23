using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.ReportingAndStatistics.Model
{
    public class PrescriptionInvolvedParties
    {
        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        public PrescriptionInvolvedParties() { }

        public PrescriptionInvolvedParties(string patientName, string doctorName)
        {
            PatientName = patientName;
            DoctorName = doctorName;
        }
    }
}
