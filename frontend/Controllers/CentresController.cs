using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Assignment2.Models;

namespace Assignment2.Controllers
{
    public class CentresController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Centres";
            return View();
        }
        [HttpGet]
        public IActionResult Search(string id)
        {
            RestClient restClient = new RestClient("http://localhost:64571/");
            RestRequest restRequest = new RestRequest("api/centres", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);
            List<Centre> returnCentre = JsonConvert.DeserializeObject<List<Centre>>(restResponse.Content);
            List<Centre> foundCentres = new List<Centre>();
            foreach (Centre centre in returnCentre)
            {
                if (id == null || centre.Name.Contains(id) || centre.Id == 0)
                {
                    foundCentres.Add(centre);
                }
            }
            return Ok(JsonConvert.SerializeObject(foundCentres, Formatting.Indented));
        }
        
        [HttpPost]
        public IActionResult Insert([FromBody] Centre centre)
        {
            RestClient restClient = new RestClient("http://localhost:64571/");
            RestRequest restRequest = new RestRequest("api/centres", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(centre));
            RestResponse restResponse = restClient.Execute(restRequest);

            Centre returnCentre = JsonConvert.DeserializeObject<Centre>(restResponse.Content);
            if (returnCentre != null)
            {
                return Ok(returnCentre);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
