namespace BLL.UI.Models
{
  public class BllCategory
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int ParentId { get; set; }
    public bool HasChildCategory { get; internal set; }
    public int PagesCount { get; internal set; }

    public BllCategory()
    {
    }

    public BllCategory(int pagesCount, bool hasChild)
    {
      HasChildCategory = hasChild;
      PagesCount = PagesCount;
    }
  }
}