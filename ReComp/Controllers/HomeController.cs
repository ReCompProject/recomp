using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReComp.Models;

namespace ReComp.Controllers
{
    public class HomeController : Controller
    {
        REEntities db = new REEntities();
        public ActionResult Index()
        {
            ViewBag.Home1 = db.Home1.First();
            ViewBag.Home1Image = db.Home1Image.ToList();
            ViewBag.Home2 = db.Home2.First();
            ViewBag.Home3Head = db.Home3Head.First();
            ViewBag.Home3 = db.Home3.ToList();
            ViewBag.Home4 = db.Home4.ToList();
            ViewBag.Home5 = db.Home5.First();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}