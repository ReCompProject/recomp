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
    public class FootorsController : Controller
    {
        private REcompEntities db = new REcompEntities();

        // GET: Admin/Footors
        public ActionResult Index()
        {
            return View(db.Footors.First());
        }

        // GET: Admin/Footors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Footor footor = db.Footors.Find(id);
            if (footor == null)
            {
                return HttpNotFound();
            }
            return View(footor);
        }

        // POST: Admin/Footors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,SubTitle,Number,Mail,Address")] Footor footor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(footor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit" + "/1");
            }
            return View(footor);
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
