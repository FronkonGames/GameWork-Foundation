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
using FronkonGames.GameWork.Foundation;

namespace FronkonGames.GameWork.Foundation.Tests
{
  /// <summary> Vector3 extensions new methods tests. </summary>
  [TestFixture]
  public class Vector3ExtensionsNewTests
  {
    private const float Tolerance = 0.01f;

    /// <summary> WithX replaces the X component. </summary>
    [Test]
    public void WithX_ReplacesXComponent()
    {
      Vector3 v = new(1.0f, 2.0f, 3.0f);

      Vector3 result = v.WithX(5.0f);

      Assert.AreEqual(new Vector3(5.0f, 2.0f, 3.0f), result);
    }

    /// <summary> WithY replaces the Y component. </summary>
    [Test]
    public void WithY_ReplacesYComponent()
    {
      Vector3 v = new(1.0f, 2.0f, 3.0f);

      Vector3 result = v.WithY(5.0f);

      Assert.AreEqual(new Vector3(1.0f, 5.0f, 3.0f), result);
    }

    /// <summary> WithZ replaces the Z component. </summary>
    [Test]
    public void WithZ_ReplacesZComponent()
    {
      Vector3 v = new(1.0f, 2.0f, 3.0f);

      Vector3 result = v.WithZ(5.0f);

      Assert.AreEqual(new Vector3(1.0f, 2.0f, 5.0f), result);
    }

    /// <summary> IsInsideCube returns true for point inside. </summary>
    [Test]
    public void IsInsideCube_PointInside_ReturnsTrue()
    {
      Vector3 point = new(1.0f, 1.0f, 1.0f);
      Vector3 cubePos = Vector3.zero;
      Vector3 cubeSize = new(2.0f, 2.0f, 2.0f);

      Assert.IsTrue(point.IsInsideCube(cubePos, cubeSize));
    }

    /// <summary> IsInsideCube returns false for point outside. </summary>
    [Test]
    public void IsInsideCube_PointOutside_ReturnsFalse()
    {
      Vector3 point = new(5.0f, 5.0f, 5.0f);
      Vector3 cubePos = Vector3.zero;
      Vector3 cubeSize = new(2.0f, 2.0f, 2.0f);

      Assert.IsFalse(point.IsInsideCube(cubePos, cubeSize));
    }

    /// <summary> IsInsideSphere returns true for point inside. </summary>
    [Test]
    public void IsInsideSphere_PointInside_ReturnsTrue()
    {
      Vector3 point = new(1.0f, 0.0f, 0.0f);
      Vector3 spherePos = Vector3.zero;
      float radius = 2.0f;

      Assert.IsTrue(point.IsInsideSphere(spherePos, radius));
    }

    /// <summary> IsInsideSphere returns false for point outside. </summary>
    [Test]
    public void IsInsideSphere_PointOutside_ReturnsFalse()
    {
      Vector3 point = new(5.0f, 0.0f, 0.0f);
      Vector3 spherePos = Vector3.zero;
      float radius = 2.0f;

      Assert.IsFalse(point.IsInsideSphere(spherePos, radius));
    }

    /// <summary> MaxComponent returns the maximum of x, y, z. </summary>
    [Test]
    public void MaxComponent_ReturnsMaximum()
    {
      Vector3 v = new(1.0f, 5.0f, 3.0f);

      float result = v.MaxComponent();

      Assert.AreEqual(5.0f, result, Tolerance);
    }

    /// <summary> MinComponent returns the minimum of x, y, z. </summary>
    [Test]
    public void MinComponent_ReturnsMinimum()
    {
      Vector3 v = new(1.0f, 5.0f, 3.0f);

      float result = v.MinComponent();

      Assert.AreEqual(1.0f, result, Tolerance);
    }

    /// <summary> Multiply performs component-wise multiplication. </summary>
    [Test]
    public void Multiply_ComponentWise()
    {
      Vector3 a = new(2.0f, 3.0f, 4.0f);
      Vector3 b = new(5.0f, 6.0f, 7.0f);

      Vector3 result = a.Multiply(b);

      Assert.AreEqual(new Vector3(10.0f, 18.0f, 28.0f), result);
    }

    /// <summary> Divide performs component-wise division. </summary>
    [Test]
    public void Divide_ComponentWise()
    {
      Vector3 a = new(10.0f, 18.0f, 28.0f);
      Vector3 b = new(2.0f, 3.0f, 4.0f);

      Vector3 result = a.Divide(b);

      Assert.AreEqual(new Vector3(5.0f, 6.0f, 7.0f), result);
    }

    /// <summary> SetMagnitude sets the vector magnitude. </summary>
    [Test]
    public void SetMagnitude_SetsMagnitude()
    {
      Vector3 v = new(1.0f, 0.0f, 0.0f);

      Vector3 result = v.SetMagnitude(5.0f);

      Assert.AreEqual(5.0f, result.magnitude, Tolerance);
      Assert.AreEqual(new Vector3(5.0f, 0.0f, 0.0f), result);
    }

    /// <summary> SetMagnitude on zero vector returns zero. </summary>
    [Test]
    public void SetMagnitude_ZeroVector_ReturnsZero()
    {
      Vector3 v = Vector3.zero;

      Vector3 result = v.SetMagnitude(5.0f);

      Assert.AreEqual(Vector3.zero, result);
    }

    /// <summary> ToVector2XY converts to Vector2 with x and y. </summary>
    [Test]
    public void ToVector2XY_ConvertsXY()
    {
      Vector3 v = new(1.0f, 2.0f, 3.0f);

      Vector2 result = v.ToVector2XY();

      Assert.AreEqual(new Vector2(1.0f, 2.0f), result);
    }

    /// <summary> ToVector2XZ converts to Vector2 with x and z. </summary>
    [Test]
    public void ToVector2XZ_ConvertsXZ()
    {
      Vector3 v = new(1.0f, 2.0f, 3.0f);

      Vector2 result = v.ToVector2XZ();

      Assert.AreEqual(new Vector2(1.0f, 3.0f), result);
    }

    /// <summary> LerpUnclamped interpolates without clamping. </summary>
    [Test]
    public void LerpUnclamped_Interpolates()
    {
      Vector3 a = Vector3.zero;
      Vector3 b = new(10.0f, 10.0f, 10.0f);

      Vector3 result = a.LerpUnclamped(b, 0.5f);

      Assert.AreEqual(new Vector3(5.0f, 5.0f, 5.0f), result);
    }

    /// <summary> LerpUnclamped with t > 1 extrapolates beyond target. </summary>
    [Test]
    public void LerpUnclamped_Extrapolates()
    {
      Vector3 a = Vector3.zero;
      Vector3 b = new(10.0f, 10.0f, 10.0f);

      Vector3 result = a.LerpUnclamped(b, 2.0f);

      Assert.AreEqual(new Vector3(20.0f, 20.0f, 20.0f), result);
    }

    /// <summary> ApproximatelyEqual returns true for close vectors. </summary>
    [Test]
    public void ApproximatelyEqual_CloseVectors_ReturnsTrue()
    {
      Vector3 a = new(1.0f, 2.0f, 3.0f);
      Vector3 b = new(1.005f, 2.005f, 3.005f);

      Assert.IsTrue(a.ApproximatelyEqual(b));
    }

    /// <summary> ApproximatelyEqual returns false for distant vectors. </summary>
    [Test]
    public void ApproximatelyEqual_DistantVectors_ReturnsFalse()
    {
      Vector3 a = new(1.0f, 2.0f, 3.0f);
      Vector3 b = new(5.0f, 6.0f, 7.0f);

      Assert.IsFalse(a.ApproximatelyEqual(b));
    }

    [Test]
    public void Remap_RemapsCorrectly()
    {
      Vector3 value = new(5.0f, 25.0f, 50.0f);
      Vector3 from1 = Vector3.zero;
      Vector3 to1 = new(10.0f, 50.0f, 100.0f);
      Vector3 from2 = new(100.0f, 0.0f, -10.0f);
      Vector3 to2 = new(200.0f, 10.0f, 10.0f);

      Vector3 result = value.Remap(from1, to1, from2, to2);

      Assert.AreEqual(150.0f, result.x, Tolerance);
      Assert.AreEqual(5.0f, result.y, Tolerance);
      Assert.AreEqual(0.0f, result.z, Tolerance);
    }

    [Test]
    public void Remap01_RemapsToZeroOne()
    {
      Vector3 value = new(5.0f, 25.0f, 75.0f);
      Vector3 from = Vector3.zero;
      Vector3 to = new(10.0f, 50.0f, 100.0f);

      Vector3 result = value.Remap01(from, to);

      Assert.AreEqual(0.5f, result.x, Tolerance);
      Assert.AreEqual(0.5f, result.y, Tolerance);
      Assert.AreEqual(0.75f, result.z, Tolerance);
    }

    [Test]
    public void Multiply_WithDouble_MultipliesCorrectly()
    {
      Vector3 v = new(2.0f, 3.0f, 4.0f);

      Vector3 result = v.Multiply(2.5);

      Assert.AreEqual(5.0f, result.x, Tolerance);
      Assert.AreEqual(7.5f, result.y, Tolerance);
      Assert.AreEqual(10.0f, result.z, Tolerance);
    }

    [Test]
    public void Average_ReturnsAverageOfVectors()
    {
      Vector3[] vectors =
      {
        new(2.0f, 4.0f, 6.0f),
        new(4.0f, 6.0f, 8.0f),
        new(6.0f, 8.0f, 10.0f)
      };

      Vector3 result = vectors.Average();

      Assert.AreEqual(4.0f, result.x, Tolerance);
      Assert.AreEqual(6.0f, result.y, Tolerance);
      Assert.AreEqual(8.0f, result.z, Tolerance);
    }

    [Test]
    public void Average_OnEmpty_ReturnsZero()
    {
      Assert.AreEqual(Vector3.zero, new Vector3[0].Average());
    }

    [Test]
    public void Average_OnNull_ReturnsZero()
    {
      Assert.AreEqual(Vector3.zero, ((Vector3[])null).Average());
    }

    [Test]
    public void XY_ReturnsXYComponents()
    {
      Vector2 result = new Vector3(1.0f, 2.0f, 3.0f).XY();

      Assert.AreEqual(1.0f, result.x, Tolerance);
      Assert.AreEqual(2.0f, result.y, Tolerance);
    }

    [Test]
    public void XZ_ReturnsXZComponents()
    {
      Vector2 result = new Vector3(1.0f, 2.0f, 3.0f).XZ();

      Assert.AreEqual(1.0f, result.x, Tolerance);
      Assert.AreEqual(3.0f, result.y, Tolerance);
    }

    [Test]
    public void YX_ReturnsYXComponents()
    {
      Vector2 result = new Vector3(1.0f, 2.0f, 3.0f).YX();

      Assert.AreEqual(2.0f, result.x, Tolerance);
      Assert.AreEqual(1.0f, result.y, Tolerance);
    }

    [Test]
    public void ZX_ReturnsZXComponents()
    {
      Vector2 result = new Vector3(1.0f, 2.0f, 3.0f).ZX();

      Assert.AreEqual(3.0f, result.x, Tolerance);
      Assert.AreEqual(1.0f, result.y, Tolerance);
    }

    [Test]
    public void YXZ_ReturnsYXZSwizzle()
    {
      Vector3 result = new Vector3(1.0f, 2.0f, 3.0f).YXZ();

      Assert.AreEqual(2.0f, result.x, Tolerance);
      Assert.AreEqual(1.0f, result.y, Tolerance);
      Assert.AreEqual(3.0f, result.z, Tolerance);
    }

    [Test]
    public void AdjX_AdjustsX()
    {
      Vector3 result = new Vector3(1.0f, 2.0f, 3.0f).AdjX(5.0f);

      Assert.AreEqual(6.0f, result.x, Tolerance);
      Assert.AreEqual(2.0f, result.y, Tolerance);
      Assert.AreEqual(3.0f, result.z, Tolerance);
    }

    [Test]
    public void AdjY_AdjustsY()
    {
      Vector3 result = new Vector3(1.0f, 2.0f, 3.0f).AdjY(5.0f);

      Assert.AreEqual(1.0f, result.x, Tolerance);
      Assert.AreEqual(7.0f, result.y, Tolerance);
      Assert.AreEqual(3.0f, result.z, Tolerance);
    }

    [Test]
    public void AdjZ_AdjustsZ()
    {
      Vector3 result = new Vector3(1.0f, 2.0f, 3.0f).AdjZ(5.0f);

      Assert.AreEqual(1.0f, result.x, Tolerance);
      Assert.AreEqual(2.0f, result.y, Tolerance);
      Assert.AreEqual(8.0f, result.z, Tolerance);
    }

    [Test]
    public void AdjAll_AdjustsAllComponents()
    {
      Vector3 result = new Vector3(1.0f, 2.0f, 3.0f).AdjAll(5.0f);

      Assert.AreEqual(6.0f, result.x, Tolerance);
      Assert.AreEqual(7.0f, result.y, Tolerance);
      Assert.AreEqual(8.0f, result.z, Tolerance);
    }

    [Test]
    public void WithXY_ReplacesXY()
    {
      Vector3 result = new Vector3(1.0f, 2.0f, 3.0f).WithXY(10.0f, 20.0f);

      Assert.AreEqual(10.0f, result.x, Tolerance);
      Assert.AreEqual(20.0f, result.y, Tolerance);
      Assert.AreEqual(3.0f, result.z, Tolerance);
    }

    [Test]
    public void WithXZ_ReplacesXZ()
    {
      Vector3 result = new Vector3(1.0f, 2.0f, 3.0f).WithXZ(10.0f, 30.0f);

      Assert.AreEqual(10.0f, result.x, Tolerance);
      Assert.AreEqual(2.0f, result.y, Tolerance);
      Assert.AreEqual(30.0f, result.z, Tolerance);
    }

    [Test]
    public void WithYZ_ReplacesYZ()
    {
      Vector3 result = new Vector3(1.0f, 2.0f, 3.0f).WithYZ(20.0f, 30.0f);

      Assert.AreEqual(1.0f, result.x, Tolerance);
      Assert.AreEqual(20.0f, result.y, Tolerance);
      Assert.AreEqual(30.0f, result.z, Tolerance);
    }

    [Test]
    public void Vector3Int_CompareTo_LessOnZ_ReturnsNegative()
    {
      Assert.Less(new Vector3Int(1, 2, 3).CompareTo(new Vector3Int(1, 2, 4)), 0);
    }

    [Test]
    public void Vector3Int_CompareTo_Equal_ReturnsZero()
    {
      Assert.AreEqual(0, new Vector3Int(1, 2, 3).CompareTo(new Vector3Int(1, 2, 3)));
    }

    [Test]
    public void Vector3Int_SwizzleYXZ_SwizzlesCorrectly()
    {
      Vector3Int result = new Vector3Int(1, 2, 3).SwizzleYXZ();

      Assert.AreEqual(2, result.x);
      Assert.AreEqual(1, result.y);
      Assert.AreEqual(3, result.z);
    }

    [Test]
    public void Vector3Int_WithXY_ReplacesXY()
    {
      Vector3Int result = new Vector3Int(1, 2, 3).WithXY(10, 20);

      Assert.AreEqual(10, result.x);
      Assert.AreEqual(20, result.y);
      Assert.AreEqual(3, result.z);
    }

    [Test]
    public void Vector3Int_AdjXYZ_AdjustsAllComponents()
    {
      Vector3Int result = new Vector3Int(1, 2, 3).AdjXYZ(1, 2, 3);

      Assert.AreEqual(2, result.x);
      Assert.AreEqual(4, result.y);
      Assert.AreEqual(6, result.z);
    }
  }
}
