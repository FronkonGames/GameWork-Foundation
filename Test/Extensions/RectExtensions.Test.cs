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

/// <summary> Rect extensions test. </summary>
[TestFixture]
public class RectExtensionsTests
{
  private Rect rect;

  [SetUp]
  public void SetUp()
  {
    rect = new Rect(0f, 0f, 10f, 10f);
  }

  [Test]
  public void Contains_Inside_ReturnsTrue()
  {
    Assert.IsTrue(rect.Contains(5f, 5f));
    Assert.IsTrue(rect.Contains(0f, 0f));
    Assert.IsTrue(rect.Contains(10f, 10f));
    Assert.IsTrue(rect.Contains(2.5f, 7.5f));
  }

  [Test]
  public void Contains_Outside_ReturnsFalse()
  {
    Assert.IsFalse(rect.Contains(-1f, 5f));
    Assert.IsFalse(rect.Contains(11f, 5f));
    Assert.IsFalse(rect.Contains(5f, -1f));
    Assert.IsFalse(rect.Contains(5f, 11f));
  }

  [Test]
  public void ClampPoint_ClampsCorrectly()
  {
    Assert.AreEqual(new Vector2(5f, 5f), rect.ClampPoint(new Vector2(5f, 5f)));
    Assert.AreEqual(new Vector2(0f, 0f), rect.ClampPoint(new Vector2(-5f, -5f)));
    Assert.AreEqual(new Vector2(10f, 10f), rect.ClampPoint(new Vector2(15f, 15f)));
    Assert.AreEqual(new Vector2(10f, 0f), rect.ClampPoint(new Vector2(20f, -10f)));
  }

  [Test]
  public void BottomLeft_ReturnsCorrectCorner()
  {
    Assert.AreEqual(new Vector2(0f, 0f), rect.BottomLeft());
  }

  [Test]
  public void BottomRight_ReturnsCorrectCorner()
  {
    Assert.AreEqual(new Vector2(10f, 0f), rect.BottomRight());
  }

  [Test]
  public void TopLeft_ReturnsCorrectCorner()
  {
    Assert.AreEqual(new Vector2(0f, 10f), rect.TopLeft());
  }

  [Test]
  public void TopRight_ReturnsCorrectCorner()
  {
    Assert.AreEqual(new Vector2(10f, 10f), rect.TopRight());
  }

  [Test]
  public void Center_ReturnsCorrectCenter()
  {
    Assert.AreEqual(new Vector2(5f, 5f), rect.Center());
  }

  [Test]
  public void Scaled_ScalesCorrectly()
  {
    Rect scaled = rect.Scaled(2f, 2f);

    Assert.AreEqual(new Vector2(5f, 5f), scaled.center);
    Assert.AreEqual(20f, scaled.width, 0.001f);
    Assert.AreEqual(20f, scaled.height, 0.001f);
  }

  [Test]
  public void Expanded_ExpandsCorrectly()
  {
    Rect expanded = rect.Expanded(5f);

    Assert.AreEqual(-5f, expanded.x, 0.001f);
    Assert.AreEqual(-5f, expanded.y, 0.001f);
    Assert.AreEqual(20f, expanded.width, 0.001f);
    Assert.AreEqual(20f, expanded.height, 0.001f);
  }

  [Test]
  public void WithRoundedCoordinates_RoundsAllComponents()
  {
    Rect rounded = new Rect(1.2f, 2.6f, 3.4f, 4.5f).WithRoundedCoordinates();

    Assert.AreEqual(1.0f, rounded.x, 0.001f);
    Assert.AreEqual(3.0f, rounded.y, 0.001f);
    Assert.AreEqual(3.0f, rounded.width, 0.001f);
    Assert.AreEqual(4.0f, rounded.height, 0.001f);
  }

  [Test]
  public void CutVertically_FromLeft_SplitsRect()
  {
    (Rect leftRect, Rect rightRect) = rect.CutVertically(3.0f);

    Assert.AreEqual(new Rect(0.0f, 0.0f, 3.0f, 10.0f), leftRect);
    Assert.AreEqual(new Rect(3.0f, 0.0f, 7.0f, 10.0f), rightRect);
  }

  [Test]
  public void AddHorizontalPadding_NarrowsRect()
  {
    Rect padded = rect.AddHorizontalPadding(2.0f, 3.0f);

    Assert.AreEqual(2.0f, padded.x, 0.001f);
    Assert.AreEqual(5.0f, padded.width, 0.001f);
  }

  [Test]
  public void AlignMiddleVertically_CentersHeight()
  {
    Rect aligned = rect.AlignMiddleVertically(4.0f);

    Assert.AreEqual(3.0f, aligned.y, 0.001f);
    Assert.AreEqual(4.0f, aligned.height, 0.001f);
  }
}
