using BLL.UI.Models;
using BLL.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UI.Infrastructure.Filters;
using UI.Infrastructure.Mappers;
using UI.Models;
using UI.Models.Categories;

namespace UI.Controllers
{
  [Culture]
  [Authorize(Roles = "Admin")]
  public class ContentController : Controller
  {
    private readonly PagesService _pagesService;
    private readonly CategoriesService _categoriesService;
    private readonly SeoService _seoService;

    public ContentController(PagesService pagesService, CategoriesService categoriesService, SeoService seoService)
    {
      _pagesService = pagesService;
      _categoriesService = categoriesService;
      _seoService = seoService;
    }

    [AllowAnonymous]
    public ActionResult Index(string id)
    {
      var result = _pagesService.Get(id).ToViewModel();

      if (result != null)
      {
        ViewBag.ReturnUrl = this.Url.RouteUrl(new { controller = "Content", action = "Index", id = id });

        return this.ResultView("Index", result);
      }

      return this.RedirectToLocal(ViewBag.ReturnUrl);
    }

    public ActionResult AddPage()
    {
      ViewBag.ReturnUrl = this.Url.Action("AddPage", "Content");

      return this.View();
    }

    [HttpPost]
    [ValidateInput(false)]
    public ActionResult AddPage(PageViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return this.View(model);
      }

      var result = _pagesService.Get(model.Id);

      if (result != null)
      {
        var seoEquals = _seoService.Get(model.Id)?.Name.Equals(model.SeoUrl);

        var seoUrl = new BllSeo
        {
          Id = model.Id,
          Name = model.SeoUrl
        };

        if (seoEquals.HasValue && !seoEquals.Value)
        {
          _seoService.Update(new BllSeo
          {
            Id = model.Id,
            Name = model.SeoUrl
          });
        }

        ViewBag.ReturnUrl = this.Url.RouteUrl("Default", new { action = "AddPage", controller = "Content", id = model.SeoUrl});
        
        _pagesService.Update(model.ToBll(seoUrl));

        return this.View(model);
      }

      if (_seoService.Get(model.SeoUrl) != null)
      {
        ModelState.AddModelError("SeoUrl", "Url is already defined");
        return this.View(model);
      }

      ViewBag.ReturnUrl = this.Url.Action("AddPage", "Content");

      var seo = _seoService.Create(new BllSeo
      {
        Name = model.SeoUrl
      });

      _pagesService.Create(model.ToBll(seo));

      return this.View(model);
    }

    [HttpGet]
    public ActionResult AddPage(int? id)
    {
      ViewBag.ReturnUrl = this.Url.Action("AddPage", "Content");

      if (id.HasValue)
      {
        var page = _pagesService.Get(id.Value);

        if (page != null)
        {
          return this.View("AddPage", page.ToViewModel());
        }
      }

      return this.View("AddPage");
    }

    public ActionResult RemovePage(int? id)
    {
      var delete = _pagesService.Get(id.Value);

      if (delete != null)
      {
        _pagesService.Delete(delete.Id);
        _seoService.Delete(delete.UrlId);

        return this.RedirectToAction("Index", "Home");
      }

      return this.HttpResult(HttpStatusCode.NoContent);
    }

    [HttpGet]
    [AllowAnonymous]
    public JsonResult AllPages()
    {
      return this.Json(_pagesService.GetAll().Select(p => p.ToViewModel().GetSmartViewModel()), JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult GetPageByCategoryId(int? id)
    {
      if (!id.HasValue)
      {
        return this.HttpResult(HttpStatusCode.NoContent);
      }

      var result = _pagesService.GetByCategory(id.Value)?.Select(c => c.ToViewModel());

      if (result == null)
      {
        return this.HttpResult(HttpStatusCode.NoContent);
      }

      return this.Json(result, JsonRequestBehavior.AllowGet);
    }

    #region Categories
    [HttpGet]
    public ActionResult Categories()
    {
      ViewBag.ReturnUrl = this.Url.Action("Categories", "Content");

      return this.ResultView("Categories");
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult CategoriesByParentId(int? id)
    {
      if (!id.HasValue)
      {
        return this.HttpResult(HttpStatusCode.NoContent);
      }

      var category = _categoriesService.Get(id.Value);

      if (category == null && id.Value < 2)
      {
        return this.HttpResult(HttpStatusCode.NoContent);
      }

      var childs = _categoriesService.GetAll().Where(c => c.ParentId == id.Value).OrderBy(m => !m.HasChildCategory).Select(c => c.ToViewModel());

      return this.Json(childs, JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    [AllowAnonymous]
    public JsonResult AllCategories()
    {
      return this.Json(_categoriesService.GetAll().Select(c => c.ToViewModel()), JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public ActionResult AddCategory(CategoryViewModel model)
    {
      var result = _categoriesService.Get(model.Id);

      if (result == null)
      {
        var res = _categoriesService.Create(model.ToBll()).ToViewModel();

        if (res == null)
        {
          return this.HttpResult(HttpStatusCode.Conflict);
        }

        return this.Json(res, JsonRequestBehavior.AllowGet);
      }

      return this.HttpResult(HttpStatusCode.OK);
    }

    [HttpDelete]
    public ActionResult RemoveCategory(int? id)
    {
      if (!id.HasValue)
      {
        return this.HttpResult(HttpStatusCode.NoContent);
      }

      var delete = _categoriesService.Get(id.Value);

      if (delete != null)
      {
        _categoriesService.Delete(delete.Id);
        return new HttpStatusCodeResult(HttpStatusCode.OK);
      }

      return this.HttpResult(HttpStatusCode.NoContent);
    }

    [HttpPut]
    public ActionResult UpdateCategory(CategoryViewModel model)
    {
      var result = _categoriesService.Get(model.Id);

      if (result != null)
      {
        result.Name = model.Name;
        result.ParentId = model.ParentId;

        _categoriesService.Update(result);

        return new HttpStatusCodeResult(HttpStatusCode.OK);
      }

      return this.HttpResult(HttpStatusCode.NoContent);
    }
    #endregion

    public ActionResult Search(string id)
    {
      var categories = _categoriesService.GetAll().Where(c => c.Name.Contains(id));
      var pages = _pagesService.GetAll().Where(p => p.Title.Contains(id) || p.Content.Contains(id)).ToList();

      foreach (var category in categories)
      {
        pages.AddRange(_pagesService.GetByCategory(category.Id));
      }

      pages.Distinct();

      return this.Json(pages.Select(p => p.ToViewModel().GetSmartViewModel()), JsonRequestBehavior.AllowGet);
    }

    public ActionResult SearchByString(string id)
    {
      var result = new List<SearchViewModel>();

      var categories = _categoriesService.GetAll().Where(c => c.Name.Contains(id));
      var pages = _pagesService.GetAll().Where(p => p.Title.Contains(id) || p.Content.Contains(id)).ToList();

      result.AddRange(categories.Select(c => new SearchViewModel
      {
        Name = c.Name,
        Link = c.Id.ToString(),
        IsPage = false
      }));

      result.AddRange(pages.Select(p => new SearchViewModel
      {
        Name = p.Title,
        Link = this.Url.Action("Index", "Content", new { id = p.Url }),
        IsPage = true
      }));

      return this.Json(result, JsonRequestBehavior.AllowGet);
    }

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (this.Url.IsLocalUrl(returnUrl))
      {
        return this.Redirect(returnUrl);
      }

      return this.RedirectToAction("Index", "Home");
    }

    private ActionResult ResultView(string page)
    {
      if (Request.IsAjaxRequest())
      {
        return this.PartialView(page);
      }

      return this.View(page);
    }

    private ActionResult ResultView(string page, object model)
    {
      if (Request.IsAjaxRequest())
      {
        return this.PartialView(page, model);
      }

      return this.View(page, model);
    }

    private ActionResult HttpResult(HttpStatusCode status)
    {
      return new HttpStatusCodeResult(status);
    }
  }
}