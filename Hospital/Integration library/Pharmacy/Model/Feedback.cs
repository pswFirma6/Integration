using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Model
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string FeedbackDate { get; set; }
        public string PharmacyName { get; set; }
    }
}
