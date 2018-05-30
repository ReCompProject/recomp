using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReComp.Areas.Admin.Controllers
{
    [Authorize(Roles= "Administrator,Editor,User")]
    public class PanelController : Controller
    {
        // GET: Admin/Panel
        public ActionResult ViewPanel()
        {
            return View();
        }
    }
}