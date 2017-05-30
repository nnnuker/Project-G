using System.Web.Mvc;
using System.Web.Routing;

namespace UI
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
      name: "robots",
      url: "robots.txt",
      defaults: new { controller = "Home", action = "RobotsText" }
      );

      routes.MapRoute(
      name: "sitemap",
      url: "sitemap.xml",
      defaults: new { controller = "Home", action = "SitemapXml" }
      );

      routes.MapRoute(
          name: "Default",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
      );
    }
  }
}