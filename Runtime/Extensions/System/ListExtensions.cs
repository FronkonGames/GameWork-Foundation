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
using System.Collections.Generic;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// List extensions.
  /// </summary>
  public static class ListExtensions
  {
    /// <summary>
    /// Swaps a pair of elements.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <typeparam name="T"></typeparam>
    public static void Swap<T>(this IList<T> self, int i, int j) => (self[i], self[j]) = (self[j], self[i]);

    /// <summary>
    /// Eliminate an items range.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="entries"></param>
    /// <typeparam name="T"></typeparam>
    public static void RemoveRange<T>(this IList<T> self, IEnumerable<T> entries)
    {
      foreach (T item in entries)
        self.Remove(item);
    }

    /// <summary>
    /// Reverses a list of items in place.
    /// </summary>
    /// <param name="self"></param>
    /// <typeparam name="T"></typeparam>
    public static void Reverse<T>(this IList<T> self) => Reverse(self, 0, self.Count);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <typeparam name="T"></typeparam>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <typeparam name="T"></typeparam>
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
  }
}