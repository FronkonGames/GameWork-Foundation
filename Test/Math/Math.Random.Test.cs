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
using UnityEngine;

/// <summary>
/// Math tests.
/// </summary>
public partial class MathTests
{
  private const int Tries = 10000;

  /// <summary>
  /// Random extensions test.
  /// </summary>
  [UnityTest]
  public IEnumerator Random()
  {
    // 1D
    for (int i = 0; i < Tries; ++i)
    {
      float value = Rand.Value;
      
      Assert.IsTrue(value >= 0.0f && value <= 1.0f);
    }

    for (int i = 0; i < Tries; ++i)
    {
      float sign = Rand.Sign;
      
      Assert.IsTrue(sign.NearlyEquals(1.0f) || sign.NearlyEquals(-1.0f));
    }

    for (int i = 0; i < Tries; ++i)
    {
      float dir = Rand.Direction1D;
      
      Assert.IsTrue(dir.NearlyEquals(1.0f) || dir.NearlyEquals(-1.0f));
    }

    const float min = 0.0f;
    const float max = 10.0f;
    for (int i = 0; i < Tries; ++i)
    {
      float value = Rand.Range(min, max);

      Assert.IsTrue(value >= min && value <= max);
    }

    // Int range
    for (int i = 0; i < Tries; ++i)
    {
      int value = Rand.Range(1, 6);

      Assert.IsTrue(value >= 1 && value <= 6);
    }

    // Dice
    for (int i = 0; i < Tries; ++i)
    {
      Assert.IsTrue(Rand.D4() >= 1 && Rand.D4() <= 4);
      Assert.IsTrue(Rand.D6() >= 1 && Rand.D6() <= 6);
      Assert.IsTrue(Rand.D10() >= 1 && Rand.D10() <= 10);
      Assert.IsTrue(Rand.D20() >= 1 && Rand.D20() <= 20);
      Assert.IsTrue(Rand.D100() >= 1 && Rand.D100() <= 100);
    }

    // 2D
    for (int i = 0; i < Tries; ++i)
    {
      Vector2 circle = Rand.OnUnitCircle;

      Assert.IsTrue(circle.sqrMagnitude.NearlyEquals(1.0f, 0.01f));
    }

    for (int i = 0; i < Tries; ++i)
    {
      Vector2 dir2D = Rand.Direction2D;

      Assert.IsTrue(dir2D.sqrMagnitude.NearlyEquals(1.0f, 0.01f));
    }

    for (int i = 0; i < Tries; ++i)
    {
      Vector2 inCircle = Rand.InUnitCircle;

      Assert.IsTrue(inCircle.sqrMagnitude <= 1.0f);
    }

    for (int i = 0; i < Tries; ++i)
    {
      Vector2 inSquare = Rand.InUnitSquare;

      Assert.IsTrue(inSquare.x >= 0.0f && inSquare.x <= 1.0f);
      Assert.IsTrue(inSquare.y >= 0.0f && inSquare.y <= 1.0f);
    }

    // 3D
    for (int i = 0; i < Tries; ++i)
    {
      Vector3 sphere = Rand.OnUnitSphere;

      Assert.IsTrue(sphere.sqrMagnitude.NearlyEquals(1.0f, 0.01f));
    }

    for (int i = 0; i < Tries; ++i)
    {
      Vector3 dir3D = Rand.Direction3D;

      Assert.IsTrue(dir3D.sqrMagnitude.NearlyEquals(1.0f, 0.01f));
    }

    for (int i = 0; i < Tries; ++i)
    {
      Vector3 inSphere = Rand.InUnitSphere;

      Assert.IsTrue(inSphere.sqrMagnitude <= 1.0f);
    }

    for (int i = 0; i < Tries; ++i)
    {
      Vector3 inCube = Rand.InUnitCube;

      Assert.IsTrue(inCube.x >= 0.0f && inCube.x <= 1.0f);
      Assert.IsTrue(inCube.y >= 0.0f && inCube.y <= 1.0f);
      Assert.IsTrue(inCube.z >= 0.0f && inCube.z <= 1.0f);
    }

    // Angle
    for (int i = 0; i < Tries; ++i)
    {
      float angle = Rand.Angle;

      Assert.IsTrue(angle >= 0.0f && angle <= FronkonGames.GameWork.Foundation.MathConstants.Tau);
    }

    // Rotation
    for (int i = 0; i < Tries; ++i)
    {
      Quaternion rotation = Rand.Rotation;

      Assert.IsTrue(Mathf.Abs(Quaternion.Dot(rotation, rotation) - 1.0f) < 0.01f);
    }

    Assert.AreEqual(0, Rand.PickWeighted(new[] { 1.0f }));
    Assert.AreEqual(0, Rand.PickWeighted(new[] { 10.0f, 0.0f, 0.0f }));

    int firstWeightHits = 0;
    for (int i = 0; i < Tries; ++i)
    {
      if (Rand.PickWeighted(new[] { 100.0f, 1.0f }) == 0)
        firstWeightHits++;
    }

    Assert.Greater(firstWeightHits, Tries * 0.9f);

    float normalSum = 0.0f;
    const float mean = 5.0f;
    const float stdDev = 2.0f;
    for (int i = 0; i < Tries; ++i)
      normalSum += Rand.NextNormal(mean, stdDev);

    Assert.AreEqual(mean, normalSum / Tries, 0.25f);

    Assert.IsTrue(Rand.Chance(100.0f, 100.0f));
    Assert.IsFalse(Rand.Chance(0.0f, 100.0f));
    Assert.IsTrue(Rand.ChancePercent(100.0f));
    Assert.IsFalse(Rand.ChancePercent(0.0f));

    for (int i = 0; i < Tries; ++i)
    {
      float lerped = Rand.Lerp(0.0f, 10.0f);
      Assert.IsTrue(lerped >= 0.0f && lerped <= 10.0f);

      Vector3 lerpedVector = Rand.Lerp(Vector3.zero, Vector3.one);
      Assert.IsTrue(lerpedVector.x >= 0.0f && lerpedVector.x <= 1.0f);
    }

    yield return null;
  }
}
