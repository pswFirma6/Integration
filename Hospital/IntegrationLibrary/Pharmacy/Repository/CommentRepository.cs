using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Repository
{
    public class CommentRepository: Repo<PharmacyComment>, ICommentRepository
    {
        public CommentRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
