using System;
using BLL.UI.Providers;

namespace UI.Models
{
  public class PageViewModel
  {
    private readonly PageDescriptionProvider _descriptionProvider = new PageDescriptionProvider();

    public int Id { get; set; }
    public string Page { get; set; }
    public string Title { get; set; }
    public string SeoUrl { get; set; }
    public int CategoryId { get; set; }
    public DateTime Date { get; set; }
    
    public PageSmartViewModel GetSmartViewModel()
    {
      return new PageSmartViewModel()
      {
        Id = this.Id,
        SeoUrl = this.SeoUrl,
        Date = this.Date,
        Title = this.Title,
        CategoryId = this.CategoryId,
        PageDescription = _descriptionProvider.GetValue(this.Page, "p")
      };
    }
  }

  public class PageSmartViewModel
  {
    public int Id { get; set; }
    public string PageDescription { get; set; }
    public string SeoUrl { get; set; }
    public string Title { get; set; }
    public int CategoryId { get; set; }
    public DateTime Date { get; set; }
  }
}