using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Service;
using RestSharp;
using System.Text.Json;
using System.Text;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Repository;

namespace IntegrationAPI.Controller
{
    [Route("api/[controller]")]

    [ApiController]
    public class FileController : ControllerBase
    {
        private FileService service;

        public FileController()
        {
            service = new FileService();
        }

        [HttpGet]
        public void ZipOldFiles()
        {
            var directory = service.CreateDirectory(service.GetPath());
            if (service.CheckForPDFFiles(directory))
            {
                service.ZipFiles(directory);
            }
        }
    }
}
