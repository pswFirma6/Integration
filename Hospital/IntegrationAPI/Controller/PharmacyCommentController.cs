using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationLibrary.Pharmacy.Model;

namespace IntegrationAPI.Controller
{
    [ApiController]
    public class PharmacyCommentController
    {
        private CommentService service;
        private ICommentRepository repository;

        public PharmacyCommentController(DatabaseContext context)
        {
            repository = new CommentRepository(context);
            service = new CommentService(repository);
        }

        [HttpGet]
        [Route("comments/{pharmacyName}")]
        public List<PharmacyComment> GetPharmacyByName([FromRoute] string pharmacyName)
        {
            return service.GetCommentsFromPharmacy(pharmacyName);
        }
    }
}
