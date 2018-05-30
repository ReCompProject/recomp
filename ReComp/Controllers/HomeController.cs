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
            ViewBag.Projects = db.Projects.Where(pr => pr.Status == true).ToList();
            ViewBag.ProjectPhoto = db.Project_Photo.ToList();
            ViewBag.Stickers = db.Stickers.Where(st => st.Status == false).ToList();
            ViewBag.StickType = db.Stick_Type.ToList();
            ViewBag.objectType = db.Object_Type.ToList();
            ViewBag.SticPhoto = db.Stick_Photo.ToList();
            ViewBag.Cities = db.Cities.ToList();
            ViewBag.Footer = db.Footors.First();
            return View();
        }


        public ActionResult Stickers(int page = 1)
        {
            int perPage = 3;
            IList<Sticker> sticker = db.Stickers.OrderBy(x => x.İD).Skip((page - 1) * perPage).Take(perPage).ToList();
            ViewBag.TotalItems = Math.Round((double)(db.Stickers.ToList().Count / perPage)) + 1;
            return View(sticker);
        }

        public ActionResult About()
        {


            return View();
        }

        public ActionResult Projects()
        {
            ViewBag.Footer = db.Footors.First();
            ViewBag.Projects = db.Projects.Where(pr => pr.Status==true).ToList();
            ViewBag.ProjectPhoto = db.Project_Photo.ToList();
            ViewBag.ProjectService = db.Project_sevices.ToList();

            return View();
        }
        
    }
}


