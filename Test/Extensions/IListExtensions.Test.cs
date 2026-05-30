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

/// <summary>
/// IList extensions test.
/// </summary>
[TestFixture]
public class IListExtensionsTests
{
  [Test]
  public void At2D_ReturnsCorrectElement()
  {
    List<int> grid = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    // 3x3 grid:
    // 0 1 2
    // 3 4 5
    // 6 7 8

    Assert.AreEqual(0, grid.At2D(0, 0, 3, 3));
    Assert.AreEqual(4, grid.At2D(1, 1, 3, 3));
    Assert.AreEqual(8, grid.At2D(2, 2, 3, 3));
    Assert.AreEqual(3, grid.At2D(0, 1, 3, 3));
    Assert.AreEqual(5, grid.At2D(2, 1, 3, 3));
    Assert.AreEqual(6, grid.At2D(0, 2, 3, 3));
  }

  [Test]
  public void At2D_ThrowsForOutOfBounds()
  {
    List<int> grid = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

    Assert.Throws<IndexOutOfRangeException>(() => grid.At2D(-1, 0, 3, 3));
    Assert.Throws<IndexOutOfRangeException>(() => grid.At2D(3, 0, 3, 3));
    Assert.Throws<IndexOutOfRangeException>(() => grid.At2D(0, -1, 3, 3));
    Assert.Throws<IndexOutOfRangeException>(() => grid.At2D(0, 3, 3, 3));
  }

  [Test]
  public void At3D_ReturnsCorrectElement()
  {
    List<int> grid = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
    // 2x3x4 grid (width=2, height=3, depth=4)
    // z=0: 0 1, z=1: 2 3, ... indexing: (z * height + y) * width + x

    Assert.AreEqual(0, grid.At3D(0, 0, 0, 2, 3, 4));
    Assert.AreEqual(1, grid.At3D(1, 0, 0, 2, 3, 4));
    Assert.AreEqual(2, grid.At3D(0, 1, 0, 2, 3, 4));
    Assert.AreEqual(11, grid.At3D(1, 2, 1, 2, 3, 4));
    Assert.AreEqual(23, grid.At3D(1, 2, 3, 2, 3, 4));
  }

  [Test]
  public void At3D_ThrowsForOutOfBounds()
  {
    List<int> grid = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

    Assert.Throws<IndexOutOfRangeException>(() => grid.At3D(-1, 0, 0, 2, 3, 4));
    Assert.Throws<IndexOutOfRangeException>(() => grid.At3D(2, 0, 0, 2, 3, 4));
    Assert.Throws<IndexOutOfRangeException>(() => grid.At3D(0, -1, 0, 2, 3, 4));
    Assert.Throws<IndexOutOfRangeException>(() => grid.At3D(0, 3, 0, 2, 3, 4));
    Assert.Throws<IndexOutOfRangeException>(() => grid.At3D(0, 0, -1, 2, 3, 4));
    Assert.Throws<IndexOutOfRangeException>(() => grid.At3D(0, 0, 4, 2, 3, 4));
  }

  [Test]
  public void TryAt2D_ReturnsTrueAndValueWhenInBounds()
  {
    List<int> grid = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

    Assert.IsTrue(grid.TryAt2D(0, 0, 3, 3, out int v0));
    Assert.AreEqual(0, v0);

    Assert.IsTrue(grid.TryAt2D(1, 1, 3, 3, out int v1));
    Assert.AreEqual(4, v1);

    Assert.IsTrue(grid.TryAt2D(2, 2, 3, 3, out int v2));
    Assert.AreEqual(8, v2);
  }

  [Test]
  public void TryAt2D_ReturnsFalseWhenOutOfBounds()
  {
    List<int> grid = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

    Assert.IsFalse(grid.TryAt2D(-1, 0, 3, 3, out int v0));
    Assert.AreEqual(default(int), v0);

    Assert.IsFalse(grid.TryAt2D(3, 0, 3, 3, out int v1));
    Assert.AreEqual(default(int), v1);

    Assert.IsFalse(grid.TryAt2D(0, -1, 3, 3, out int v2));
    Assert.AreEqual(default(int), v2);

    Assert.IsFalse(grid.TryAt2D(0, 3, 3, 3, out int v3));
    Assert.AreEqual(default(int), v3);
  }

  [Test]
  public void TryAt3D_ReturnsTrueAndValueWhenInBounds()
  {
    List<int> grid = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

    Assert.IsTrue(grid.TryAt3D(0, 0, 0, 2, 3, 4, out int v0));
    Assert.AreEqual(0, v0);

    Assert.IsTrue(grid.TryAt3D(1, 2, 3, 2, 3, 4, out int v1));
    Assert.AreEqual(23, v1);
  }

  [Test]
  public void TryAt3D_ReturnsFalseWhenOutOfBounds()
  {
    List<int> grid = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

    Assert.IsFalse(grid.TryAt3D(-1, 0, 0, 2, 3, 4, out int v0));
    Assert.AreEqual(default(int), v0);

    Assert.IsFalse(grid.TryAt3D(2, 0, 0, 2, 3, 4, out int v1));
    Assert.AreEqual(default(int), v1);

    Assert.IsFalse(grid.TryAt3D(0, 0, 4, 2, 3, 4, out int v2));
    Assert.AreEqual(default(int), v2);
  }

  [Test]
  public void Set2D_SetsCorrectElement()
  {
    List<int> grid = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

    grid.Set2D(0, 0, 3, 3, 100);
    Assert.AreEqual(100, grid[0]);

    grid.Set2D(1, 1, 3, 3, 200);
    Assert.AreEqual(200, grid[4]);

    grid.Set2D(2, 2, 3, 3, 300);
    Assert.AreEqual(300, grid[8]);
  }

  [Test]
  public void Set3D_SetsCorrectElement()
  {
    List<int> grid = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

    grid.Set3D(0, 0, 0, 2, 3, 4, 100);
    Assert.AreEqual(100, grid[0]);

    grid.Set3D(1, 2, 3, 2, 3, 4, 200);
    Assert.AreEqual(200, grid[23]);

    grid.Set3D(1, 1, 2, 2, 3, 4, 300);
    Assert.AreEqual(300, grid[(2 * 3 + 1) * 2 + 1]);
  }
}
