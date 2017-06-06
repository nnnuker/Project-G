using System.ComponentModel.DataAnnotations;

namespace UI.Models.Categories
{
  public class CategoryViewModel
  {
    public int Id { get; set; }
    public int ParentId { get; set; }

    [Required]
    public string Name { get; set; }
    public bool HasChilds { get; set; }
    public int ChildCount { get; set; }
  }
}