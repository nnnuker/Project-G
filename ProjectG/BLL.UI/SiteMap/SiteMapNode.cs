using System;

namespace BLL.UI.SiteMap
{
  public class SitemapNode
  {
    public SitemapFrequency? Frequency { get; set; }
    public DateTime? LastModified { get; set; }
    public double? Priority { get; set; }
    public string Url { get; set; }
  }
}