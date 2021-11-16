using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Integration_library.Pharmacy.Model;
using Integration_library.Pharmacy.Service;
using RestSharp;
using System.Text.Json;
using System.Text;
using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Repository;

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
            Response.Headers.Add("ApiKey", pharmacyService.GetPharmacyApiKey(feedback.PharmacyName));
            return Ok();
        }

    }
}
