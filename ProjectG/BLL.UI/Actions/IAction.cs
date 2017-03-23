using System.Web.Mvc;

namespace BLL.UI.Actions
{
  /// <summary>
  /// Represents mvc action.
  /// </summary>
  public interface IAction
  {
    /// <summary>
    /// Processes the request asynchronous.
    /// </summary>
    /// <returns>Action result.</returns>
    ActionResult ProcessAsync();
  }
}
