using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseWorkV1_2.Models;
using System.Web.Mvc;


namespace CourseWorkV1_2.Controllers
{
    [Authorize]
    public class EditUserController : Controller
    {
        public static List<string> GetAllUserName()
        {
            List<string> users = new List<string>();
            using (var db = new ApplicationDbContext())
            {
                foreach(var user in db.Users)
                {
                    users.Add(user.UserName);
                }
            }
            return users;
        }
    }
}