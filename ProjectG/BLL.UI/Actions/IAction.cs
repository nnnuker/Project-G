using System.Threading.Tasks;

namespace BLL.UI.Actions
{
  /// <summary>
  /// Represents mvc action.
  /// </summary>
  /// <typeparam name="TRequest">The type of the request.</typeparam>
  /// <typeparam name="TResponse">The type of the response.</typeparam>
  public interface IAction<in TRequest, TResponse>
  {
    /// <summary>
    /// Processes the request asynchronous.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Response after process request.</returns>
    Task<TResponse> ProcessAsync(TRequest request);
  }
}