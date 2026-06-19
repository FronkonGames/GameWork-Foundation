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

/// <summary> Math tests. </summary>
public partial class MathTests
{
  /// <summary> MathUtils curve and angle clamp tests. </summary>
  [UnityTest]
  public IEnumerator MathUtilsCurveAndAngles()
  {
    Vector3 start = Vector3.zero;
    Vector3 end = Vector3.right;
    Vector3 midpoint = MathUtils.Bezier(start, end, 0.5f);

    Assert.AreEqual(0.5f, midpoint.x, 0.001f);
    Assert.AreEqual(0.0f, midpoint.y, 0.001f);
    Assert.AreEqual(0.0f, midpoint.z, 0.001f);

    Vector3 curved = MathUtils.Bezier(start, end, 0.5f, new[] { Vector3.up });
    Assert.Greater(curved.y, 0.0f);

    Vector2 start2D = Vector2.zero;
    Vector2 end2D = Vector2.right;
    Vector2 midpoint2D = MathUtils.Bezier(start2D, end2D, 0.5f);
    Assert.AreEqual(0.5f, midpoint2D.x, 0.001f);

    Assert.AreEqual(45.0f, MathUtils.ClampAngle(45.0f, 0.0f, 90.0f), 0.001f);
    Assert.AreEqual(90.0f, MathUtils.ClampAngle(100.0f, 0.0f, 90.0f), 0.001f);
    Assert.AreEqual(0.0f, MathUtils.ClampAngleNormalized(0.0f, -45.0f, 45.0f), 0.001f);
    Assert.AreEqual(45.0f, MathUtils.ClampAngleNormalized(100.0f, -45.0f, 45.0f), 0.001f);

    yield return null;
  }
}
