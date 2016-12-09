using AccountControllerWithEmail.Data;
using AccountControllerWithEmail.Data.Models.Admin;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace AccountControllerWithEmail.Tests.Helpers
{
  public class RepositoryHelper : IDisposable
  {
    public ApplicationDbContext Context { get; private set; }

    private DbContextTransaction _transaction;

    public RepositoryHelper()
    {
      Context = new ApplicationDbContext("name=ApplicationDbTestConnection");

      _transaction = Context.Database.BeginTransaction();
    }

    internal List<EmailLog> SeedEmailLogs(int count = 1)
    {
      var emailLogs = new List<EmailLog>(count);

      for (int i = 0; i < count; i++)
      {
        emailLogs.Add(new EmailLog().RandomizeProperties());
      }

      try
      {
        Context.EmailLogs.AddRange(emailLogs);
        Context.SaveChanges();

        return emailLogs;
      }
      catch (Exception)
      {
        throw;
      }
    }

    internal TException Catch<TException>(Action action) where TException : Exception
    {
      try
      {
        action();

        return null;
      }
      catch (TException ex)
      {
        return ex;
      }
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (_transaction != null)
        {
          //roll back the transaction, so the database is clean for the next test run
          _transaction.Rollback();
          _transaction.Dispose();
        }

        if (Context != null)
        {
          Context.Dispose();
        }
      }
    }
  }
}
