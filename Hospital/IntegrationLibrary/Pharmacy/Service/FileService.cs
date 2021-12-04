using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Service
{
    public class FileService
    {
        private readonly string path = Directory.GetCurrentDirectory();
        public FileService()
        {

        }

        public string GetPath()
        {
            string dirPath = path.Substring(0, path.Length - 14) + "Data";
            return dirPath;
        }

        public DirectoryInfo CreateDirectory(string dirPath)
        {
            var directory = new DirectoryInfo(dirPath);
            return directory;
        }

        public bool CheckForPDFFiles(DirectoryInfo directory)
        {
            int i = 0;
            foreach (var file in directory.GetFiles("*.pdf", SearchOption.AllDirectories))
            {
                i++;
            }
            if(i > 0)
            {
                return true;
            }
            return false;
        }

        public void ZipFiles(DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles("*.pdf", SearchOption.AllDirectories))
            {
               
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddFile(file.FullName, "");
                        zip.Save(file.FullName.Substring(0, file.FullName.Length - 4) + ".zip");
                    }
                
            }
        }

        public bool CheckForZipFiles(DirectoryInfo directory)
        {
            int i = 0;
            foreach (var file in directory.GetFiles("*.zip", SearchOption.AllDirectories))
            {
                i++;
                File.Delete(file.FullName);
            }
            if (i > 0)
            {
                return true;
            }
            return false;
        }
    }
}
