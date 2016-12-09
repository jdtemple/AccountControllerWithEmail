using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AccountControllerWithEmail.Tests.Helpers
{
  public static class TestExtensions
  {
    private static Random _random = new Random();

    public static T RandomizeProperties<T>(this T obj)
    {
      var modelType = typeof(T);
      var modelProperties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

      foreach (var prop in modelProperties)
      {
        if (prop.CanWrite)
        {
          if (prop.PropertyType == typeof(string))
          {
            var dataTypeAttr = prop.GetCustomAttribute<DataTypeAttribute>();
            if (dataTypeAttr != null)
            {
              switch (dataTypeAttr.DataType)
              {
                case DataType.PhoneNumber:
                  var phoneNum = string.Concat("(", _random.Next(999).ToString(), ")-", _random.Next(999).ToString(), _random.Next(9999).ToString());

                  prop.SetValue(obj, phoneNum);
                  break;
                case DataType.EmailAddress:
                  var emailAddress = string.Concat("test", _random.Next().ToString(), "@mail.com");

                  prop.SetValue(obj, emailAddress);
                  break;
                case DataType.PostalCode:
                  var postalCode = _random
                    .Next(999999999)
                    .ToString()
                    .Insert(5, "-");

                  prop.SetValue(obj, postalCode);
                  break;
              }
            }
            else
            {
              prop.SetValue(obj, Guid.NewGuid().ToString());
            }
          }
          else if (prop.PropertyType == typeof(decimal))
          {
            var nextVal = Math.Round(Convert.ToDecimal(_random.NextDouble()), 2, MidpointRounding.AwayFromZero);

            prop.SetValue(obj, nextVal);
          }
          else if (prop.PropertyType == typeof(decimal?))
          {
            var nextVal = (decimal?)Math.Round(Convert.ToDecimal(_random.NextDouble()), 2, MidpointRounding.AwayFromZero);

            prop.SetValue(obj, nextVal);
          }
          else if (prop.PropertyType == typeof(double))
          {
            prop.SetValue(obj, _random.NextDouble() * double.MaxValue);
          }
          else if (prop.PropertyType == typeof(int))
          {
            if (!prop.Name.EndsWith("Id"))
            {
              //check for a range attribute and set the value accordingly
              var rangeAttributes = prop.GetCustomAttributes(typeof(RangeAttribute), false) as RangeAttribute[];

              if (rangeAttributes != null && rangeAttributes.Length > 0)
              {
                prop.SetValue(obj, _random.Next((int)rangeAttributes[0].Minimum, (int)rangeAttributes[0].Maximum));
              }
              else
              {
                prop.SetValue(obj, _random.Next(int.MaxValue));
              }
            }
          }
          else if (prop.PropertyType == typeof(Guid))
          {
            prop.SetValue(obj, Guid.NewGuid().ToString());
          }
          else if (prop.PropertyType == typeof(DateTime))
          {
            var start = new DateTime(1753, 1, 2);
            int range = (DateTime.Today - start).Days;

            var randDay = start.AddDays(_random.Next(range));

            prop.SetValue(obj, randDay);
          }
        }
      }

      return obj;
    }
  }
}
