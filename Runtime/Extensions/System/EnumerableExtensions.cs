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
  }
}