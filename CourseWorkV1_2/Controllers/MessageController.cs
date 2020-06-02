using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseWorkV1_2.Models;
using System.Web.Mvc;


namespace CourseWorkV1_2.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        public List<Models.Massage> GetAllMyMessage(string UserId)
        {
            List<Models.Massage> massages = new List<Massage>();
            using (var db = new Entities())
            {
                foreach(var massage in db.Massages)
                {
                    if(massage.UserId == UserId+new string(' ', 128 - UserId.Length))
                    {
                        massages.Add(massage);
                    }
                }
                return massages;
            }
        }
    }
}