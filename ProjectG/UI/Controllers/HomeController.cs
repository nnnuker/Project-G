using System.Text;
using System.Web.Mvc;
using BLL.UI.SiteMap;
using UI.Infrastructure.Filters;
using BLL.UI.Robots;

namespace UI.Controllers
{
  [Culture]
  public class HomeController : Controller
  {
    private readonly SiteMapBuilder _siteMapBuilder;
    private readonly RobotsBuilder _robotsBuilder;

    public HomeController(SiteMapBuilder siteMapBuilder, RobotsBuilder robotsBuilder)
    {
      _siteMapBuilder = siteMapBuilder;
      _robotsBuilder = robotsBuilder;
    }

    public ActionResult Index()
    {
      ViewBag.ReturnUrl = this.Url.Action("Index", "Home");

      return this.View();
    }

    [Route("robots.txt", Name = "GetRobotsText"), OutputCache(Duration = 86400)]
    public ContentResult RobotsText()
    {
      var result = _robotsBuilder.Build(this.Url.RouteUrl("sitemap", null, this.Request.Url.Scheme));

      return this.Content(result, "text/plain", Encoding.UTF8);
    }

    [Route("sitemap.xml", Name = "GetSitemapXml"), OutputCache(Duration = 86400)]
    public ContentResult SitemapXml()
    {
      var sitemapNodes = _siteMapBuilder.GetSitemapNodes(this.Url);
      string xml = _siteMapBuilder.GetSitemapDocument(sitemapNodes);
      return this.Content(xml, "text/xml", Encoding.UTF8);
    }

    public ActionResult Error()
    {
      return this.View("Error");
    }
  }
}