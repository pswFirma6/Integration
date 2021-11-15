using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Model
{
    public class MedicationConsumption
    {
        public MedicationConsumption(int id, string medicationName, int medicationId, DateTime date, int amountConsumed)
        {
            Id = id;
            MedicationName = medicationName;
            MedicationId = medicationId;
            Date = date;
            AmountConsumed = amountConsumed;
        }

        public int Id { get; set; }
        public string MedicationName { get; set; }
        public int MedicationId { get; set; }
        public DateTime Date { get; set; }
        public int AmountConsumed { get; set; }


    }
}
