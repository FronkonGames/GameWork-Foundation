////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Martin Bustos @FronkonGames <fronkongames@gmail.com>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Reflection extensions. </summary>
  public static class ReflectionExtensions
  {
    /// <summary> Returns an attribute or null. </summary>
    /// <param name="self"> The object. </param>
    /// <returns> Attribute or null. </returns>
    public static T GetAttribute<T>(this FieldInfo self, bool inherit = true) where T : Attribute
    {
      object[] objects = self.GetCustomAttributes(typeof(T), inherit);

      if (objects.Length == 0)
        Log.Warning($"Attribute '{typeof(T).Name}' not found");

      return objects.Length > 0 ? objects[0] as T: null;
    }

    /// <summary> Does it have the attribute? </summary>
    /// <returns> True or false. </returns>
    public static bool HasAttribute<T>(this FieldInfo self, bool inherit = true) where T : Attribute => self.GetCustomAttributes(typeof(T), inherit).Length > 0;

    /// <summary> Returns property or null. </summary>
    /// <param name="self"> The object. </param>
    /// <param name="propertyName"> Property name. </param>
    /// <param name="attribute"> Attribute or null. </param>
    /// <param name="propertyInfo"> Property info. </param>
    /// <returns> True or false. </returns>
    public static bool GetProperty<T>(this object self, string propertyName, out T attribute, out PropertyInfo propertyInfo) where T : PropertyAttribute
    {
      attribute = null;
      propertyInfo = null;

      PropertyInfo[] properties = self.GetType().GetProperties();
      for (int i = 0; i < properties.Length && attribute == null; ++i)
      {
        if (properties[i].Name == propertyName)
        {
          object[] attributes = properties[i].GetCustomAttributes(true);
          for (int j = 0; j < attributes.Length; ++j)
          {
            attribute = attributes[j] as T;
            propertyInfo = properties[i];
          }
        }
      }

      return attribute != null && propertyInfo != null;
    }

    /// <summary> Returns private property by name or null. </summary>
    /// <param name="self"> The object. </param>
    /// <param name="propertyName"> Property name. </param>
    /// <returns> Property or null. </returns>
    public static PropertyInfo GetProperty(this object self, string propertyName) => self.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);

    /// <summary> Returns private field by name or null. </summary>
    /// <param name="self"> The object. </param>
    /// <param name="fieldName"> Field name. </param>
    /// <returns> Field or null. </returns>
    public static FieldInfo GetField(this object self, string fieldName) => self.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
  }
}