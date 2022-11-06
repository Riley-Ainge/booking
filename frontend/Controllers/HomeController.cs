using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using Assignment2.Models;

namespace Assignment2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            RestClient restClient = new RestClient("http://localhost:64571/");
            RestRequest restRequest = new RestRequest("api/centres/", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);

            List<Centre> bookings = JsonConvert.DeserializeObject<List<Centre>>(restResponse.Content);
            return View(bookings);
        }
    }
}
