using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
  public class RegisterViewModel
  {
    [Required]
    [Display(ResourceType = typeof(Resources.Resource), Name = "EmailTitle")]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(ResourceType = typeof(Resources.Resource), Name = "PasswordTitle")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }
}