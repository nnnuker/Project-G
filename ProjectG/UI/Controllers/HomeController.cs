using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using UI.Infrastructure.Filters;

namespace UI.Controllers
{
  /// <summary>
  /// Home controller class.
  /// </summary>
  /// <seealso cref="System.Web.Mvc.Controller" />
  [Culture]
  public class HomeController : Controller
  {
    /// <summary>
    /// Returns main page.
    /// </summary>
    /// <returns>Main page.</returns>
    public ActionResult Index()
    {
      return this.View();
    }
  }
}