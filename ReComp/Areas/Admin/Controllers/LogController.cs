using ReComp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ReComp.Areas.Admin.Controllers
{
    public class LogController : Controller
    {
        private REcompEntities db = new REcompEntities();
        // GET: Admin/Log
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ViewPanel", "Panel");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, bool remember = false)
        {
            string hashPassword = HashMD5(password);
            ReComp.Models.User user = db.Users.Where(x => x.UserName == username
                                                    && x.Password == hashPassword).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, remember);
                var authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.User_Type.Name);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                var us = User;
                return RedirectToAction("ViewPanel", "Panel");
            }
            ViewBag.AdminLoginError = "Username or Password wrong";
            return View();

        }

        [HttpGet]
        public ActionResult LogOut()
        {
            bool s = User.Identity.IsAuthenticated;
            FormsAuthentication.SignOut();
            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();
            return RedirectToAction("Login");
        }

        private string HashMD5(string password)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }
    }
}