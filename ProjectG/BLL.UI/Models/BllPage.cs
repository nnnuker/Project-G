using System;

namespace BLL.UI.Models
{
  public class BllPage
  {
    public int Id { get; set; }
    public string Content { get; set; }
    public string Title { get; set; }
    public int UrlId { get; set; }
    public int CategoryId { get; set; }
    public DateTime? CreationDate { get; set; }
    public string Url { get; set; }
  }
}