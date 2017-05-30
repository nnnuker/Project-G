using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UI.Infrastructure.Filters;
using UI.Models;
using UI.Models.Categories;

namespace UI.Controllers
{
  [Culture]
  public class ContentController : Controller
  {
    private static List<CategoryViewModel> list = new List<CategoryViewModel>
      {
        new CategoryViewModel { Id = 0, ParentId = -1, Name = "Category 0" },
        new CategoryViewModel { Id = 1, ParentId = 0, Name = "Category 1" },
        new CategoryViewModel { Id = 2, ParentId = 0, Name = "Category 2" },
        new CategoryViewModel { Id = 3, ParentId = 2, Name = "Category 3" },
        new CategoryViewModel { Id = 4, ParentId = 3, Name = "Category 4" },
        new CategoryViewModel { Id = 5, ParentId = 3, Name = "Category 5" },
        new CategoryViewModel { Id = 6, ParentId = 7, Name = "Category 6" },
        new CategoryViewModel { Id = 7, ParentId = -1, Name = "Category 7" }
      };

    private static List<PageViewModel> pages = new List<PageViewModel>
    {
      new PageViewModel(new AddPageViewModel{TextArea="<h1>Heading</h1><p>paragraph</p>", Category=4, SeoUrl="first-paragraph"})
      {
        Id = 0,
        Date = DateTime.Now
      },
      new PageViewModel(new AddPageViewModel{TextArea="<h1>Heading2</h1><p>paragraph2</p>", Category=5, SeoUrl="second-paragraph"})
      {
        Id = 1,
        Date = DateTime.Now
      },
      new PageViewModel(new AddPageViewModel{TextArea="<h1>Heading3</h1><p>paragraph3paragraph3paragraph3paragraph3paragraph3paragraph3 paragraph3paragraph3 paragraph3paragraph3paragraph3paragraph3paragraph3paragraph3</p>", Category=6, SeoUrl="third-paragraph"})
      {
        Id = 2,
        Date = DateTime.Now
      }
    };

    public ActionResult Index(string id)
    {
      var result = pages.FirstOrDefault(p => p.SeoUrl.Equals(id, StringComparison.InvariantCultureIgnoreCase));

      if (result != null)
      {
        ViewBag.ReturnUrl = this.Url.RouteUrl(new { controller = "Content", action = "Index", id = id });

        return this.View(result);
      }

      return this.RedirectToLocal(ViewBag.ReturnUrl);
    }

    public ActionResult AddPage()
    {
      ViewBag.ReturnUrl = this.Url.Action("AddPage", "Content");

      return this.View();
    }

    public ActionResult UpdatePage(int? id)
    {
      ViewBag.ReturnUrl = this.Url.Action("AddPage", "Content");

      return this.View("AddPage");
    }

    [HttpPost]
    [ValidateInput(false)]
    public ActionResult AddPage(AddPageViewModel model)
    {
      ViewBag.ReturnUrl = this.Url.Action("AddPage", "Content");

      pages.Add(new PageViewModel(model));

      return this.View();
    }

    [HttpGet]
    public JsonResult AllPages()
    {
      return this.Json(pages.Select(p => p.GetSmartViewModel()), JsonRequestBehavior.AllowGet);
    }

    [HttpDelete]
    public ActionResult RemovePage(int? id)
    {
      var delete = pages.FirstOrDefault(d => d.Id == id.Value);

      if (delete != null)
      {
        pages.Remove(delete);
        return new HttpStatusCodeResult(HttpStatusCode.OK);
      }

      return new HttpStatusCodeResult(HttpStatusCode.NotFound);
    }

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (this.Url.IsLocalUrl(returnUrl))
      {
        return this.Redirect(returnUrl);
      }

      return this.RedirectToAction("Index", "Home");
    }

    #region Categories
    [HttpGet]
    public ActionResult Categories()
    {
      ViewBag.ReturnUrl = this.Url.Action("Categories", "Content");

      return this.View();
    }

    [HttpGet]
    public JsonResult AllCategories()
    {
      return this.Json(list, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public ActionResult AddCategory(CategoryViewModel model)
    {
      var result = list.FirstOrDefault(d => d.Id == model.Id);

      if (result == null)
      {
        model.Id = list.Count;
        list.Add(model);

        return this.Json(model, JsonRequestBehavior.AllowGet);
      }

      return new HttpStatusCodeResult(HttpStatusCode.OK);
    }

    [HttpDelete]
    public ActionResult RemoveCategory(int? id)
    {
      var delete = list.FirstOrDefault(d => d.Id == id.Value);

      if (delete != null)
      {
        list.Remove(delete);
        return new HttpStatusCodeResult(HttpStatusCode.OK);
      }

      return new HttpStatusCodeResult(HttpStatusCode.NotFound);
    }

    [HttpPut]
    public ActionResult UpdateCategory(CategoryViewModel model)
    {
      var result = list.FirstOrDefault(m => m.Id == model.Id);

      if (result != null)
      {
        result.Name = model.Name;
        result.ParentId = model.ParentId;
        return new HttpStatusCodeResult(HttpStatusCode.OK);
      }

      return new HttpStatusCodeResult(HttpStatusCode.NotFound);
    }
    #endregion
  }
}