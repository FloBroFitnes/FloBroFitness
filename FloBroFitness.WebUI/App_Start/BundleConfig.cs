using System.Web;
using System.Web.Optimization;

namespace FloBroFitness.WebUI
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
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/AdminStyle").Include(
                "~/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                "~/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                "~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                "~/assets/global/css/components.css",
                "~/assets/global/css/plugins.min.css",
                "~/Content/Style.css",
                "~/Content/Site.css",
                "~/assets/layouts/layout3/css/layout.min.css",
                "~/assets/layouts/layout3/css/themes/default.min.css",
                "~/assets/global/plugins/datatables/datatables.min.css",
                "~/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css",
                "~/assets/layouts/layout3/css/custom.css"
                     //"~/Content/jquery-te-1.4.0.css"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/Adminjquery").Include(
           "~/Scripts/jquery-3.3.1.min.js",
            "~/assets/global/plugins/bootstrap/js/bootstrap.min.js",
            "~/assets/global/plugins/js.cookie.min.js",
            "~/assets/global/plugins/jquery.blockui.min.js",
            "~/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
            "~/assets/global/plugins/jquery-repeater/jquery.repeater.js",
            "~/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js",
            "~/assets/global/scripts/app.min.js",
            "~/assets/pages/scripts/form-repeater.min.js",
            "~/assets/pages/scripts/components-date-time-pickers.min.js",
            "~/assets/layouts/layout3/scripts/layout.min.js",
            "~/assets/layouts/layout3/scripts/demo.min.js",
            "~/assets/layouts/global/scripts/quick-sidebar.min.js",
            "~/assets/layouts/global/scripts/quick-nav.min.js",
              "~/assets/global/scripts/datatable.js",
              "~/assets/global/plugins/datatables/datatables.js",
              "~/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js"
              //"~/assets/pages/scripts/table-datatables-managed.js"

              //"~/Scripts/jquery-te-1.4.0.min.js"
              ));
            bundles.Add(new ScriptBundle("~/bundles/AdminValidation").Include(
           "~/Scripts/jquery.validate.min.js",
           "~/Scripts/jquery.validate.unobtrusive.min.js"
             ));
        }
    }
}
