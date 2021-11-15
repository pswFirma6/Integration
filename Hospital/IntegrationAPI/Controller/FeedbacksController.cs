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

namespace IntegrationAPI.Controller
{
    [Route("api/[controller]")]

    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private FeedbackService service;
        private PharmacyService pharmacyService;
    
        public FeedbacksController(DatabaseContext context)
        {
            service = new FeedbackService(context);
            pharmacyService = new PharmacyService(context);
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
