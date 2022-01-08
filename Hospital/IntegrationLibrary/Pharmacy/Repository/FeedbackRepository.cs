using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Repository
{
    public class FeedbackRepository : Repo<Model.Feedback>, IFeedbackRepository
    {

        public FeedbackRepository(DatabaseContext context) : base(context)
        {
        }
    }

}
