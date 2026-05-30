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

namespace FronkonGames.GameWork.Foundation.Tests
{
  /// <summary> List extensions new methods tests. </summary>
  [TestFixture]
  public class ListExtensionsNewTests
  {
    /// <summary> Pop removes and returns the first element. </summary>
    [Test]
    public void Pop_RemovesAndReturnsFirstElement()
    {
      List<int> list = new() { 10, 20, 30 };

      int result = list.Pop();

      Assert.AreEqual(10, result);
      Assert.AreEqual(2, list.Count);
      Assert.AreEqual(20, list[0]);
    }

    /// <summary> Pop on empty list returns default. </summary>
    [Test]
    public void Pop_EmptyList_ReturnsDefault()
    {
      List<int> list = new();

      Assert.AreEqual(default(int), list.Pop());
    }

    /// <summary> PopLast removes and returns the last element. </summary>
    [Test]
    public void PopLast_RemovesAndReturnsLastElement()
    {
      List<int> list = new() { 10, 20, 30 };

      int result = list.PopLast();

      Assert.AreEqual(30, result);
      Assert.AreEqual(2, list.Count);
      Assert.AreEqual(20, list[1]);
    }

    /// <summary> PopLast on empty list returns default. </summary>
    [Test]
    public void PopLast_EmptyList_ReturnsDefault()
    {
      List<int> list = new();

      Assert.AreEqual(default(int), list.PopLast());
    }

    /// <summary> HasIndex returns true for valid index. </summary>
    [Test]
    public void HasIndex_ValidIndex_ReturnsTrue()
    {
      List<int> list = new() { 1, 2, 3 };

      Assert.IsTrue(list.HasIndex(0));
      Assert.IsTrue(list.HasIndex(1));
      Assert.IsTrue(list.HasIndex(2));
    }

    /// <summary> HasIndex returns false for invalid index. </summary>
    [Test]
    public void HasIndex_InvalidIndex_ReturnsFalse()
    {
      List<int> list = new() { 1, 2, 3 };

      Assert.IsFalse(list.HasIndex(-1));
      Assert.IsFalse(list.HasIndex(3));
      Assert.IsFalse(list.HasIndex(100));
    }

    /// <summary> Get returns element at valid index. </summary>
    [Test]
    public void Get_ValidIndex_ReturnsElement()
    {
      List<int> list = new() { 10, 20, 30 };

      Assert.AreEqual(10, list.Get(0));
      Assert.AreEqual(20, list.Get(1));
      Assert.AreEqual(30, list.Get(2));
    }

    /// <summary> Get returns default for invalid index. </summary>
    [Test]
    public void Get_InvalidIndex_ReturnsDefault()
    {
      List<int> list = new() { 10, 20, 30 };

      Assert.AreEqual(default(int), list.Get(-1));
      Assert.AreEqual(default(int), list.Get(3));
      Assert.AreEqual(default(int), list.Get(100));
    }

    /// <summary> TryGet returns true and value for valid index. </summary>
    [Test]
    public void TryGet_ValidIndex_ReturnsTrueAndValue()
    {
      List<int> list = new() { 10, 20, 30 };

      bool found = list.TryGet(1, out int value);

      Assert.IsTrue(found);
      Assert.AreEqual(20, value);
    }

    /// <summary> TryGet returns false for invalid index. </summary>
    [Test]
    public void TryGet_InvalidIndex_ReturnsFalse()
    {
      List<int> list = new() { 10, 20, 30 };

      bool found = list.TryGet(-1, out int value);

      Assert.IsFalse(found);
      Assert.AreEqual(default(int), value);
    }

    /// <summary> Clone returns a new list with the same elements. </summary>
    [Test]
    public void Clone_ReturnsNewListWithSameElements()
    {
      List<int> list = new() { 1, 2, 3, 4, 5 };

      List<int> clone = list.Clone();

      Assert.AreNotSame(list, clone);
      Assert.AreEqual(list.Count, clone.Count);
      for (int i = 0; i < list.Count; i++)
        Assert.AreEqual(list[i], clone[i]);
    }

    /// <summary> Clone does not affect the original list. </summary>
    [Test]
    public void Clone_DoesNotAffectOriginal()
    {
      List<int> list = new() { 1, 2, 3 };

      List<int> clone = list.Clone();
      clone[0] = 999;

      Assert.AreEqual(1, list[0]);
    }

    /// <summary> FindRandom returns a matching element. </summary>
    [Test]
    public void FindRandom_WithPredicate_ReturnsMatchingElement()
    {
      List<int> list = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

      int result = list.FindRandom(x => x > 8);

      Assert.IsTrue(result > 8);
    }

    /// <summary> FindRandom returns default when no element matches. </summary>
    [Test]
    public void FindRandom_NoMatch_ReturnsDefault()
    {
      List<int> list = new() { 1, 2, 3 };

      int result = list.FindRandom(x => x > 100);

      Assert.AreEqual(default(int), result);
    }

    /// <summary> NextIndex returns next index cyclically. </summary>
    [Test]
    public void NextIndex_ReturnsNextIndexCyclically()
    {
      List<int> list = new() { 10, 20, 30 };

      Assert.AreEqual(1, list.NextIndex(0));
      Assert.AreEqual(2, list.NextIndex(1));
      Assert.AreEqual(0, list.NextIndex(2));
    }

    /// <summary> PreviousIndex returns previous index cyclically. </summary>
    [Test]
    public void PreviousIndex_ReturnsPreviousIndexCyclically()
    {
      List<int> list = new() { 10, 20, 30 };

      Assert.AreEqual(2, list.PreviousIndex(0));
      Assert.AreEqual(0, list.PreviousIndex(1));
      Assert.AreEqual(1, list.PreviousIndex(2));
    }

    /// <summary> Next returns next element cyclically. </summary>
    [Test]
    public void Next_ReturnsNextElementCyclically()
    {
      List<int> list = new() { 10, 20, 30 };

      Assert.AreEqual(20, list.Next(10));
      Assert.AreEqual(30, list.Next(20));
      Assert.AreEqual(10, list.Next(30));
    }

    /// <summary> Previous returns previous element cyclically. </summary>
    [Test]
    public void Previous_ReturnsPreviousElementCyclically()
    {
      List<int> list = new() { 10, 20, 30 };

      Assert.AreEqual(30, list.Previous(10));
      Assert.AreEqual(10, list.Previous(20));
      Assert.AreEqual(20, list.Previous(30));
    }

    [Test]
    public void UniqueCount_ReturnsDistinctElements()
    {
      List<int> list = new() { 1, 2, 2, 3, 3, 3 };

      Assert.AreEqual(3, list.UniqueCount());
    }

    [Test]
    public void AreAllUnique_ReturnsFalseWhenDuplicatesExist()
    {
      List<int> list = new() { 1, 2, 2 };

      Assert.IsFalse(list.AreAllUnique());
      Assert.IsTrue(new List<int> { 1, 2, 3 }.AreAllUnique());
    }
  }
}
