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
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// A high-performance list that uses ArrayPool for memory efficiency and swap-remove for O(1) element removal.
  ///
  /// Benefits over List<T>:
  /// - Zero GC allocations: uses ArrayPool<T>.Shared to rent/return backing arrays.
  /// - O(1) RemoveAt: swaps the removed element with the last element instead of shifting.
  /// - Struct enumerator: foreach allocates no heap memory.
  /// - 2x capacity growth: amortized O(1) adds.
  ///
  /// Trade-offs:
  /// - RemoveAt does NOT preserve element order (swap-with-last).
  /// - Implements IReadOnlyList<T> (read-only interface) to discourage misuse.
  /// - Must call Dispose() to return the pooled array, or use with 'using'.
  ///
  /// Usage:
  /// <code>
  /// // Basic usage
  /// using var list = new FastList<int>(16);
  /// list.Add(42);
  /// list.Add(99);
  /// list.RemoveAt(0); // O(1) swap-remove
  ///
  /// // Iteration (zero allocation)
  /// foreach (var item in list)
  ///     Debug.Log(item);
  ///
  /// // Direct array access (for performance-critical code)
  /// int[] raw = list.GetInternalArray();
  /// for (int i = 0; i < list.Count; i++)
  ///     Process(raw[i]);
  /// </code>
  /// </summary>
  /// <typeparam name="T">Element type.</typeparam>
  public sealed class FastList<T> : IReadOnlyList<T>, IDisposable
  {
    private static readonly ArrayPool<T> Pool = ArrayPool<T>.Shared;

    private T[] items;
    private bool disposed;

    /// <summary> Number of elements. </summary>
    public int Count { get; private set; }

    /// <summary> Current capacity. </summary>
    public int Capacity { get; private set; }

    /// <summary> Creates a new FastList with the specified initial capacity. </summary>
    /// <param name="capacity">Initial capacity.</param>
    public FastList(int capacity = 1)
    {
      if (capacity < 1)
        capacity = 1;

      items = Pool.Rent(capacity);
      Capacity = items.Length;
      Count = 0;
      disposed = false;
    }

    /// <summary> Gets or sets element at index. </summary>
    /// <param name="index">Index of the element.</param>
    /// <returns>The element at the specified index.</returns>
    public T this[int index]
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get
      {
        if ((uint)index >= (uint)Count)
          throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} out of range [0, {Count - 1}].");

        return items[index];
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      set
      {
        if ((uint)index >= (uint)Count)
          throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} out of range [0, {Count - 1}].");

        items[index] = value;
      }
    }

    /// <summary> Adds an element to the end. </summary>
    /// <param name="item">Item to add.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(T item)
    {
      if (disposed)
        throw new ObjectDisposedException(nameof(FastList<T>));

      if (Count == Capacity)
        EnsureCapacity(Count + 1);

      items[Count++] = item;
    }

    /// <summary> Removes element at index by swapping with last. Returns false if index invalid. </summary>
    /// <param name="index">Index of element to remove.</param>
    /// <returns>True if removed, false if index invalid.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool RemoveAt(int index)
    {
      if ((uint)index >= (uint)Count)
        return false;

      Count--;
      items[index] = items[Count];
      items[Count] = default;

      return true;
    }

    /// <summary> Removes the last element. Returns false if empty. </summary>
    /// <returns>True if removed, false if empty.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool RemoveLast()
    {
      if (Count == 0)
        return false;

      Count--;
      items[Count] = default;

      return true;
    }

    /// <summary> Removes the first occurrence of an item. Returns true if found and removed. </summary>
    /// <param name="item">Item to remove.</param>
    /// <returns>True if found and removed, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(T item)
    {
      int index = IndexOf(item);
      if (index >= 0)
        return RemoveAt(index);

      return false;
    }

    /// <summary> Returns true if the list contains the item. </summary>
    /// <param name="item">Item to search for.</param>
    /// <returns>True if found, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(T item) => IndexOf(item) >= 0;

    /// <summary> Returns the index of the item, or -1 if not found. </summary>
    /// <param name="item">Item to search for.</param>
    /// <returns>Index of the item, or -1 if not found.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int IndexOf(T item)
    {
      EqualityComparer<T> comparer = EqualityComparer<T>.Default;

      for (int i = 0; i < Count; ++i)
      {
        if (comparer.Equals(items[i], item))
          return i;
      }

      return -1;
    }

    /// <summary> Clears all elements. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
      if (Count > 0)
      {
        Array.Clear(items, 0, Count);
        Count = 0;
      }
    }

    /// <summary> Returns the internal array (use with caution). </summary>
    /// <returns>The internal backing array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] GetInternalArray() => items;

    /// <summary> Trims excess capacity and returns the array to the pool. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void TrimExcess()
    {
      int threshold = (int)(Capacity * 0.9);
      if (Count < threshold)
      {
        T[] newItems = Pool.Rent(Count);
        Array.Copy(items, newItems, Count);
        Pool.Return(items, clearArray: true);
        items = newItems;
        Capacity = items.Length;
      }
    }

    /// <summary> Returns the internal array to the pool. </summary>
    public void Dispose()
    {
      if (!disposed)
      {
        disposed = true;

        if (items != null)
        {
          Pool.Return(items, clearArray: true);
          items = null;
        }

        Count = 0;
        Capacity = 0;
      }
    }

    /// <summary> Returns an enumerator. </summary>
    /// <returns>A struct enumerator.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator GetEnumerator() => new Enumerator(this);

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnsureCapacity(int minCapacity)
    {
      if (Capacity < minCapacity)
      {
        int newCapacity = Capacity == 0 ? 1 : Capacity * 2;
        if (newCapacity < minCapacity)
          newCapacity = minCapacity;

        T[] newItems = Pool.Rent(newCapacity);
        if (Count > 0)
          Array.Copy(items, newItems, Count);

        Pool.Return(items, clearArray: true);
        items = newItems;
        Capacity = items.Length;
      }
    }

    /// <summary> Struct enumerator for zero-allocation iteration. </summary>
    public struct Enumerator : IEnumerator<T>
    {
      private readonly T[] items;
      private readonly int count;
      private int index;

      /// <summary> Current element. </summary>
      public T Current { get; private set; }

      internal Enumerator(FastList<T> list)
      {
        items = list.items;
        count = list.Count;
        index = -1;
        Current = default;
      }

      /// <summary> Advances to the next element. </summary>
      /// <returns>True if there are more elements, false otherwise.</returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public bool MoveNext()
      {
        int next = index + 1;
        if (next < count)
        {
          index = next;
          Current = items[next];
          return true;
        }

        Current = default;
        return false;
      }

      /// <summary> Resets the enumerator. </summary>
      public void Reset()
      {
        index = -1;
        Current = default;
      }

      object IEnumerator.Current => Current;

      /// <summary> Disposes the enumerator. </summary>
      public void Dispose() { }
    }
  }
}
