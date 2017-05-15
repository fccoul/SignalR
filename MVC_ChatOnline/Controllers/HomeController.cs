using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_ChatOnline.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your Contact Page";
            return View();
        }

        public ActionResult Chat()
        {
            ViewBag.Message = "Your Chat Page";
            return View();
        }

    }
}
