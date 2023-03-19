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
using System.Globalization;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> PlayerPrefs utilities. </summary>
  /// <remarks>
  /// The variable names are composed internally as follows: {Application.productName}+ValueName, so different
  /// applications will have their variables isolated.
  /// </remarks>
  public static class PlayerPrefsUtils
  {
    /// <summary> Returns true if the given key exists, otherwise returns false. </summary>
    /// <param name="key">Key name</param>
    /// <returns>True or false</returns>
    public static bool HasKey(string key) => PlayerPrefs.HasKey(ProductKey(key));

    /// <summary> Removes the given key. </summary>
    /// <param name="key">Key name</param>
    public static void DeleteKey(string key) => PlayerPrefs.DeleteKey(ProductKey(key));

    /// <summary> Delete all keys. </summary>
    public static void DeleteAll() => PlayerPrefs.DeleteAll();

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static bool GetBool(string key, bool defaultValue = false) => PlayerPrefs.GetInt(ProductKey(key), defaultValue == true ? 1 : 0) == 1;

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetBool(string key, bool value) => PlayerPrefs.SetInt(ProductKey(key), value == true ? 1 : 0);

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static int GetInt(string key, int defaultValue = 0) => PlayerPrefs.GetInt(ProductKey(key), defaultValue);

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetInt(string key, int value) => PlayerPrefs.SetInt(ProductKey(key), value);

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static float GetFloat(string key, float defaultValue = 0.0f) => PlayerPrefs.GetFloat(ProductKey(key), defaultValue);

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetFloat(string key, float value) => PlayerPrefs.SetFloat(ProductKey(key), value);

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static string GetString(string key, string defaultValue = "") => PlayerPrefs.GetString(ProductKey(key), defaultValue);

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetString(string key, string value) => PlayerPrefs.SetString(ProductKey(key), value);
    
    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static DateTime GetDateTime(string key, DateTime defaultValue = default)
    {
      string value = PlayerPrefs.GetString(ProductKey(key), null);

      if (string.IsNullOrEmpty(value) == false && DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) == true)
        return result;

      return defaultValue;
    }

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetDateTime(string key, DateTime value) =>
      PlayerPrefs.SetString(ProductKey(key), value.ToString(CultureInfo.InvariantCulture));

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static Vector2 GetVector2(string key, Vector2 defaultValue = default)
    {
      string value = PlayerPrefs.GetString(ProductKey(key), null);
      if (string.IsNullOrEmpty(value) == false)
      {
        string[] components = value.Split(':');
        if (components.Length == 2)
        {
          float x, y;
          if (float.TryParse(components[0], out x) == true)
          {
            if (float.TryParse(components[1], out y) == true)
              return new Vector2(x, y);
          }
        }
      }

      return defaultValue;
    }

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetVector2(string key, Vector2 value) => PlayerPrefs.SetString(ProductKey(key), $"{value.x}:{value.y}");

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static Vector3 GetVector3(string key, Vector3 defaultValue = default)
    {
      string value = PlayerPrefs.GetString(ProductKey(key), null);
      if (string.IsNullOrEmpty(value) == false)
      {
        string[] components = value.Split(':');
        if (components.Length == 3)
        {
          float x, y, z;
          if (float.TryParse(components[0], out x) == true)
          {
            if (float.TryParse(components[1], out y) == true)
            {
              if (float.TryParse(components[2], out z) == true)
                return new Vector3(x, y, z);
            }
          }
        }
      }

      return defaultValue;
    }

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetVector3(string key, Vector3 value) => PlayerPrefs.SetString(ProductKey(key), $"{value.x}:{value.y}:{value.z}");

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static Vector4 GetVector4(string key, Vector4 defaultValue = default)
    {
      string value = PlayerPrefs.GetString(ProductKey(key), null);
      if (string.IsNullOrEmpty(value) == false)
      {
        string[] components = value.Split(':');
        if (components.Length == 4)
        {
          float x, y, z, w;
          if (float.TryParse(components[0], out x) == true)
          {
            if (float.TryParse(components[1], out y) == true)
            {
              if (float.TryParse(components[2], out z) == true)
              {
                if (float.TryParse(components[3], out w) == true)
                  return new Vector4(x, y, z, w);
              }
            }
          }
        }
      }

      return defaultValue;
    }

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetVector4(string key, Vector4 value) => PlayerPrefs.SetString(ProductKey(key), $"{value.x}:{value.y}:{value.z}:{value.w}");

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static Quaternion GetQuaternion(string key, Quaternion defaultValue = default) =>
      Quaternion.Euler(GetVector3(key, defaultValue.eulerAngles));

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetQuaternion(string key, Quaternion value) => SetVector3(key, value.eulerAngles);

    private static string ProductKey(string key) =>
      string.IsNullOrEmpty(Application.productName) == false ? $"{Application.productName}.{key}" : key;
  }
}
