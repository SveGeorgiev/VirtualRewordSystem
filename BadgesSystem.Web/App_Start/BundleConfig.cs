using System.Web;
using System.Web.Optimization;

namespace BadgesSystem.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquerybootstrap").Include(
                              "~/Scripts/jquery-{version}.js",
                              "~/Scripts/bootstrap.js",
                              "~/Scripts/respond.js",
                              "~/Scripts/jquery.scrollUp.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                              "~/Scripts/angular.js",
                              "~/Scripts/angular-route.js",
                              "~/Scripts/ngProgress.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                              "~/Content/bootstrap.css",
                              "~/Content/bootstrap-theme.css",
                              "~/Content/site.css",
                              "~/Content/appcss.css",
                              "~/Content/ngProgress.css"));

            bundles.Add(new ScriptBundle("~/bundles/appjs").IncludeDirectory(
                              "~/app", "*.js", true));
        }
    }
}
