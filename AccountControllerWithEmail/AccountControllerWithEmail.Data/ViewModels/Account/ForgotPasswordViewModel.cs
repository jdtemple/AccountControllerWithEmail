using System.ComponentModel.DataAnnotations;

namespace AccountControllerWithEmail.Data.ViewModels.Account
{
  public class ForgotPasswordViewModel
  {
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
  }
}
