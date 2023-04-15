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

/// <summary> Extensions test. </summary>
public partial class ExtensionsTests
{
  /// <summary> Color extensions test. </summary>
  [UnityTest]
  public IEnumerator Color()
  {
    Assert.AreEqual("#FF00FF", UnityEngine.Color.magenta.ToHex());

    Assert.IsTrue(UnityEngine.Color.black.IsApproximatelyBlack());
    Assert.IsFalse(UnityEngine.Color.white.IsApproximatelyBlack());
    Assert.IsTrue((UnityEngine.Color.white * MathConstants.Epsilon).IsApproximatelyBlack());

    Assert.IsFalse(UnityEngine.Color.black.IsApproximatelyWhite());
    Assert.IsTrue(UnityEngine.Color.white.IsApproximatelyWhite());
    Assert.IsTrue((UnityEngine.Color.white * (1.0f - MathConstants.Epsilon)).IsApproximatelyWhite());

    Assert.AreEqual(UnityEngine.Color.black, UnityEngine.Color.white.Invert());

    Assert.AreEqual(new UnityEngine.Color(0.0f, 1.0f, 1.0f, 1.0f), UnityEngine.Color.white.SetR(0.0f));
    Assert.AreEqual(new UnityEngine.Color(1.0f, 0.0f, 1.0f, 1.0f), UnityEngine.Color.white.SetG(0.0f));
    Assert.AreEqual(new UnityEngine.Color(1.0f, 1.0f, 0.0f, 1.0f), UnityEngine.Color.white.SetB(0.0f));
    Assert.AreEqual(new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.0f), UnityEngine.Color.white.SetA(0.0f));

    Assert.AreEqual(UnityEngine.Color.red, UnityEngine.Color.red.SetHue(0.0f));
    Assert.AreEqual(UnityEngine.Color.red, UnityEngine.Color.red.SetHue(1.0f));
    Assert.AreEqual(UnityEngine.Color.white, UnityEngine.Color.white.SetSaturation(0.0f));
    Assert.AreEqual(UnityEngine.Color.red, UnityEngine.Color.red.SetSaturation(1.0f));
    Assert.AreEqual(UnityEngine.Color.white, UnityEngine.Color.red.SetSaturation(0.0f));
    
    yield return null;
  }
}
