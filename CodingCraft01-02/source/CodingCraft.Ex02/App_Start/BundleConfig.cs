using System.Web.Optimization;

namespace CodingCraft.Ex02
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

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
               "~/Scripts/jquery.validate.custom.pt-br*"));

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

            // Zurb Foundation
            bundles.Add(new StyleBundle("~/Content/foundation/css").Include(
                       "~/Content/foundation/foundation.css",
                       "~/Content/foundation/foundation.min.css",
                       "~/Content/foundation/app.css"));

            bundles.Add(new ScriptBundle("~/bundles/foundation").Include(
                      "~/Scripts/foundation/foundation.js",
                      "~/Scripts/foundation/foundation.*",
                      "~/Scripts/foundation/app.js"));

            // Ink
            bundles.Add(new ScriptBundle("~/Content/ink/css").Include(
                        "~/Content/ink/ink.css", 
                        "~/Content/ink/ink.min.css",
                        "~/Content/ink/ink-flex.min.css",
                        "~/Content/ink/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/ink").Include(
                        "~/Scripts/ink/ink-all.js",
                        "~/Scripts/ink/ink-all.min.js",
                        "~/Scripts/ink/holder.js",
                        "~/Scripts/ink/autoload.js"));
        }
    }
}