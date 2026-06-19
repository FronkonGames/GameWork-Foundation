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
using System.Linq;

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

    /// <summary> Creates a new dictionary by transforming each value. </summary>
    /// <typeparam name="TKey"> Key type. </typeparam>
    /// <typeparam name="TIn"> Input value type. </typeparam>
    /// <typeparam name="TOut"> Output value type. </typeparam>
    /// <param name="source"> Source dictionary. </param>
    /// <param name="valueSelector"> Value transform. </param>
    /// <returns> Transformed dictionary. </returns>
    public static Dictionary<TKey, TOut> SelectDictionary<TKey, TIn, TOut>(this IDictionary<TKey, TIn> source, Func<TIn, TOut> valueSelector)
    {
      return source.ToDictionary(pair => pair.Key, pair => valueSelector(pair.Value));
    }

    /// <summary> Normalizes float dictionary values so they sum to 1.0f. </summary>
    /// <typeparam name="TKey"> Key type. </typeparam>
    /// <param name="source"> Source dictionary. </param>
    /// <returns> Normalized dictionary. </returns>
    public static Dictionary<TKey, float> Normalize<TKey>(this IDictionary<TKey, float> source)
    {
      float sum = source.Values.Sum();

      if (sum == 0.0f)
        throw new InvalidOperationException("Cannot normalize a dictionary whose values sum to zero.");

      return source.SelectDictionary(value => value / sum);
    }

    /// <summary> Sums values across multiple float dictionaries by key. </summary>
    /// <typeparam name="TKey"> Key type. </typeparam>
    /// <param name="source"> Dictionaries to merge. </param>
    /// <returns> Combined dictionary. </returns>
    public static Dictionary<TKey, float> SumTogether<TKey>(this IEnumerable<IDictionary<TKey, float>> source)
    {
      Dictionary<TKey, float> result = new();

      foreach (IDictionary<TKey, float> dictionary in source)
      {
        foreach (KeyValuePair<TKey, float> pair in dictionary)
        {
          if (result.ContainsKey(pair.Key) == false)
            result[pair.Key] = 0.0f;

          result[pair.Key] += pair.Value;
        }
      }

      return result;
    }
  }
}
