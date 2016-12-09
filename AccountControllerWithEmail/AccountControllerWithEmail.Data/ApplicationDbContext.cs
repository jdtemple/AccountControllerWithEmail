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
        : base("ApplicationDbConnection")
    {
      Configuration.LazyLoadingEnabled = false;
      Configuration.ProxyCreationEnabled = false;
    }

    /// <summary>
    /// testing constructor, allows us to supply our own connection string
    /// </summary>
    /// <param name="connStr"></param>
    public ApplicationDbContext(string connStr)
      : base(connStr)
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

    public void RejectChanges()
    {
      foreach (var entry in ChangeTracker.Entries())
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entry.State = EntityState.Detached;
            break;
          case EntityState.Modified:
            entry.CurrentValues.SetValues(entry.OriginalValues);
            entry.State = EntityState.Unchanged;
            break;
          case EntityState.Deleted:
            entry.State = EntityState.Unchanged;
            break;
        }
      }
    }
  }
}
