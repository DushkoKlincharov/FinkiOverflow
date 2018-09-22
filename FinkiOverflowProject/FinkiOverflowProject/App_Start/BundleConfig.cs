using System.Web;
using System.Web.Optimization;

namespace FinkiOverflowProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/Datatables/jquery.dataTables.js",
                        "~/Scripts/Datatables/dataTables.bootstrap4.min.js",
                        "~/Scripts/bootbox.js"
                        ));

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
                      "~/Content/bootstrap.min.css",
                      "~/Content/DataTables/css/dataTables.bootstrap4.min.css",
                      "~/Content/font-awesome/css/font-awesome.css",
                      "~/Content/custom.css"));

            bundles.Add(new StyleBundle("~/Vendor/css").Include(
                    "~/vendor/bootstrap/css/bootstrap.min.css",
                    "~/vendor/fontawesome-free/css/all.min.css",
                    "~/vendor/datatables/dataTables.bootstrap4.css",
                    "~/css/sb-admin.css"));

            bundles.Add(new ScriptBundle("~/Vendor/jquery").Include(
                  "~/vendor/jquery/jquery.min.js",
                  "~/vendor/bootstrap/js/bootstrap.bundle.min.js",
                  "~/vendor/jquery-easing/jquery.easing.min.js",
                  "~/vendor/chart.js/Chart.min.js",
                  "~/vendor/datatables/jquery.dataTables.js",
                  "~/vendor/datatables/dataTables.bootstrap4.js",
                  "~/js/sb-admin.min.js",
                  "~/js/demo/datatables-demo.js",
                  "~/js/demo/chart-area-demo.js"
                  ));

            bundles.Add(new StyleBundle("~/About/css").Include(
                    "~/Content/custom.css"));
        }
    }
}
