using System.ComponentModel.DataAnnotations;

namespace AccountControllerWithEmail.Data.Models.Base
{
  public abstract class Entity
  {
    [Key]
    public virtual int Id { get; set; }
  }
}
