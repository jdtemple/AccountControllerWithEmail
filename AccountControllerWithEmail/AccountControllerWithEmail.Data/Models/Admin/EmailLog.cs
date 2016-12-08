using AccountControllerWithEmail.Data.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace AccountControllerWithEmail.Data.Models.Admin
{
  public class EmailLog : Entity
  {
    /// <summary>
    /// clickable url contained in the email
    /// </summary>
    public string CallbackUrl { get; set; }

    /// <summary>
    /// sender
    /// </summary>
    [Required]
    public string From { get; set; }

    /// <summary>
    /// date and time the email was sent
    /// </summary>
    public DateTime Sent { get; set; }

    /// <summary>
    /// email subject
    /// </summary>
    [Required]
    public string Subject { get; set; }

    /// <summary>
    /// csv list of recipients
    /// </summary>
    [Required]
    public string To { get; set; }
    
    /// <summary>
    /// name of the originating view model
    /// </summary>
    public string ViewModel { get; set; }
  }
}
