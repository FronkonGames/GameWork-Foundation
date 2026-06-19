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
using UnityEngine;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> Extensions test. </summary>
public partial class ExtensionsTests
{
  /// <summary> Vector2 extensions test. </summary>
  [UnityTest]
  public IEnumerator Vector2()
  {
    Vector2 positive = new(1.0f, 2.0f);
    Vector2 negative = new(-1.0f, -2.0f);

    Assert.AreEqual(new Vector2(1.0f, 2.0f), positive.Abs());
    Assert.AreEqual(new Vector2(1.0f, 2.0f), negative.Abs());

    Assert.AreEqual(new Vector2(2.0f, 3.0f), new Vector2(1.1f, 2.1f).Ceil());
    Assert.AreEqual(new Vector2(1.0f, 2.0f), new Vector2(1.0f, 2.0f).Ceil());

    Assert.AreEqual(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f).Clamp01());
    Assert.AreEqual(new Vector2(0.0f, 0.0f), new Vector2(-1.0f, -2.0f).Clamp01());
    Assert.AreEqual(new Vector2(1.0f, 1.0f), new Vector2(2.0f, 3.0f).Clamp01());

    Assert.AreEqual(new Vector2(1.0f, 2.0f), new Vector2(1.5f, 2.5f).Floor());
    Assert.AreEqual(new Vector2(-2.0f, -3.0f), new Vector2(-1.5f, -2.5f).Floor());

    Assert.AreEqual(new Vector2(2.0f, 3.0f), new Vector2(1.6f, 2.6f).Rounded());
    Assert.AreEqual(new Vector2(1.0f, 2.0f), new Vector2(1.4f, 2.4f).Rounded());

    Assert.AreEqual(new Vector2(1.0f, 2.0f), new Vector2(5.0f, 6.0f).Remainder(new Vector2(4.0f, 4.0f)));

    Assert.IsTrue(positive.NearlyEquals(new Vector2(1.0f, 2.0f)));
    Assert.IsFalse(positive.NearlyEquals(new Vector2(1.1f, 2.1f)));

    Assert.AreEqual("(1.00, 2.00)", positive.ToString());

    Assert.AreEqual(new Vector2(1.0f, 2.0f), new Vector2(1.0f, 2.0f).Clamp(new Vector2(0.0f, 0.0f), new Vector2(3.0f, 3.0f)));
    Assert.AreEqual(new Vector2(0.0f, 0.0f), new Vector2(-1.0f, -2.0f).Clamp(UnityEngine.Vector2.zero, UnityEngine.Vector2.one));
    Assert.AreEqual(new Vector2(1.0f, 1.0f), new Vector2(5.0f, 6.0f).Clamp(UnityEngine.Vector2.zero, UnityEngine.Vector2.one));

    Vector2 remapValue = new(5.0f, 25.0f);
    Vector2 remapped = remapValue.Remap(UnityEngine.Vector2.zero, new Vector2(10.0f, 50.0f), new Vector2(100.0f, 0.0f), new Vector2(200.0f, 10.0f));
    Assert.AreEqual(150.0f, remapped.x, 0.01f);
    Assert.AreEqual(5.0f, remapped.y, 0.01f);

    Vector2 remapped01 = remapValue.Remap01(UnityEngine.Vector2.zero, new Vector2(10.0f, 50.0f));
    Assert.AreEqual(0.5f, remapped01.x, 0.01f);
    Assert.AreEqual(0.5f, remapped01.y, 0.01f);

    Vector2 multiplied = new Vector2(2.0f, 3.0f).Multiply(2.5);
    Assert.AreEqual(5.0f, multiplied.x, 0.01f);
    Assert.AreEqual(7.5f, multiplied.y, 0.01f);

    Vector2[] vectors = { new(2.0f, 4.0f), new(4.0f, 6.0f), new(6.0f, 8.0f) };
    Vector2 average = vectors.Average();
    Assert.AreEqual(4.0f, average.x, 0.01f);
    Assert.AreEqual(6.0f, average.y, 0.01f);

    Vector3 toVector3 = new Vector2(1.0f, 2.0f).ToVector3(3.0f);
    Assert.AreEqual(3.0f, toVector3.z, 0.01f);

    Vector2 swizzled = new Vector2(1.0f, 2.0f).SwizzleYX();
    Assert.AreEqual(2.0f, swizzled.x, 0.01f);
    Assert.AreEqual(1.0f, swizzled.y, 0.01f);

    Vector2Int compareLess = new(1, 5);
    Assert.Less(compareLess.CompareTo(new Vector2Int(2, 1)), 0);
    Assert.AreEqual(0, new Vector2Int(3, 4).CompareTo(new Vector2Int(3, 4)));
    Assert.AreEqual(3, new Vector2Int(1, 2).ToVector3Int(3).z);
    Assert.AreEqual(6, new Vector2Int(1, 2).AdjAll(5).x);
    Assert.IsTrue(new List<Vector2> { new(2, 4), new(4, 8) }.TryCalculateAverage(out Vector2 avg));
    Assert.AreEqual(3.0f, avg.x, 0.01f);

    Vector2 rotated = new Vector2(1.0f, 0.0f).Rotate(MathConstants.PiHalf);
    Assert.IsTrue(rotated.x.NearlyEquals(0.0f, 0.01f));
    Assert.IsTrue(rotated.y.NearlyEquals(1.0f, 0.01f));

    yield return null;
  }
}
