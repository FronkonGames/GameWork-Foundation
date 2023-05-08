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
#define ENABLE_EXCEPTIONS
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Generic array-based list. </summary>
  public class ArrayList<T> : IEnumerable<T>
  {
    /// <summary> Number of elements. </summary>
    public int Count => size;

    /// <summary> Capacity of list. </summary>
    public int Capacity => data.Length;

    /// <summary> Is empty? </summary>
    public bool IsEmpty => Count == 0;

    /// <summary> First element in the list. </summary>
    public T First
    {
      get
      {
#if ENABLE_EXCEPTIONS
        if (IsEmpty == true)
          Log.ExceptionArgumentOutOfRange("The list is empty.");
#endif
        return data[0];
      }
    }

    /// <summary> Last element in the list. </summary>
    public T Last
    {
      get
      {
#if ENABLE_EXCEPTIONS
        if (IsEmpty == true)
          Log.ExceptionIndexOutOfRange("The list is empty.");
#endif
        return data[Count - 1];
      }
    }

    /// <summary> Gets or sets the item at the specified index. </summary>
    public T this[int index]
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get
      {
#if ENABLE_EXCEPTIONS
        if (index < 0 || index >= size)
          Log.ExceptionIndexOutOfRange($"Index {index} out of size {size}.");
#endif
        return data[index];
      }
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      set
      {
#if ENABLE_EXCEPTIONS
        if (index < 0 || index >= size)
          Log.ExceptionIndexOutOfRange($"Index {index} out of size {size}.");
#endif

        data[index] = value;
      }
    }

    private bool IsMaximumCapacityReached = false;

    // http://referencesource.microsoft.com/#mscorlib/system/array.cs,2d2b551eabe74985
    private int MaxArrayCapacity => Environment.Is64BitOperatingSystem == true ? 0X7FEFFFFF : 0x8000000;
    private const int DefaultCapacity = 8;
    private T[] data;
    private int size;

    private static readonly T[] EmptyArray = new T[0];

    /// <summary> Constructor. </summary>
    public ArrayList() : this(capacity: 0) { }

    /// <summary> Constructor. </summary>
    public ArrayList(int capacity)
    {
#if ENABLE_EXCEPTIONS
      if (capacity < 0)
        Log.ExceptionArgumentOutOfRange("Capacity cant be negative.");
#endif
      data = capacity == 0 ? EmptyArray : new T[capacity];
      size = 0;
    }

    /// <summary> Add item. </summary>
    /// <param name="item">Item.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(T item)
    {
      if (size == data.Length)
        EnsureCapacity(size + 1);

      data[size++] = item;
    }

    /// <summary> Adds an collection of items. </summary>
    /// <param name="items">Items</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddRange(IEnumerable<T> items)
    {
#if ENABLE_EXCEPTIONS
      if (items is null)
        Log.ExceptionArgumentNull("Null items.");
#endif
      int count = items.Count();
      if (size + count > MaxArrayCapacity)
        throw new OverflowException();

      if (count > 0)
      {
        EnsureCapacity(size + count);

        foreach (T element in items)
          Add(element);
      }
    }

    /// <summary> Inserts a new element at an index. </summary>
    /// <param name="index">Index of insertion.</param>
    /// <param name="item">Item to insert.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Insert(int index, T item)
    {
#if ENABLE_EXCEPTIONS
      if (index < 0 || index > size)
        Log.ExceptionIndexOutOfRange($"Index {index} out of range [0, {size - 1}].");
#endif
      if (size == data.Length)
        EnsureCapacity(size + 1);

      if (index < size)
        Array.Copy(data, index, data, index + 1, size - index);

      data[index] = item;
      size++;
    }

    /// <summary> Removes a specified item. </summary>
    /// <param name="item">Item.</param>
    /// <returns>True if removed successfully, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(T item)
    {
      int index = IndexOf(item);
      if (index >= 0)
        RemoveAt(index);

      return index >= 0;
    }

    /// <summary> Removes item at the specified index. </summary>
    /// <param name="index">Index of element.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveAt(int index)
    {
#if ENABLE_EXCEPTIONS
      if (index < 0 || index >= size)
        Log.ExceptionIndexOutOfRange($"Index {index} out of range [0, {size - 1}].");
#endif
      size--;

      // O(N).
      if (index < size)
        Array.Copy(data, index + 1, data, index, size - index);

      data[size] = default;
    }

    /// <summary> Clear list. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
      if (size > 0)
      {
        size = 0;
        Array.Clear(data, 0, size);
        data = EmptyArray;
      }
    }

    /// <summary> Resize the List to a new size. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Resize(int newSize) => Resize(newSize, default);

    /// <summary> Resize the list to a new size. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Resize(int newSize, T defaultValue)
    {
      int currentSize = Count;

      if (newSize < currentSize)
        EnsureCapacity(newSize);
      else if (newSize > currentSize)
      {
        if (newSize > data.Length)
          EnsureCapacity(newSize + 1);

        T[] fill = new T[newSize - currentSize];
        Array.Fill(fill, defaultValue);
        AddRange(fill);
      }
    }

    /// <summary> Reverses this list. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reverse() => Reverse(0, size);

    /// <summary> Reverses the order of a number of elements. </summary>
    /// <param name="start">Start index.</param>
    /// <param name="count">Count of elements to reverse.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reverse(int start, int count)
    {
#if ENABLE_EXCEPTIONS
      if (start < 0 || start >= size)
        Log.ExceptionIndexOutOfRange($"Index {start} out of range [0, {size - 1}].");

      if (count < 0 || start > (size - count))
        Log.ExceptionIndexOutOfRange($"Index {start} out of range [0, {size - 1}].");
#endif
      // Array.Reverse uses TrySZReverse.
      Array.Reverse(data, start, count);
    }

    /// <summary> For each element in list, apply the specified action to it. </summary>
    /// <param name="action">Action.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ForEach(Action<T> action)
    {
#if ENABLE_EXCEPTIONS
      if (action is null)
        Log.ExceptionArgumentNull("Invalid action");
#endif
      for (int i = 0; i < size; ++i)
        action(data[i]);
    }

    /// <summary> Contains the specified item? </summary>
    /// <param name="item">Item.</param>
    /// <returns>True if list contains the dataItem, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(T item)
    {
      if (item is null)
      {
        for (int i = 0; i < size; ++i)
        {
          if (data[i] is null)
            return true;
        }
      }
      else
      {
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;

        for (int i = 0; i < size; ++i)
        {
          if (comparer.Equals(data[i], item))
            return true;
        }
      }

      return false;
    }

    /// <summary> Contains the specified item? </summary>
    /// <param name="item">Data item.</param>
    /// <param name="comparer">The Equality Comparer object.</param>
    /// <returns>True if list contains the item, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(T item, IEqualityComparer<T> comparer)
    {
#if ENABLE_EXCEPTIONS
      if (comparer is null)
        Log.ExceptionArgumentNull("Null comparer.");
#endif
      if (item is null)
      {
        for (int i = 0; i < size; ++i)
        {
          if (data[i] is null)
            return true;
        }
      }
      else
      {
        for (int i = 0; i < size; ++i)
        {
          if (comparer.Equals(data[i], item))
            return true;
        }
      }

      return false;
    }

    /// <summary> Checks whether an item specified via a Predicate<T> function exists in list. </summary>
    /// <param name="searchMatch">Match predicate.</param>
    /// <returns>True if list contains the item, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Exists(Predicate<T> searchMatch) => FindIndex(searchMatch) != -1;

    /// <summary> Finds the index of the element that matches the predicate. </summary>
    /// <param name="searchMatch">Match predicate.</param>
    /// <returns>The index of element if found, -1 otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int FindIndex(Predicate<T> searchMatch) => FindIndex(0, size, searchMatch);

    /// <summary> Finds the index of the element that matches the predicate. </summary>
    /// <param name="start">Starting index to search from.</param>
    /// <param name="searchMatch">Match predicate.</param>
    /// <returns>The index of the element if found, -1 otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int FindIndex(int start, Predicate<T> searchMatch) => FindIndex(start, size - start, searchMatch);

    /// <summary> Finds the index of the first element that matches the given predicate function. </summary>
    /// <param name="start">Starting index of search.</param>
    /// <param name="count">Count of elements to search through.</param>
    /// <param name="searchMatch">Match predicate function.</param>
    /// <returns>The index of element if found, -1 if not found.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int FindIndex(int start, int count, Predicate<T> searchMatch)
    {
#if ENABLE_EXCEPTIONS
      if (start < 0 || start > size)
        Log.ExceptionIndexOutOfRange($"Invalid start index {start}");

      if (count < 0 || start > (size - count))
        Log.ExceptionArgumentOutOfRange($"Invalid range [{start} - {start + count}]");

      if (searchMatch is null)
        Log.ExceptionArgumentNull("Invalid searchMach");
#endif
      int endIndex = start + count;
      for (int index = start; index < endIndex; ++index)
      {
        if (searchMatch(data[index]) == true)
          return index;
      }

      return -1;
    }

    /// <summary> Returns the index of a item. </summary>
    /// <param name="item">Item.</param>
    /// <returns>Index of element in list.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int IndexOf(T item) => IndexOf(item, 0, size);

    /// <summary> Returns the index of a given dataItem. </summary>
    /// <param name="item">Item.</param>
    /// <param name="start">The starting index to search from.</param>
    /// <returns>Index of element in list.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int IndexOf(T item, int start) => IndexOf(item, start, size);

    /// <summary> Returns the index of a given item. </summary>
    /// <param name="item">Data item.</param>
    /// <param name="start">The starting index to search from.</param>
    /// <param name="count">Count of elements to search through.</param>
    /// <returns>Index of element in list.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int IndexOf(T item, int start, int count)
    {
#if ENABLE_EXCEPTIONS
      if (start < 0 || (uint)start > (uint)size)
        Log.ExceptionIndexOutOfRange($"Invalid starting index {start}");

      if (count < 0 || start > (size - count))
        Log.ExceptionArgumentOutOfRange($"Invalid range [{start} - {start + count}]");
#endif
      return Array.IndexOf(data, item, start, count);
    }

    /// <summary> Find the specified element that matches the search predication. </summary>
    /// <param name="searchMatch">Match predicate.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Find(Predicate<T> searchMatch)
    {
#if ENABLE_EXCEPTIONS
      if (searchMatch is null)
        Log.ExceptionArgumentNull("Invalid searchMatch");
#endif
      for (int i = 0; i < size; ++i)
      {
        if (searchMatch(data[i]) == true)
          return data[i];
      }

      return default;
    }

    /// <summary> Finds all the elements that match the typed search predicate. </summary>
    /// <param name="searchMatch">Match predicate.</param>
    /// <returns>ArrayList<T> of matched elements. Empty list is returned if not element was found.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ArrayList<T> FindAll(Predicate<T> searchMatch)
    {
#if ENABLE_EXCEPTIONS
      if (searchMatch is null)
        Log.ExceptionArgumentNull("Invalid searchMatch");
#endif
      ArrayList<T> matchedElements = new();

      for (int i = 0; i < size; ++i)
      {
        if (searchMatch(data[i]))
          matchedElements.Add(data[i]);
      }

      return matchedElements;
    }

    /// <summary> Get a range of elements, starting from an index. </summary>
    /// <param name="start">Start index to get range from.</param>
    /// <param name="count">Count of elements.</param>
    /// <returns>The range as ArrayList<T>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ArrayList<T> GetRange(int start, int count)
    {
#if ENABLE_EXCEPTIONS
      if (start < 0 || (uint)start > (uint)size)
        Log.ExceptionIndexOutOfRange($"Invalid starting index {start}");

      if (count < 0 || start > (size - count))
        Log.ExceptionArgumentOutOfRange($"Invalid range [{start} - {start + count}]");
#endif
      ArrayList<T> newArrayList = new(count);
      Array.Copy(data, start, newArrayList.data, 0, count);
      newArrayList.size = count;

      return newArrayList;
    }

    /// <summary> Return an array version of this list. </summary>
    /// <returns>Array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ToArray()
    {
      T[] newArray = new T[Count];

      if (Count > 0)
        Array.Copy(data, 0, newArray, 0, Count);

      return newArray;
    }

    /// <summary> Return an array version of this list. </summary>
    /// <returns>Array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public List<T> ToList()
    {
      List<T> newList = new(Count);

      if (Count > 0)
      {
        for (int i = 0; i < Count; ++i)
          newList.Add(data[i]);
      }

      return newList;
    }

    /// <summary> Return a human readable string. </summary>
    /// <returns>The human readable string.</returns>
    public override string ToString() => string.Join(",", data);

    /// <summary> Ensures the capacity. </summary>
    /// <param name="minCapacity">Minimum capacity.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnsureCapacity(int minCapacity)
    {
      if (data.Length < minCapacity && IsMaximumCapacityReached == false)
      {
        int newCapacity = data.Length == 0 ? DefaultCapacity : data.Length * 2;
        int maxCapacity = MaxArrayCapacity;

        if (newCapacity < minCapacity)
          newCapacity = minCapacity;

        if (newCapacity >= maxCapacity)
        {
          newCapacity = maxCapacity - 1;
          IsMaximumCapacityReached = true;
        }

        ResizeCapacity(newCapacity);
      }
    }

    /// <summary> Resizes the collection to a new maximum number of capacity. </summary>
    /// <param name="newCapacity">New capacity.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ResizeCapacity(int newCapacity)
    {
      if (newCapacity != data.Length && newCapacity > size)
      {
#if ENABLE_EXCEPTIONS
        try
        {
          Array.Resize<T>(ref data, newCapacity);
        }
        catch
        {
          throw;
        }
#else
        Array.Resize<T>(ref data, newCapacity);
#endif
      }
    }

    public IEnumerator<T> GetEnumerator()
    {
      for (int i = 0; i < Count; ++i)
        yield return data[i];
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
