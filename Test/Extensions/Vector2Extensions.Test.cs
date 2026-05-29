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

    yield return null;
  }
}
