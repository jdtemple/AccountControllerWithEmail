using AccountControllerWithEmail.Data.Interfaces;
using AccountControllerWithEmail.Data.Models.Admin;
using AccountControllerWithEmail.Data.Repositories;
using AccountControllerWithEmail.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AccountControllerWithEmail.Tests.IntegrationTests
{
  [TestClass]
  public class EmailLogRepositoryTest
  {
    private readonly IEmailLogRepository _emailLogRepo;
    private RepositoryHelper _repoHelper;

    public EmailLogRepositoryTest()
    {
      _repoHelper = new RepositoryHelper();
      _emailLogRepo = new EmailLogRepository(_repoHelper.Context);
    }

    [TestMethod]
    public void EmailLogRepo_Get()
    {
      //seed an entity
      var emailLog = _repoHelper.SeedEmailLogs().First();

      //get the entity from the db
      var emailLogDb = _emailLogRepo.Get(emailLog.Id);

      //check to see if the db matches what we seeded
      var comparer = new PropertyComparer<EmailLog>();

      Assert.IsTrue(comparer.Equals(emailLog, emailLogDb));
    }

    [TestMethod]
    public async Task EmailLogRepo_GetAllAsync()
    {
      //seed some data
      var emailLogs = _repoHelper.SeedEmailLogs(10);

      Assert.AreEqual(10, emailLogs.Count());

      foreach (var el in emailLogs)
      {
        Assert.IsTrue(el.Id > 0);
      }

      //get them from the db
      var emailLogsDb = await _emailLogRepo.GetAllAsync();

      Assert.IsNotNull(emailLogsDb);
      Assert.IsTrue(emailLogsDb.Count() > 0);

      //compare the db with the seed
      var comparer = new PropertyComparer<EmailLog>();

      CollectionAssert.AreEqual(emailLogs, emailLogsDb.ToList(), comparer);
    }

    [TestMethod]
    public void EmailLogRepo_Create()
    {
      var emailLog = new EmailLog().RandomizeProperties();

      //ensure we get an argument exception for having an invalid email address
      var ex = _repoHelper.Catch <ArgumentException>(() => _emailLogRepo.Save(emailLog));

      Assert.IsNotNull(ex);
      Assert.IsTrue(ex.Message.StartsWith("Email has one or more invalid recipients"));

      //give it some valid email recipient
      emailLog.To = "foo@bar.com";

      //save it again
      _emailLogRepo.Save(emailLog);

      Assert.IsNotNull(emailLog);
      Assert.IsTrue(emailLog.Id > 0);
      Assert.IsTrue(!string.IsNullOrEmpty(emailLog.To));
      Assert.IsTrue(!string.IsNullOrEmpty(emailLog.Subject));

      var emailLogDb = _repoHelper.Context.EmailLogs.Single(x => x.Id == emailLog.Id);

      var comparer = new PropertyComparer<EmailLog>();

      Assert.IsTrue(comparer.Equals(emailLog, emailLogDb));
    }

    [TestMethod]
    public void EmailLogRepo_Edit()
    {
      //seed an entity
      var emailLog = _repoHelper.SeedEmailLogs().First();

      //make some modifications
      var oldRecipients = emailLog.To;

      emailLog.To = "foo@bar.com";

      Assert.AreNotEqual(oldRecipients, emailLog.To);

      //save the change
      _emailLogRepo.Save(emailLog);

      //fetch the modified record
      var emailLogDb = _repoHelper.Context.EmailLogs.Single(x => x.Id == emailLog.Id);

      Assert.IsNotNull(emailLogDb);
      Assert.IsTrue(emailLogDb.Id > 0);
      Assert.IsTrue(!string.IsNullOrEmpty(emailLogDb.To));
      Assert.IsTrue(!string.IsNullOrEmpty(emailLogDb.Subject));

      var comparer = new PropertyComparer<EmailLog>();

      Assert.IsTrue(comparer.Equals(emailLog, emailLogDb));
    }
  }
}
