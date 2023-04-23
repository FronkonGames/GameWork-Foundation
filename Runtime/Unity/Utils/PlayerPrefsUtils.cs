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
  /// The variable names are composed internally as follows: {Application.productName.}+ValueName, so different
  /// applications will have their variables isolated.
  /// </remarks>
  public static class PlayerPrefsUtils
  {
    /// <summary> Returns true if the given key exists, otherwise returns false. </summary>
    /// <param name="key">Key name</param>
    /// <returns>True or false</returns>
    public static bool HasKey(string key) => PlayerPrefs.HasKey(Prefix(key));

    /// <summary> Removes the given key. </summary>
    /// <param name="key">Key name</param>
    public static void DeleteKey(string key) => PlayerPrefs.DeleteKey(Prefix(key));

    /// <summary> Delete all keys. </summary>
    public static void DeleteAll() => PlayerPrefs.DeleteAll();

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static bool GetBool(string key, bool defaultValue = false) => PlayerPrefs.GetInt(Prefix(key), defaultValue == true ? 1 : 0) == 1;

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetBool(string key, bool value) => PlayerPrefs.SetInt(Prefix(key), value == true ? 1 : 0);

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static int GetInt(string key, int defaultValue = 0) => PlayerPrefs.GetInt(Prefix(key), defaultValue);

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetInt(string key, int value) => PlayerPrefs.SetInt(Prefix(key), value);

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static float GetFloat(string key, float defaultValue = 0.0f) => PlayerPrefs.GetFloat(Prefix(key), defaultValue);

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetFloat(string key, float value) => PlayerPrefs.SetFloat(Prefix(key), value);

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static string GetString(string key, string defaultValue = "") => PlayerPrefs.GetString(Prefix(key), defaultValue);

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetString(string key, string value) => PlayerPrefs.SetString(Prefix(key), value);
    
    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static DateTime GetDateTime(string key, DateTime defaultValue = default)
    {
      string value = PlayerPrefs.GetString(Prefix(key), null);

      if (string.IsNullOrEmpty(value) == false && DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) == true)
        return result;

      return defaultValue;
    }

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetDateTime(string key, DateTime value) =>
      PlayerPrefs.SetString(Prefix(key), value.ToString(CultureInfo.InvariantCulture));

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static Vector2 GetVector2(string key, Vector2 defaultValue = default)
    {
      string value = PlayerPrefs.GetString(Prefix(key), null);
      if (string.IsNullOrEmpty(value) == false)
        return value.ToVector2();

      return defaultValue;
    }

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetVector2(string key, Vector2 value) => PlayerPrefs.SetString(Prefix(key), Vector2Extensions.ToString(value));

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static Vector3 GetVector3(string key, Vector3 defaultValue = default)
    {
      string value = PlayerPrefs.GetString(Prefix(key), null);
      if (string.IsNullOrEmpty(value) == false)
        return value.ToVector3();

      return defaultValue;
    }

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetVector3(string key, Vector3 value) => PlayerPrefs.SetString(Prefix(key), Vector3Extensions.ToString(value));

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static Vector4 GetVector4(string key, Vector4 defaultValue = default)
    {
      string value = PlayerPrefs.GetString(Prefix(key), null);
      if (string.IsNullOrEmpty(value) == false)
        return value.ToVector4();

      return defaultValue;
    }

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetVector4(string key, Vector4 value) => PlayerPrefs.SetString(Prefix(key), Vector4Extensions.ToString(value));

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

    /// <summary> Returns the value of a PlayerPrefs variable or its default value. </summary>
    /// <param name="key">Key name</param>
    /// <param name="defaultValue"> Value if the variable is not found. </param>
    /// <returns>Value or default</returns>
    public static Color GetColor(string key, Color defaultValue = default)
    {
      string value = PlayerPrefs.GetString(Prefix(key), null);
      if (string.IsNullOrEmpty(value) == false)
        return value.ToColor();

      return defaultValue;
    }

    /// <summary> Sets the value of a variable. </summary>
    /// <param name="key">Key name</param>
    /// <param name="value">Value</param>
    public static void SetColor(string key, Color value) => PlayerPrefs.SetString(Prefix(key), ColorExtensions.ToString(value));

    private static string Prefix(string key) =>
      string.IsNullOrEmpty(Application.productName) == false ? $"{Application.productName}.{key}" : key;
  }
}
