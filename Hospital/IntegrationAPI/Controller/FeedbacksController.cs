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
        [Route("pharmacyNames")]
        public List<string> getPharmacyNames()
        {
            List<string> names = new List<string>();
            _pharmacycontext.Pharmacies.ToList().ForEach(pharmacy => names.Add(pharmacy.PharmacyName));
            return names;
        }

        [HttpPost]
        [Route("registerPharmacy")]
        public IActionResult AddPharmacy(Pharmacy pharmacy)
        {
            pharmacy.ApiKey = generateApiKey();
            _pharmacycontext.Pharmacies.Add(pharmacy);
            _pharmacycontext.SaveChanges();
            return Ok();
        }

        private String generateApiKey()
        {
            const string src = "abcdefghijklmnopqrstuvwxyz0123456789";
            int length = 16;
            var sb = new StringBuilder();
            Random RNG = new Random();
            for (var i = 0; i < length; i++)
            {
                var c = src[RNG.Next(0, src.Length)];
                sb.Append(c);
            }
            return sb.ToString();
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

        // GET: api/Feedbacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
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



        // DELETE: api/Feedbacks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Feedback>> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.Id == id);
        }
    }
}
