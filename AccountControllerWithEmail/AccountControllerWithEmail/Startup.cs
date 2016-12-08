using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AccountControllerWithEmail.Startup))]
namespace AccountControllerWithEmail
{
  public partial class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      ConfigureAuth(app);
    }
  }
}
