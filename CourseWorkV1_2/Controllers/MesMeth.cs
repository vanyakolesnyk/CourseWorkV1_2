using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseWorkV1_2.Models.Massage
{
    public static class MesMeth
    {
        public static bool AddToDb(Loots.Lot lot)
        {
            using (CourseMessageDB db = new CourseMessageDB())
            {
                db.Massages.Add(new Massage()
                {
                    Id = db.Massages.Count() + 1,
                    UserId = lot.UserId,
                    Text = "Congratulations, you have successfully purchased the lot number " + lot.Id + ", " + lot.Name + ", for the amount " + lot.RealPrice
                });
            return false;
            }
        }
    }
}