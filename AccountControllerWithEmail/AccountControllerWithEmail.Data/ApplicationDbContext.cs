using AccountControllerWithEmail.Data.Conventions;
using AccountControllerWithEmail.Data.Models.Admin;
using AccountControllerWithEmail.Data.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AccountControllerWithEmail.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext()
        : base("ApplicationDbConnection", throwIfV1Schema: false)
    {
      Configuration.LazyLoadingEnabled = false;
      Configuration.ProxyCreationEnabled = false;
    }

    public static ApplicationDbContext Create()
    {
      return new ApplicationDbContext();
    }

    //Add your DbSets here in alphabetical order, using a region structure that matches the project directory structure.
    #region Admin
    public virtual DbSet<EmailLog> EmailLogs { get; set; }
    #endregion //Admin

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      //Conventions
      //DateTime Convention, setting precision to 0
      modelBuilder.Conventions.Add<DateTime2Convention>();
    }
  }
}
