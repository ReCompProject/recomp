using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReComp.Areas.Admin.Controllers
{
    public class LogController : Controller
    {
        // GET: Admin/Log
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            //return Content(username + " " + password);
            if (username == "Admin" && password == "12345")
            {
                return RedirectToAction("ViewPanel", "Panel");
            }
            ViewBag.AdminLoginError = "Username or Password wrong";
            return View();
        }
    }
}