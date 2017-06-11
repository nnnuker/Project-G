using BLL.UI.Services;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using UI.Infrastructure.Filters;
using UI.Infrastructure.Mappers;
using UI.Infrastructure.Providers;
using UI.Models;

namespace UI.Controllers
{
  [Authorize(Roles = "Admin")]
  [Culture]
  public class AdminController : Controller
  {
    private readonly UsersService _usersService;
    private readonly RolesService _rolesService;

    public AdminController(UsersService usersService, RolesService rolesService)
    {
      _usersService = usersService;
      _rolesService = rolesService;
    }

    [HttpGet]
    public ActionResult Users()
    {
      ViewBag.ReturnUrl = this.Url.Action("Users", "Admin");

      return this.ResultView("Users");
    }

    [HttpGet]
    [AllowAnonymous]
    public JsonResult AllUsers()
    {
      return this.Json(_usersService.GetAll().Select(c => c.ToViewModel()), JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public ActionResult AddUser(UserViewModel model)
    {
      if (ModelState.IsValid)
      {
        var provider = ((CustomMembershipProvider)Membership.Provider);

        if (provider.GetUser(model.Email, false) != null)
        {
          ModelState.AddModelError("", "Email already exist");
          return View(model);
        }

        var membershipUser = provider.CreateUser(model.Email, model.FirstName, model.LastName, model.Password);

        if (membershipUser != null)
        {
          return this.Json(_usersService.Get(membershipUser.Email).ToViewModel());
        }
        else
        {
          ModelState.AddModelError("", "Error while registration");
        }
      }

      return this.View("Users", model);
    }

    [HttpDelete]
    public ActionResult RemoveUser(int? id)
    {
      if (!id.HasValue)
      {
        return this.HttpResult(HttpStatusCode.NoContent);
      }

      var delete = _usersService.Get(id.Value);

      if (delete != null)
      {
        _usersService.Delete(delete.Id);
        return new HttpStatusCodeResult(HttpStatusCode.OK);
      }

      return this.HttpResult(HttpStatusCode.NoContent);
    }

    [HttpPut]
    public ActionResult UpdateUser(UserViewModel model)
    {
      if (ModelState.IsValid)
      {
        var result = _usersService.Get(model.Id);

        if (result != null)
        {
          var provider = ((CustomMembershipProvider)Membership.Provider);

          provider.UpdateUser(model.ToBll());

          return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
      }      

      return this.HttpResult(HttpStatusCode.NoContent);
    }

    [HttpGet]
    public ActionResult AllRoles()
    {
      return this.Json(_rolesService.GetAll(), JsonRequestBehavior.AllowGet);
    }

    #region Helpers
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
    #endregion
  }
}