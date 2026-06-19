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
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> Extensions test. </summary>
public partial class ExtensionsTests
{
  /// <summary> Enumerable extensions test. </summary>
  [UnityTest]
  public IEnumerator Enumerable()
  {
    List<int> empty = new();
    List<int> single = new() { 1 };
    List<int> multiple = new() { 1, 2, 3, 4, 5 };

    Assert.AreEqual(0, empty.Count());
    Assert.AreEqual(1, single.Count());
    Assert.AreEqual(5, multiple.Count());

    LinkedList<int> list = new(new[] { 1, 2, 3 });
    List<int> nodeValues = new();
    foreach (LinkedListNode<int> node in list.EnumerateNodes())
      nodeValues.Add(node.Value);
    CollectionAssert.AreEqual(new[] { 1, 2, 3 }, nodeValues);

    List<int> rangeValues = new();
    foreach (int i in (2..5))
      rangeValues.Add(i);
    CollectionAssert.AreEqual(new[] { 2, 3, 4 }, rangeValues);

    List<int> countValues = new();
    foreach (int i in 3)
      countValues.Add(i);
    CollectionAssert.AreEqual(new[] { 0, 1, 2 }, countValues);

    Dictionary<string, int> lookup = new() { { "a", 1 }, { "b", 2 } };
    List<int> pulled = new(new[] { "a", "missing", "b" }.TryPullFromDictionary(lookup));
    CollectionAssert.AreEqual(new[] { 1, 2 }, pulled);

    List<int[]> buffered = new(new int[] { 1, 2, 3, 4, 5 }.Buffer(2));
    Assert.AreEqual(2, buffered.Count);
    CollectionAssert.AreEqual(new[] { 1, 2 }, buffered[0]);
    CollectionAssert.AreEqual(new[] { 3, 4 }, buffered[1]);

    List<IList<int>> windows = new(new int[] { 1, 2, 3, 4 }.RollingWindow(2));
    Assert.AreEqual(3, windows.Count);
    CollectionAssert.AreEqual(new[] { 1, 2 }, windows[0]);
    CollectionAssert.AreEqual(new[] { 2, 3 }, windows[1]);
    CollectionAssert.AreEqual(new[] { 3, 4 }, windows[2]);

    List<string> selected = new(new[] { 1, 2, 3, 4 }.SelectWhere(value => (value % 2 == 0, $"even-{value}")));
    CollectionAssert.AreEqual(new[] { "even-2", "even-4" }, selected);

    yield return null;
  }
}
