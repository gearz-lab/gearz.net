using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gearz.Models;

namespace Gearz.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var data = this.ApplicationViewModel();

            if (this.Request.IsAjaxRequest())
                return this.Json(data, JsonRequestBehavior.AllowGet);

            return this.View("React", data);
        }

        public ActionResult About()
        {
            var data = this.ApplicationViewModel();

            data.Data.Message = "Your application description page.";

            if (this.Request.IsAjaxRequest())
                return this.Json(data, JsonRequestBehavior.AllowGet);

            return this.View("React", data);
        }

        public ActionResult Contact()
        {
            var data = this.ApplicationViewModel();

            data.Data.Message = "Your contact page.";

            if (this.Request.IsAjaxRequest())
                return this.Json(data, JsonRequestBehavior.AllowGet);

            return this.View("React", data);
        }

        private ApplicationViewModel ApplicationViewModel()
        {
            var data = new ApplicationViewModel
            {
                App = new
                {
                    name = "Application name",
                    year = DateTime.Now.Year,
                    company = "My ASP.NET Application",
                    location = "Home",
                },
                Meta = new
                {
                    areas = new
                    {
                        root = new
                        {
                            home = new
                            {
                                index = new
                                {
                                    url = Url.Action("Index", "Home", new { area = "" }),
                                    title = "Home",
                                    location = "Home"
                                },
                                about = new
                                {
                                    url = Url.Action("About", "Home", new { area = "" }),
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
                }
            };
            return data;
        }
    }
}