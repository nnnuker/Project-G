using BLL.UI.SiteMap.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BLL.UI.SiteMap
{
  public class SiteMapBuilder
  {
    public IReadOnlyCollection<SitemapNode> GetSitemapNodes(UrlHelper urlHelper)
    {
      List<SitemapNode> nodes = new List<SitemapNode>();

      nodes.Add(
          new SitemapNode()
          {
            Url = urlHelper.AbsoluteRouteUrl("Default", new { controller = "Home", action = "Index"}),
            Priority = 1
          });
      nodes.Add(
         new SitemapNode()
         {
           Url = urlHelper.AbsoluteRouteUrl("Default", new { controller = "Account", action = "Register" }),
           Priority = 0.9
         });

      //foreach (int productId in productRepository.GetProductIds())
      //{
      //  nodes.Add(
      //     new SitemapNode()
      //     {
      //       Url = urlHelper.AbsoluteRouteUrl("ProductGetProduct", new { id = productId }),
      //       Frequency = SitemapFrequency.Weekly,
      //       Priority = 0.8
      //     });
      //}

      return nodes;
    }

    public string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
    {
      XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
      XElement root = new XElement(xmlns + "urlset");

      foreach (SitemapNode sitemapNode in sitemapNodes)
      {
        XElement urlElement = new XElement(
            xmlns + "url",
            new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
            sitemapNode.LastModified == null ? null : new XElement(
                xmlns + "lastmod",
                sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
            sitemapNode.Frequency == null ? null : new XElement(
                xmlns + "changefreq",
                sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
            sitemapNode.Priority == null ? null : new XElement(
                xmlns + "priority",
                sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
        root.Add(urlElement);
      }

      XDocument document = new XDocument(root);
      return document.ToString();
    }
  }
}