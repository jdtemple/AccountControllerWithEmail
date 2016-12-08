using System.Collections.Generic;
using System.Web.Mvc;

namespace AccountControllerWithEmail.Data.ViewModels.Account
{
  public class ConfigureTwoFactorViewModel
  {
    public string SelectedProvider { get; set; }
    public ICollection<SelectListItem> Providers { get; set; }
  }
}
