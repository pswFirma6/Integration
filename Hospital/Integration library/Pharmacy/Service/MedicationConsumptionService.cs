using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using Integration_library.Pharmacy.DTO;
using Integration_library.Pharmacy.Repository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Integration_library.Pharmacy.Service
{
    public class MedicationConsumptionService
    {
        private IMedicationConsumptionRepository repository;
        public MedicationConsumptionService(IMedicationConsumptionRepository iRepository)
        {
            repository = iRepository;
        }

        public void GenerateReport(TimePeriodDTO timePeriod)
        {
            String filePath = @"C:\Users\Milica\Desktop\PSW\psw_hospital\Hospital";
            String fileName = "MedicationConsumptionReport ("
                             + timePeriod.StartDate.ToString("MM/dd/yyyy") + " - " + timePeriod.EndDate.ToString("MM/dd/yyyy") + ").txt";


            StreamWriter File = new StreamWriter(Path.Combine(filePath, "MedicationConsumptionReport.txt"), true);
            File.Write(GetReportContent(timePeriod));
            File.Close();

        }

        private String GetReportContent(TimePeriodDTO timePeriod)
        {
            String content = "Medication consumption report for " + timePeriod.StartDate.ToString("MM/dd/yyyy") + " - " + timePeriod.StartDate.ToString("MM/dd/yyyy") + " :\r\n\n";
            List<MedicationConsumption> requiredConsumptions = GetConsumptionsForTimePeriod(timePeriod);
            List<String> evaluatedMedications = new List<String>();

            foreach (MedicationConsumption c in requiredConsumptions)
            {
                if (!isEvaluated(evaluatedMedications, c.MedicationName))
                {
                    content += GetReportContentForCertainMedication(c.MedicationName, requiredConsumptions);
                    evaluatedMedications.Add(c.MedicationName);
                }
            }
            return content;
        }

        private bool isEvaluated(List<String> list, String MedicationName)
        {
            return list.Any(p => p.Equals(MedicationName));
        }

        private String GetReportContentForCertainMedication(String medicationName, List<MedicationConsumption> consumptions)
        {
            String content = "";
            List<MedicationConsumption> individualConsumptions = GetConsumptionsForCertainMedication(medicationName, consumptions);

            content += "Total consumption for item - " + medicationName + " is " + GetConsumptionsAmountForCertainMedication(individualConsumptions).ToString() + ".\r\n";
            content += "Individual consumptions by days:\r\n";

            foreach (MedicationConsumption consumption in individualConsumptions)
                content += "Date: " + consumption.Date.ToString("MM/dd/yyyy") + ", consumed amount is " + consumption.AmountConsumed.ToString() + ".\r\n";

            content += "\r\n";
            return content;
        }

        private List<MedicationConsumption> GetConsumptionsForCertainMedication(String medicationName, List<MedicationConsumption> consumptions)
        {
            List<MedicationConsumption> consumptionsForMedication = new List<MedicationConsumption>();

            foreach (MedicationConsumption consumption in consumptions)
            {
                if (consumption.MedicationName.Equals(medicationName))
                    consumptionsForMedication.Add(consumption);
            }
            return consumptionsForMedication;
        }

        private int GetConsumptionsAmountForCertainMedication(List<MedicationConsumption> medicationConsumptions)
        {
            int amount = 0;
            foreach (MedicationConsumption consumption in medicationConsumptions)
                amount += consumption.AmountConsumed;
            return amount;
        }

        public List<MedicationConsumption> GetConsumptionsForTimePeriod(TimePeriodDTO timePeriod)
        {
            List<MedicationConsumption> consumptionsForTimePeriod = new List<MedicationConsumption>();

            foreach (MedicationConsumption consumption in repository.GetAll())
            {
                if (IsWithinRange(consumption.Date, timePeriod))
                    consumptionsForTimePeriod.Add(consumption);
            }
            return consumptionsForTimePeriod;
        }

        public bool IsWithinRange(DateTime consumptionDate, TimePeriodDTO timePeriod)
        {
            return DateTime.Compare(timePeriod.StartDate, consumptionDate) <= 0 && DateTime.Compare(timePeriod.EndDate, consumptionDate) >= 0;
        }

        public List<MedicationConsumption> GetConsumptions()
        {
            List<MedicationConsumption> consumptions = repository.GetAll();

            return consumptions;
        }



    }
}
