using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AccountControllerWithEmail.Data.Helpers
{
  public static class RegexHelper
  {
    //email validation source
    //https://msdn.microsoft.com/en-us/library/01escwtf(v=vs.110).aspx

    #region Public Methods
    public static bool IsValidSsn(string ssn)
    {
      return Regex.IsMatch(ssn, @"^\d{3}-\d{2}-\d{4}$");
    }

    public static bool IsValidEin(string ein)
    {
      return Regex.IsMatch(ein, @"^\d{2}-\d{7}$");
    }

    /// <summary>
    /// Validates a csv list of email addresses. Returns true if they're all valid. Puts the invalid ones in invalidEmails.
    /// </summary>
    /// <param name="emailCsv"></param>
    /// <param name="invalidEmails"></param>
    /// <returns></returns>
    public static bool IsValidEmailCsv(string emailCsv, out List<string> invalidEmails)
    {
      var emails = emailCsv.Split(',').ToList();
      var allValid = true;

      invalidEmails = new List<string>();

      foreach (var email in emails)
      {
        if (!IsValidEmail(email))
        {
          invalidEmails.Add(email);

          allValid = false;
        }
      }

      return allValid;
    }

    /// <summary>
    /// Validates an email address. Returns true if it's valid.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool IsValidEmail(string email)
    {
      if (string.IsNullOrEmpty(email))
      {
        return false;
      }

      //check the domain name
      try
      {
        email = Regex.Replace(email,
          @"(@)(.+)$",
          DomainMapper,
          RegexOptions.None,
          TimeSpan.FromMilliseconds(200));
      }
      catch (Exception ex)
      {
        if (ex is ArgumentException || ex is RegexMatchTimeoutException)
        {
          return false;
        }

        throw;
      }

      //check the email format
      try
      {
        return Regex.IsMatch(email,
          @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
          RegexOptions.IgnoreCase,
          TimeSpan.FromMilliseconds(250));
      }
      catch (Exception ex)
      {
        if (ex is RegexMatchTimeoutException)
        {
          return false;
        }

        throw;
      }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// translate unicode characters that are outside the US-ASCII range to Punycode, throws exception if any invalid characters are detected
    /// </summary>
    /// <param name="match"></param>
    /// <returns></returns>
    private static string DomainMapper(Match match)
    {
      var idn = new IdnMapping();
      var domainName = match.Groups[2].Value;

      try
      {
        domainName = idn.GetAscii(domainName);
      }
      catch (ArgumentException)
      {
        throw;
      }

      return match.Groups[1].Value + domainName;
    }
    #endregion
  }
}
