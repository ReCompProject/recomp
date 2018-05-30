using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ReComp.Models;

namespace ReComp.Controllers
{
    public class ProjectController : Controller
    {
        REcompEntities db = new REcompEntities();


        // GET: Project
        public ActionResult Project(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.sticphoto = db.Stick_Photo.Where(ph => ph.Stick_İD == id).ToList();
            ViewBag.sticker = db.Stickers.Find(id);
            ViewBag.Footer = db.Footors.First();
            ViewBag.rooms = db.Rooms.Where(rm => rm.Stick_İD == id).ToList();
            ViewBag.roomtype = db.Room_Type.ToList();
            ViewBag.sxem = db.Stick_sxem.Where(sxem => sxem.Stick_Id == id).ToList();

            return View();
        }
    }

   
}