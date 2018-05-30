using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReComp.Models;

namespace ReComp.Controllers
{
    public class SingleController : Controller
    {
        REcompEntities db = new REcompEntities();
        public ActionResult Single(int? id)
        {
            ViewBag.Sticker = db.Stickers.ToList();
            ViewBag.StickPhoto = db.Stick_Photo.ToList();
            return View();
        }
    }
}