using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gearz
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // maps a client-route for the Index action of the Home controller
            routes.MapClientRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            // all other ASP.NET MVC default routes will be resolved in the server
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

    public class UrlParameterJsonConverter : JsonConverter
    {
        public static readonly UrlParameterJsonConverter Instance = new UrlParameterJsonConverter();

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == UrlParameter.Optional)
                writer.WriteNull();

            throw new Exception("Unsupported UrlParameter");
        }

        // Other JsonConverterMethods
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(UrlParameter);
        }

        public override bool CanWrite { get { return true; } }
        public override bool CanRead { get { return false; } }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public static class JsonHelper
    {
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(
                obj,
                FLAGS.DEBUG ? Formatting.Indented : Formatting.None,
                UrlParameterJsonConverter.Instance);
        }
    }

    public static class FLAGS
    {
#if DEBUG
        public static readonly bool DEBUG = true;
#else
        public static readonly bool DEBUG = false;
#endif
    }

    /// <summary>
    /// Represents a route that can be used either server-side or client-side.
    /// </summary>
    public class ClientRoute : Route
    {
        public ClientRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }
    }

    public static class RouteCollectionExtensions
    {
        public static void MapClientRoute(
            this RouteCollection routes,
            string name,
            string url,
            object defaults = null,
            object constraints = null,
            RoutingModes routingModes = RoutingModes.Client)
        {
            if ((routingModes & RoutingModes.ClientAndServer) == 0)
                throw new ArgumentException("Invalid routing mode.", "routingModes");

            // adding the route
            routes.Add(
                name,
                new ClientRoute(
                    url,
                    CreateRouteValueDictionary(defaults),
                    CreateRouteValueDictionary(constraints),
                    CreateRouteValueDictionary(new
                        {
                            isServer = (routingModes & RoutingModes.Server) != 0,
                            isClient = (routingModes & RoutingModes.Client) != 0,
                        }),
                    new MvcRouteHandler()));
        }

        private static RouteValueDictionary CreateRouteValueDictionary(object defaults)
        {
            var dic = defaults as IDictionary<string, object>
                      ?? (defaults == null
                          ? null
                          : defaults.GetType()
                              .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                              .Where(p => !p.IsSpecialName && p.CanRead)
                              .ToDictionary(p => p.Name, p => p.GetValue(defaults)));

            return dic == null ? new RouteValueDictionary() : new RouteValueDictionary(dic);
        }
    }

    [Flags]
    public enum RoutingModes
    {
        /// <summary>
        /// Indicates that a route must be resolved in client-side.
        /// </summary>
        Client = 1,

        /// <summary>
        /// Indicates that a route must be resolved in server-side.
        /// </summary>
        Server = 2,

        /// <summary>
        /// Indicates that a route must be resolved in two steps:
        /// first in the client (fast update), and then in the server (slow update).
        /// </summary>
        ClientAndServer = 3,
    }
}
