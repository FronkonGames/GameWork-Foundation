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
  /// <summary> Float extensions new methods tests. </summary>
  [TestFixture]
  public class FloatExtensionsNewTests
  {
    private const float Tolerance = 0.01f;

    /// <summary> IsBetween inclusive returns true for value in range. </summary>
    [Test]
    public void IsBetween_Inclusive_ReturnsTrue()
    {
      float value = 5.0f;
      float min = 3.0f;
      float max = 7.0f;

      Assert.IsTrue(value.IsBetween(min, max, inclusiveMin: true, inclusiveMax: true));
    }

    /// <summary> IsBetween inclusive returns true for boundary values. </summary>
    [Test]
    public void IsBetween_Inclusive_BoundaryValues_ReturnsTrue()
    {
      float min = 3.0f;
      float max = 7.0f;

      Assert.IsTrue(min.IsBetween(min, max, inclusiveMin: true, inclusiveMax: true));
      Assert.IsTrue(max.IsBetween(min, max, inclusiveMin: true, inclusiveMax: true));
    }

    /// <summary> IsBetween exclusive returns false for boundary values. </summary>
    [Test]
    public void IsBetween_Exclusive_BoundaryValues_ReturnsFalse()
    {
      float min = 3.0f;
      float max = 7.0f;

      Assert.IsFalse(min.IsBetween(min, max, inclusiveMin: false, inclusiveMax: false));
      Assert.IsFalse(max.IsBetween(min, max, inclusiveMin: false, inclusiveMax: false));
    }

    /// <summary> IsBetween exclusive returns true for interior value. </summary>
    [Test]
    public void IsBetween_Exclusive_InteriorValue_ReturnsTrue()
    {
      float value = 5.0f;

      Assert.IsTrue(value.IsBetween(3.0f, 7.0f, inclusiveMin: false, inclusiveMax: false));
    }

    /// <summary> Remap remaps value correctly. </summary>
    [Test]
    public void Remap_RemapsCorrectly()
    {
      float value = 0.5f;

      float result = value.Remap(0.0f, 1.0f, 10.0f, 20.0f);

      Assert.AreEqual(15.0f, result, Tolerance);
    }

    /// <summary> Remap at start of range returns target start. </summary>
    [Test]
    public void Remap_AtStart_ReturnsTargetStart()
    {
      float value = 0.0f;

      float result = value.Remap(0.0f, 1.0f, 10.0f, 20.0f);

      Assert.AreEqual(10.0f, result, Tolerance);
    }

    /// <summary> Remap at end of range returns target end. </summary>
    [Test]
    public void Remap_AtEnd_ReturnsTargetEnd()
    {
      float value = 1.0f;

      float result = value.Remap(0.0f, 1.0f, 10.0f, 20.0f);

      Assert.AreEqual(20.0f, result, Tolerance);
    }

    /// <summary> Normalize normalizes value to 0-1 range. </summary>
    [Test]
    public void Normalize_NormalizesCorrectly()
    {
      float value = 5.0f;

      float result = value.Normalize(0.0f, 10.0f);

      Assert.AreEqual(0.5f, result, Tolerance);
    }

    /// <summary> Normalize at min returns 0. </summary>
    [Test]
    public void Normalize_AtMin_ReturnsZero()
    {
      float result = 2.0f.Normalize(2.0f, 8.0f);

      Assert.AreEqual(0.0f, result, Tolerance);
    }

    /// <summary> Normalize at max returns 1. </summary>
    [Test]
    public void Normalize_AtMax_ReturnsOne()
    {
      float result = 8.0f.Normalize(2.0f, 8.0f);

      Assert.AreEqual(1.0f, result, Tolerance);
    }

    /// <summary> Normalize with equal min and max returns 0. </summary>
    [Test]
    public void Normalize_EqualMinMax_ReturnsZero()
    {
      float result = 5.0f.Normalize(5.0f, 5.0f);

      Assert.AreEqual(0.0f, result, Tolerance);
    }

    /// <summary> ToRoundedInt rounds correctly. </summary>
    [Test]
    public void ToRoundedInt_RoundsCorrectly()
    {
      Assert.AreEqual(3, 2.7f.ToRoundedInt());
      Assert.AreEqual(2, 2.3f.ToRoundedInt());
      Assert.AreEqual(-2, -1.6f.ToRoundedInt());
    }

    /// <summary> ToStringFixed formats correctly. </summary>
    [Test]
    public void ToStringFixed_FormatsCorrectly()
    {
      float value = 3.14159f;

      string result = value.ToStringFixed(2);

      Assert.AreEqual("3.14", result);
    }

    /// <summary> ToStringFixed with default decimals. </summary>
    [Test]
    public void ToStringFixed_DefaultDecimals_FormatsToTwoPlaces()
    {
      string result = 3.14159f.ToStringFixed();

      Assert.AreEqual("3.14", result);
    }

    /// <summary> ToStringFixed with zero decimals. </summary>
    [Test]
    public void ToStringFixed_ZeroDecimals_FormatsWithoutDecimals()
    {
      string result = 3.5f.ToStringFixed(0);

      Assert.AreEqual("4", result);
    }

    /// <summary> IsZero on zero returns true. </summary>
    [Test]
    public void IsZero_OnZero_ReturnsTrue()
    {
      Assert.IsTrue(0.0f.IsZero());
    }

    /// <summary> IsZero on very small value returns true. </summary>
    [Test]
    public void IsZero_OnSmallValue_ReturnsTrue()
    {
      Assert.IsTrue(0.0000001f.IsZero());
    }

    /// <summary> IsZero on non-zero returns false. </summary>
    [Test]
    public void IsZero_OnNonZero_ReturnsFalse()
    {
      Assert.IsFalse(1.0f.IsZero());
      Assert.IsFalse((-1.0f).IsZero());
      Assert.IsFalse(0.5f.IsZero());
    }

    /// <summary> SnapToGrid snaps to nearest grid size. </summary>
    [Test]
    public void SnapToGrid_SnapsCorrectly()
    {
      Assert.AreEqual(0.0f, 0.2f.SnapToGrid(0.5f), Tolerance);
      Assert.AreEqual(0.5f, 0.3f.SnapToGrid(0.5f), Tolerance);
      Assert.AreEqual(1.0f, 0.8f.SnapToGrid(0.5f), Tolerance);
      Assert.AreEqual(10.0f, 12.0f.SnapToGrid(10.0f), Tolerance);
    }

    /// <summary> SnapToGrid with zero grid size returns original value. </summary>
    [Test]
    public void SnapToGrid_ZeroGridSize_ReturnsOriginal()
    {
      Assert.AreEqual(3.7f, 3.7f.SnapToGrid(0.0f), Tolerance);
    }

    /// <summary> ToRadians converts degrees to radians. </summary>
    [Test]
    public void ToRadians_ConvertsCorrectly()
    {
      Assert.AreEqual(0.0f, 0.0f.ToRadians(), Tolerance);
      Assert.AreEqual(Mathf.PI / 2.0f, 90.0f.ToRadians(), Tolerance);
      Assert.AreEqual(Mathf.PI, 180.0f.ToRadians(), Tolerance);
      Assert.AreEqual(2.0f * Mathf.PI, 360.0f.ToRadians(), Tolerance);
    }

    /// <summary> ToDegrees converts radians to degrees. </summary>
    [Test]
    public void ToDegrees_ConvertsCorrectly()
    {
      Assert.AreEqual(0.0f, 0.0f.ToDegrees(), Tolerance);
      Assert.AreEqual(90.0f, (Mathf.PI / 2.0f).ToDegrees(), Tolerance);
      Assert.AreEqual(180.0f, Mathf.PI.ToDegrees(), Tolerance);
      Assert.AreEqual(360.0f, (2.0f * Mathf.PI).ToDegrees(), Tolerance);
    }

    /// <summary> ToRadians and ToDegrees are inverse. </summary>
    [Test]
    public void ToRadians_ToDegrees_AreInverse()
    {
      float degrees = 45.0f;

      Assert.AreEqual(degrees, degrees.ToRadians().ToDegrees(), Tolerance);
    }

    /// <summary> ToPercentageString converts correctly. </summary>
    [Test]
    public void ToPercentageString_ConvertsCorrectly()
    {
      Assert.AreEqual("75%", 0.75f.ToPercentageString());
      Assert.AreEqual("0%", 0.0f.ToPercentageString());
      Assert.AreEqual("100%", 1.0f.ToPercentageString());
    }

    /// <summary> ToPercentageString with decimals. </summary>
    [Test]
    public void ToPercentageString_WithDecimals_FormatsCorrectly()
    {
      Assert.AreEqual("75.5%", 0.755f.ToPercentageString(1));
      Assert.AreEqual("33.33%", 0.3333f.ToPercentageString(2));
    }

    /// <summary> RoundToNearest rounds to nearest multiple of step. </summary>
    [Test]
    public void RoundToNearest_RoundsCorrectly()
    {
      Assert.AreEqual(0.0f, 0.2f.RoundToNearest(0.5f), Tolerance);
      Assert.AreEqual(0.5f, 0.3f.RoundToNearest(0.5f), Tolerance);
      Assert.AreEqual(1.0f, 0.8f.RoundToNearest(0.5f), Tolerance);
      Assert.AreEqual(10.0f, 12.0f.RoundToNearest(10.0f), Tolerance);
    }

    /// <summary> RoundToNearest with zero step returns original. </summary>
    [Test]
    public void RoundToNearest_ZeroStep_ReturnsOriginal()
    {
      Assert.AreEqual(3.7f, 3.7f.RoundToNearest(0.0f), Tolerance);
    }

    /// <summary> Clamped clamps to range. </summary>
    [Test]
    public void Clamped_ClampsCorrectly()
    {
      Assert.AreEqual(5.0f, 5.0f.Clamped(0.0f, 10.0f), Tolerance);
      Assert.AreEqual(0.0f, (-3.0f).Clamped(0.0f, 10.0f), Tolerance);
      Assert.AreEqual(10.0f, 15.0f.Clamped(0.0f, 10.0f), Tolerance);
    }

    /// <summary> Clamped at boundaries returns exact value. </summary>
    [Test]
    public void Clamped_AtBoundaries_ReturnsExact()
    {
      Assert.AreEqual(0.0f, 0.0f.Clamped(0.0f, 10.0f), Tolerance);
      Assert.AreEqual(10.0f, 10.0f.Clamped(0.0f, 10.0f), Tolerance);
    }

    /// <summary> Wrapped wraps value within range. </summary>
    [Test]
    public void Wrapped_WrapsCorrectly()
    {
      Assert.AreEqual(2.0f, 12.0f.Wrapped(0.0f, 10.0f), Tolerance);
      Assert.AreEqual(8.0f, (-2.0f).Wrapped(0.0f, 10.0f), Tolerance);
      Assert.AreEqual(5.0f, 5.0f.Wrapped(0.0f, 10.0f), Tolerance);
    }

    /// <summary> Wrapped at min boundary returns min. </summary>
    [Test]
    public void Wrapped_AtMin_ReturnsMin()
    {
      Assert.AreEqual(3.0f, 3.0f.Wrapped(3.0f, 7.0f), Tolerance);
    }

    /// <summary> Wrapped at max boundary wraps to min. </summary>
    [Test]
    public void Wrapped_AtMax_ReturnsMin()
    {
      Assert.AreEqual(3.0f, 7.0f.Wrapped(3.0f, 7.0f), Tolerance);
    }

    /// <summary> Wrapped with equal min and max returns min. </summary>
    [Test]
    public void Wrapped_EqualMinMax_ReturnsMin()
    {
      Assert.AreEqual(5.0f, 5.0f.Wrapped(5.0f, 5.0f), Tolerance);
    }

    [Test]
    public void Remap01_NormalizesToZeroOne()
    {
      Assert.AreEqual(0.5f, 5.0f.Remap01(0.0f, 10.0f), Tolerance);
    }

    [Test]
    public void RemapUnclamped_ExceedsRange()
    {
      Assert.AreEqual(150.0f, 15.0f.RemapUnclamped(0.0f, 10.0f, 0.0f, 100.0f), Tolerance);
    }

    [Test]
    public void SnapToCeil_SnapsUp()
    {
      Assert.AreEqual(15, 13.2f.SnapToCeil(5));
    }

    [Test]
    public void SnapToFloor_SnapsDown()
    {
      Assert.AreEqual(10, 13.8f.SnapToFloor(5));
    }

    [Test]
    public void ToMilliseconds_ConvertsSeconds()
    {
      Assert.AreEqual(1500, 1.5f.ToMilliseconds());
    }
  }
}
