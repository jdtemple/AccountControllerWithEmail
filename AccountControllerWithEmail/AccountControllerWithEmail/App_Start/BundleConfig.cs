using System.Web.Optimization;

namespace AccountControllerWithEmail
{
  public class BundleConfig
  {
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
    {
      //manually enable minification
      //running in debug sets this to false and release sets it to true
      //you don't have to manually set this unless you're troubleshooting something
      //or if you want to publish minified code in debug mode for dev deployments
      BundleTable.EnableOptimizations = true;

      #region Shared
      bundles.Add(new StyleBundle("~/content/layout")
        .Include("~/Content/bootstrap.css")
        .Include("~/Content/site.css")
        );

      // Use the development version of Modernizr to develop with and learn from. Then, when you're
      // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
      bundles.Add(new ScriptBundle("~/scripts/layout")
        .Include("~/Scripts/modernizr-*")
        .Include("~/Scripts/jquery-{version}.js")
        .Include("~/Scripts/jquery.validate*")
        .Include("~/Scripts/bootstrap*")
        .Include("~/Scripts/respond*")
        );
      #endregion //Shared
    }
  }
}
