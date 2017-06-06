using System.Web.Mvc;
using UI.Infrastructure.Filters;
using UI.Models;
using System.Web.Security;
using UI.Infrastructure.Providers;

namespace UI.Controllers
{
  [Authorize]
  [Culture]
  public class AccountController : Controller
  {
    public ActionResult Login()
    {
      return this.View();
    }
    
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult Login(LoginViewModel model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        if (Membership.ValidateUser(model.Email, model.Password))
        {
          FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
          var flag = User.Identity.IsAuthenticated;
          return this.RedirectToLocal(returnUrl);
        }
        ModelState.AddModelError("", "Wrong password or email");
      }

      return View(model);
    }
    
    [AllowAnonymous]
    public ActionResult Register(string returnUrl)
    {
      ViewBag.ReturnUrl = returnUrl;

      if (Request.IsAjaxRequest())
      {
        return this.PartialView("Register");
      }

      return this.View("Register");
    }
    
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult Register(RegisterViewModel model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        var provider = ((CustomMembershipProvider)Membership.Provider);

        if (provider.GetUser(model.Email, false) != null)
        {
          ModelState.AddModelError("", "Email already exist");
          return View(model);
        }

        var membershipUser = provider.CreateUser(model.Email, null, null, model.Password);

        if (membershipUser != null)
        {
          FormsAuthentication.SetAuthCookie(model.Email, false);
          return this.RedirectToLocal(returnUrl);
        }
        else
        {
          ModelState.AddModelError("", "Error while registration");
        }
      }

      return this.ResultView("Register", model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult LogOff()
    {
      FormsAuthentication.SignOut();

      return RedirectToAction("Index", "Home");
    }

    #region Helpers

    private ActionResult ResultView(string page, object model)
    {
      if (Request.IsAjaxRequest())
      {
        return this.PartialView(page, model);
      }

      return this.View(page, model);
    }

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (this.Url.IsLocalUrl(returnUrl))
      {
        return this.Redirect(returnUrl);
      }

      return this.RedirectToAction("Index", "Home");
    }
    #endregion
  }
}