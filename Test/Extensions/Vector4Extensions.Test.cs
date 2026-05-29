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
  /// <summary> Vector4 extensions test. </summary>
  [UnityTest]
  public IEnumerator Vector4()
  {
    Vector4 positive = new(1.0f, 2.0f, 3.0f, 4.0f);
    Vector4 negative = new(-1.0f, -2.0f, -3.0f, -4.0f);

    Assert.AreEqual(new Vector4(1.0f, 2.0f, 3.0f, 4.0f), positive.Abs());
    Assert.AreEqual(new Vector4(1.0f, 2.0f, 3.0f, 4.0f), negative.Abs());

    Assert.AreEqual(new Vector4(2.0f, 3.0f, 4.0f, 5.0f), new Vector4(1.1f, 2.1f, 3.1f, 4.1f).Ceil());
    Assert.AreEqual(new Vector4(1.0f, 2.0f, 3.0f, 4.0f), new Vector4(1.0f, 2.0f, 3.0f, 4.0f).Ceil());

    Assert.AreEqual(new Vector4(0.5f, 0.5f, 0.5f, 0.5f), new Vector4(0.5f, 0.5f, 0.5f, 0.5f).Clamp01());
    Assert.AreEqual(UnityEngine.Vector4.zero, new Vector4(-1.0f, -2.0f, -3.0f, -4.0f).Clamp01());
    Assert.AreEqual(UnityEngine.Vector4.one, new Vector4(2.0f, 3.0f, 4.0f, 5.0f).Clamp01());

    Assert.AreEqual(new Vector4(1.0f, 2.0f, 3.0f, 4.0f), new Vector4(1.5f, 2.5f, 3.5f, 4.5f).Floor());
    Assert.AreEqual(new Vector4(-2.0f, -3.0f, -4.0f, -5.0f), new Vector4(-1.5f, -2.5f, -3.5f, -4.5f).Floor());

    Assert.AreEqual(new Vector4(2.0f, 3.0f, 4.0f, 5.0f), new Vector4(1.6f, 2.6f, 3.6f, 4.6f).Rounded());
    Assert.AreEqual(new Vector4(1.0f, 2.0f, 3.0f, 4.0f), new Vector4(1.4f, 2.4f, 3.4f, 4.4f).Rounded());

    Assert.AreEqual(new Vector4(1.0f, 2.0f, 3.0f, 0.0f), new Vector4(5.0f, 6.0f, 7.0f, 8.0f).Remainder(new Vector4(4.0f, 4.0f, 4.0f, 4.0f)));

    Assert.IsTrue(positive.NearlyEquals(new Vector4(1.0f, 2.0f, 3.0f, 4.0f)));
    Assert.IsFalse(positive.NearlyEquals(new Vector4(1.1f, 2.1f, 3.1f, 4.1f)));

    Assert.AreEqual("(1.00, 2.00, 3.00, 4.00)", positive.ToString());

    Assert.AreEqual(new Vector4(1.0f, 2.0f, 3.0f, 4.0f), new Vector4(1.0f, 2.0f, 3.0f, 4.0f).Clamp(UnityEngine.Vector4.zero, new Vector4(5.0f, 5.0f, 5.0f, 5.0f)));
    Assert.AreEqual(UnityEngine.Vector4.zero, new Vector4(-1.0f, -2.0f, -3.0f, -4.0f).Clamp(UnityEngine.Vector4.zero, UnityEngine.Vector4.one));
    Assert.AreEqual(UnityEngine.Vector4.one, new Vector4(5.0f, 6.0f, 7.0f, 8.0f).Clamp(UnityEngine.Vector4.zero, UnityEngine.Vector4.one));

    yield return null;
  }
}
