using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReComp.Areas.Admin.Models;
using ReComp.Models;

namespace ReComp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator,Editor,User")]
    public class StickersController : Controller
    {
        private REcompEntities db = new REcompEntities();
        ViewModelStick model = new ViewModelStick();
        // GET: Admin/Stickers
        public ActionResult Index(int page = 1)
        {

            model.SSticker = db.Stickers.ToList();
            model.SSticker_Photo = db.Stick_Photo.ToList();
            model.SSticker_Sxem = db.Stick_sxem.ToList();
            model.SStick_Type = db.Stick_Type.ToList();
            model.RRoom = db.Rooms.ToList();
            model.RRoom_Type = db.Room_Type.ToList();
            //int perPage = 10;
            //IList<Sticker> sticker = db.Stickers.OrderBy(x => x.İD).Skip((page - 1) * perPage).Take(perPage).ToList();
            //ViewBag.TotalItems = Math.Round((double)(db.Stickers.ToList().Count / perPage)) + 1;

            return View(model);
            //return View(model);
        }

        // GET: Admin/Stickers/Create
        public ActionResult Create()
        {
            //ViewBag.Project_İD = new SelectList(db.Projects, "İD", "Name");
            //ViewBag.Type_İD = new SelectList(db.Stick_Type, "İD", "Name");
            ViewModelStick model = new ViewModelStick { PProject = db.Projects.ToList(), SStick_Type = db.Stick_Type.ToList() };
            return View(model);
        }

        // POST: Admin/Stickers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "İD,Name,Project_İD,Price,Short_info,Type_İD,Area,GarageStatus,Status,fileModel.files")]*/ ViewModelStick sticker)
        {
            if (ModelState.IsValid)
            {
                sticker.TheSticker.Status = false;

                db.Stickers.Add(sticker.TheSticker);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = sticker.TheSticker.İD });
            }
            foreach (var item in sticker.TheSticker.fileModels.files)
            {
                var FileName = Path.GetFileName(item.FileName);
                var path = Path.Combine(Server.MapPath("/Public/images/"), FileName);
                item.SaveAs(path);

                db.Stick_Photo.Add(new Stick_Photo { Name = "/Public/images/" + FileName, Sticker = sticker.TheSticker });
                db.SaveChanges();
            }
            foreach (var itemSxem in sticker.TheSticker.fileModelsxem.files)
            {
                var FileNameSxem = Path.GetFileName(itemSxem.FileName);
                var path = Path.Combine(Server.MapPath("/Public/images/"), FileNameSxem);
                itemSxem.SaveAs(path);

                db.Stick_sxem.Add(new Stick_sxem { Name = "/Public/images/" + FileNameSxem, Sticker = sticker.TheSticker });
                db.SaveChanges();
            }



            ViewBag.Project_İD = new SelectList(db.Projects, "İD", "Name", sticker.TheSticker.Project_İD);
            ViewBag.Type_İD = new SelectList(db.Stick_Type, "İD", "Name", sticker.TheSticker.Type_İD);
            return View(sticker);
        }

        //[HttpPost]
        //public JsonResult AddPhoto(HttpPostedFileBase file)
        //{
        //    var FileName = Path.GetFileName(file.FileName);
        //    var path = Path.Combine(Server.MapPath("/Public/images/sticker/"), FileName);
        //    file.SaveAs(path);
        //    int id = Int32.Parse(System.Web.HttpContext.Current.Request.UrlReferrer.Segments[4]);
        //    Sticker stick = db.Stickers.AsNoTracking().Where(x => x.İD == id).FirstOrDefault();
        //    var stp = new Stick_Photo { Name = "/Public/images/sticker/" + FileName, Stick_İD = stick.İD };
        //    db.Stick_Photo.Add(stp);


        //    db.SaveChanges();
        //    return Json("Success");
        //}


        //[HttpPost]
        //public JsonResult AddSxem(HttpPostedFileBase file)
        //{
        //    var FileName = Path.GetFileName(file.FileName);
        //    var path = Path.Combine(Server.MapPath("/Public/images/sxem/"), FileName);
        //    file.SaveAs(path);
        //    int id = Int32.Parse(System.Web.HttpContext.Current.Request.UrlReferrer.Segments[4]);
        //    Sticker stick = db.Stickers.AsNoTracking().Where(x => x.İD == id).FirstOrDefault();
        //    var stp = new Stick_sxem { Name = "/Public/images/sxem/" + FileName, Stick_Id = stick.İD };
        //    db.Stick_sxem.Add(stp);


        //    db.SaveChanges();
        //    return Json("Success");
        //}

        // GET: Admin/Stickers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.TheSticker = db.Stickers.Find(id);
            model.SSticker_Photo = db.Stick_Photo.Where(prf => prf.Stick_İD == id).ToList();
            model.SSticker_Sxem = db.Stick_sxem.Where(prs => prs.Stick_Id == id).ToList();
            model.PProject = db.Projects.ToList();
            model.SStick_Type = db.Stick_Type.ToList();
            if (model.TheSticker == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModelStick model, string type, string prtype, string name = "")
        {
            if (ModelState.IsValid)
            {
                model.TheSticker.Project_İD = Convert.ToInt32(prtype);
                model.TheSticker.Type_İD = Convert.ToInt32(type);

                if (User.IsInRole("Administrator"))
                {
                    foreach (var item in model.fileModels.files)
                    {
                        if (item != null)
                        {
                            var FileName = Path.GetFileName(item.FileName);
                            var path = Path.Combine(Server.MapPath("/Public/images/"), FileName);
                            item.SaveAs(path);

                            db.Stick_Photo.Add(new Stick_Photo { Name = "/Public/images/" + FileName, Stick_İD = model.TheSticker.İD });
                            db.SaveChanges();
                        }
                    }

                    foreach (var item in model.fileModelsxem.files)
                    {
                        if (item != null)
                        {
                            var FileNameSxem = Path.GetFileName(item.FileName);
                            var path = Path.Combine(Server.MapPath("/Public/images/"), FileNameSxem);
                            item.SaveAs(path);

                            db.Stick_sxem.Add(new Stick_sxem { Name = "/Public/images/" + FileNameSxem, Stick_Id = model.TheSticker.İD });
                            db.SaveChanges();
                        }
                    }
                    var status = db.Stickers.AsNoTracking().Where(x => x.İD == model.TheSticker.İD).FirstOrDefault().Status;
                    int userId = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().ID;
                    if (status != model.TheSticker.Status)
                    {
                        //eger statusun false edirse demeli sell bazasinda silirem
                        if (model.TheSticker.Status == false)
                        {
                            Sell sl = db.Sells.Where(x => x.Stick_ID == model.TheSticker.İD).FirstOrDefault();
                            db.Sells.Remove(sl);
                        }
                        else if (model.TheSticker.Status == true)
                        {
                            Sell sell = new Sell
                            {
                                Name = name,
                                Stick_ID = model.TheSticker.İD,
                                Users_Id = userId,
                                Price = model.TheSticker.Price
                            };
                            db.Sells.Add(sell);
                            db.SaveChanges();
                        }
                    }
                    db.Entry(model.TheSticker).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (User.IsInRole("Editor"))
                {
                    var sticker = db.Stickers.AsNoTracking().Where(x => x.İD == model.TheSticker.İD).FirstOrDefault();
                    model.TheSticker.Status = sticker.Status;
                    db.Entry(model.TheSticker).State = EntityState.Modified;
                    db.SaveChanges();


                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //ancaq editor ve user ucun
        [HttpPost]
        public ActionResult StickerSell(ViewModelStick model, string name = "")
        {
            if (ModelState.IsValid)
            {

                var sticker = db.Stickers.Find(model.TheSticker.İD);
                if (sticker.Status != model.TheSticker.Status)
                {
                    //eger status true onda ev satilmiw hesab olunnur ve bazaya elave olunur
                    if (model.TheSticker.Status == true)
                    {
                        int userId = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().ID;
                        Sell sell = new Sell
                        {
                            Name = name,
                            Stick_ID = model.TheSticker.İD,
                            Users_Id = userId,
                            Price = sticker.Price
                        };
                        db.Sells.Add(sell);
                        db.SaveChanges();
                        sticker.Status = model.TheSticker.Status;
                        db.Entry(sticker).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    //false ede bilmez ona gore else yoxdu
                }

            }
            return RedirectToAction("Edit", new { id = model.TheSticker.İD });
        }

        public ActionResult DeletePhoto(int photoId, int stickerId)
        {
            Stick_Photo pp = db.Stick_Photo.Where(x => x.İD == photoId).First();
            db.Stick_Photo.Remove(pp);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = stickerId });
        }

        public ActionResult DeleteSxem(int sxemId, int stickerId)
        {
            Stick_sxem ps = db.Stick_sxem.Where(y => y.Id == sxemId).First();
            db.Stick_sxem.Remove(ps);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = stickerId });
        }
        // GET: Admin/Stickers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room rmm = db.Rooms.Find(id);
            db.Rooms.Remove(rmm);
            db.SaveChanges();
            if (rmm == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        // POST: Admin/Stickers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Sticker sticker = db.Stickers.Find(id);
        //    db.Stickers.Remove(sticker);
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
        // Room get 
        public ActionResult Room(int? ro, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.RRoom_Type = db.Room_Type.ToList();
            model.TheSticker = db.Stickers.Find(id);
            Room_Type roomType = db.Room_Type.Find(ro);

            return View(model);
        }

        //   Room Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Room(string roomName, int? ro, int? id)
        {
            if (ModelState.IsValid)
            {

                Sticker roomSticker = db.Stickers.Find(id);
                Room_Type roomType = db.Room_Type.Find(ro);

                var rm = new Room
                {

                    Area = roomName,
                    Stick_İD = roomSticker.İD,
                    Type_İD = roomType.İD
                };
                db.Rooms.Add(rm);
                db.SaveChanges();




                return RedirectToAction("Index");
            }
            return View();
        }

        //public ActionResult Search(string name = "")
        //{
        //    //IEnumerable<Sticker> stickers = db.Stickers.Where(x => x.Name.Contains(name)).ToList();
        //    ViewModelStick sticker1 = db.Stickers.Where(b => b.Name.Contains(name)).ToList();
        //    return View("Index", ViewBag.sticker);
        //}
    }
}
