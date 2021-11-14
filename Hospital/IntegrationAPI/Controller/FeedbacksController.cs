using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Integration_library.Pharmacy.Model;
using RestSharp;
using System.Text.Json;
using System.Text;

namespace IntegrationAPI.Controller
{
    [Route("api/[controller]")]

    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly PharmacyDbContext _pharmacycontext;

        public FeedbacksController(DatabaseContext context,PharmacyDbContext c)
        {
            _context = context;
            _pharmacycontext = c;
        }

        // GET: api/Feedbacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            return await _context.Feedbacks.ToListAsync();
        }


        [HttpGet]
        [Route("pharmacy/getFeedbackResponse")]
        public String GetFeedbackResponses()
        {

            String url = "https://localhost:44377/api/FeedbackResponses";
            var client = new RestClient(url);
            var request = new RestRequest();

            var response = client.Get(request);
            return response.Content.ToString();
        }

        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            String url = "https://localhost:44377/api/Feedbacks";
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddJsonBody(feedback);

            String hospitalApiKey = getPharmacyApiKey(feedback.PharmacyName);
            request.AddHeader("ApiKey", hospitalApiKey);

            Response.Headers.Add("ApiKey", hospitalApiKey);

            var response = client.Post(request);
            return CreatedAtAction("GetFeedback", new { id = feedback.Id }, feedback);
        }

        private String getPharmacyApiKey(String pharmacyName)
        {
            List<Pharmacy> result = new List<Pharmacy>();
            _pharmacycontext.Pharmacies.ToList().ForEach
                (pharmacy => result.Add(pharmacy));

            foreach(Pharmacy p in result){
                if (p.PharmacyName.Equals(pharmacyName))
                    return p.ApiKey;
            }

            return "Apoteka ne postoji u bazi";
        }

    }
}
