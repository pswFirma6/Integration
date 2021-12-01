using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Renci.SshNet;
using IntegrationLibrary.ReportingAndStatistics.Model;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;
//using ZXing.QrCode;
//using ZXing;
using System.Drawing.Imaging;

namespace IntegrationLibrary.ReportingAndStatistics.Service
{
    public class PrescriptionService
    {
        private string server = "https://localhost:44377/";

        public PrescriptionService()
        {
        }
        public void GenerateReport(Prescription prescription)
        {
            String filePath = Directory.GetCurrentDirectory();
            String fileName = "Prescription.pdf";

            PdfDocument doc = new PdfDocument();
            PdfPageBase page = doc.Pages.Add();

            page.Canvas.DrawString(GetContent(prescription), new PdfFont(PdfFontFamily.Helvetica, 11f), new PdfSolidBrush(Color.Black), 10, 10);

            StreamWriter File = new StreamWriter(Path.Combine(filePath, fileName), true);
            //File.Write(GetContent(prescription));
            doc.SaveToStream(File.BaseStream);
            File.Close();

            //SendReport(Path.Combine(filePath, fileName));
        }

        public String GetContent(Prescription prescription)
        {
            String content = "\n";
            content += "Prescription for medicine: " + prescription.MedicineName + ".\r\n\n"
                    + "Quantity: " + prescription.Quantity + ".\r\n"
                    + "Medicine description: " + prescription.Description + ".\r\n"
                    + "Recommended dose is: " + ".\r\n\n"

                    + "Patiend: " + prescription.DoctorName + ".\r\n"
                    + "Patient diagnosis: " + prescription.Diagnosis + ".\r\n\n"

                    + "Therapy start: " + prescription.TherapyStart + ".\r\n"
                    + "Therapy end: " + prescription.TherapyEnd + ".\r\n\n\n"

                    + "Doctor: " + prescription.DoctorName + ".\r\n"
                    + "Prescription date:" + prescription.PrescriptionDate + ".\r\n"
                    + "Expiration date:" + prescription.PrescriptionDate + ".\r\n";
            return content;
        }
    }
}
