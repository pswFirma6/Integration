using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using IntegrationLibrary.Pharmacy.Model;
using IntegrationLibrary.Pharmacy.Service;
using IntegrationLibrary.Pharmacy.IRepository;
using IntegrationLibrary.Pharmacy.Repository;

namespace IntegrationAPI.Controller
{
    [Route("api/[controller]")]

    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private FeedbackService service;
        private PharmacyService pharmacyService;
        private IFeedbackRepository feedbackRepository;
        private IPharmacyRepository pharmacyRepository;

        public FeedbacksController(DatabaseContext context)
        {
            feedbackRepository = new FeedbackRepository(context);
            pharmacyRepository = new PharmacyRepository(context);
            service = new FeedbackService(feedbackRepository);
            pharmacyService = new PharmacyService(pharmacyRepository);
        }

        // GET: api/Feedbacks
        [HttpGet]
        public List<Feedback> GetFeedbacks()
        {
            return service.GetFeedbacks();
        }

        [HttpGet]
        [Route("pharmacy/getFeedbackResponse")]
        public String GetFeedbackResponses()
        {
            return service.GetFeedbackResponses();
        }

        [HttpPost]
        public IActionResult PostFeedback(Feedback feedback)
        {
           
            service.SendFeedback(feedback);
           
            Response.Headers.Add("ApiKey",
                pharmacyService.GetPharmacyByName(feedback.PharmacyName).PharmacyConnectionInfo.ApiKey);
            return Ok();
        }

    }
}
