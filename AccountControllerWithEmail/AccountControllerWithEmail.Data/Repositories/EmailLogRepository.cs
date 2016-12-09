using AccountControllerWithEmail.Data.Helpers;
using AccountControllerWithEmail.Data.Interfaces;
using AccountControllerWithEmail.Data.Models.Admin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountControllerWithEmail.Data.Repositories
{
  public class EmailLogRepository : IEmailLogRepository
  {
    protected readonly ApplicationDbContext Context;

    public EmailLogRepository(ApplicationDbContext context)
    {
      Context = context;
    }

    public EmailLog Get(int id)
    {
      if (id == 0)
      {
        ThrowArgumentException("Must be greater than zero.", "id");
      }

      return Context.EmailLogs.Single(x => x.Id == id);
    }

    public async Task<ICollection<EmailLog>> GetAllAsync()
    {
      return await Context.EmailLogs.AsNoTracking().ToListAsync();
    }

    public EmailLog Save(EmailLog entity)
    {
      if (string.IsNullOrEmpty(entity.To))
      {
        ThrowArgumentException("An email must have one or more recipients.", "Recipients");
      }

      var invalidRecipients = new List<string>();

      if (!RegexHelper.IsValidEmailCsv(entity.To, out invalidRecipients))
      {
        var msg = new StringBuilder();

        foreach (var recipient in invalidRecipients)
        {
          msg.AppendFormat("{0},", recipient);
        }

        ThrowArgumentException(string.Format("Email has one or more invalid recipients: {0}", msg.ToString().Substring(0, msg.Length - 1)), "Recipients");
      }

      if (string.IsNullOrEmpty(entity.Subject))
      {
        ThrowArgumentException("An email must have a subject.", "Subject");
      }

      entity.Sent = DateTime.Now;

      Context.EmailLogs.Add(entity);

      Context.SaveChanges();

      return entity;
    }

    private void ThrowArgumentException(string message, string paramName)
    {
      Context.RejectChanges();

      throw new ArgumentException(message, paramName);
    }
  }
}
