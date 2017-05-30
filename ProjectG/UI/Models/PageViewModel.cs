using System;
using BLL.UI.Providers;

namespace UI.Models
{
  public class PageViewModel
  {
    private readonly PageTitleProvider _titleProvider = new PageTitleProvider();
    private readonly PageDescriptionProvider _descriptionProvider = new PageDescriptionProvider();

    public int Id { get; set; }
    public string Page { get; set; }
    public string Title { get; private set; }
    public string SeoUrl { get; set; }
    public int CategoryId { get; set; }
    public DateTime Date { get; set; }

    public PageViewModel()
    {
      Title = _titleProvider.GetValue(Page, "h1");
    }

    public PageViewModel(AddPageViewModel model)
    {
      this.Page = model.TextArea;
      this.SeoUrl = model.SeoUrl;
      this.CategoryId = model.Category;
      this.Title = _titleProvider.GetValue(Page, "h1");
    }

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