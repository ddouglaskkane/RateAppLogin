using RateAppLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateAppLogin.Controllers
{
    public class CBLoginController : Controller
    {
        // GET: CBLogin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserLandingView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CBUserModel model)
        {
            ConsumerReviewEntities cbe = new ConsumerReviewEntities();
            var s = cbe.GetCBLoginInfo(model.UserName, model.Password);

            var item = s.FirstOrDefault();
            if (item == "Success")
            {

                return View("Home/Index");
            }
            else if (item == "User Does not Exists")

            {
                ViewBag.NotValidUser = item;

            }
            else
            {
                ViewBag.Failedcount = item;
            }

            return View("Index");
        }

    }
}