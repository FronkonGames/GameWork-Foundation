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
using System.Collections;
using System.Collections.Generic;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Faster list without checks and with access to the internal array.
  /// </summary>
  [Serializable]
  public sealed class FastList<T> : IList<T>
  {
    /// <summary>
    /// Size.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Capacity.
    /// </summary>
    public int Capacity { get; private set; }

    /// <summary>Cast to object to compare.</summary>
    public bool UseObjectCastComparer { get; set; }

    /// <summary>
    /// Returns an item in index.
    /// </summary>
    public T this[int index]
    {
      get
      {
        Check.True(index < Count);

        return data[index];
      }

      set
      {
        Check.True(index < Count);

        data[index] = value;
      }
    }

    // ILIst.
    public bool IsReadOnly => false;

    private T[] data;

    private const int InitialCapacity = 8;

    private readonly bool isNullable;

    private readonly EqualityComparer<T> comparer;

    /// <summary>
    /// Constructor.
    /// </summary>
    public FastList() : this(null) { }

    /// <summary>
    /// Constructor with custom comparer.
    /// </summary>
    public FastList(EqualityComparer<T> comparer) : this(InitialCapacity, comparer) { }

    /// <summary>
    /// Constructor with custom comparer and capacity.
    /// </summary>
    public FastList(int capacity, EqualityComparer<T> comparer = null)
    {
      Check.True(capacity > 0);

      Type type = typeof(T);

      isNullable = !type.IsValueType || (Nullable.GetUnderlyingType(type) != null);
      this.Capacity = capacity > InitialCapacity ? capacity : InitialCapacity;
      Count = 0;
      this.comparer = comparer;

      data = new T[capacity];
    }

    /// <summary>
    /// AAdd an item.
    /// </summary>
    public void Add(T item)
    {
      if (Count == Capacity)
      {
        if (Capacity > 0)
          Capacity <<= 1;
        else
          Capacity = InitialCapacity;

        T[] items = new T[Capacity];

        Array.Copy(data, items, Count);

        data = items;
      }

      data[Count] = item;
      Count++;
    }

    /// <summary>
    /// Adds range of items.
    /// </summary>
    public void AddRange(IEnumerable<T> range)
    {
      Check.IsNotNull(range);

      ICollection<T> casted = range as ICollection<T>;
      if (casted != null)
      {
        int amount = casted.Count;
        if (amount <= 0)
          return;

        Reserve(amount, false, false);
        casted.CopyTo(data, Count);
        Count += amount;
      }
      else
      {
        if (range != null)
        {
          using (IEnumerator<T> it = range.GetEnumerator())
          {
            while (it.MoveNext() == true)
              Add(it.Current);
          }
        }
      }
    }


    /// <summary>
    /// Assign data.
    /// </summary>
    /// <remarks>Danger zone.</remarks>
    public void AssignData(T[] newData, int newCount)
    {
      Check.IsNotNull(newData);

      data = newData;
      Count = newCount >= 0 ? newCount : 0;
      Capacity = data.Length;
    }

    /// <summary>
    /// Clean, without erasing the memory.
    /// </summary>
    public void Clear() => Clear(false);

    /// <summary>
    /// Cleans by erasing the memory.
    /// </summary>
    public void Clear(bool forceSetDefaultValues)
    {
      if (isNullable == true || forceSetDefaultValues == true)
      {
        for (var i = Count - 1; i >= 0; i--)
          data[i] = default(T);
      }

      Count = 0;
    }

    /// <summary>
    /// Does it contain this?
    /// </summary>
    public bool Contains(T item) => IndexOf(item) != -1;

    /// <summary>
    /// Copy to an array.
    /// </summary>
    public void CopyTo(T[] array, int arrayIndex)
    {
      Check.GreaterOrEqual(arrayIndex, 0);

      Array.Copy(data, 0, array, arrayIndex, Count);
    }

    /// <summary>
    /// Fills.
    /// </summary>
    public void FillWithEmpty(int amount, bool clearCollection = false, bool forceSetDefaultValues = true)
    {
      if (amount <= 0)
        return;

      if (clearCollection == true)
        Count = 0;

      Reserve(amount, clearCollection, forceSetDefaultValues);

      Count += amount;
    }

    /// <summary>
    /// Index of an item.
    /// </summary>
    public int IndexOf(T item)
    {
      int i = -1;
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

      return i;
    }

    /// <summary>
    /// Insert an item in an index.
    /// </summary>
    public void Insert(int index, T item)
    {
      Check.True(index >= 0 && index < Count, "Insert() Invalid index.");

      Reserve(1, false, false);

      Array.Copy(data, index, data, index + 1, Count - index);
      data[index] = item;
      Count++;
    }

    /// <summary>
    /// Returns array of data.
    /// </summary>
    /// <remarks>Danger zone.</remarks>
    public T[] GetData() => data;

    /// <summary>
    /// Not supported, do not use.
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
      Log.Exception("Not supported", new NotSupportedException("IEnumerator<T> GetEnumerator()."));

      return null;
    }

    /// <summary>
    /// Not supported, do not use.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
      Log.Exception("Not supported", new NotSupportedException("IEnumerator<T> GetEnumerator()."));

      return null;
    }

    /// <summary>
    /// Borra un item.
    /// </summary>
    public bool Remove(T item)
    {
      int id = Array.IndexOf(data, item);
      if (id == -1)
        return false;

      RemoveAt(id);

      return true;
    }

    /// <summary>
    /// Delete an item.
    /// </summary>
    public void RemoveAt(int index)
    {
      if (index < 0 || index >= Count)
        return;

      Count--;

      Array.Copy(data, index + 1, data, index, Count - index);
    }

    /// <summary>
    /// Delete the last item.
    /// </summary>
    public bool RemoveLast(bool forceSetDefaultValues = true)
    {
      if (Count <= 0)
        return false;

      Count--;

      if (forceSetDefaultValues == true)
        data[Count] = default(T);

      return true;
    }

    /// <summary>
    /// Memory reserve.
    /// </summary>
    public void Reserve(int amount, bool totalAmount = false, bool forceSetDefaultValues = true)
    {
      if (amount <= 0)
        return;

      int start = totalAmount == true ? 0 : Count;
      int newCount = start + amount;

      if (newCount > Capacity)
      {
        if (Capacity <= 0)
          Capacity = InitialCapacity;

        while (Capacity < newCount)
          Capacity <<= 1;

        T[] items = new T[Capacity];

        Array.Copy(data, items, Count);
        data = items;
      }

      if (forceSetDefaultValues == true)
      {
        for (int i = Count; i < newCount; ++i)
          data[i] = default(T);
      }
    }

    /// <summary>
    /// Turn the array around.
    /// </summary>
    public void Reverse()
    {
      if (Count <= 0)
        return;

      T temp;

      for (int i = 0, iMax = Count >> 1; i < iMax; ++i)
      {
        temp = data[i];
        data[i] = data[Count - i - 1];
        data[Count - i - 1] = temp;
      }
    }

    /// <summary>
    /// Returns a copy of the array.
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
