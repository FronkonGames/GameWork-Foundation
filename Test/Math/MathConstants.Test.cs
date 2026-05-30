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

/// <summary> Math tests. </summary>
public partial class MathTests
{
  /// <summary> MathConstants test. </summary>
  [UnityTest]
  public IEnumerator MathConstantsValues()
  {
    Assert.AreEqual(3.14159265358979f, MathConstants.Pi, 0.0001f);
    Assert.AreEqual(MathConstants.Pi * 0.5f, MathConstants.PiHalf, 0.0001f);
    Assert.AreEqual(MathConstants.Pi * 2.0f, MathConstants.Pi2, 0.0001f);
    Assert.AreEqual(2.71828182846f, MathConstants.E, 0.0001f);
    Assert.AreEqual(6.28318530717959f, MathConstants.Tau, 0.0001f);
    Assert.AreEqual(1.61803398875f, MathConstants.GoldenRatio, 0.0001f);

    Assert.AreEqual(MathConstants.Tau / 360.0f, MathConstants.Deg2Rad, 0.0001f);
    Assert.AreEqual(360.0f / MathConstants.Tau, MathConstants.Rad2Deg, 0.0001f);

    Assert.AreEqual(1.0f / 3.0f, MathConstants.OneThird, 0.0001f);
    Assert.AreEqual(2.0f / 3.0f, MathConstants.TwoThirds, 0.0001f);
    Assert.AreEqual(1.0f / 6.0f, MathConstants.OneSixth, 0.0001f);

    Assert.AreEqual(float.PositiveInfinity, MathConstants.Infinity);
    Assert.AreEqual(float.NegativeInfinity, MathConstants.NegativeInfinity);

    Assert.IsTrue(float.IsNaN(MathConstants.NaNVector2.x));
    Assert.IsTrue(float.IsNaN(MathConstants.NaNVector3.x));
    Assert.IsTrue(float.IsNaN(MathConstants.NaNVector4.x));

    Assert.AreEqual(MathConstants.Infinity, MathConstants.InfinityVector2.x);
    Assert.AreEqual(MathConstants.Infinity, MathConstants.InfinityVector3.x);
    Assert.AreEqual(MathConstants.Infinity, MathConstants.InfinityVector4.x);

    yield return null;
  }
}
