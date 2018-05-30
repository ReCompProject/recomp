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

namespace ReComp.Controllers
{
   
    public class AjaxController : Controller
    {
        REcompEntities db = new REcompEntities();
        // GET: Ajax
        public ActionResult GetRegions(int? id)
        {
            object data = null;
            if(id == null)
            {
                data = new
                {
                    status = 404,
                    message = "Id not found",
                    response = ""
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            City seher = db.Cities.FirstOrDefault(ct => ct.Id == id);
            if (seher==null)
            {
                data = new
                {
                    status = 404,
                    message = "Id does not exist",
                    response = ""
                };
                return Json(data, JsonRequestBehavior.AllowGet);

            }

            var regions = db.Regions.Where(rg => rg.City_Id == id).
                Select(rg => new {
                    rg.Id,
                    rg.Name
                }).ToList();


            data = new
            {
                status = 200,
                message = "Succes",
                response = regions
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Pagination(int skip, int take)
        {
            return Json(
                    new
                    {
                        status = 200,
                        error = "",
                        data = db.Stickers.OrderBy(st => st.İD).Skip(skip).Take(take)
                    }
            );
        }
    }
}