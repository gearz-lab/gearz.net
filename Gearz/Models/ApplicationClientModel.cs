using System.Collections.Generic;
using System.Dynamic;
using System.Web.Routing;

namespace Gearz.Models
{
    public class ApplicationClientModel
    {
        public ApplicationClientModel()
        {
            this.ViewData = new ExpandoObject();
            this.AppState = new AppStateClientModel();
        }

        /// <summary>
        /// Gets or sets the object that contains the application meta-data.
        /// </summary>
        public Dictionary<string, dynamic> AppMeta { get; set; }

        /// <summary>
        /// Gets or sets global application state that is independent from the current view.
        /// (e.g. application settings)
        /// </summary>
        public AppStateClientModel AppState { get; private set; }

        /// <summary>
        /// Gets or sets the current view data.
        /// </summary>
        public dynamic ViewData { get; private set; }
    }

    public class AppStateClientModel
    {
        public AppStateClientModel()
        {
            this.Options = new ExpandoObject();
        }

        /// <summary>
        /// Gets an object that contains almost static user options and settings.
        /// </summary>
        public dynamic Options { get; private set; }

        public Dictionary<string, string> Versions { get; set; }
    }

    public class AppMetaClientModel
    {
        public IEnumerable<RouteModel> routes;
        public object app;
        public object areas;
    }

    public class RouteModel
    {
        public RouteModel(Route route)
        {
            this.Uri = route.Url;
            this.Defaults = route.Defaults;
            this.Constraints = route.Constraints;
            this.DataTokens = route.DataTokens;
            this.RouteHandler = route.RouteHandler.GetType().FullName;
            this.Type = route.GetType().FullName;
        }

        public RouteValueDictionary Defaults { get; set; }

        public RouteValueDictionary Constraints { get; set; }

        public RouteValueDictionary DataTokens { get; set; }

        public string Uri { get; set; }

        public string RouteHandler { get; set; }

        public string Type { get; set; }
    }
}