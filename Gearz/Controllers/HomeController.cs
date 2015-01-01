using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gearz.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var data = new
                       {
                           app = new
                                 {
                                     name = "Application name",
                                     year = DateTime.Now.Year,
                                     company = "My ASP.NET Application",
                                     location = "Home",
                                 },
                           areas = new
                                   {
                                       root = new
                                              {
                                                  home = new
                                                         {
                                                             index = new
                                                                     {
                                                                         url =
                                                                             Url.Action("Index", "Home", new { area = "" }),
                                                                         title = "Home",
                                                                         location = "Home"
                                                                     },
                                                             about = new
                                                                     {
                                                                         url =
                                                                             Url.Action("About", "Home", new { area = "" }),
                                                                         title = "About",
                                                                         location = "About"
                                                                     },
                                                             contact = new
                                                                       {
                                                                           url = Url.Action("Contact", "Home", new { area = "" }),
                                                                           title = "Contact",
                                                                           location = "Contact"
                                                                       },
                                                         }
                                              }
                                   }
                       };

            if (this.Request.IsAjaxRequest())
                return this.Json(data, JsonRequestBehavior.AllowGet);

            return View("React", data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}