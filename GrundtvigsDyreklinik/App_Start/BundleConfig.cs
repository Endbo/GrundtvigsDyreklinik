using System.Web;
using System.Web.Optimization;

namespace GrundtvigsDyreklinik
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // jq datepicker script and css bundles start
            //script
            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js",
            //            "~/Scripts/jquery-ui-timepicker-addon.min.js"));
            //css  
            //bundles.Add(new StyleBundle("~/Content/cssjqryUi").Include(
            //       "~/Content/themes/base/jquery-ui.css",
            //        "~/Content/jquery-ui-timepicker-addon.css"));

            // jq datepicker script and css bundles end

            //jQuery fullcalendar and datepicker plugin css
            bundles.Add(new StyleBundle("~/Content/fullcalendar").Include(
                                      "~/Content/fullcalendar.css",
                                      "~/Content/themes/base/jquery-ui.css",
                        "~/Content/jquery-ui-timepicker-addon.css"));

            //jQuery fullcalendar and datepicker plugin js
            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                                      "~/Scripts/moment.js",
                                      "~/Scripts/fullcalendar.js",
                                      "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery-ui-timepicker-addon.min.js"));
        }
    }
}
