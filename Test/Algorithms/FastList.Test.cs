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
using NUnit.Framework;
using FronkonGames.GameWork.Foundation;

/// <summary> FastList tests. </summary>
[TestFixture]
public class FastListTests
{
  /// <summary> Add increases count. </summary>
  [Test]
  public void Add_IncreasesCount()
  {
    FastList<int> list = new();

    list.Add(42);

    Assert.AreEqual(1, list.Count);
  }

  /// <summary> Add multiple elements. </summary>
  [Test]
  public void Add_MultipleElements()
  {
    FastList<int> list = new();

    list.Add(1);
    list.Add(2);
    list.Add(3);

    Assert.AreEqual(3, list.Count);
    Assert.AreEqual(1, list[0]);
    Assert.AreEqual(2, list[1]);
    Assert.AreEqual(3, list[2]);
  }

  /// <summary> Capacity grows automatically. </summary>
  [Test]
  public void Capacity_GrowsAutomatically()
  {
    FastList<int> list = new(2);
    int initialCapacity = list.Capacity;

    for (int i = 0; i < initialCapacity + 10; ++i)
      list.Add(i);

    Assert.Greater(list.Capacity, initialCapacity);
    Assert.AreEqual(initialCapacity + 10, list.Count);
  }

  /// <summary> Initial capacity is correct. </summary>
  [Test]
  public void InitialCapacity_IsCorrect()
  {
    FastList<int> list = new(16);

    Assert.AreEqual(16, list.Capacity);
    Assert.AreEqual(0, list.Count);
  }

  /// <summary> Get returns correct value. </summary>
  [Test]
  public void Indexer_Get_ReturnsCorrectValue()
  {
    FastList<int> list = new();
    list.Add(10);
    list.Add(20);
    list.Add(30);

    Assert.AreEqual(10, list[0]);
    Assert.AreEqual(20, list[1]);
    Assert.AreEqual(30, list[2]);
  }

  /// <summary> Set updates value. </summary>
  [Test]
  public void Indexer_Set_UpdatesValue()
  {
    FastList<int> list = new();
    list.Add(1);
    list.Add(2);

    list[0] = 99;

    Assert.AreEqual(99, list[0]);
    Assert.AreEqual(2, list[1]);
  }

  /// <summary> Index out of range throws. </summary>
  [Test]
  public void Indexer_OutOfRange_Throws()
  {
    FastList<int> list = new();
    list.Add(1);

    Assert.Throws<ArgumentOutOfRangeException>(() => _ = list[1]);
    Assert.Throws<ArgumentOutOfRangeException>(() => _ = list[-1]);
  }

  /// <summary> RemoveAt removes element and returns true. </summary>
  [Test]
  public void RemoveAt_RemovesElement_ReturnsTrue()
  {
    FastList<int> list = new();
    list.Add(1);
    list.Add(2);
    list.Add(3);

    bool removed = list.RemoveAt(1);

    Assert.True(removed);
    Assert.AreEqual(2, list.Count);
  }

  /// <summary> RemoveAt swaps with last element. </summary>
  [Test]
  public void RemoveAt_SwapsWithLastElement()
  {
    FastList<int> list = new();
    list.Add(1);
    list.Add(2);
    list.Add(3);
    list.Add(4);

    list.RemoveAt(1);

    Assert.AreEqual(3, list.Count);
    Assert.AreEqual(1, list[0]);
    Assert.AreEqual(4, list[1]);
    Assert.AreEqual(3, list[2]);
  }

  /// <summary> RemoveAt invalid index returns false. </summary>
  [Test]
  public void RemoveAt_InvalidIndex_ReturnsFalse()
  {
    FastList<int> list = new();
    list.Add(1);

    Assert.False(list.RemoveAt(5));
    Assert.False(list.RemoveAt(-1));
    Assert.AreEqual(1, list.Count);
  }

  /// <summary> RemoveAt last element. </summary>
  [Test]
  public void RemoveAt_LastElement()
  {
    FastList<int> list = new();
    list.Add(1);
    list.Add(2);
    list.Add(3);

    bool removed = list.RemoveAt(2);

    Assert.True(removed);
    Assert.AreEqual(2, list.Count);
    Assert.AreEqual(1, list[0]);
    Assert.AreEqual(2, list[1]);
  }

  /// <summary> RemoveLast removes last element. </summary>
  [Test]
  public void RemoveLast_RemovesLastElement()
  {
    FastList<int> list = new();
    list.Add(1);
    list.Add(2);
    list.Add(3);

    bool removed = list.RemoveLast();

    Assert.True(removed);
    Assert.AreEqual(2, list.Count);
    Assert.AreEqual(1, list[0]);
    Assert.AreEqual(2, list[1]);
  }

  /// <summary> RemoveLast empty list returns false. </summary>
  [Test]
  public void RemoveLast_EmptyList_ReturnsFalse()
  {
    FastList<int> list = new();

    Assert.False(list.RemoveLast());
    Assert.AreEqual(0, list.Count);
  }

  /// <summary> Remove removes first occurrence. </summary>
  [Test]
  public void Remove_RemovesFirstOccurrence()
  {
    FastList<int> list = new();
    list.Add(1);
    list.Add(2);
    list.Add(3);
    list.Add(4);

    bool removed = list.Remove(2);

    Assert.True(removed);
    Assert.AreEqual(3, list.Count);
    Assert.False(list.Contains(2));
    Assert.True(list.Contains(4));
  }

  /// <summary> Remove item not found returns false. </summary>
  [Test]
  public void Remove_ItemNotFound_ReturnsFalse()
  {
    FastList<int> list = new();
    list.Add(1);
    list.Add(2);

    bool removed = list.Remove(99);

    Assert.False(removed);
    Assert.AreEqual(2, list.Count);
  }

  /// <summary> Contains returns true when item exists. </summary>
  [Test]
  public void Contains_ItemExists_ReturnsTrue()
  {
    FastList<int> list = new();
    list.Add(10);
    list.Add(20);
    list.Add(30);

    Assert.True(list.Contains(20));
    Assert.True(list.Contains(10));
    Assert.True(list.Contains(30));
  }

  /// <summary> Contains returns false when item not found. </summary>
  [Test]
  public void Contains_ItemNotFound_ReturnsFalse()
  {
    FastList<int> list = new();
    list.Add(1);
    list.Add(2);

    Assert.False(list.Contains(99));
  }

  /// <summary> IndexOf returns correct index. </summary>
  [Test]
  public void IndexOf_ReturnsCorrectIndex()
  {
    FastList<int> list = new();
    list.Add(10);
    list.Add(20);
    list.Add(30);

    Assert.AreEqual(0, list.IndexOf(10));
    Assert.AreEqual(1, list.IndexOf(20));
    Assert.AreEqual(2, list.IndexOf(30));
  }

  /// <summary> IndexOf returns -1 when not found. </summary>
  [Test]
  public void IndexOf_NotFound_ReturnsMinusOne()
  {
    FastList<int> list = new();
    list.Add(1);
    list.Add(2);

    Assert.AreEqual(-1, list.IndexOf(99));
  }

  /// <summary> Clear resets count to 0. </summary>
  [Test]
  public void Clear_ResetsCountToZero()
  {
    FastList<int> list = new();
    list.Add(1);
    list.Add(2);
    list.Add(3);

    list.Clear();

    Assert.AreEqual(0, list.Count);
  }

  /// <summary> Foreach iterates all elements. </summary>
  [Test]
  public void Foreach_IteratesAllElements()
  {
    FastList<int> list = new();
    list.Add(10);
    list.Add(20);
    list.Add(30);

    List<int> collected = new();
    foreach (int item in list)
      collected.Add(item);

    Assert.AreEqual(3, collected.Count);
    Assert.AreEqual(10, collected[0]);
    Assert.AreEqual(20, collected[1]);
    Assert.AreEqual(30, collected[2]);
  }

  /// <summary> Enumerator works correctly. </summary>
  [Test]
  public void Enumerator_WorksCorrectly()
  {
    FastList<int> list = new();
    list.Add(5);
    list.Add(10);
    list.Add(15);

    using FastList<int>.Enumerator enumerator = list.GetEnumerator();

    Assert.True(enumerator.MoveNext());
    Assert.AreEqual(5, enumerator.Current);

    Assert.True(enumerator.MoveNext());
    Assert.AreEqual(10, enumerator.Current);

    Assert.True(enumerator.MoveNext());
    Assert.AreEqual(15, enumerator.Current);

    Assert.False(enumerator.MoveNext());
  }

  /// <summary> Dispose clears the list. </summary>
  [Test]
  public void Dispose_ClearsList()
  {
    FastList<int> list = new();
    list.Add(1);
    list.Add(2);
    list.Add(3);

    list.Dispose();

    Assert.AreEqual(0, list.Count);
    Assert.AreEqual(0, list.Capacity);
  }

  /// <summary> Empty list operations. </summary>
  [Test]
  public void EmptyList_Operations()
  {
    FastList<int> list = new();

    Assert.AreEqual(0, list.Count);
    Assert.False(list.Contains(1));
    Assert.AreEqual(-1, list.IndexOf(1));
    Assert.False(list.Remove(1));
    Assert.False(list.RemoveAt(0));
    Assert.False(list.RemoveLast());
    Assert.Throws<ArgumentOutOfRangeException>(() => _ = list[0]);
  }

  /// <summary> Single element operations. </summary>
  [Test]
  public void SingleElement_Operations()
  {
    FastList<int> list = new();
    list.Add(42);

    Assert.AreEqual(1, list.Count);
    Assert.AreEqual(42, list[0]);
    Assert.True(list.Contains(42));
    Assert.AreEqual(0, list.IndexOf(42));

    list[0] = 99;
    Assert.AreEqual(99, list[0]);

    Assert.True(list.RemoveLast());
    Assert.AreEqual(0, list.Count);
  }
}
