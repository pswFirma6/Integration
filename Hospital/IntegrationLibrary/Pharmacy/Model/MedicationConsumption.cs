using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Model
{
    public class MedicationConsumption
    {

        public int Id { get; set; }
        public string MedicationName { get; set; }
        public int MedicationId { get; set; }
        public DateTime Date { get; set; }
        public int AmountConsumed { get; set; }

        public MedicationConsumption() { }

        public MedicationConsumption(int id, string medicationName, int medicationId, DateTime date, int amountConsumed)
        {
            Id = id;
            MedicationName = medicationName;
            MedicationId = medicationId;
            Date = date;
            AmountConsumed = amountConsumed;
        }
    }
}
