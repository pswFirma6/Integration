using System;
using System.IO;
using Renci.SshNet;
using IntegrationLibrary.ReportingAndStatistics.Model;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;
using ZXing.QrCode;
using ZXing;
using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using IntegrationLibrary.Exceptions;

namespace IntegrationLibrary.ReportingAndStatistics.Service
{
    public class PrescriptionService
    {
        private readonly PharmacyService pharmacyService;

        public PrescriptionService()
        {
            DatabaseContext context = new DatabaseContext();
            IPharmacyRepository pharmacyRepository = new PharmacyRepository(context);
            pharmacyService = new PharmacyService(pharmacyRepository);
        }

        public void GenerateReport(PharmacyPrescription prescription)
        {
            String filePath = GetPrescriptionsDirectory();
            String QRCodesDirectoryPath = GetQRcodesDirectory();
            String fileName = "Prescription" +prescription.Prescription.Id +".pdf";

            PdfDocument doc = new PdfDocument();
            PdfPageBase page = doc.Pages.Add();

            Pharmacy.Model.Pharmacy pharmacy = pharmacyService.GetPharmacyByName(prescription.PharmacyName);

            page.Canvas.DrawString(GetContent(prescription.Prescription), new PdfFont(PdfFontFamily.Helvetica, 11f), new PdfSolidBrush(Color.Black), 10, 10);

            if (pharmacy.PharmacyConnectionInfo.FileProtocol.Equals("HTTP"))
            {
                CreateQRCode(prescription.Prescription.Id.ToString(), QRCodesDirectoryPath);
                PdfImage pdfimage = PdfImage.FromFile(Path.Combine(QRCodesDirectoryPath, "QRcode" + prescription.Prescription.Id + ".png"));
                PdfPageBase qrpage = doc.Pages.Add();
                qrpage.Canvas.DrawImage(pdfimage, new PointF(5, 5));
            }


            StreamWriter File = new StreamWriter(Path.Combine(filePath, fileName), true);
            doc.SaveToStream(File.BaseStream);
            File.Close();
            SendReport(pharmacy.PharmacyConnectionInfo.FileProtocol,fileName);
        }

        public string GetPrescriptionsDirectory()
        {
            return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(),"Data\\Prescriptions\\");
        }

        public string GetQRcodesDirectory()
        {
            return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "Data\\QRcodes\\");
        }

        public String GetContent(Prescription prescription)
        {
            String content = "\n";
            content += "Prescription for medicine: " + prescription.Medicine.Name + "\r\n\n"
                    + "Quantity: " + prescription.Medicine.Quantity + "\r\n"
                    + "Recommended dose is: " + "\r\n\n"
                    + "Patiend: " + prescription.InvolvedParties.PatientName + "\r\n"
                    + "Patient diagnosis: " + prescription.Therapy.Diagnosis + "\r\n\n"
                    + "Therapy start: " + prescription.Therapy.TherapyDuration.StartDate + "\r\n"
                    + "Therapy end: " + prescription.Therapy.TherapyDuration.EndDate + "\r\n\n\n"
                    + "Doctor: " + prescription.InvolvedParties.DoctorName + "\r\n"
                    + "Prescription date:" + prescription.PrescriptionDate + "\r\n"
                    + "Expiration date:" + prescription.PrescriptionDate + "\r\n";
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
            image.Save(Path.Combine(filePath, "QRcode" + qrvalue + ".png"));
            return result;
        }

        public void SendReport(string method,string filePath)
        {
            if (method.Equals("HTTP"))
            {
                //TODO: Send file using http requets
            }
            else
            {
                SendUsingSftp(Path.Combine(GetPrescriptionsDirectory(),filePath));
            }
        }

        public void SendUsingSftp(string fileName)
        {
            using (SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.56.1", "tester", "password")))
            {
                try
                {
                    client.Connect();
                } catch
                {
                    throw new DomainNotFoundException("Sftp server refuses to connect!");
                }

                using (Stream stream = File.OpenRead(fileName))
                {
                    client.UploadFile(stream, @"\public\prescriptions\" + Path.GetFileName(fileName), null);
                }
                client.Disconnect();
            }

        }
    }
}
