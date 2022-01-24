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
using AutoMapper;

using IntegrationLibrary.Exceptions;

namespace IntegrationAPI.Controller
{
    [ApiController]
    public class PharmacyCommentController
    {
        private readonly CommentService service;
        private readonly IMapper _mapper;

        public PharmacyCommentController(DatabaseContext context, IMapper mapper)
        {
            ICommentRepository repository = new CommentRepository(context);
            service = new CommentService(repository);
            _mapper = mapper;
        }

        [HttpGet]
        [Route("comments/{pharmacyName}")]
        public List<PharmacyComment> GetPharmacyByName([FromRoute] string pharmacyName)
        {
            List<PharmacyComment> pharmacyComments = service.GetCommentsFromPharmacy(pharmacyName);
            if (pharmacyComments == null)
            {
                throw new DomainNotFoundException("No comments from" + pharmacyName  +" pharmacy by name ");
            }
            return pharmacyComments;
        }

        [HttpPost]
        [Route("addComment")]
        public void AddComment(PharmacyCommentDto commentDto)
        {
            //PharmacyComment comment = new PharmacyComment( pharmacyComment.PharmacyName, pharmacyComment.Content);
            var comment = _mapper.Map<PharmacyComment>(commentDto);
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
