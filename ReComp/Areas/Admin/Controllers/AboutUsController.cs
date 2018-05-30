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
    [Authorize(Roles = "Administrator,Editor")]
    public class AboutUsController : Controller
    {
        private REcompEntities db = new REcompEntities();
        ViewModelAbout model = new ViewModelAbout();
        // GET: Admin/AboutUs
        public ActionResult Index()
        {
            if (model.TheAboutService == null)
            {
                model.TheAboutU = db.AboutUs.First();

                model.AAboutService = db.AboutServices.ToList();
                return View(model);
            }
            model.TheAboutU = db.AboutUs.First();
            model.TheAboutService = db.AboutServices.First();
            model.AAboutService = db.AboutServices.ToList();
            return View(model);
        }



        // GET: Admin/AboutUs/Create
        public ActionResult Create()
        {
            ViewModelAbout model = new ViewModelAbout();
            return View(model);
        }

        // POST: Admin/AboutUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelAbout model)
        {

            if (ModelState.IsValid)
            {
                //db.AboutUs.Add(model.TheAboutU);
                //db.SaveChanges();
                //model.TheAboutService.AboutİD = model.TheAboutU.İD;
                db.AboutServices.Add(model.TheAboutService);
                db.SaveChanges();
                return RedirectToAction("Edit" + "/1");
            }

            return View();
        }

        // GET: Admin/AboutUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (model.TheAboutService == null)
            {
                model.AAboutService = db.AboutServices.ToList();
                model.TheAboutU = db.AboutUs.Find(id);
                return View(model);
            }
            model.AAboutService = db.AboutServices.ToList();
            model.TheAboutU = db.AboutUs.Find(id);
            model.TheAboutService = db.AboutServices.First();
            if (model.TheAboutU == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: Admin/AboutUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModelAbout model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model.TheAboutU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit" + "/1", "AboutUs");
            }
            return View(model);
        }

        // GET: Admin/AboutUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutService aboutU = db.AboutServices.Find(id);
            if (aboutU != null)
            {
                db.AboutServices.Remove(aboutU);
                db.SaveChanges();
            }
            return RedirectToAction("Edit" + "/1");
        }

        // POST: Admin/AboutUs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    AboutU aboutU = db.AboutUs.Find(id);
        //    db.AboutUs.Remove(aboutU);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
