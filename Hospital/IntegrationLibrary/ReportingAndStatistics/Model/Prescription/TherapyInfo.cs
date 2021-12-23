using IntegrationLibrary.Shared.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.ReportingAndStatistics.Model
{
    public class TherapyInfo
    {
        public DateRange TherapyDuration { get; set; }
        public string Diagnosis { get; set; }

        public TherapyInfo() { }

        public TherapyInfo(DateRange therapyDuration, string diagnosis)
        {
            TherapyDuration = therapyDuration;
            Diagnosis = diagnosis;
        }
    }
}
