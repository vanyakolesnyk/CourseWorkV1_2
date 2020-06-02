using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace CourseWorkV1_2.Models.Loots
{
    public static class LotsMeth
    {
        public static bool Add(string Name, string Type, byte[] Image, decimal MinPrice, decimal RealPrice, System.DateTime TimeStart, System.DateTime TimeEnd)
        {
            using (var db = new CourseEntitiesDB())
            {
            db.Lots.Add(new Lot
            {
                Id = db.Lots.Count() + 1,
                UserId = HttpContext.Current.User.Identity.GetUserId(),
                Name = Name,
                Type = Type,
                Image = Image,
                MinPrice = MinPrice,
                RealPrice = RealPrice,
                TimeEnd = TimeEnd,
                TimeStart = TimeStart,
                IsActive = true

            });

                db.SaveChanges();
            }
            return true;
        }
        public static Lot GetAtId(int Id)
        {
            using (CourseEntitiesDB db = new CourseEntitiesDB())
            {
                foreach (Lot lot in db.Lots)
                {
                    if (Id == lot.Id)
                    {
                        return lot;
                    }
                }
                return null;
            }
        }
        public static List<Lot> GetAllLots()
        {
            using (CourseEntitiesDB db = new CourseEntitiesDB())
            {
                List<Lot> lots = new List<Lot>();
                foreach (Lot lot in db.Lots)
                {
                    if(lot.IdWin==null||lot.IdWin == '0'+ new string(' ',127) && lot.IsActive)
                    lots.Add(lot);
                }
                return lots;
            }
        }
        public static List<Lot> GetAllMyLots()
        {
            string UserId = HttpContext.Current.User.Identity.GetUserId();
            using (CourseEntitiesDB db = new CourseEntitiesDB())
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
        public static List<Lot> GetAllWinLots()
        {
            string UserId = HttpContext.Current.User.Identity.GetUserId();
            using (CourseEntitiesDB db = new CourseEntitiesDB())
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
        public static void DeleteLot(int Id)
        {
            using (CourseEntitiesDB db = new CourseEntitiesDB())
            {
                foreach(Lot lot in db.Lots)
                {
                    if (lot.Id == Id && lot.IsActive)
                        db.Lots.Remove(lot);
                }
                db.SaveChanges();
            }
        }
        public static void Change(Lot lot)
        {
            using (CourseEntitiesDB db = new CourseEntitiesDB())
            {
                LotsMeth.DeleteLot(lot.Id);
                db.Lots.Add(lot);
                db.SaveChanges();
            }
        }
        public static void SetWin(int Id, string WinId)
        {
            using(CourseEntitiesDB db = new CourseEntitiesDB())
            {
                db.Database.ExecuteSqlCommand("UPDATE dbo.Lots SET IdWin = {1} WHERE Id = {0}", Id, WinId);
                db.SaveChanges();
            }
        }
        public static bool SetRealPrice(int Id, decimal RealPrice)
        {
            using (CourseEntitiesDB db = new CourseEntitiesDB())
            {
                db.Database.ExecuteSqlCommand("UPDATE dbo.Lots SET IdWin = {0} WHERE Id = {1} AND RealPrice < {2}",HttpContext.Current.User.Identity.GetUserId(),Id, RealPrice);
                db.Database.ExecuteSqlCommand("UPDATE dbo.Lots SET RealPrice = {0} WHERE Id = {1} AND RealPrice < {2}", RealPrice, Id, RealPrice);
                db.SaveChanges();
                return true;
            }
        }
    }
}
