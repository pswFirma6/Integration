using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.DTO;
using IntegrationLibrary.Pharmacy.Repository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Renci.SshNet;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;

namespace IntegrationLibrary.ReportingAndStatistics.Service
{
    public class MedicineConsumptionService
    {
        private IMedicationConsumptionRepository repository;
        private string server = "https://localhost:44377/";

        public MedicineConsumptionService(IMedicationConsumptionRepository iRepository)
        {
            repository = iRepository;
        }

        public void GenerateReport(TimePeriodDTO timePeriod)
        {
            String filePath = GetConsumptionsDirectory();
            String fileName = "MedicationConsumptionReport.pdf";

            PdfDocument doc = new PdfDocument();
            PdfPageBase page = doc.Pages.Add();

            page.Canvas.DrawString(GetReportContent(timePeriod), new PdfFont(PdfFontFamily.Helvetica, 11f), new PdfSolidBrush(Color.Black), 10, 10);

            StreamWriter File = new StreamWriter(Path.Combine(filePath, fileName), true);
            doc.SaveToStream(File.BaseStream);
            File.Close();

            SendReport(Path.Combine(filePath, fileName));

        }

        public string GetConsumptionsDirectory()
        {
            return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "Data\\Consumptions\\");
        }

        public void SendReport(String filePath)
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.56.1", "tester", "password")))
            {
                client.Connect();

                using (Stream stream = File.OpenRead(filePath))
                {
                    client.UploadFile(stream, @"\public\consumptions" + Path.GetFileName(filePath), null);
                }
                client.Disconnect();
            }
        }

        private String GetReportContent(TimePeriodDTO timePeriod)
        {

            String content = "Medication consumption report for " + timePeriod.StartDate.ToString("MM/dd/yyyy") + " - " + timePeriod.EndDate.ToString("MM/dd/yyyy") + " :\r\n\n";

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


        public bool isEvaluated(List<String> list, String MedicationName)
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

        public List<MedicationConsumption> GetConsumptionsForCertainMedication(String medicationName, List<MedicationConsumption> consumptions)
        {
            List<MedicationConsumption> consumptionsForMedication = new List<MedicationConsumption>();

            foreach (MedicationConsumption consumption in consumptions)
            {
                if (consumption.MedicationName.Equals(medicationName))
                    consumptionsForMedication.Add(consumption);
            }
            return consumptionsForMedication;
        }

        public int GetConsumptionsAmountForCertainMedication(List<MedicationConsumption> medicationConsumptions)
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
    }
}
