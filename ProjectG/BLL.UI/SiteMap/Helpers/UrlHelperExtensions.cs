using System.Web.Mvc;

namespace BLL.UI.SiteMap.Helpers
{
  public static class UrlHelperExtensions
  {
    public static string AbsoluteRouteUrl(
        this UrlHelper urlHelper,
        string routeName,
        object routeValues = null)
    {
      string scheme = urlHelper.RequestContext.HttpContext.Request.Url.Scheme;
      return urlHelper.RouteUrl(routeName, routeValues, scheme);
    }
  }
}