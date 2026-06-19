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
  /// <summary> IEnumerable extensions. </summary>
  public static class EnumerableExtensions
  {
    /// <summary> Number of elements in IEnumerable. </summary>
    /// <param name="self">IEnumerable</param>
    /// <typeparam name="T">Type</typeparam>
    /// <returns>Number of elements.</returns>
    public static int Count<T>(this IEnumerable<T> self)
    {
      int count = 0;
      IEnumerator<T> enumerator = self.GetEnumerator();

      while (enumerator.MoveNext() == true)
        count++;

      return count;
    }

    /// <summary> Enumerates all nodes in a linked list. </summary>
    public static IEnumerable<LinkedListNode<T>> EnumerateNodes<T>(this LinkedList<T> list)
    {
      for (LinkedListNode<T> node = list.First; node != null; node = node.Next)
        yield return node;
    }

    /// <summary> Returns an enumerator for the given range (start inclusive, end exclusive). </summary>
    public static RangeEnumerator GetEnumerator(this Range range)
    {
      if (range.End.IsFromEnd)
        throw new System.NotSupportedException("Range with from-end index is not supported.");

      return new RangeEnumerator(range);
    }

    /// <summary> Returns an enumerator from 0 to count (exclusive). </summary>
    public static RangeEnumerator GetEnumerator(this int count)
    {
      if (count <= 0)
        throw new System.NotSupportedException("Count must be greater than 0.");

      return new RangeEnumerator(0..count);
    }

    /// <summary> Lightweight integer range enumerator. </summary>
    public struct RangeEnumerator
    {
      private int current;
      private readonly int end;

      public RangeEnumerator(Range range)
      {
        current = range.Start.Value - 1;
        end = range.End.Value;
      }

      public readonly int Current => current;

      public bool MoveNext()
      {
        current++;
        return current < end;
      }
    }

    /// <summary> Yields dictionary values for keys that exist in the dictionary. </summary>
    /// <typeparam name="TKey"> Key type. </typeparam>
    /// <typeparam name="TValue"> Value type. </typeparam>
    /// <param name="self"> Keys to look up. </param>
    /// <param name="dictionary"> Source dictionary. </param>
    /// <returns> Matching values. </returns>
    public static IEnumerable<TValue> TryPullFromDictionary<TKey, TValue>(this IEnumerable<TKey> self, IDictionary<TKey, TValue> dictionary)
    {
      foreach (TKey item in self)
      {
        if (dictionary.TryGetValue(item, out TValue value) == true)
          yield return value;
      }
    }

    /// <summary> Partitions the sequence into fixed-size arrays. Trailing partial buffers are omitted. </summary>
    /// <typeparam name="T"> Element type. </typeparam>
    /// <param name="source"> Source sequence. </param>
    /// <param name="bufferSize"> Size of each buffer. </param>
    /// <returns> Full buffers only. </returns>
    public static IEnumerable<T[]> Buffer<T>(this IEnumerable<T> source, int bufferSize)
    {
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException(nameof(bufferSize), "Buffer size must be greater than zero.");

      IEnumerator<T> iterator = source.GetEnumerator();

      while (true)
      {
        T[] workingList = new T[bufferSize];
        int position;

        for (position = 0; position < bufferSize && iterator.MoveNext() == true; position++)
          workingList[position] = iterator.Current;

        if (position < bufferSize)
          yield break;

        yield return workingList;
      }
    }

    /// <summary> Emits a sliding window of the given size over the sequence. </summary>
    /// <typeparam name="T"> Element type. </typeparam>
    /// <param name="source"> Source sequence. </param>
    /// <param name="window"> Window size. </param>
    /// <returns> Window snapshots. </returns>
    public static IEnumerable<IList<T>> RollingWindow<T>(this IEnumerable<T> source, int window)
    {
      if (window <= 0)
        throw new ArgumentOutOfRangeException(nameof(window), "Window size must be greater than zero.");

      IEnumerator<T> iterator = source.GetEnumerator();
      int position = 0;
      List<T> workingList = new(window);

      while (position < window && iterator.MoveNext() == true)
      {
        workingList.Add(iterator.Current);
        position++;
      }

      if (position < window)
        yield break;

      yield return workingList.ToArray();

      while (iterator.MoveNext() == true)
      {
        workingList.RemoveAt(0);
        workingList.Add(iterator.Current);
        yield return workingList.ToArray();
      }
    }

    /// <summary> Projects elements that pass an eligibility check in a single pass. </summary>
    /// <typeparam name="TSource"> Source element type. </typeparam>
    /// <typeparam name="TResult"> Result element type. </typeparam>
    /// <param name="source"> Source sequence. </param>
    /// <param name="predicate"> Returns eligibility and the projected value. </param>
    /// <returns> Matching projected values. </returns>
    public static IEnumerable<TResult> SelectWhere<TSource, TResult>(
      this IEnumerable<TSource> source,
      Func<TSource, (bool eligible, TResult result)> predicate)
    {
      foreach (TSource element in source)
      {
        (bool eligible, TResult result) = predicate(element);

        if (eligible == true)
          yield return result;
      }
    }
  }
}