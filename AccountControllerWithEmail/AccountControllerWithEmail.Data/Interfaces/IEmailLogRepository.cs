using AccountControllerWithEmail.Data.Models.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountControllerWithEmail.Data.Interfaces
{
  public interface IEmailLogRepository
  {
    EmailLog Get(int id);

    EmailLog Save(EmailLog entity);

    Task<ICollection<EmailLog>> GetAllAsync();
  }
}
