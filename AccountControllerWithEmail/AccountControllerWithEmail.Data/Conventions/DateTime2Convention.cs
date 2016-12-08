using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AccountControllerWithEmail.Data.Conventions
{
  public class DateTime2Convention : Convention
  {
    public DateTime2Convention()
    {
      Properties<DateTime>().Configure(x =>
      {
        x.HasColumnType("datetime2").HasPrecision(0);
      });
    }
  }
}
