using System.ComponentModel.DataAnnotations;

namespace AccountControllerWithEmail.Data.ViewModels.Account
{
  public class ExternalLoginConfirmationViewModel
  {
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
  }
}
