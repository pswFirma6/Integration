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
        

        [IgnoreOnNondevelopmentPhase]
        public void CheckPdfForHttpMethod()
        {
            Prescription prescription = new Prescription
               (9, "1.1.2022.", "",new TherapyInfo(new DateRange(new DateTime(),new DateTime()),"Diagnosis"),
                                new PrescriptionInvolvedParties("Patient name", "Doctor name"), new MedicineInfo("Medicine name", 22 ,"recommended dose"));
            PharmacyPrescription pp = new PharmacyPrescription("Benu",prescription);

            service.GenerateReport(pp);

            CheckIfPrescriptionIsCreated("Prescription" + prescription.Id + ".pdf").ShouldBeTrue();
            CheckIfQrCodeIsCreated("QRcode" + prescription.Id + ".png").ShouldBeTrue();

            //Empty(service.GetPrescriptionsDirectory());
            //Empty(service.GetQRcodesDirectory());      

        }

        [IgnoreOnNondevelopmentPhase]
        public void CheckPdfForSftpMethod()
        {

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

    public sealed class IgnoreOnNondevelopmentPhase : FactAttribute
    {
        private static TestConfig config = new TestConfig();
        public IgnoreOnNondevelopmentPhase()
        {
            if (!IsDevelopmentEnvironment())
            {
                Skip = "Written only for development phase.";
            }
        }

        private static bool IsDevelopmentEnvironment()
        {
            return !config.Environment.Equals("Development");
        }
    }
}