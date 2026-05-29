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

/// <summary> Patterns tests. </summary>
public partial class PatternsTests
{
  /// <summary> Object Pool test. </summary>
  [UnityTest]
  public IEnumerator ObjectPool()
  {
    int createCount = 0;
    int getCount = 0;
    int releaseCount = 0;

    ObjectPool<string> pool = new(
      createFunc: () => $"Item_{createCount++}",
      onGet: item => getCount++,
      onRelease: item => releaseCount++,
      initialSize: 3
    );

    Assert.AreEqual(3, pool.CountAvailable);

    string item1 = pool.Get();
    Assert.AreEqual("Item_2", item1);
    Assert.AreEqual(2, pool.CountAvailable);

    string item2 = pool.Get();
    Assert.AreEqual("Item_1", item2);
    string item3 = pool.Get();
    Assert.AreEqual("Item_0", item3);
    string item4 = pool.Get();
    Assert.AreEqual("Item_3", item4);

    pool.Release(item1);
    pool.Release(item2);
    Assert.AreEqual(2, pool.CountAvailable);

    string item5 = pool.Get();
    Assert.AreEqual("Item_1", item5);

    pool.Clear();
    Assert.AreEqual(0, pool.CountAvailable);

    yield return null;
  }
}
