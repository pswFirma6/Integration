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
using ZXing.QrCode;
using ZXing;
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


            CreateQRCode(prescription.Id,filePath);
            PdfImage pdfimage = PdfImage.FromFile(Path.Combine(filePath, "qrcode"+prescription.Id +".png"));
            PdfPageBase qrpage = doc.Pages.Add();
            qrpage.Canvas.DrawImage(pdfimage, new PointF(5, 5));

            StreamWriter File = new StreamWriter(Path.Combine(filePath, fileName), true);
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

        public Bitmap CreateQRCode(String qrvalue,String filePath)
        {
            var writer = new QRCodeWriter();
            var resultBit = writer.encode(qrvalue, BarcodeFormat.QR_CODE, 200, 200);

            var matrix = resultBit;
            int scale = 2;
            Bitmap result = new Bitmap(matrix.Width * scale, matrix.Height * scale);
            for (int x = 0; x < matrix.Height; x++)
            {
                for (int y = 0; y < matrix.Width; y++)
                {
                    Color pixel = matrix[x, y] ? Color.Black : Color.White;
                    for (int i = 0; i < scale; i++)
                        for (int j = 0; j < scale; j++)
                            result.SetPixel(x * scale + i, y * scale + j, pixel);

                }
            }

            Image image = (Image)result;
            image.Save(Path.Combine(filePath, "qrcode" + qrvalue + ".png"));
            return result;
        }
    }
}
