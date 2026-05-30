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
  /// <summary> List extensions. </summary>
  public static class ListExtensions
  {
    /// <summary> Returns an unordered list. </summary>
    /// <param name="self"> The list. </param>
    /// <returns> Unordered list. </returns>
    public static T Random<T>(this IList<T> self) => self[Rand.Range(0, self.Count)];

    /// <summary> Swaps a pair of elements. </summary>
    /// <param name="self"> The list. </param>
    /// <param name="i"> First element. </param>
    /// <param name="j"> Second element. </param>
    public static void Swap<T>(this IList<T> self, int i, int j) => (self[i], self[j]) = (self[j], self[i]);

    /// <summary> Eliminate an items range. </summary>
    /// <param name="self"> The list. </param>
    /// <param name="entries">Elements to be removed. </param>
    public static void RemoveRange<T>(this IList<T> self, IEnumerable<T> entries)
    {
      foreach (T item in entries)
        self.Remove(item);
    }

    /// <summary> Is the list null or empty? </summary>
    public static bool IsEmptyOrNull<T>(this IList<T> self) => self == null || self.Count == 0;

    /// <summary> Is the list empty? </summary>
    public static bool IsEmpty<T>(this IList<T> self) => self.Count == 0;

    /// <summary> Remove duplicate entries from the list. </summary>
    public static void RemoveDuplicates<T>(this IList<T> self)
    {
      HashSet<T> seen = new();
      for (int i = self.Count - 1; i >= 0; i--)
      {
        if (seen.Add(self[i]) == false)
          self.RemoveAt(i);
      }
    }

    /// <summary> Reverses a list of items in place. </summary>
    /// <param name="self"> The list. </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Reverse<T>(this IList<T> self) => Reverse(self, 0, self.Count);

    /// <summary> Reverses a list of items. </summary>
    /// <param name="self"> The list. </param>
    /// <param name="from"> From. </param>
    /// <param name="to"> To. </param>
    public static void Reverse<T>(this IList<T> self, int from, int to)
    {
      if (self is T[] array)
      {
        array.Reverse(from, to);
        return;
      }

      while (--to > from)
        self.Swap(from++, to);
    }

    /// <summary> It unordered the list. </summary>
    /// <param name="self"> The list. </param>
    public static void Shuffle<T>(this IList<T> self)
    {
      int n = self.Count - 1;
      while (n > 1)
      {
        n--;
        int k = UnityEngine.Random.Range(0, self.Count);
        (self[k], self[n]) = (self[n], self[k]);
      }
    }

    /// <summary> Returns the maximum value in the list. </summary>
    /// <typeparam name="T">The type of value to check.</typeparam>
    /// <param name="self">The values to check.</param>
    /// <returns>The maximum value in the list.</returns>
    public static T Max<T>(this List<T> self) where T : IComparable<T>
    {
      if (self == null || self.Count == 0)
        return default;

      T max = self[0];
      int total = self.Count;
      for (int i = 1; i < total; ++i)
      {
        T element = self[i];
        if (element.CompareTo(max) > 0)
          max = element;
      }

      return max;
    }

    /// <summary> Returns the minimum value in the list. </summary>
    /// <typeparam name="T">The type of value to check.</typeparam>
    /// <param name="self">The values to check.</param>
    /// <returns>The minimum value in the list.</returns>
    public static T Min<T>(this List<T> self) where T : IComparable<T>
    {
      if (self == null || self.Count == 0)
        return default;

      T min = self[0];
      int total = self.Count;
      for (int i = 1; i < total; ++i)
      {
        T element = self[i];
        if (element.CompareTo(min) < 0)
          min = element;
      }

      return min;
    }

    /// <summary> Removes and returns the first element. </summary>
    /// <param name="self"> The list. </param>
    /// <returns> The removed first element. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Pop<T>(this IList<T> list)
    {
      if (list.Count == 0)
        return default;

      T value = list[0];
      list.RemoveAt(0);
      return value;
    }

    /// <summary> Removes and returns the last element. </summary>
    /// <param name="list"> The list. </param>
    /// <returns> The removed last element. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T PopLast<T>(this IList<T> list)
    {
      int count = list.Count;
      if (count == 0)
        return default;

      int index = count - 1;
      T value = list[index];
      list.RemoveAt(index);
      return value;
    }

    /// <summary> Returns true if the index is valid for this list. </summary>
    /// <param name="list"> The list. </param>
    /// <param name="index"> The index to check. </param>
    /// <returns> True if the index is within range. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasIndex<T>(this IList<T> list, int index) => index >= 0 && index < list.Count;

    /// <summary> Safely gets element at index, returning default(T) if out of range. </summary>
    /// <param name="list"> The list. </param>
    /// <param name="index"> The index. </param>
    /// <returns> The element or default(T). </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Get<T>(this IList<T> list, int index) => list.HasIndex(index) ? list[index] : default;

    /// <summary> Tries to get element at index. </summary>
    /// <param name="list"> The list. </param>
    /// <param name="index"> The index. </param>
    /// <param name="value"> The output value. </param>
    /// <returns> True if the element was retrieved. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet<T>(this IList<T> list, int index, out T value)
    {
      if (list.HasIndex(index))
      {
        value = list[index];
        return true;
      }

      value = default;
      return false;
    }

    /// <summary> Returns a shallow clone of the list. </summary>
    /// <param name="list"> The list. </param>
    /// <returns> A new list with the same elements. </returns>
    public static List<T> Clone<T>(this IList<T> list)
    {
      List<T> clone = new List<T>(list.Count);
      for (int i = 0; i < list.Count; i++)
        clone.Add(list[i]);
      return clone;
    }

    /// <summary> Finds a random element matching the predicate using reservoir sampling. </summary>
    /// <param name="list"> The list. </param>
    /// <param name="match"> The predicate to match. </param>
    /// <returns> A random matching element, or default(T) if none match. </returns>
    public static T FindRandom<T>(this IList<T> list, Predicate<T> match)
    {
      T result = default;
      int count = 0;

      for (int i = 0; i < list.Count; i++)
      {
        if (match(list[i]))
        {
          count++;
          if (Rand.Range(0, count) == 0)
            result = list[i];
        }
      }

      return result;
    }

    /// <summary> Returns the next index (cyclic). </summary>
    /// <param name="list"> The list. </param>
    /// <param name="index"> The current index. </param>
    /// <returns> The next index, wrapping to 0 if at the end. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int NextIndex<T>(this IList<T> list, int index) => (index + 1) % list.Count;

    /// <summary> Returns the previous index (cyclic). </summary>
    /// <param name="list"> The list. </param>
    /// <param name="index"> The current index. </param>
    /// <returns> The previous index, wrapping to the end if at 0. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int PreviousIndex<T>(this IList<T> list, int index) => (index - 1 + list.Count) % list.Count;

    /// <summary> Returns the next element cyclically. </summary>
    /// <param name="list"> The list. </param>
    /// <param name="value"> The current value. </param>
    /// <returns> The next element in the list. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Next<T>(this IList<T> list, T value)
    {
      int index = list.IndexOf(value);
      return list[(index + 1) % list.Count];
    }

    /// <summary> Returns the previous element cyclically. </summary>
    /// <param name="list"> The list. </param>
    /// <param name="value"> The current value. </param>
    /// <returns> The previous element in the list. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Previous<T>(this IList<T> list, T value)
    {
      int index = list.IndexOf(value);
      return list[(index - 1 + list.Count) % list.Count];
    }

    /// <summary> Returns the number of unique elements. </summary>
    public static int UniqueCount<T>(this List<T> list) => list.UniqueCount(item => item.GetHashCode());

    /// <summary> Returns the number of unique elements using a custom hash function. </summary>
    public static int UniqueCount<T>(this List<T> list, Func<T, int> getHashCode)
    {
      HashSet<int> unique = new(list.Count);

      for (int i = 0; i < list.Count; i++)
        unique.Add(getHashCode(list[i]));

      return unique.Count;
    }

    /// <summary> Returns true if all elements are unique. </summary>
    public static bool AreAllUnique<T>(this List<T> list) => list.AreAllUnique(item => item.GetHashCode());

    /// <summary> Returns true if all elements are unique using a custom hash function. </summary>
    public static bool AreAllUnique<T>(this List<T> list, Func<T, int> getHashCode)
    {
      HashSet<int> unique = new(list.Count);

      for (int i = 0; i < list.Count; i++)
      {
        if (unique.Add(getHashCode(list[i])) == false)
          return false;
      }

      return true;
    }
  }
}
