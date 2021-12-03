using IntegrationLibrary.Pharmacy.Service;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class FileTests
    {
        private FileService service;
        private string path = Directory.GetCurrentDirectory();

        public FileTests()
        {

        }

        [Fact]
        public void Check_if_directory_exists()
        {
            service = new FileService();
            var directory = service.CreateDirectory(path);
            directory.Exists.ShouldBeTrue();
        }

        [Fact]
        public void Check_For_PDF_files()
        {
            service = new FileService();
            var directory = service.CreateDirectory(path);
            service.CheckForPDFFiles(directory).ShouldBeTrue();
        }

        [Fact]
        public void Check_Compression()
        {
            service = new FileService();
            var directory = service.CreateDirectory(path);
            service.ZipFiles(directory);
            service.CheckForZipFiles(directory).ShouldBeTrue();
        }

       
    }
}
