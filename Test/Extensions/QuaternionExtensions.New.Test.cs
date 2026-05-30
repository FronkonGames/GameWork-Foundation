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
using NUnit.Framework;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation.Tests
{
  /// <summary> Quaternion extensions new methods tests. </summary>
  [TestFixture]
  public class QuaternionExtensionsNewTests
  {
    private const float Tolerance = 0.01f;

    /// <summary> Negative returns negated quaternion. </summary>
    [Test]
    public void Negative_ReturnsNegatedQuaternion()
    {
      Quaternion q = new(0.1f, 0.2f, 0.3f, 0.4f);

      Quaternion result = q.Negative();

      Assert.AreEqual(-0.1f, result.x, Tolerance);
      Assert.AreEqual(-0.2f, result.y, Tolerance);
      Assert.AreEqual(-0.3f, result.z, Tolerance);
      Assert.AreEqual(-0.4f, result.w, Tolerance);
    }

    /// <summary> Normalized returns normalized quaternion. </summary>
    [Test]
    public void Normalized_ReturnsNormalizedQuaternion()
    {
      Quaternion q = new(1.0f, 2.0f, 3.0f, 4.0f);

      Quaternion result = q.Normalized();

      float magnitude = Mathf.Sqrt(result.x * result.x + result.y * result.y + result.z * result.z + result.w * result.w);
      Assert.AreEqual(1.0f, magnitude, Tolerance);
    }

    /// <summary> AngleTo returns correct angle between quaternions. </summary>
    [Test]
    public void AngleTo_ReturnsCorrectAngle()
    {
      Quaternion from = Quaternion.identity;
      Quaternion to = Quaternion.Euler(90.0f, 0.0f, 0.0f);

      float result = from.AngleTo(to);

      Assert.AreEqual(90.0f, result, Tolerance);
    }

    /// <summary> IsIdentity on identity returns true. </summary>
    [Test]
    public void IsIdentity_Identity_ReturnsTrue()
    {
      Quaternion q = Quaternion.identity;

      Assert.IsTrue(q.IsIdentity());
    }

    /// <summary> IsIdentity on non-identity returns false. </summary>
    [Test]
    public void IsIdentity_NonIdentity_ReturnsFalse()
    {
      Quaternion q = Quaternion.Euler(45.0f, 45.0f, 45.0f);

      Assert.IsFalse(q.IsIdentity());
    }

    /// <summary> ToEulerVector returns correct euler angles. </summary>
    [Test]
    public void ToEulerVector_ReturnsCorrectEulerAngles()
    {
      Quaternion q = Quaternion.Euler(30.0f, 60.0f, 90.0f);

      Vector3 result = q.ToEulerVector();

      Assert.AreEqual(30.0f, result.x, Tolerance);
      Assert.AreEqual(60.0f, result.y, Tolerance);
      Assert.AreEqual(90.0f, result.z, Tolerance);
    }
  }
}
