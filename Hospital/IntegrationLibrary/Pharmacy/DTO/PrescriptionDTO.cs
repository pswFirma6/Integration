using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.DTO
{
    public class PrescriptionDTO
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public string RecommendedDose { get; set; }
        public string PrescriptionDate { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string TherapyStart { get; set; }
        public string TherapyEnd { get; set; }
        public string Diagnosis { get; set; }
        public string PharmacyName { get; set; }

        public PrescriptionDTO () { }

        public PrescriptionDTO (int id, string medicineName, int quantity, string recommendedDose, string prescriptionDate, string doctorName, 
            string patientName, string therapyStart, string therapyEnd, string diagnosis, string pharmacyName)
        {
            Id = id;
            MedicineName = medicineName;
            Quantity = quantity;
            RecommendedDose = recommendedDose;
            PrescriptionDate = prescriptionDate;
            DoctorName = doctorName;
            PatientName = patientName;
            TherapyStart = therapyStart;
            TherapyEnd = therapyEnd;
            Diagnosis = diagnosis;
            PharmacyName = pharmacyName;
        }
    }
}
