using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RateAppLogin.Models;
//using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace RateAppLogin.Controllers
{
    public class RatingController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<Ratings> ratingList = new List<Ratings>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://tcapprateapi.azurewebsites.net/api/Commands"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ratingList = JsonConvert.DeserializeObject<List<Ratings>>(apiResponse);
                }
            }
            return View(ratingList);
        }

        public ViewResult GetRating() => View();

        [HttpGet]
        public async Task<ActionResult> GetRating(int Id)
        {
            Ratings rating = new Ratings();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://tcapprateapi.azurewebsites.net/api/Commands/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    rating = JsonConvert.DeserializeObject<Ratings>(apiResponse);
                }
            }
            return View(rating);
        }

        //public ViewResult AddRating() => View();

        [HttpPost]
        public async Task<ActionResult> AddRating(Ratings rating)
        {

            List<Ratings> ratingListCount = new List<Ratings>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://tcapprateapi.azurewebsites.net/api/Commands"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ratingListCount = JsonConvert.DeserializeObject<List<Ratings>>(apiResponse);
                }
            }
            int _runningListCount = new int();

            foreach (Ratings aratingListCount in ratingListCount)
            {
                _runningListCount += 1;
            }
            _runningListCount += 1;
            rating.Id = _runningListCount;
            rating.RateID = ViewBag.RateID.Value;

            Ratings receivedRating = new Ratings();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(rating), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://tcapprateapi.azurewebsites.net/api/Commands/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedRating = JsonConvert.DeserializeObject<Ratings>(apiResponse);
                }
            }
            return View(receivedRating);
        }

        public ActionResult AddRating()
        {
            //Creating generic list
            List<SelectListItem> ObjList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Excelent", Value = "1" },
                new SelectListItem { Text = "Moderate", Value = "2" },
                new SelectListItem { Text = "Needs Improvement", Value = "3" }

            };
            //Assigning generic list to ViewBag
            ViewBag.RateID = ObjList;

            return View();

        }
    }
}