using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
  public class LoginViewModel
  {
    [Required]
    [Display(ResourceType = typeof(Resources.Resource), Name = "EmailTitle")]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(ResourceType = typeof(Resources.Resource), Name = "PasswordTitle")]
    public string Password { get; set; }

    [Display(ResourceType = typeof(Resources.Resource), Name = "RememberMeTitle")]
    public bool RememberMe { get; set; }
  }
}