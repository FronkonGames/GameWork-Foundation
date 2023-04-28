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
using UnityEngine.Assertions;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Array extensions. </summary>
  public static class ArrayExtensions
  {
    /// <summary> Adds an element to the end of the array. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="item"> Item. </param>
    /// <returns> Array with added value. </returns>
    public static T[] Append<T>(this T[] self, T item)
    {
      T[] result = new T[self.Length + 1];
      result[^1] = item;

      return result;
    }

    /// <summary> Adds an array to the end of the array. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="items"> Array. </param>
    /// <returns> Array with added values. </returns>
    public static T[] Append<T>(this T[] self, T[] items)
    {
      Assert.IsNotNull(items);

      T[] result = new T[self.Length + items.Length];
      self.CopyTo(result, 0);
      items.CopyTo(result, self.Length);

      return result;
    }

    /// <summary> Contains an element? </summary>
    /// <param name="self"> The array. </param>
    /// <param name="element"> Item. </param>
    /// <returns> True if it contains at least one element. </returns>
    public static bool Contains<T>(this T[] self, T element) where T : IComparable
    {
      for (int i = 0, len = self.Length; i < len; ++i)
      {
        if (self[i].Equals(element) == true)
          return true;
      }

      return false;
    }

    /// <summary> Position of the first element in the array. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="value"> Item. </param>
    /// <returns> 0-based index of the element or -1. </returns>
    public static int IndexOf<T>(this T[] self, T value) where T : IComparable
    {
      for (int i = 0; i < self.Length; ++i)
      {
        if (self[i].Equals(value) == true)
          return i;
      }

      return -1;
    }

    /// <summary> Position of the first element in the array that meets a condition. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="condition"> Condition. </param>
    /// <returns> 0-based index of the element or -1. </returns>
    public static int IndexOf<T>(this T[] array, Func<T, bool> condition) where T : IComparable
    {
      for (int i = 0; i < array.Length; ++i)
      {
        if (condition(array[i]) == true)
          return i;
      }

      return -1;
    }

    /// <summary> Removes the first occurrence of an element. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="value"> Item. </param>
    /// <returns> Array without the first element. </returns>
    public static T[] Remove<T>(this T[] self, T value)
    {
      List<T> elems = new(self);
      elems.Remove(value);

      return elems.ToArray();
    }

    /// <summary> Deletes an element according to its index. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="index"> Index. </param>
    /// <returns> Array without that element. </returns>
    public static T[] RemoveAt<T>(this T[] self, int index)
    {
      T[] result = new T[self.Length - 1];

      if (index == 0 && result.Length > 0)
        Array.Copy(self, 1, result, 0, self.Length - 1);
      else if (index == self.Length - 1)
        Array.Copy(self, result, self.Length - 1);
      else
      {
        Array.Copy(self, 0, result, 0, index);
        Array.Copy(self, index + 1, result, index, self.Length - index - 1);
      }

      return result;    
    }

    /// <summary> Fills an array of one element. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="value"> Value. </param>
    /// <returns> Array filled with that element. </returns>
    public static T[] Fill<T>(this T[] self, T value)
    {
      for (int i = 0; i < self.Length; ++i)
        self[i] = value;

      return self;
    }

    /// <summary> Fills an array using a function. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="filler"> Filling function. </param>
    /// <returns> Filled array. </returns>
    public static T[] Fill<T>(this T[] self, Func<int, T> filler)
    {
      for (int i = 0; i < self.Length; ++i)
        self[i] = filler(i);

      return self;
    }

    /// <summary> Returns a random element. </summary>
    /// <param name="self"> The array. </param>
    /// <returns> Value or default. </returns>
    public static T Random<T>(this T[] self) => self.Length > 0 ? self[Rand.Range(0, self.Length)] : default;

    /// <summary> Sets array to default. </summary>
    /// <param name="self"> The array. </param>
    public static void Clear<T>(this T[] self) => Array.Clear(self, 0, self.Length);

    /// <summary> Sets a range of elements in the array to default. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="index"> Start of the range. </param>
    /// <param name="count"> Number of elements to be cleaned. </param>
    public static void Clear<T>(this T[] self, int index, int count) => Array.Clear(self, index, count);

    /// <summary> Copies one array into another. </summary>
    /// <param name="self"> The array. </param>
    /// <returns> Copy of the array. </returns>
    public static T[] Copy<T>(this T[] self)
    {
      T[] result = new T[self.Length];
      Array.Copy(self, result, self.Length);

      return result;
    }

    /// <summary> Get a subarray. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="offset"> Offset. </param>
    /// <param name="length"> Length. </param>
    /// <returns> Subarray. </returns>
    public static T[] Sub<T>(this T[] self, int offset, int length) => new ArraySegment<T>(self, offset, length).ToArray();

    /// <summary> Swaps a pair of elements. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="i"> First element. </param>
    /// <param name="j"> Second element. </param>
    public static void Swap<T>(this T[] self, int i, int j) => (self[i], self[j]) = (self[j], self[i]);

    /// <summary> Reverses an array of items in place. </summary>
    /// <param name="self"> The array. </param>
    public static void Reverse<T>(this T[] self) => Reverse(self, 0, self.Length);

    /// <summary> Reverses the order of the items within the specified range in place. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="from"> From. </param>
    /// <param name="to"> To. </param>
    public static void Reverse<T>(this T[] self, int from, int to)
    {
      while (--to > from)
        self.Swap(from++, to);
    }

    /// <summary> Unsorts an array. </summary>
    /// <param name="self"> The array. </param>
    public static void Shuffle<T>(this T[] self)
    {
      for (int i = self.Length - 1; i > 0; i--)
      {
        int j = Rand.Range(0, i);
        self.Swap(i, j);
      }
    }

    /// <summary> Unsorts an array. </summary>
    /// <param name="self"> The array. </param>
    /// <param name="index"> Start element. </param>
    /// <param name="count"> Number of items. </param>
    public static void Shuffle<T>(this T[] self, int index, int count)
    {
      for (int i = count - 1; i > 0; i--)
      {
        int j = Rand.Range(0, i);
        self.Swap(i + index, j + index);
      }
    }

    /// <summary> Calculates the sum of all elements in the array. </summary>
    /// <param name="self"> The array. </param>
    /// <returns> The sum of all elements in the array. </returns>
    public static int Sum(this int[] self)
    {
      int sum = 0, total = self.Length;

      for (int i = 0; i < total; ++i)
        sum += self[i];

      return sum;
    }

    /// <summary> Calculates the sum of all elements in the array. </summary>
    /// <param name="self"> The array. </param>
    /// <returns> The sum of all elements in the array. </returns>
    public static float Sum(this float[] self)
    {
      float sum = 0.0f;
      int total = self.Length;

      for (int i = 0; i < total; ++i)
        sum += self[i];

      return sum;
    }

    /// <summary> Returns the maximum value in the array. </summary>
    /// <typeparam name="T">The type of value to check.</typeparam>
    /// <param name="self"> The array. </param>
    /// <returns>The maximum value in the array.</returns>
    public static T Max<T>(this T[] self) where T : IComparable<T>
    {
      if (self == null || self.Length == 0)
        return default;

      T max = self[0];
      int total = self.Length;
      for (int i = 1; i < total; ++i)
      {
        T element = self[i];
        if (element.CompareTo(max) > 0)
          max = element;
      }

      return max;
    }

    /// <summary> Returns the minimum value in the array. </summary>
    /// <typeparam name="T">The type of value to check.</typeparam>
    /// <param name="self"> The array. </param>
    /// <returns>The minimum value in the array.</returns>
    public static T Min<T>(this T[] self) where T : IComparable<T>
    {
      if (self == null || self.Length == 0)
        return default;

      T min = self[0];
      int total = self.Length;
      for (int i = 1; i < total; ++i)
      {
        T element = self[i];
        if (element.CompareTo(min) < 0)
          min = element;
      }

      return min;
    }
  }
}