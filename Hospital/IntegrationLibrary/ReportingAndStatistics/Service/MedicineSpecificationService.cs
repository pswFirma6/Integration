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

namespace IntegrationLibrary.ReportingAndStatistics.Service
{
    public class MedicineSpecificationService
    {
        private IMedicationConsumptionRepository repository;
        private string server = "https://localhost:44377/";

        public MedicineSpecificationService(IMedicationConsumptionRepository iRepository)
        {
            repository = iRepository;
        }
        public String RequestReport(ReportRequestDTO req)
        {
            var client = new RestClient(server + "report");
            var request = new RestRequest();

            request.AddJsonBody(req.MedicationName);
            var response = client.Post(request);
            if (response.Content.ToString().Equals("\"OK\""))
                GetSpecificationnReport(req.MedicationName);

            return response.Content.ToString();
        }

        public string GetSpecificationsDirectory()
        {
            return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "Data\\Specifications\\");
        }

        private void GetSpecificationnReport(String medicineName)
        {
            String fileName = "MedicineSpecification (" + medicineName + ").pdf";
            String localFile = Path.Combine(GetSpecificationsDirectory(), fileName);
            String serverFile = @"\public\specifications" + fileName;

            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.56.1", "tester", "password")))
            {
                client.Connect();
                using (Stream stream = File.OpenWrite(localFile))
                {
                    client.DownloadFile(serverFile, stream, null);
                }
                client.Disconnect();
            }
        }

        public String RequestMedicationNames(string pharmacyName)
        {
            var client = new RestClient(server + "pharmacyMedicine");
            var request = new RestRequest();
            var response = client.Get(request);

            return response.Content.ToString();
        }


    }
}
