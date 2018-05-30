using ReComp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReComp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SellController : Controller
    {
        private REcompEntities db = new REcompEntities();
        // GET: Admin/Sell
        public ActionResult Index(int page = 1)
        {
            int perPage = 10;
            IList<Sell> sells = db.Sells.OrderBy(x=>x.ID).Skip((page - 1) * perPage).Take(perPage).ToList();
            ViewBag.TotalItems = Math.Round((double)(db.Sells.ToList().Count / perPage)) + 1;
            return View(sells);
        }

        public ActionResult DeleteSell(int id)
        {
            Sell sell = db.Sells.Find(id);
            Sticker st = sell.Sticker;
            st.Status = false;
            db.Sells.Remove(sell);
            db.Entry(st).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Search(string name = "")
        {
            IEnumerable<Sell> sells = db.Sells.Where(x => x.Sticker.Name.Contains(name)).ToList();
            return View("Index", sells);
        }
    }
}