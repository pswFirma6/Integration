using IntegrationLibrary.ReportingAndStatistics.Service;
using IntegrationLibrary.ReportingAndStatistics.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
using System.IO;
using IntegrationTests;
using IntegrationLibrary.Shared.Model;

namespace IntegrationAppTests.IntegrationTests
{
    public class PrescriptionTests
    {
        private PrescriptionService service = new PrescriptionService();
        private TestConfig config = new TestConfig();


        public PrescriptionTests()
        {
            //if (config.Environment.Equals("Development")) return;

        }

        [Fact]
        public void CheckPdfForHttpMethod()
        {
            if (!config.Environment.Equals("Development")) return;

            Prescription prescription = new Prescription
               (9, "1.1.2022.", "",new TherapyInfo(new DateRange(new DateTime(),new DateTime()),"Diagnosis"),
                                new PrescriptionInvolvedParties("Patient name", "Doctor name"), new MedicineInfo("Medicine name", 22 ,"recommended dose"));
            PharmacyPrescription pp = new PharmacyPrescription("Benu",prescription);

            service.GenerateReport(pp);

            CheckIfPrescriptionIsCreated("Prescription" + prescription.Id + ".pdf").ShouldBeTrue();
            CheckIfQrCodeIsCreated("QRcode" + prescription.Id + ".png").ShouldBeTrue();

            //Empty(service.GetPrescriptionsDirectory());
            //Empty(service.GetQRcodesDirectory());      

            string env = config.Environment;
            env.ShouldBe("Development");
        }

        [Fact]
        public void CheckPdfForSftpMethod()
        {
            if (!config.Environment.Equals("Development")) return;

            Prescription prescription = new Prescription
                (10, "1.1.2022.", "", new TherapyInfo(new DateRange(new DateTime(), new DateTime()), "Diagnosis"),
                                new PrescriptionInvolvedParties("Patient name", "Doctor name"), new MedicineInfo("Medicine name", 22, "recommended dose"));

            PharmacyPrescription pp = new PharmacyPrescription("Benu", prescription);

            service.GenerateReport(pp);

            CheckIfPrescriptionIsCreated("Prescription" + prescription.Id + ".pdf").ShouldBeTrue();
            CheckIfQrCodeIsCreated("QRcode" + prescription.Id + ".png").ShouldBeFalse();

            // Empty(service.GetPrescriptionsDirectory());
            
        }


        public bool CheckIfPrescriptionIsCreated(string fileName)
        {
            return File.Exists(Path.Combine(service.GetPrescriptionsDirectory(), fileName));
        }

        public bool CheckIfQrCodeIsCreated(string fileName)
        {
            return File.Exists(Path.Combine(service.GetQRcodesDirectory(), fileName));
        }

        public static void Empty(string directory)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(directory);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }


    }
}