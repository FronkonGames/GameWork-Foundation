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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Reflection extensions.
  /// </summary>
  public static class ReflectionExtensions
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="attributeName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetAttribute<T>(this object self, string attributeName) where T : Attribute
    {
      T attribute = null;

      const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

      List<FieldInfo> fieldInfos = new List<FieldInfo>();
      Type type = self.GetType();

      do
        fieldInfos.AddRange(type.GetFields(bindingFlags));
      while ((type = type.BaseType) != null && type != typeof(MonoBehaviour));

      for (int i = 0; i < fieldInfos.Count && attribute == null; ++i)
      {
        if (fieldInfos[i].Name.Equals(attributeName) == true)
          attribute = Attribute.GetCustomAttribute(fieldInfos[i], typeof(T)) as T;
      }

      if (attribute == null)
        Log.Warning($"Attribute '{attributeName}' not found.");

      return attribute;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="attributeName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool HasAttribute<T>(this object self) where T : Attribute
    {
      FieldInfo[] fields = self.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
      for (int i = 0; i < fields.Length; ++i)
      {
        FieldInfo field = fields[i];
        if (field.GetCustomAttributes(typeof(T), true).Length > 0)
          return true;
      }

      return false;
    }

    public static T[] GetAttributes<T>(this object self) where T : Attribute
    {
      List<T> attributes = new List<T>();

      FieldInfo[] fields = self.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
      for (int i = 0; i < fields.Length; ++i)
      {
        object[] objs = fields[i].GetCustomAttributes(typeof(T), true);
        for (int j = 0; j < objs.Length; ++j)
          attributes.Add(objs[j] as T);
      }

      return attributes.ToArray();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="propertyName"></param>
    /// <param name="attribute"></param>
    /// <param name="propertyInfo"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool GetProperty<T>(this object self, string propertyName, out T attribute, out PropertyInfo propertyInfo) where T : PropertyAttribute
    {
      attribute = null;
      propertyInfo = null;

      PropertyInfo[] properties = self.GetType().GetProperties();
      for (int i = 0; i < properties.Length && attribute == null; ++i)
      {
        if (properties[i].Name.Equals(propertyName) == true)
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
  }
}