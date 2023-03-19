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
/// Extensions tests.
/// </summary>
public partial class ExtensionsTests
{
  /// <summary>
  /// Float extensions test.
  /// </summary>
  [UnityTest]
  public IEnumerator Float()
  {
    Assert.AreEqual(0.0f.Sign(), 1.0f);
    Assert.AreEqual(1.0f.Sign(), 1.0f);
    Assert.AreEqual((-1.0f).Sign(), -1.0f);

    Assert.AreEqual(0.5f.Ceil(), 1.0f);
    Assert.AreEqual((-0.5f).Ceil(), 0.0f);
    Assert.AreEqual(1.0f.Ceil(), 1.0f);
    Assert.AreEqual((-1.0f).Ceil(), -1.0f);

    Assert.AreEqual(0.5f.Round(), 0.0f);
    Assert.AreEqual(0.51f.Round(), 1.0f);
    Assert.AreEqual((-0.5f).Round(), 0.0f);
    Assert.AreEqual((-0.51f).Round(), -1.0f);

    Assert.AreEqual(0.5f.ToIntFloor(), 0);
    Assert.AreEqual(0.9f.ToIntFloor(), 0);
    Assert.AreEqual((-0.5f).ToIntFloor(), -1);
    Assert.AreEqual((-0.9f).ToIntFloor(), -1);
    Assert.AreEqual((-1.5f).ToIntFloor(), -2);

    Assert.AreEqual(0.5f.ToIntCeil(), 1);
    Assert.AreEqual(0.9f.ToIntCeil(), 1);
    Assert.AreEqual((-0.5f).ToIntCeil(), 0);
    Assert.AreEqual((-0.9f).ToIntCeil(), 0);
    Assert.AreEqual((-1.5f).ToIntCeil(), -1);

    Assert.AreEqual(0.5f.ToIntRound(), 0);
    Assert.AreEqual(0.9f.ToIntRound(), 1);
    Assert.AreEqual((-0.5f).ToIntRound(), 0);
    Assert.AreEqual((-0.9f).ToIntRound(), -1);
    Assert.AreEqual((-1.5f).ToIntRound(), -2);

    Assert.AreEqual(0.0f.Frac(), 0.0f);
    Assert.AreEqual(0.5f.Frac(), 0.5f);
    Assert.AreEqual(1.5f.Frac(), 0.5f);

    Assert.AreEqual(0.0f.Max(1.0f), 1.0f);
    Assert.AreEqual(1.0f.Max(0.0f), 1.0f);
    Assert.AreEqual(1.0f.Max(-1.0f), 1.0f);
    Assert.AreEqual(0.0f.Min(1.0f), 0.0f);
    Assert.AreEqual(1.0f.Min(0.0f), 0.0f);
    Assert.AreEqual(1.0f.Min(-1.0f), -1.0f);

    Assert.AreEqual(0.0f.Abs(), 0.0f);
    Assert.AreEqual(1.0f.Abs(), 1.0f);
    Assert.AreEqual((-1.0f).Abs(), 1.0f);

    Assert.AreEqual(0.5f.Snap(1.0f), 0.0f);
    Assert.AreEqual(0.6f.Snap(1.0f), 1.0f);
    Assert.AreEqual(1.1f.Snap(1.0f), 1.0f);
    Assert.AreEqual(1.9f.Snap(1.0f), 2.0f);
    
    Assert.AreEqual(1.0f.Clamp(0.0f, 2.0f), 1.0f);
    Assert.AreEqual((-1.0f).Clamp(0.0f, 2.0f), 0.0f);
    Assert.AreEqual(3.0f.Clamp(0.0f, 2.0f), 2.0f);

    Assert.AreEqual(0.5f.Clamp01(), 0.5f);
    Assert.AreEqual(2.0f.Clamp01(), 1.0f);
    Assert.AreEqual((-1.0f).Clamp01(), 0.0f);

    Assert.IsTrue(1.0f.NearlyEquals(1.0f));
    Assert.IsFalse(0.0f.NearlyEquals(0.0001f));
    Assert.IsTrue(0.0f.NearlyEquals(MathConstants.Epsilon * 0.5f));

    Assert.IsTrue(0.0f.NearlyEquals(1.0f, 2.0f));
    Assert.IsFalse(0.0f.NearlyEquals(2.0f, 1.0f));

    Assert.IsTrue(0.5f.IsBetween(0.0f, 1.0f));
    Assert.IsTrue(0.0f.IsBetween(0.0f, 1.0f));
    Assert.IsTrue(1.0f.IsBetween(0.0f, 1.0f));
    Assert.IsFalse(2.0f.IsBetween(0.0f, 1.0f));
    Assert.IsFalse((-2.0f).IsBetween(0.0f, 1.0f));

    Assert.IsTrue(0.5f.IsBetweenExclusive(0.0f, 1.0f));
    Assert.IsFalse(0.0f.IsBetweenExclusive(0.0f, 1.0f));
    Assert.IsFalse(1.0f.IsBetweenExclusive(0.0f, 1.0f));
    Assert.IsFalse(2.0f.IsBetweenExclusive(0.0f, 1.0f));
    Assert.IsFalse((-2.0f).IsBetweenExclusive(0.0f, 1.0f));

    Assert.AreEqual(3.1415f.ToInvariantCulture(), "3.1415");
    
    yield return null;
  }
}
