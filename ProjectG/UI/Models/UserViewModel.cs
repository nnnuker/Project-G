using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
  public class UserViewModel
  {
    public int Id { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    public int RoleId { get; set; }
  }
}