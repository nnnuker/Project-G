using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace UI.Infrastructure.Filters
{
  /// <summary>
  /// Class defined culture attribute.
  /// </summary>
  /// <seealso cref="System.Web.Mvc.IActionFilter" />
  public class CultureAttribute : FilterAttribute, IActionFilter
  {
    private readonly string _defaultCulture;

    /// <summary>
    /// Initializes a new instance of the <see cref="CultureAttribute"/> class.
    /// </summary>
    public CultureAttribute()
    {
      _defaultCulture = "ru";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CultureAttribute"/> class.
    /// </summary>
    /// <param name="culture">The culture short string.</param>
    public CultureAttribute(string culture)
    {
      _defaultCulture = culture;
    }

    /// <summary>
    /// Called before an action method executes.
    /// </summary>
    /// <param name="filterContext">The filter context.</param>
    /// <exception cref="System.NotSupportedException"></exception>
    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
      HttpCookie cookie = filterContext.HttpContext.Request.Cookies["lang"];

      var culture = cookie != null ? cookie.Value : _defaultCulture;

      try
      {
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(culture);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
      }
      catch (CultureNotFoundException)
      {
        throw new NotSupportedException($"ERROR: Invalid language code '{culture}'.");
      }
    }

    /// <summary>
    /// Called after the action method executes.
    /// </summary>
    /// <param name="filterContext">The filter context.</param>
    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
  }
}