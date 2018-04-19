using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReComp.Models;

namespace ReComp.Areas.Admin.Controllers
{
    public class StickersController : Controller
    {
        private REcompEntities db = new REcompEntities();
        ViewModelStick model = new ViewModelStick();
        // GET: Admin/Stickers
        public ActionResult Index()
        {
            model.SSticker = db.Stickers.ToList();
            model.SSticker_Photo = db.Stick_Photo.ToList();
            model.SStick_Type = db.Stick_Type.ToList();
            model.RRoom = db.Rooms.ToList();
            model.RRoom_Type = db.Room_Type.ToList();
            return View(model);
        }

        // GET: Admin/Stickers/Create
        public ActionResult Create()
        {
            ViewBag.Project_İD = new SelectList(db.Projects, "İD", "Name");
            ViewBag.Type_İD = new SelectList(db.Stick_Type, "İD", "Name");
            return View();
        }

        // POST: Admin/Stickers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "İD,Name,Project_İD,Price,Short_info,Type_İD,Area,GarageStatus,Status")] Sticker sticker)
        {
            if (ModelState.IsValid)
            {
                db.Stickers.Add(sticker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Project_İD = new SelectList(db.Projects, "İD", "Name", sticker.Project_İD);
            ViewBag.Type_İD = new SelectList(db.Stick_Type, "İD", "Name", sticker.Type_İD);
            return View(sticker);
        }

        // GET: Admin/Stickers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.TheSticker = db.Stickers.Find(id);
            model.SSticker_Photo = db.Stick_Photo.Where(prf => prf.Stick_İD == id).ToList();
            model.PProject = db.Projects.ToList();
            model.SStick_Type = db.Stick_Type.ToList();
            if (model.TheSticker == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Admin/Stickers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModelStick model,string type, string prtype)
        {
            if (ModelState.IsValid)
            {
                model.TheSticker.Project_İD = Convert.ToInt32(prtype);
                model.TheSticker.Type_İD = Convert.ToInt32(type);
                db.Entry(model.TheSticker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Stickers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sticker sticker = db.Stickers.Find(id);
            if (sticker == null)
            {
                return HttpNotFound();
            }
            return View(sticker);
        }

        // POST: Admin/Stickers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sticker sticker = db.Stickers.Find(id);
            db.Stickers.Remove(sticker);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
