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
    public class ProjectsController : Controller
    {
        private REcompEntities db = new REcompEntities();
        ViewModelProject model = new ViewModelProject();
        // GET: Admin/Projects
        public ActionResult Index()
        {
            model.PProject = db.Projects.ToList();
            model.PProject_Photo = db.Project_Photo.ToList();
            return View(model);
        }

        // GET: Admin/Projects/Details/5

        // GET: Admin/Projects/Create
        public ActionResult Create()
        {
            model.TheProject = db.Projects.First();
            model.TheProject_Photo = db.Project_Photo.First();
            model.PProject_Photo = db.Project_Photo.ToList();
            return View(model);
        }

        // POST: Admin/Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelProject model)
        {


            if (ModelState.IsValid)
            {
                db.Projects.Add(model.TheProject);
                db.SaveChanges();
                model.TheProject_Photo.Project_İD = model.TheProject.İD;
                db.Project_Photo.Add(model.TheProject_Photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Admin/Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.TheProject = db.Projects.Find(id);
            model.PProject_Photo = db.Project_Photo.Where(prf => prf.Project_İD == id).ToList();

            
            if (model.TheProject == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Admin/Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModelProject model)
        {

            if (ModelState.IsValid)
            {

                db.Entry(model.TheProject).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Admin/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Project_Photo> Pfoto = db.Project_Photo.Where(pf => pf.Project_İD == id).ToList();
            foreach (var ph in Pfoto)
            {
                db.Project_Photo.Remove(ph);
                db.SaveChanges();
            }
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
