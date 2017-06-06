using System;
using System.Data.Entity;

namespace BLL
{
  public class DataInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
  {
    protected override void Seed(DatabaseContext context)
    {
      context.Categories.Add(new Category { Name = "zero", ParentId = 1 });
      context.Categories.Add(new Category { Name = "root", ParentId = 1 });
      context.Categories.Add(new Category { Name = "Category 3", ParentId = 2 });
      context.Categories.Add(new Category { Name = "Category 4", ParentId = 2 });
      context.Categories.Add(new Category { Name = "Category 5", ParentId = 4 });
      context.Categories.Add(new Category { Name = "Category 6", ParentId = 2 });
      context.Categories.Add(new Category { Name = "Category 7", ParentId = 6 });

      context.Seos.Add(new Seo { Name = "page-1" });
      context.Seos.Add(new Seo { Name = "page-2" });
      context.Seos.Add(new Seo { Name = "page-3" });
      context.Seos.Add(new Seo { Name = "page-4" });
      context.Seos.Add(new Seo { Name = "page-5" });
      context.Seos.Add(new Seo { Name = "page-6" });
      context.Seos.Add(new Seo { Name = "page-7" });

      context.Pages.Add(CreatePage(1, 2));
      context.Pages.Add(CreatePage(2, 3));
      context.Pages.Add(CreatePage(3, 4));
      context.Pages.Add(CreatePage(4, 4));
      context.Pages.Add(CreatePage(5, 4));
      context.Pages.Add(CreatePage(6, 6));
      context.Pages.Add(CreatePage(7, 5));

      context.Roles.Add(new Role { Name = "Admin" });
      context.Roles.Add(new Role { Name = "User" });
    }

    private Page CreatePage(int number, int category)
    {
      return new Page
      {
        Title = $"page {number}",
        Content = $"<h1>page {number}</h1><p>paragraph paragraph paragraph</p>",
        CategoryId = category,
        CreationDate = DateTime.Now,
        UrlId = number
      };
    }
  }
}