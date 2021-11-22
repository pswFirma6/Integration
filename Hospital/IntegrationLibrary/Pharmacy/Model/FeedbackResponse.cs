using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Model
{
    public class FeedbackResponse
    {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public string Content { get; set; }
        public string FeedbackResponseDate { get; set; }
    }
}
