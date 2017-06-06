using System.Text;
using System.Web.Mvc;
using BLL.UI.SiteMap;
using UI.Infrastructure.Filters;

namespace UI.Controllers
{
  [Culture]
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      ViewBag.ReturnUrl = this.Url.Action("Index", "Home");

      return this.View();
    }

    [Route("robots.txt", Name = "GetRobotsText"), OutputCache(Duration = 86400)]
    public ContentResult RobotsText()
    {
      StringBuilder stringBuilder = new StringBuilder();

      stringBuilder.AppendLine("user-agent: *");
      stringBuilder.AppendLine("disallow:");
      stringBuilder.AppendLine("allow:");
      stringBuilder.Append("sitemap: ");
      stringBuilder.AppendLine(this.Url.RouteUrl("sitemap", null, this.Request.Url.Scheme).TrimEnd('/'));

      return this.Content(stringBuilder.ToString(), "text/plain", Encoding.UTF8);
    }

    [Route("sitemap.xml", Name = "GetSitemapXml"), OutputCache(Duration = 86400)]
    public ContentResult SitemapXml()
    {
      var sitemapBuilder = new SiteMapBuilder();
      var sitemapNodes = sitemapBuilder.GetSitemapNodes(this.Url);
      string xml = sitemapBuilder.GetSitemapDocument(sitemapNodes);
      return this.Content(xml, "text/xml", Encoding.UTF8);
    }

    public ActionResult Error()
    {
      return this.View("Error");
    }
  }
}