using ReComp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReComp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator,Editor")]
    public class RoomController : Controller
    {
        private REcompEntities db = new REcompEntities();
        // GET: Admin/Room
        public ActionResult Index()
        {
            IEnumerable<Room_Type> room_Types = db.Room_Type.ToList();
            return View(room_Types);
        }

        public ActionResult CreateEditRoomType()
        {
            return View("CreateEditRoomType", new Room_Type());
        }

        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                Room_Type room_Type = db.Room_Type.Find(id);
                if (room_Type != null)
                {
                    return View("CreateEditRoomType", room_Type);
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEditRoomType(Room_Type room_type)
        {
            if (room_type.İD == 0)
            {
                db.Room_Type.Add(room_type);
                db.SaveChanges();
            }
            else
            {
                Room_Type rt = db.Room_Type.Find(room_type.İD);
                if (rt == null)
                {
                    return RedirectToAction("Index");
                }
                db.Entry(room_type).State = EntityState.Modified;
                db.SaveChanges();

                //return RedirectToAction("Index", "Stickers");
            }
            return RedirectToAction("Index", "Room");
            //return View("CreateEditRoomType", room_type);
        }

        public ActionResult DeleteRoomType(int? id)
        {
            if (id != null)
            {
                Room_Type roomType = db.Room_Type.Find(id);
                if (roomType != null)
                {
                    try
                    {
                        db.Room_Type.Remove(roomType);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult RoomIndex()
        {
            IEnumerable<Room> rooms = db.Rooms.ToList();
            return View(rooms);
        }

        public ActionResult CreateEditRoom()
        {
            ViewBag.Room_Types = db.Room_Type.ToList();
            ViewBag.Sticks = db.Stickers.ToList();
            return View("CreateEditRoom", new Room());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEditRoom(Room room)
        {
            if (room.İD == 0)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
            }
            else
            {
                Room r = db.Rooms.Find(room.İD);
                if (r == null)
                {
                    return RedirectToAction("RoomIndex");
                }
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("CreateEditRoom", room);
        }

        public ActionResult DeleteRoom(int? id)
        {
            if (id != null)
            {
                Room room = db.Rooms.Find(id);
                if (room != null)
                {
                    db.Rooms.Remove(room);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("RoomIndex");
        }
    }
}