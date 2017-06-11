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
    [Display(ResourceType = typeof(Resources.Resource), Name = "FirstNameTitle")]
    [DataType(DataType.Text)]
    public string FirstName { get; set; }

    [Required]
    [Display(ResourceType = typeof(Resources.Resource), Name = "LastNameTitle")]
    [DataType(DataType.Text)]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(ResourceType = typeof(Resources.Resource), Name = "PasswordTitle")]
    [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "StringLengthError")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(ResourceType = typeof(Resources.Resource), Name = "ConfirmPasswordTitle")]
    [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordsMatchError")]
    public string ConfirmPassword { get; set; }
  }
}