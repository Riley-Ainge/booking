using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Assignment2.Models;

namespace Assignment2.Controllers
{
    public class BookingsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Bookings";
            return View();
        }
        [HttpGet]
        public IActionResult Search(string term)
        {
            RestClient restClient = new RestClient("http://localhost:64571/");
            RestRequest restRequest = new RestRequest("api/bookings/{term}", Method.Get);
            restRequest.AddUrlSegment("term", term);
            RestResponse restResponse = restClient.Execute(restRequest);
            return Ok(restResponse.Content);
        }
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            RestClient restClient = new RestClient("http://localhost:64571/");
            RestRequest restRequest = new RestRequest("api/bookings", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);
            return Ok(restResponse.Content);
        }
        [HttpPost]
        public IActionResult Insert([FromBody] Booking booking)
        {
            if(booking == null)
            {
                return BadRequest();
            }
            RestClient restClient = new RestClient("http://localhost:64571/");
            RestRequest restRequest = new RestRequest("api/bookings", Method.Get);
            RestResponse restResponse = restClient.Execute(restRequest);
            List<Booking> bookings = JsonConvert.DeserializeObject<List<Booking>>(restResponse.Content);
                restClient = new RestClient("http://localhost:64571/");
                restRequest = new RestRequest("api/bookings", Method.Post);
                bool inBooking = false;
                foreach (Booking book in bookings)
                {
                    if (((book.Start < booking.Start && booking.End < book.End) || (booking.Start < book.Start && book.Start < booking.End) || (booking.Start < book.End && book.End < booking.End) || (booking.Start < book.Start && book.End < booking.End)) && booking.Start >= DateTime.Now && booking.CentreID == book.CentreID)
                    {
                        inBooking = true;
                    }
                }
                Booking returnBooking = null;
                if (inBooking)
                {
                    restRequest.AddJsonBody(JsonConvert.SerializeObject(booking));
                    restResponse = restClient.Execute(restRequest);
                    returnBooking = JsonConvert.DeserializeObject<Booking>(restResponse.Content);
                }
                returnBooking = JsonConvert.DeserializeObject<Booking>(restResponse.Content);

                if (returnBooking != null)
                {
                    return Ok(returnBooking);
                }
                else
                {
                    return BadRequest();
                }
        }
    }
}
