using System.Threading.Tasks;

namespace BLL.UI.Actions
{
  /// <summary>
  /// Represents mvc action.
  /// </summary>
  /// <typeparam name="TResponse">The type of the response.</typeparam>
  public interface IAction<TResponse>
  {
    /// <summary>
    /// Processes the asynchronous.
    /// </summary>
    /// <returns>
    /// Response after process action.
    /// </returns>
    Task<TResponse> ProcessAsync();
  }
}
