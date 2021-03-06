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
using NUnit.Framework;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary>
/// Data tests.
/// </summary>
public partial class DataTests
{
  /// <summary>
  /// FastList test.
  /// </summary>
  [UnityTest]
  public IEnumerator FastList()
  {
    FastList<int> fastList = new FastList<int>();
    Assert.AreEqual(fastList.Count, 0);

    fastList.Add(1);
    Assert.AreEqual(fastList.Count, 1);
    Assert.AreEqual(fastList[0], 1);

    fastList.Clear();
    Assert.AreEqual(fastList.Count, 0);

    int[] range = { 0, 1, 3 };
    fastList.AddRange(range);
    Assert.AreEqual(fastList.Count, 3);
    Assert.AreEqual(fastList[0], 0);
    Assert.AreEqual(fastList[1], 1);
    Assert.AreEqual(fastList[2], 3);
    Assert.IsTrue(fastList.Contains(0));
    Assert.IsFalse(fastList.Contains(4));
    Assert.AreEqual(fastList.IndexOf(3), 2);
    Assert.AreEqual(fastList.IndexOf(4), -1);

    fastList.Insert(2, 2);
    Assert.AreEqual(fastList.Count, 4);
    Assert.AreEqual(fastList[2], 2);
    Assert.AreEqual(fastList[3], 3);

    fastList.Remove(3);
    Assert.AreEqual(fastList.Count, 3);
    Assert.AreEqual(fastList.IndexOf(3), -1);
    Assert.AreEqual(fastList[2], 2);

    fastList.RemoveAt(0);
    Assert.AreEqual(fastList.Count, 2);
    Assert.AreEqual(fastList[0], 1);

    yield return null;
  }
}
