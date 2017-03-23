using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
  /// <summary>
  /// Language controller that changes language.
  /// </summary>
  /// <seealso cref="System.Web.Mvc.Controller" />
  public class LanguageController : Controller
  {
    /// <summary>
    /// Changes the language.
    /// </summary>
    /// <param name="lang">The language.</param>
    /// <param name="returnUrl">The return URL.</param>
    /// <returns>Redirect to return URL.</returns>
    public ActionResult ChangeLanguage(string lang, string returnUrl)
    {
      var ci = CultureInfo.GetCultureInfo(lang);

      HttpCookie cookie = HttpContext.Request.Cookies["lang"];

      if (cookie != null)
      {
        var responseCookie = HttpContext.Response.Cookies["lang"];
        if (responseCookie != null)
        {
          responseCookie.Value = ci.Name;
        }
      }
      else
      {
        cookie = new HttpCookie("lang", ci.Name);

        var addYears = cookie.Expires.AddYears(1);
        cookie.Expires = addYears;

        HttpContext.Response.Cookies.Add(cookie);
      }

      return this.Redirect(returnUrl);
    }
  }
}