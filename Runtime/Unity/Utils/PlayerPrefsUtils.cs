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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// PlayerPrefs utilities.
  /// </summary>
  public static class PlayerPrefsUtils
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static bool GetBool(string key, bool defaultValue = false) => PlayerPrefs.GetInt(ProductKey(key), defaultValue == true ? 1 : 0) == 1;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetBool(string key, bool value) => PlayerPrefs.SetInt(ProductKey(key), value == true ? 1 : 0);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int GetInt(string key, int defaultValue = 0) => PlayerPrefs.GetInt(ProductKey(key), defaultValue);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetInt(string key, int value) => PlayerPrefs.SetInt(ProductKey(key), value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static float GetFloat(string key, float defaultValue = 0.0f) => PlayerPrefs.GetFloat(ProductKey(key), defaultValue);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetFloat(string key, float value) => PlayerPrefs.SetFloat(ProductKey(key), value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static DateTime GetDateTime(string key, DateTime defaultValue = default(DateTime))
    {
      string value = PlayerPrefs.GetString(ProductKey(key), null);
      if (string.IsNullOrEmpty(value) == false)
      {
        DateTime result;
        if (DateTime.TryParse(value, out result))
          return result;
      }

      return defaultValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetDateTime(string key, DateTime value) => PlayerPrefs.SetString(ProductKey(key), value.ToString(System.Globalization.CultureInfo.InvariantCulture));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Vector2 GetVector2(string key, Vector2 defaultValue = default(Vector2))
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetVector2(string key, Vector2 value) => PlayerPrefs.SetString(ProductKey(key), $"{value.x}:{value.y}");

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Vector3 GetVector3(string key, Vector3 defaultValue = default(Vector3))
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetVector3(string key, Vector3 value) => PlayerPrefs.SetString(ProductKey(key), $"{value.x}:{value.y}:{value.z}");

    private static string ProductKey(string key) =>
      string.IsNullOrEmpty(Application.productName) == false ? $"{Application.productName}.{key}" : key;
  }
}
