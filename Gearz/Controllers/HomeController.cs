using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Gearz.Models;

namespace Gearz.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var data = this.ApplicationViewModel();
            data.PageData.location = "Home";

            if (this.Request.IsAjaxRequest())
                return this.Json(data, JsonRequestBehavior.AllowGet);

            return this.View("React", data);
        }

        public ActionResult About()
        {
            var data = this.ApplicationViewModel();

            data.PageData.location = "About";
            data.PageData.pageData = new
                {
                    message = "Your application description page.",
                };

            if (this.Request.IsAjaxRequest())
                return this.Json(data, JsonRequestBehavior.AllowGet);

            return this.View("React", data);
        }

        public ActionResult Contact()
        {
            var data = this.ApplicationViewModel();

            data.PageData.location = "Contact";
            data.PageData.pageData = new
                {
                    message = "Your contact page.",
                };

            if (this.Request.IsAjaxRequest())
                return this.Json(data, JsonRequestBehavior.AllowGet);

            return this.View("React", data);
        }

        private ApplicationClientModel ApplicationViewModel()
        {
            var data = new ApplicationClientModel
                {
                    AppMeta = new Dictionary<string, object>
                        {
                            {
                                "my-app-id en-US v0.1.0",
                                new AppMetaClientModel
                                    {
                                        routes = RouteTable.Routes.OfType<Route>().Select(r => new RouteModel(r)),
                                        app = new
                                            {
                                                name = "Application name",
                                                year = DateTime.Now.Year,
                                                company = "My ASP.NET Application"
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
                                                                            Url.Action(
                                                                                "Index",
                                                                                "Home",
                                                                                new { area = "" }),
                                                                        title = "Home",
                                                                        location = "Home"
                                                                    },
                                                                about = new
                                                                    {
                                                                        url =
                                                                            Url.Action(
                                                                                "About",
                                                                                "Home",
                                                                                new { area = "" }),
                                                                        title = "About",
                                                                        location = "About"
                                                                    },
                                                                contact = new
                                                                    {
                                                                        url =
                                                                            Url.Action(
                                                                                "Contact",
                                                                                "Home",
                                                                                new { area = "" }),
                                                                        title = "Contact",
                                                                        location = "Contact"
                                                                    },
                                                            }
                                                    }
                                            }
                                    }
                            }
                        }
                };

            data.AppState.Versions = new Dictionary<string, string>
                {
                    // when the user changes application settings
                    // that alternate between monolithic modules,
                    // this can be made through this property:
                    //  - the key is the module name
                    //  - the value is the unique module version identifier to be used
                    // these will be used by the Application component
                    // to get the correct metadata and pass into other
                    // subcomponents (e.g. MetaPage components and derivations,
                    // and all other sorts of page components)
                    // TODO: map the module version identifier to an integer value, to make the gzAMV cookie smaller
                    { "app", "my-app-id en-US v0.1.0" },
                    { "module", "my-mod-id en-US v0.1.0" }
                };

            return data;
        }
    }
}