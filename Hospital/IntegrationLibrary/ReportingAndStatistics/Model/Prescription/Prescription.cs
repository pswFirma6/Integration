using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.ReportingAndStatistics.Model
{
    public class Prescription
    {
        public int Id { get; set; }
        public string PrescriptionDate { get; set; }
        public TherapyInfo Therapy { get; set; }
        public PrescriptionInvolvedParties InvolvedParties { get; set; }
        public MedicineInfo Medicine { get; set; }

        public Prescription()
        {
        }

        public Prescription(int id, string prescriptionDate, TherapyInfo therapy, PrescriptionInvolvedParties involvedParties, MedicineInfo medicine)
        {
            Id = id;
            PrescriptionDate = prescriptionDate;
            Therapy = therapy;
            InvolvedParties = involvedParties;
            Medicine = medicine;
        }
        
    }

}
