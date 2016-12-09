[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AccountControllerWithEmail.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(AccountControllerWithEmail.App_Start.NinjectWebCommon), "Stop")]

namespace AccountControllerWithEmail.App_Start
{
  using Microsoft.Web.Infrastructure.DynamicModuleHelper;
  using Ninject;
  using Ninject.Extensions.Conventions;
  using Ninject.Web.Common;
  using System;
  using System.Web;

  public static class NinjectWebCommon
  {
    private static readonly Bootstrapper bootstrapper = new Bootstrapper();

    /// <summary>
    /// Starts the application
    /// </summary>
    public static void Start()
    {
      DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
      DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
      bootstrapper.Initialize(CreateKernel);
    }

    /// <summary>
    /// Stops the application.
    /// </summary>
    public static void Stop()
    {
      bootstrapper.ShutDown();
    }

    /// <summary>
    /// Creates the kernel that will manage your application.
    /// </summary>
    /// <returns>The created kernel.</returns>
    private static IKernel CreateKernel()
    {
      var kernel = new StandardKernel();
      try
      {
        kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
        kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

        //bind all assemblies that follow the IClassName/ClassName interface implementation convention
        kernel.Bind(x => x
          .FromAssembliesMatching("*")
          .SelectAllClasses()
          .BindDefaultInterface());

        //bind specific interface/class
        //kernel.Bind<IStoryRepository>().To<StoryRepository>();

        RegisterServices(kernel);
        return kernel;
      }
      catch
      {
        kernel.Dispose();
        throw;
      }
    }

    /// <summary>
    /// Load your modules or register your services here!
    /// </summary>
    /// <param name="kernel">The kernel.</param>
    private static void RegisterServices(IKernel kernel)
    {
    }
  }
}
