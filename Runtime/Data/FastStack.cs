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
  /// <summary>
  /// Stack with custom EqualityCompare and fast comparison.
  /// </summary>
  [Serializable]
  public sealed class FastStack<T>
  {
    /// <summary>Size.</summary>
    public int Count { get; private set; }

    /// <summary>Cast to object to compare.</summary>
    public bool UseObjectCastComparer { get; set; }

    private T[] data;

    private int capacity;

    private readonly bool isNullable;

    private readonly EqualityComparer<T> comparer;

    private const int InitialCapacity = 8;

    /// <summary>
    /// Constructor.
    /// </summary>
    public FastStack() : this(null) { }

    /// <summary>
    /// Constructor with custom comparer.
    /// </summary>
    public FastStack(EqualityComparer<T> comparer) : this(InitialCapacity, comparer) { }

    /// <summary>
    /// Constructor with capacity and custom comparer.
    /// </summary>
    public FastStack(int capacity, EqualityComparer<T> comparer = null)
    {
      Check.Greater(capacity, 0);

      Type type = typeof(T);
      isNullable = type.IsValueType == false || (Nullable.GetUnderlyingType(type) != null);
      this.capacity = capacity > InitialCapacity ? capacity : InitialCapacity;
      Count = 0;
      this.comparer = comparer;
      data = new T[this.capacity];
    }

    /// <summary>
    /// Clean without erasing the memory.
    /// </summary>
    public void Clear()
    {
      if (isNullable == true)
      {
        for (var i = Count - 1; i >= 0; i--)
          data[i] = default(T);
      }

      Count = 0;
    }

    /// <summary>
    /// Contains this?
    /// </summary>
    public bool Contains(T item)
    {
      int i;
      if (UseObjectCastComparer == true && isNullable == true)
      {
        for (i = Count - 1; i >= 0; i--)
        {
          if ((object)data[i] == (object)item)
            break;
        }
      }
      else
      {
        if (comparer != null)
        {
          for (i = Count - 1; i >= 0; i--)
          {
            if (comparer.Equals(data[i], item))
              break;
          }
        }
        else
          i = Array.IndexOf(data, item, 0, Count);
      }

      return i != -1;
    }

    /// <summary>
    /// Copy to Array.
    /// </summary>
    public void CopyTo(T[] array, int arrayIndex)
    {
      Check.GreaterOrEqual(arrayIndex, 0);

      Array.Copy(data, 0, array, arrayIndex, Count);
    }

    /// <summary>
    /// Returns the tail.
    /// </summary>
    public T Peek()
    {
      if (Count == 0)
        Log.Error("Peek() count == 0");

      return data[Count - 1];
    }

    /// <summary>
    /// Remove the tail.
    /// </summary>
    public T Pop()
    {
      if (Count == 0)
        Log.Error("Pop() count == 0");

      Count--;
      T target = data[Count];
      if (isNullable == true)
        data[Count] = default(T);

      return target;
    }

    /// <summary>
    /// Add to the tail.
    /// </summary>
    public void Push(T item)
    {
      if (Count == capacity)
      {
        if (capacity > 0)
          capacity <<= 1;
        else
          capacity = InitialCapacity;

        T[] items = new T[capacity];

        Array.Copy(data, items, Count);
        data = items;
      }

      data[Count] = item;
      Count++;
    }

    /// <summary>
    /// Returns an array with items.
    /// </summary>
    public T[] ToArray()
    {
      T[] target = new T[Count];

      if (Count > 0)
        Array.Copy(data, target, Count);

      return target;
    }
  }
}
