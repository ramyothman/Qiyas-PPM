using System.Web;
using System.Web.Optimization;
using System.Linq;
using System.Linq.Expressions;
namespace Qiyas.WebAdmin
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

            bundles.Add(new StyleBundle("~/Assets/css").Include(
                      "~/App_Assets/css/bootstrap.css",
                      "~/App_Assets/css/se7en-font.css",
                      "~/App_Assets/css/style.css",
                      "~/App_Assets/css/qiyas.css"));

            var bundle = bundles.GetBundleFor("~/bundles/jqueryval");
            bundle.Include("~/Scripts/globalize/cultures/globalize.culture." + System.Threading.Thread.CurrentThread.CurrentCulture + ".js");
            bundle.Include("~/Scripts/globalize/globalize.validation.js");

            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                      "~/Scripts/globalize/cultures/globalize.culture." + System.Threading.Thread.CurrentThread.CurrentCulture + ".js",
                      "~/Scripts/jquery.validate.js",
                      "~/Scripts/jquery.validate.globalize.js",
                      "~/Scripts/jquery.validate.unobtrusive.js",
                      "~/Scripts/globalize/globalize.validation.js"));
        }
    }
}
