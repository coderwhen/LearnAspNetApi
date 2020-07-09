using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestOA.WebApp.Models;

namespace TestOA.WebApp.Controllers
{
    //[MyActionFilter]
    public class HomeController : Controller
    {
        [MyActionFilter]
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [MyActionFilter]
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }
    }
}