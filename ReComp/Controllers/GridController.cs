using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReComp.Models;

namespace ReComp.Controllers
{
    public class GridController : Controller
    {
        // GET: Grid
        REcompEntities db = new REcompEntities();
        public ActionResult Grid()
        {
            ViewBag.Stickers = db.Stickers.ToList();
            ViewBag.StickType = db.Stick_Type.ToList();
            ViewBag.SticPhoto = db.Stick_Photo.ToList();
            ViewBag.Room = db.Rooms.ToList();
            ViewBag.Footer = db.Footors.First();
            return View();
        }
        public ActionResult _Layout()
        {
            ViewBag.Footer = db.Footors.First();
            return View();
        }
    }
}