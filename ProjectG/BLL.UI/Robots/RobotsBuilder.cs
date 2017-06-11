using System.Text;

namespace BLL.UI.Robots
{
  public class RobotsBuilder
  {
    public string Build(string url)
    {
      StringBuilder stringBuilder = new StringBuilder();

      stringBuilder.AppendLine("user-agent: *");
      stringBuilder.AppendLine("disallow:");
      stringBuilder.AppendLine("allow:");
      stringBuilder.Append("sitemap: ");
      stringBuilder.AppendLine(url.TrimEnd('/'));

      return stringBuilder.ToString();
    }
  }
}