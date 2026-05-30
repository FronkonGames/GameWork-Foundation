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
using System.Runtime.CompilerServices;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> HashSet extensions. </summary>
  public static class HashSetExtensions
  {
    /// <summary> Returns a random element from the HashSet. </summary>
    /// <param name="self"> The HashSet. </param>
    /// <typeparam name="T"> Element type. </typeparam>
    /// <returns> A random element. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Random<T>(this HashSet<T> self)
    {
      if (self == null || self.Count == 0)
        return default;

      int index = Rand.Range(0, self.Count);
      int current = 0;
      foreach (T item in self)
      {
        if (current == index)
          return item;

        current++;
      }

      return default;
    }

    /// <summary> Returns true if the HashSet is null or empty. </summary>
    /// <param name="self"> The HashSet. </param>
    /// <typeparam name="T"> Element type. </typeparam>
    /// <returns> True if null or empty. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEmpty<T>(this HashSet<T> self) => self == null || self.Count == 0;

    /// <summary> Returns a shallow clone of the HashSet. </summary>
    /// <param name="self"> The HashSet. </param>
    /// <typeparam name="T"> Element type. </typeparam>
    /// <returns> A new HashSet with the same elements. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static HashSet<T> Clone<T>(this HashSet<T> self) => self != null ? new HashSet<T>(self) : new HashSet<T>();

    /// <summary> Tries to get a random element from the HashSet. </summary>
    /// <param name="self"> The HashSet. </param>
    /// <param name="value"> The random element. </param>
    /// <typeparam name="T"> Element type. </typeparam>
    /// <returns> True if a random element was found. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetRandom<T>(this HashSet<T> self, out T value)
    {
      if (self != null && self.Count > 0)
      {
        value = self.Random();
        return true;
      }

      value = default;
      return false;
    }

    /// <summary> Finds a random element matching the predicate using reservoir sampling. </summary>
    /// <param name="self"> The HashSet. </param>
    /// <param name="match"> The predicate to match. </param>
    /// <typeparam name="T"> Element type. </typeparam>
    /// <returns> A random matching element, or default if none found. </returns>
    public static T FindRandom<T>(this HashSet<T> self, Predicate<T> match)
    {
      if (self == null || self.Count == 0)
        return default;

      T result = default;
      int count = 0;
      foreach (T item in self)
      {
        if (match(item))
        {
          count++;
          if (Rand.Range(0, count) == 0)
            result = item;
        }
      }

      return count > 0 ? result : default;
    }

    /// <summary> Returns true if any element matches the predicate. </summary>
    /// <param name="self"> The HashSet. </param>
    /// <param name="match"> The predicate to match. </param>
    /// <typeparam name="T"> Element type. </typeparam>
    /// <returns> True if any element matches. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains<T>(this HashSet<T> self, Predicate<T> match)
    {
      if (self == null || self.Count == 0)
        return false;

      foreach (T item in self)
      {
        if (match(item))
          return true;
      }

      return false;
    }

    /// <summary> Removes all elements matching the predicate. </summary>
    /// <param name="self"> The HashSet. </param>
    /// <param name="match"> The predicate to match. </param>
    /// <typeparam name="T"> Element type. </typeparam>
    public static void RemoveWhere<T>(this HashSet<T> self, Predicate<T> match)
    {
      if (self == null || self.Count == 0)
        return;

      self.RemoveWhere(match);
    }

    /// <summary> Shuffles the HashSet in-place. </summary>
    /// <param name="self"> The HashSet. </param>
    /// <typeparam name="T"> Element type. </typeparam>
    /// <returns> The same HashSet for chaining. </returns>
    public static HashSet<T> Shuffle<T>(this HashSet<T> self)
    {
      if (self == null || self.Count <= 1)
        return self;

      List<T> list = new List<T>(self);
      int n = list.Count;
      while (n > 1)
      {
        n--;
        int k = Rand.Range(0, n + 1);
        (list[k], list[n]) = (list[n], list[k]);
      }

      self.Clear();
      foreach (T item in list)
        self.Add(item);

      return self;
    }

    /// <summary> Returns a new shuffled HashSet. </summary>
    /// <param name="self"> The HashSet. </param>
    /// <typeparam name="T"> Element type. </typeparam>
    /// <returns> A new shuffled HashSet. </returns>
    public static HashSet<T> ShuffleNew<T>(this HashSet<T> self)
    {
      HashSet<T> clone = self.Clone();
      return clone.Shuffle();
    }

    /// <summary> Adds multiple elements to the HashSet. </summary>
    /// <param name="self"> The HashSet. </param>
    /// <param name="items"> Items to add. </param>
    /// <typeparam name="T"> Element type. </typeparam>
    public static void AddRange<T>(this HashSet<T> self, IEnumerable<T> items)
    {
      if (self == null || items == null)
        return;

      foreach (T item in items)
        self.Add(item);
    }
  }
}
