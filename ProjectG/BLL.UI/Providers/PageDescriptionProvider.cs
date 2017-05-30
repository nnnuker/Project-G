namespace BLL.UI.Providers
{
  public class PageDescriptionProvider
  {
    public string GetValue(string page, params string[] tags)
    {
      string result = string.Empty;

      foreach (var tag in tags)
      {
        var startTag = "<" + tag + ">";
        int startIndex = page.IndexOf(startTag) + startTag.Length;
        int endIndex = page.IndexOf("</" + tag + ">", startIndex);
        result = page.Substring(startIndex, endIndex - startIndex);

        if (!string.IsNullOrEmpty(result))
        {
          return result;
        }
      }

      return "Default description";
    }
  }
}