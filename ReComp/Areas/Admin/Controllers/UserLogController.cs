using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReComp.Models;

namespace ReComp.Areas.Admin.Controllers
{
    public class UserLogController : Controller
    {
        private REcompEntities db  = new REcompEntities();
        ViewModelUsers model = new ViewModelUsers();
        // GET: Admin/Log
        public ActionResult UserLog()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLog(string username, string password)
        {

            //return Content(username + " " + password);
            model.UUser = db.Users.ToList();
            foreach (var us in model.UUser) {
                if (username == us.Number && password == us.Password)
                {
                    return RedirectToAction("../../Home/Index");
                } }
            ViewBag.AdminLoginError = "Username or Password wrong";
            return View();
        }
    }
}