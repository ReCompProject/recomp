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
        REcompEntities db = new REcompEntities();
        public ActionResult Index()
        {
            ViewBag.About = db.AboutUs.First();
            ViewBag.AboutService = db.AboutServices.ToList();
            ViewBag.Home3 = db.Projects.ToList();
            ViewBag.Projects = db.Projects.ToList();
            ViewBag.ProjectPhoto = db.Project_Photo.ToList();
            ViewBag.Stickers = db.Stickers.ToList();
            ViewBag.StickType = db.Stick_Type.ToList();
            ViewBag.SticPhoto = db.Stick_Photo.ToList();
            ViewBag.Footer = db.Footors.First();
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
        public ActionResult _Layout()
        {
            ViewBag.Footer = db.Footors.First();
            return View();
        }
    }
}


