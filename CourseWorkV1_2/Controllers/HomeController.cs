using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseWorkV1_2.Models;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace CourseWorkV1_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Message()
        {

            return View();
        }
        [Authorize(Roles = "admin")]
        public ActionResult EditUser()
        {
            return View();
        }
        [Authorize(Roles = "maneger")]
        [Authorize(Roles = "admin")]
        public ActionResult EditLot()
        {
            return View();
        }
        [HttpPost]
        
        public bool Add(Lot lot)
        {
            try
            {
                using (var db = new Entities())
                {
                    db.Lots.Add(new Lot
                    {
                        Id = db.Lots.Count() + 1,
                        UserId = HttpContext.User.Identity.GetUserId(),
                        Name = lot.Name,
                        Type = lot.Type,
                        Image = lot.Image,
                        MinPrice = lot.MinPrice,
                        TimeStart = DateTime.Now,
                        TimeEnd = DateTime.Now.AddHours(1),
                        IsActive = true
                    });
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void SendAtWin(Lot lot)
        {
            using (Entities db = new Entities())
            {
                db.Massages.Add(new Massage { Id = db.Massages.Count() + 1, UserId = lot.UserId, Text = "Congratulations, you have purchased the item number {" + lot.Id + "}" });
                db.SaveChanges();
            }
        }
        public void SendOwnerLose(Lot lot)
        {
            using (Entities db = new Entities())
            {
                db.Massages.Add(new Massage { Id = db.Massages.Count() + 1, UserId = lot.UserId, Text = "Sorry, item number {" + lot.Id + "} was not sold." });
                db.SaveChanges();
            }
        }
        public void SendOwnerWin(Lot lot)
        {
            using (Entities db = new Entities())
            {
                db.Massages.Add(new Massage { Id = db.Massages.Count() + 1, UserId = lot.UserId, Text = "Congratulations, lot number {" + lot.Id + "} has been successfully sold." });
                db.SaveChanges();
            }
        }

        public ActionResult Lot()
        {
            return View();
        }
        [Authorize]
        public ActionResult Add()
        {


            return View();
        }
        public static List<Lot> GetAllLots()
        {
            using (Entities db = new Entities())
            {
                List<Lot> lots = new List<Lot>();
                foreach (Lot lot in db.Lots)
                {
                    if (lot.IdWin == null || lot.IdWin == '0' + new string(' ', 127) && lot.IsActive)
                        lots.Add(lot);
                }
                return lots;
            }
        }
        public static List<Lot> GetAllMyLots(string UserId)
        {
            using (var db = new Entities())
            {
                List<Lot> lots = new List<Lot>();
                foreach (Lot lot in db.Lots)
                {
                    if (lot.UserId == UserId && lot.IsActive)
                    {
                        lots.Add(lot);
                    }
                }
                return lots;
            }
        }
        public List<Lot> GetAllWinLots()
        {
            string UserId = User.Identity.GetUserId();
            using (var db = new Entities())
            {
                List<Lot> lots = new List<Lot>();
                foreach (Lot lot in db.Lots)
                {
                    if (lot.IdWin == UserId && lot.IsActive)
                    {
                        lots.Add(lot);
                    }
                }
                return lots;
            }
        }
        public void DeleteLot(int Id)
        {
            using (var db = new Entities())
            {
                foreach (Lot lot in db.Lots)
                {
                    if (lot.Id == Id && lot.IsActive)
                        db.Lots.Remove(lot);
                }
                db.SaveChanges();
            }
        }
        public void Change(Lot lot)
        {
            using (var db = new Entities())
            {
                DeleteLot(lot.Id);
                db.Lots.Add(lot);
                db.SaveChanges();
            }
        }
        public void SetWin(int Id, string WinId)
        {
            using (var db = new Entities())
            {
                db.Database.ExecuteSqlCommand("UPDATE dbo.Lots SET IdWin = {1} WHERE Id = {0}", Id, WinId);
                db.SaveChanges();
            }
        }
        public bool SetRealPrice(int Id, decimal RealPrice)
        {
            using (var db = new Entities())
            {
                db.Database.ExecuteSqlCommand("UPDATE dbo.Lots SET IdWin = {0} WHERE Id = {1} AND RealPrice < {2}", User.Identity.GetUserId(), Id, RealPrice);
                db.Database.ExecuteSqlCommand("UPDATE dbo.Lots SET RealPrice = {0} WHERE Id = {1} AND RealPrice < {2}", RealPrice, Id, RealPrice);
                db.SaveChanges();
                return true;
            }
        }
        public bool AddType(Models.Type type)
        {
            using (var db = new Entities())
            {
                db.Types.Add(new Models.Type
                {
                    Id = db.Types.Count() + 1,
                    Name = type.Name
                });
                db.SaveChanges();
                return true;
            }
        }
        public static List<Models.Type> GetAllTypes()
        {
            List<Models.Type> Types = new List<Models.Type>();
            using (var db = new Entities())
            {
                foreach (var type in db.Types)
                {
                    Types.Add(type);
                }
                return Types;
            }
        }
        public bool DeleteType(int Id)
        {
            using (var db = new Entities())
            {
                foreach (var type in db.Types)
                {
                    if (type.Id == Id)
                    {
                        db.Types.Remove(type);
                    }

                }
                db.SaveChanges();
                return true;
            }
        }
        [HttpPost]
        [Authorize]
        public bool Index(Lot lot, string UserId)
        {
            using (var db = new Entities())
            {
                foreach(var _lot in db.Lots)
                {
                    if (_lot.IsActive && _lot.RealPrice < lot.RealPrice && lot.RealPrice >= lot.MinPrice)
                    {
                        _lot.IdWin = UserId;
                        _lot.RealPrice = lot.RealPrice;
                    }
                }
            }
            return false;
        }
    }
}