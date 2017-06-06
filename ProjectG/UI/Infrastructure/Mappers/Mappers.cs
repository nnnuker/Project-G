using BLL.UI.Models;
using System;
using UI.Models;
using UI.Models.Categories;

namespace UI.Infrastructure.Mappers
{
  public static class Mappers
  {
    public static CategoryViewModel ToViewModel(this BllCategory entity)
    {
      return new CategoryViewModel
      {
        Id = entity.Id,
        Name = entity.Name,
        ParentId = entity.ParentId,
        ChildCount = entity.PagesCount,
        HasChilds = entity.HasChildCategory
      };
    }

    public static BllCategory ToBll(this CategoryViewModel entity)
    {
      return new BllCategory
      {
        Id = entity.Id,
        Name = entity.Name,
        ParentId = entity.ParentId
      };
    }

    public static BllPage ToBll(this PageViewModel entity, BllSeo seo)
    {
      return new BllPage
      {
        Id = entity.Id,
        Content = entity.Page,
        CategoryId = entity.CategoryId,
        Title = entity.Title,
        UrlId = seo.Id,
        CreationDate = DateTime.Now
      };
    }

    public static PageViewModel ToViewModel(this BllPage entity)
    {
      return new PageViewModel
      {
        Id = entity.Id,
        Title = entity.Title,
        CategoryId = entity.CategoryId,
        Page = entity.Content,
        Date = entity.CreationDate.Value,
        SeoUrl = entity.Url
      };
    }
  }
}