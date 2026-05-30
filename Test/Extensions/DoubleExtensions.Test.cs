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

namespace FronkonGames.GameWork.Foundation.Tests
{
  /// <summary> Double extensions tests. </summary>
  [TestFixture]
  public class DoubleExtensionsTests
  {
    private const double Tolerance = 1e-6;

    /// <summary> IsBetween inclusive returns true for value in range. </summary>
    [Test]
    public void IsBetween_Inclusive_ReturnsTrueForValueInRange()
    {
      Assert.IsTrue(0.5.IsBetween(0.0, 1.0));
      Assert.IsTrue(0.0.IsBetween(0.0, 1.0));
      Assert.IsTrue(1.0.IsBetween(0.0, 1.0));
      Assert.IsTrue((-0.5).IsBetween(-1.0, 1.0));
      Assert.IsFalse(2.0.IsBetween(0.0, 1.0));
      Assert.IsFalse((-2.0).IsBetween(0.0, 1.0));
    }

    /// <summary> IsBetween exclusive returns false for boundary values. </summary>
    [Test]
    public void IsBetween_Exclusive_ReturnsFalseForBoundaryValues()
    {
      Assert.IsTrue(0.5.IsBetween(0.0, 1.0, false, false));
      Assert.IsFalse(0.0.IsBetween(0.0, 1.0, false, false));
      Assert.IsFalse(1.0.IsBetween(0.0, 1.0, false, false));
      Assert.IsFalse(2.0.IsBetween(0.0, 1.0, false, false));
      Assert.IsFalse((-2.0).IsBetween(0.0, 1.0, false, false));
    }

    /// <summary> IsBetween throws if max is less than min. </summary>
    [Test]
    public void IsBetween_MaxLessThanMin_Throws()
    {
      Assert.Throws<System.ArgumentException>(() => 0.5.IsBetween(1.0, 0.0));
    }

    /// <summary> IsZero on zero returns true. </summary>
    [Test]
    public void IsZero_OnZero_ReturnsTrue()
    {
      Assert.IsTrue(0.0.IsZero());
    }

    /// <summary> IsZero on non-zero returns false. </summary>
    [Test]
    public void IsZero_OnNonZero_ReturnsFalse()
    {
      Assert.IsFalse(1.0.IsZero());
      Assert.IsFalse((-1.0).IsZero());
    }

    /// <summary> ApproximatelyEquals on equal values returns true. </summary>
    [Test]
    public void ApproximatelyEquals_OnEqualValues_ReturnsTrue()
    {
      Assert.IsTrue(1.0.ApproximatelyEquals(1.0));
      Assert.IsTrue(0.0.ApproximatelyEquals(0.0));
      Assert.IsTrue(1.0.ApproximatelyEquals(1.0 + 1e-11));
    }

    /// <summary> ApproximatelyEquals on different values returns false. </summary>
    [Test]
    public void ApproximatelyEquals_OnDifferentValues_ReturnsFalse()
    {
      Assert.IsFalse(1.0.ApproximatelyEquals(2.0));
      Assert.IsFalse(1.0.ApproximatelyEquals(1.1));
    }

    /// <summary> RoundToNearest rounds correctly. </summary>
    [Test]
    public void RoundToNearest_RoundsCorrectly()
    {
      Assert.AreEqual(2.0, 1.7.RoundToNearest(1.0), Tolerance);
      Assert.AreEqual(2.0, 2.0.RoundToNearest(1.0), Tolerance);
      Assert.AreEqual(0.5, 0.3.RoundToNearest(0.5), Tolerance);
      Assert.AreEqual(0.5, 0.4.RoundToNearest(0.5), Tolerance);
      Assert.AreEqual(0.0, 0.2.RoundToNearest(0.5), Tolerance);
    }

    /// <summary> ToPercentageString converts correctly. </summary>
    [Test]
    public void ToPercentageString_ConvertsCorrectly()
    {
      Assert.AreEqual("75%", 0.75.ToPercentageString());
      Assert.AreEqual("100%", 1.0.ToPercentageString());
      Assert.AreEqual("0%", 0.0.ToPercentageString());
      Assert.AreEqual("75.0%", 0.75.ToPercentageString(1));
    }

    /// <summary> Lerp interpolates correctly. </summary>
    [Test]
    public void Lerp_InterpolatesCorrectly()
    {
      Assert.AreEqual(5.0, 0.5.Lerp(0.0, 10.0), Tolerance);
      Assert.AreEqual(0.0, 0.0.Lerp(0.0, 10.0), Tolerance);
      Assert.AreEqual(10.0, 1.0.Lerp(0.0, 10.0), Tolerance);
      Assert.AreEqual(7.5, 0.5.Lerp(5.0, 10.0), Tolerance);
    }

    /// <summary> Normalize normalizes correctly. </summary>
    [Test]
    public void Normalize_NormalizesCorrectly()
    {
      Assert.AreEqual(0.5, 5.0.Normalize(0.0, 10.0), Tolerance);
      Assert.AreEqual(0.0, 0.0.Normalize(0.0, 10.0), Tolerance);
      Assert.AreEqual(1.0, 10.0.Normalize(0.0, 10.0), Tolerance);
      Assert.AreEqual(0.25, 2.5.Normalize(0.0, 10.0), Tolerance);
    }

    /// <summary> ToRadians converts correctly. </summary>
    [Test]
    public void ToRadians_ConvertsCorrectly()
    {
      Assert.AreEqual(0.0, 0.0.ToRadians(), Tolerance);
      Assert.AreEqual(System.Math.PI / 2.0, 90.0.ToRadians(), Tolerance);
      Assert.AreEqual(System.Math.PI, 180.0.ToRadians(), Tolerance);
      Assert.AreEqual(2.0 * System.Math.PI, 360.0.ToRadians(), Tolerance);
    }

    /// <summary> ToDegrees converts correctly. </summary>
    [Test]
    public void ToDegrees_ConvertsCorrectly()
    {
      Assert.AreEqual(0.0, 0.0.ToDegrees(), Tolerance);
      Assert.AreEqual(90.0, (System.Math.PI / 2.0).ToDegrees(), Tolerance);
      Assert.AreEqual(180.0, System.Math.PI.ToDegrees(), Tolerance);
      Assert.AreEqual(360.0, (2.0 * System.Math.PI).ToDegrees(), Tolerance);
    }

    /// <summary> ToRoundedInt rounds correctly. </summary>
    [Test]
    public void ToRoundedInt_RoundsCorrectly()
    {
      Assert.AreEqual(0, 0.5.ToRoundedInt());
      Assert.AreEqual(1, 1.4.ToRoundedInt());
      Assert.AreEqual(2, 1.5.ToRoundedInt());
      Assert.AreEqual(2, 2.0.ToRoundedInt());
      Assert.AreEqual(0, (-0.5).ToRoundedInt());
      Assert.AreEqual(-2, (-1.5).ToRoundedInt());
    }

    /// <summary> ToStringFixed formats correctly. </summary>
    [Test]
    public void ToStringFixed_FormatsCorrectly()
    {
      Assert.AreEqual("1.00", 1.0.ToStringFixed());
      Assert.AreEqual("1.0", 1.0.ToStringFixed(1));
      Assert.AreEqual("1.000", 1.0.ToStringFixed(3));
      Assert.AreEqual("1.50", 1.5.ToStringFixed());
    }

    /// <summary> SnapToGrid snaps correctly. </summary>
    [Test]
    public void SnapToGrid_SnapsCorrectly()
    {
      Assert.AreEqual(0.0, 0.4.SnapToGrid(1.0), Tolerance);
      Assert.AreEqual(1.0, 0.6.SnapToGrid(1.0), Tolerance);
      Assert.AreEqual(1.0, 1.1.SnapToGrid(1.0), Tolerance);
      Assert.AreEqual(2.0, 1.9.SnapToGrid(1.0), Tolerance);
    }

    /// <summary> Clamped clamps correctly. </summary>
    [Test]
    public void Clamped_ClampsCorrectly()
    {
      Assert.AreEqual(1.0, 1.0.Clamped(0.0, 2.0), Tolerance);
      Assert.AreEqual(0.0, (-1.0).Clamped(0.0, 2.0), Tolerance);
      Assert.AreEqual(2.0, 3.0.Clamped(0.0, 2.0), Tolerance);
    }

    /// <summary> Wrapped wraps correctly. </summary>
    [Test]
    public void Wrapped_WrapsCorrectly()
    {
      Assert.AreEqual(0.5, 0.5.Wrapped(0.0, 1.0), Tolerance);
      Assert.AreEqual(0.0, 1.0.Wrapped(0.0, 1.0), Tolerance);
      Assert.AreEqual(0.0, 2.0.Wrapped(0.0, 1.0), Tolerance);
      Assert.AreEqual(0.8, 2.8.Wrapped(0.0, 1.0), Tolerance);
      Assert.AreEqual(0.0, (-1.0).Wrapped(0.0, 1.0), Tolerance);
    }
  }
}
