using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReComp.Models;

namespace ReComp.Controllers
{
    public class GridController : Controller
    {
        // GET: Grid
        REcompEntities db = new REcompEntities();
        public ActionResult Grid()
        {
            ViewBag.Stickers = db.Stickers.Where(st => st.Status == false).ToList();
            ViewBag.StickType = db.Stick_Type.ToList();
            ViewBag.SticPhoto = db.Stick_Photo.ToList();
            ViewBag.Room = db.Rooms.ToList();
            ViewBag.Footer = db.Footors.First();
            return View();
        }

        [HttpPost]
        public ActionResult Search(int? Stick_Type, int? Object_Type, int? Cities, int? Regions)
        {
            Stick_Type = Stick_Type ?? null;
            Object_Type = Object_Type ?? null;
            Cities = Cities ?? null;
            Regions = Regions ?? null;
            ViewBag.StickType = db.Stick_Type.ToList();

            //return Content("Stick type: " + Stick_Type + " Object: " + Object_Type + " " + "cities: " + Cities + " Regions: " + Regions);

            //create query
            var queryString = "Select * from Stickers where Status = 0";
            if(Stick_Type != null)
            {
                queryString += " and Type_İD = " + Stick_Type;
            }

            if (Object_Type != null)
            {
                queryString += " and Object_İd = " + Object_Type;
            }

            if (Cities != null)
            {
                queryString += " and City_İd = " + Cities;
            }

            if (Regions != null)
            {
                queryString += " and Region_İd = " + Regions;
            }

            //return Content(queryString);
            //create rresponse
            List <Sticker> columnData = new List<Sticker>();

            using (SqlConnection connection = new SqlConnection("Data Source=31.31.196.127;initial catalog=u0506899_REcomp;User Id=u0506899_admin;Password=kH7l2?9e;MultipleActiveResultSets=True;App=EntityFramework&quot;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Sticker st = new Sticker
                            {
                                İD = (int)reader["İD"],
                                Name = reader["Name"].ToString(),
                                Price = reader["Price"].ToString(),
                                Short_info = reader["Short_info"].ToString(),
                                Type_İD = (int)reader["Type_İD"],
                                Area = reader["Area"].ToString(),
                                GarageStatus = (bool)reader["GarageStatus"],
                                Status = (bool)reader["Status"],
                                Long_Info = reader["Long_Info"].ToString()
                            };
                            columnData.Add(st);

                        }
                    }
                }
            }


            ViewBag.Stickers = columnData;

            ViewBag.SticPhoto = db.Stick_Photo.ToList();
            ViewBag.Room = db.Rooms.ToList();
            ViewBag.Footer = db.Footors.First();

            return View();
        }
        public ActionResult _Layout()
        {
            ViewBag.Footer = db.Footors.First();
            return View();
        }
    }
}