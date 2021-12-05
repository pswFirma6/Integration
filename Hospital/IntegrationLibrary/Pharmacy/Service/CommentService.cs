using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Service
{
    public class CommentService
    {
        private readonly ICommentRepository repository;

        public CommentService(ICommentRepository iRepository)
        {
            repository = iRepository;
        }

        public CommentService() { }

        public void AddComment(PharmacyComment pharmacyComment)
        {
            repository.Add(pharmacyComment);
            repository.Save();
        }

        public List<PharmacyComment> GetCommentsFromPharmacy(string pharmacyName)
        {
            List<PharmacyComment> allPharmacyComments = repository.GetAll();
            List<PharmacyComment> pharmacyComments = new List<PharmacyComment>();
            foreach(var comment in allPharmacyComments)
            {
                if (comment.PharmacyName.Equals(pharmacyName)) pharmacyComments.Add(comment);
            }
            return pharmacyComments;
        }

        public void DeleteComment(int id)
        {
            repository.Delete(id);
            repository.Save();
        }
    }
}
