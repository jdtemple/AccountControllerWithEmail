using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;

namespace AccountControllerWithEmail.Data.ViewModels.Account
{
  public class ManageLoginsViewModel
  {
    public IList<UserLoginInfo> CurrentLogins { get; set; }
    public IList<AuthenticationDescription> OtherLogins { get; set; }
  }
}
