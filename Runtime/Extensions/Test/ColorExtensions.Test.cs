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
/// Extensions test.
/// </summary>
public partial class ExtensionsTests
{
  /// <summary>
  /// Color extensions test.
  /// </summary>
  [UnityTest]
  public IEnumerator Color()
  {
    Assert.AreEqual(UnityEngine.Color.magenta.ToHex(), "FF00FF");

    Assert.IsTrue(UnityEngine.Color.black.IsApproximatelyBlack());
    Assert.IsFalse(UnityEngine.Color.white.IsApproximatelyBlack());
    Assert.IsTrue((UnityEngine.Color.white * MathConstants.Epsilon).IsApproximatelyBlack());

    Assert.IsFalse(UnityEngine.Color.black.IsApproximatelyWhite());
    Assert.IsTrue(UnityEngine.Color.white.IsApproximatelyWhite());
    Assert.IsTrue((UnityEngine.Color.white * (1.0f - MathConstants.Epsilon)).IsApproximatelyWhite());

    Assert.AreEqual(UnityEngine.Color.white.Invert(), UnityEngine.Color.black);

    yield return null;
  }
}
