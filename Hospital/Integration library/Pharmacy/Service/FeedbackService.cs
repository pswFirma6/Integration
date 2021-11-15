using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using Integration_library.Pharmacy.Repository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Service
{
    public class FeedbackService
    {

        private IFeedbackRepository repository;
        public FeedbackService(DatabaseContext context)
        {
            repository = new FeedbackRepository(context);
        }

        public List<Model.Feedback> GetFeedbacks()
        {
            return repository.GetAll();
        }

        public String GetFeedbackResponses()
        {
            string url = "https://localhost:44377/api/FeedbackResponses";
            return GetRequest(url);
        }

        public void SendFeedback(Feedback feedback)
        {
            repository.Add(feedback);
            string url = "https://localhost:44377/api/Feedbacks";
            PostRequest(url, feedback);
            repository.Save();
        }    
        
        private string GetRequest(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Get(request);
            return response.Content.ToString();
        }
        
        private void PostRequest(string url, Feedback feedback)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddJsonBody(feedback);
            var response = client.Post(request);
        }
    }
}
