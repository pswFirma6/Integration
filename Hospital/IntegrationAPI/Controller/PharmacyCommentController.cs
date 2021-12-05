using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.DTO;

namespace IntegrationAPI.Controller
{
    [ApiController]
    public class PharmacyCommentController
    {
        private readonly CommentService service;

        public PharmacyCommentController(DatabaseContext context)
        {
            ICommentRepository repository = new CommentRepository(context);
            service = new CommentService(repository);
        }

        [HttpGet]
        [Route("comments/{pharmacyName}")]
        public List<PharmacyComment> GetPharmacyByName([FromRoute] string pharmacyName)
        {
            return service.GetCommentsFromPharmacy(pharmacyName);
        }

        [HttpPost]
        [Route("addComment")]
        public void AddComment(PharmacyCommentDTO pharmacyComment)
        {
            PharmacyComment comment = new PharmacyComment( pharmacyComment.PharmacyName, pharmacyComment.Content);
            service.AddComment(comment);
        }

        [HttpDelete]
        [Route("deleteComment/{id}")]
        public void DeleteComment ([FromRoute] int id)
        {
            service.DeleteComment(id);
        }

    }
}
