using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using Renci.SshNet;
using System.Diagnostics;

namespace IntegrationLibrary.ReportingAndStatistics.Service
{
    public class MedicineSpecificationService
    {
        private readonly string server = "http://localhost:44377/";

        public MedicineSpecificationService()
        {
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
            return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "Data\\Specifications");
        }

        private void GetSpecificationnReport(String medicineName)
        {
            String fileName = "MedicineSpecification (" + medicineName + ").pdf";
            String localFile = Path.Combine(GetSpecificationsDirectory(), fileName);
            Debug.Write(localFile);
            String serverFile = @"\public\specifications" + fileName;

            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("127.0.0.1 ", "tester", "password")))
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
