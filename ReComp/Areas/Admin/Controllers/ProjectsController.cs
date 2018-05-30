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

namespace ReComp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator,Editor")]
    public class ProjectsController : Controller
    {
        private REcompEntities db = new REcompEntities();
        ViewModelProject model = new ViewModelProject();
        private static List<HttpPostedFileBase> files = new List<HttpPostedFileBase>();
        // GET: Admin/Projects
        public ActionResult Index()
        {
            model.PProject = db.Projects.ToList();
            model.PProject_Photo = db.Project_Photo.ToList();
            model.PProject_Service = db.Project_sevices.ToList();
            //for (int i = 0; i < model.PProject_Photo.Count(); i++)
            //{
            //    model.PProject_Photo[i].
            //}
            return View(model);
        }

        // GET: Admin/Projects/Service/5

        public ActionResult Service(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.TheProject = db.Projects.Find(id);



            if (model.TheProject == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        //-------------Post-Services
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Service(string serviceName, int? id)
        {
            if (ModelState.IsValid)
            {
                Project activeProject = db.Projects.Find(id);

                var Service = new Project_sevices
                {

                    Name = serviceName,
                    Project_Id = activeProject.İD
                };
                db.Project_sevices.Add(Service);
                db.SaveChanges();




                return RedirectToAction("Index");
            }
            return View();
        }
        // GET: Admin/Projects/Create
        public ActionResult Create()
        {
            //model.TheProject = db.Projects.First();
            //model.TheProject_Photo = db.Project_Photo.First();
            //model.PProject_Photo = db.Project_Photo.ToList();
            ViewModelProject model = new ViewModelProject();
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
                //int proId = db.Projects.Add(model.TheProject).İD;
                //db.SaveChanges();

                //model.TheProject_Photo.Project_İD = proId;
                //db.Project_Photo.Add(model.TheProject_Photo);


                //db.Projects.Add(model.TheProject);
                //db.SaveChanges();
                //model.TheProject_Photo.Project_İD = model.TheProject.İD;
                //db.Project_Photo.Add(model.TheProject_Photo);
                //db.SaveChanges();
                var entry = new Project
                {
                    Long_İnfo = model.TheProject.Long_İnfo,
                    Name = model.TheProject.Name,
                    Short_İnfo = model.TheProject.Short_İnfo,
                    Status = true,
                    Year = model.TheProject.Year,
                    Siteurl = model.TheProject.Siteurl
                };
                db.Projects.Add(entry);
                db.SaveChanges();
                int id = entry.İD;
                List<string> PhotoList = new List<string>();
                foreach (var ph in model.fileModels.files)
                {

                    try
                    {
                        if (ph != null)
                        {

                            var FileName = Path.GetFileName(ph.FileName);
                            //PhotoList.AddRange((string)FileName);

                            var path = Path.Combine(Server.MapPath("/Public/images"), FileName);
                            ph.SaveAs(path);
                            var photo = new Project_Photo
                            {
                                Link = "/Public/images/" + FileName,
                                Project_İD = id
                            };
                            db.Project_Photo.Add(photo);
                            db.SaveChanges();
                        }
                    }

                    catch
                    {
                        return RedirectToAction("Create");
                    }

                }
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = entry.İD });
            }
            else
            {
                return View();
            }
        }
        //[HttpPost]
        //public JsonResult AddPhoto(HttpPostedFileBase file)
        //{
        //    var FileName = Path.GetFileName(file.FileName);
        //    //PhotoList.AddRange((string)FileName);

        //    var path = Path.Combine(Server.MapPath("/Public/images"), FileName);
        //    file.SaveAs(path);

        //    var photo = new Project_Photo
        //    {
        //        Link = "/Public/images/" + FileName,
        //        Project_İD = Int32.Parse(System.Web.HttpContext.Current.Request.UrlReferrer.Segments[4])
        //    };
        //    db.Project_Photo.Add(photo);
        //    db.SaveChanges();
        //    return Json("Success");
        //}
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

                foreach (var ph in model.fileModels.files)
                {
                    if (ph == null)
                    {
                        model.TheProject.Status = true;
                        db.Entry(model.TheProject).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    if (ph.ContentLength > 0)
                    {

                        var FileName = Path.GetFileName(ph.FileName);
                        //PhotoList.AddRange((string)FileName);

                        var path = Path.Combine(Server.MapPath("/Public/images"), FileName);
                        ph.SaveAs(path);
                        var photo = new Project_Photo
                        {
                            Link = "/Public/images/" + FileName,
                            Project_İD = model.TheProject.İD
                        };
                        db.Project_Photo.Add(photo);
                        db.SaveChanges();
                    }

                }
                model.TheProject.Status = true;
                db.Entry(model.TheProject).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeletePhoto(int photoId, int projectId)
        {
            Project_Photo pp = db.Project_Photo.Where(x => x.İD == photoId).First();
            db.Project_Photo.Remove(pp);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = projectId });
        }
        // GET: Admin/Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project_sevices ps = db.Project_sevices.Find(id);
            //must be cascade relation
            db.Project_sevices.Remove(ps);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //// POST: Admin/Projects/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    List<Project_Photo> Pfoto = db.Project_Photo.Where(pf => pf.Project_İD == id).ToList();
        //    foreach (var ph in Pfoto)
        //    {
        //        db.Project_Photo.Remove(ph);
        //        db.SaveChanges();
        //    }
        //    Project project = db.Projects.Find(id);
        //    db.Projects.Remove(project);
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