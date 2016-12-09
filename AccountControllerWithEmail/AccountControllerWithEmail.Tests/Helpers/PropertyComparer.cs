using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AccountControllerWithEmail.Tests.Helpers
{
  public class PropertyComparer<T> : IEqualityComparer<T>, IComparer
  {
    public bool Equals(T x, T y)
    {
      var typeToCompare = typeof(T);
      var propertiesToCompare = typeToCompare.GetProperties().Where(p => {
        var isGen = p.PropertyType.IsGenericType;

        if (isGen)
        {
          var genType = p.PropertyType.GetGenericTypeDefinition();
          if (genType == typeof(ICollection<>))
          {
            return false;
          }
          else return true;
        }
        else return true;
      });
      var fieldsToCompare = typeToCompare.GetFields();

      bool equal = true;

      foreach (var prop in propertiesToCompare)
      {
        if (prop.CanRead)
        {
          var xVal = prop.GetValue(x);
          var yVal = prop.GetValue(y);

          if (xVal == null || yVal == null)
            equal &= xVal == null && yVal == null;
          else if (prop.PropertyType == typeof(DateTime))
            equal &= ((DateTime)xVal).Subtract((DateTime)yVal).TotalSeconds < 1.0;
          else
            equal &= xVal.Equals(yVal);
        }
      }

      foreach (var field in fieldsToCompare)
      {
        var xVal = field.GetValue(x);
        var yVal = field.GetValue(y);

        if (xVal == null || yVal == null)
          equal &= xVal == null && yVal == null;
        else
          equal &= xVal.Equals(yVal);
      }

      return equal;
    }

    public int GetHashCode(T obj)
    {
      return obj.GetHashCode();
    }

    public int Compare(object x, object y)
    {
      if (this.Equals((T)x, (T)y))
        return 0;
      else
        return ((T)x).GetHashCode() - ((T)y).GetHashCode();
    }
  }
}
