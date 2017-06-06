using System.Web.Optimization;

namespace UI
{
  public class BundleConfig
  {
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/JQuery/jquery-{version}.js",
                  "~/Scripts/JQueryUI/jquery-ui.min.js",
                  "~/Scripts/JQuery/jquery.unobtrusive-ajax.min.js",
                  "~/Scripts/notify.js",
                  "~/Scripts/select2.min.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/JQuery/jquery.validate.min.js"));

      // Use the development version of Modernizr to develop with and learn from. Then, when you're
      // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                  "~/Scripts/modernizr-*"));

      bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                "~/Scripts/tinymce/tinymce.min.js"));

      bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

      bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/Angular/angular.min.js",
                "~/Scripts/ui-tinymce.min.js",
                "~/Scripts/layout.js",
                "~/Scripts/Controllers/BaseController.js",
                "~/Scripts/Controllers/FilterPanelController.js",
                "~/Scripts/Controllers/RightPanelController.js",
                "~/Scripts/Controllers/AdvancedSearchFormController.js",
                "~/Scripts/Controllers/LoginPanelController.js",
                "~/Scripts/Controllers/LeftPanelController.js",
                "~/Scripts/Controllers/TinyMceController.js",
                "~/Scripts/Controllers/CategoriesController.js",
                "~/Scripts/Controllers/PagesController.js",
                "~/Scripts/Controllers/NavigatorController.js",
                "~/Scripts/Services/AccountService.js",
                "~/Scripts/Services/CategoriesService.js",
                "~/Scripts/Services/PagesService.js"));

      bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css",
                "~/Content/main.css",
                "~/Content/JQueryUI/south-street/jquery-ui.min.css",
                "~/Content/css/select2.min.css"));
    }
  }
}