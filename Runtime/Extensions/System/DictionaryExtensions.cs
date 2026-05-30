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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Dictionary extensions. </summary>
  public static class DictionaryExtensions
  {
    /// <summary> Is the dictionary null or empty? </summary>
    public static bool IsEmptyOrNull<TKey, TValue>(this Dictionary<TKey, TValue> self) => self == null || self.Count == 0;

    /// <summary> Is the dictionary empty? </summary>
    public static bool IsEmpty<TKey, TValue>(this Dictionary<TKey, TValue> self) => self.Count == 0;

    /// <summary> Get a random key from the dictionary. </summary>
    public static TKey RandomKey<TKey, TValue>(this Dictionary<TKey, TValue> self)
    {
      if (self.Count == 0)
        throw new InvalidOperationException("Dictionary is empty.");

      int index = UnityEngine.Random.Range(0, self.Count);
      foreach (TKey key in self.Keys)
      {
        if (index == 0)
          return key;
        index--;
      }

      return default;
    }

    /// <summary> Try to get the first key that matches the given value. </summary>
    public static bool TryGetFirstKeyByValue<TKey, TValue>(this Dictionary<TKey, TValue> self, TValue value, out TKey key)
    {
      foreach (KeyValuePair<TKey, TValue> pair in self)
      {
        if (EqualityComparer<TValue>.Default.Equals(pair.Value, value))
        {
          key = pair.Key;
          return true;
        }
      }

      key = default;
      return false;
    }

    /// <summary> Get the first key that matches the given value, or the default value if not found. </summary>
    public static TKey GetKeyByValue<TKey, TValue>(this Dictionary<TKey, TValue> self, TValue value, TKey defaultValue = default)
    {
      if (self.TryGetFirstKeyByValue(value, out TKey key))
        return key;

      return defaultValue;
    }

    /// <summary> Get all keys that match the given value. </summary>
    public static IEnumerable<TKey> GetKeysByValue<TKey, TValue>(this Dictionary<TKey, TValue> self, TValue value)
    {
      List<TKey> keys = new();
      foreach (KeyValuePair<TKey, TValue> pair in self)
      {
        if (EqualityComparer<TValue>.Default.Equals(pair.Value, value))
          keys.Add(pair.Key);
      }
      return keys;
    }

    /// <summary> Swap the values of two keys. </summary>
    public static void SwapValues<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key1, TKey key2)
    {
      if (self.TryGetValue(key1, out TValue value1) && self.TryGetValue(key2, out TValue value2))
      {
        self[key1] = value2;
        self[key2] = value1;
      }
    }
  }
}
