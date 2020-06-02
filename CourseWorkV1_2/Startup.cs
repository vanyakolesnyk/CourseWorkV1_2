using Microsoft.Owin;
using Owin;
using System.Threading;

[assembly: OwinStartupAttribute(typeof(CourseWorkV1_2.Startup))]
namespace CourseWorkV1_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            void StartServer()
            {
                while (true)
                {
                     using(var db = new Models.Entities())
                    {
                        foreach (var lot in db.Lots)
                        {
                            var lt = db.Lots;
                            if (lot.TimeEnd < System.DateTime.Now && lot.IdWin == null && lot.IsActive)
                            {
                                lot.IsActive = false;
                                using (var controller = new Controllers.HomeController())
                                {
                                    controller.SendOwnerLose(lot);
                                    
                                }
                            }
                            else if (lot.TimeEnd < System.DateTime.Now && lot.IdWin != null && lot.IsActive)
                            {
                                lot.IsActive = false;
                                using (var controller = new Controllers.HomeController())
                                {
                                    controller.SendAtWin(lot);
                                    controller.SendOwnerWin(lot);

                                }
                            }
                            
                        }
                        db.SaveChanges();
                        Thread.Sleep(10);
                    }
                }
            }
            Thread thread = new Thread(StartServer);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            ConfigureAuth(app);
        }
    }
}
