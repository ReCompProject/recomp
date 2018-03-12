using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReComp.Models;

namespace ReComp.Controllers
{
    public class ProjectController : Controller
    {
        REEntities db = new REEntities();
        // GET: Project
        public ActionResult Project()
        {
            ViewBag.Home5 = db.Home5.First();
            ViewBag.Project1 = db.Project1.First();
            ViewBag.Project1Image = db.Project1Image.ToList();
            ViewBag.Project2 = db.Project2.ToList();
            ViewBag.Project3Head = db.Project3Head.First();
            ViewBag.Project3 = db.Project3.ToList();
            return View();
        }
    }
}