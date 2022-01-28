using IntegrationLibrary.Exceptions;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Repository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationLibrary.Pharmacy.Service
{
    public class FeedbackService
    {
        private string server = "http://localhost:44377/api";
        private IFeedbackRepository repository;
        public FeedbackService(IFeedbackRepository iRepository)
        {
            repository = iRepository;
        }

        public List<Model.Feedback> GetFeedbacks()
        {
            return repository.GetAll();
        }

        public String GetFeedbackResponses()
        {
            string url = server + "/FeedbackResponses";
            return GetRequest(url);
        }

        public void SendFeedback(Feedback feedback)
        {
            feedback.Id = repository.GetAll().Count + 1;
            repository.Add(feedback);
            string url = server + "/Feedbacks";
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
