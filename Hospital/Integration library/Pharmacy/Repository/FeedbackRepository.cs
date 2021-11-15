using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration_library.Pharmacy.Repository
{
    public class FeedbackRepository : Repo<Model.Feedback>, IFeedbackRepository
    {

        public FeedbackRepository(DatabaseContext context) : base(context)
        {
        }
    }

}
