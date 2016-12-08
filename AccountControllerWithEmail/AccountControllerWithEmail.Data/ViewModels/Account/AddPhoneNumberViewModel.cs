using System.ComponentModel.DataAnnotations;

namespace AccountControllerWithEmail.Data.ViewModels.Account
{
  public class AddPhoneNumberViewModel
  {
    [Required]
    [Phone]
    [Display(Name = "Phone Number")]
    public string Number { get; set; }
  }
}
