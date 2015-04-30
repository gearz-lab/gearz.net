using System.Web;
using System.Web.Optimization;

namespace Gearz
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                          "~/Content/bootstrap.css",
                          "~/Content/site.css"));

            // REACT
            bundles.Add(new ScriptBundle("~/bundles/react", "//cdnjs.cloudflare.com/ajax/libs/react/0.13.2/react-with-addons.js")
                .Include("~/Scripts/Jsx/node_modules/react/dist/react-with-addons.js"));

            // IMMUTABLE
            bundles.Add(new ScriptBundle("~/bundles/immutable", "//cdn.jsdelivr.net/immutable.js/3.2.1/immutable.min.js")
                .Include("~/Scripts/immutable.min.js"));

            // Main Application
            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/Scripts/Jsx/build/client.bundle.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
