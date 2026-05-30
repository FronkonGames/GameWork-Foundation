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

/// <summary> Bounds extensions test. </summary>
[TestFixture]
public class BoundsExtensionsTests
{
  private Bounds bounds;

  [SetUp]
  public void SetUp()
  {
    bounds = new Bounds(new Vector3(5f, 5f, 5f), new Vector3(10f, 10f, 10f));
  }

  [Test]
  public void RandomPoint_ReturnsPointInsideBounds()
  {
    for (int i = 0; i < 100; i++)
    {
      Vector3 point = bounds.RandomPoint();

      Assert.GreaterOrEqual(point.x, bounds.min.x);
      Assert.LessOrEqual(point.x, bounds.max.x);
      Assert.GreaterOrEqual(point.y, bounds.min.y);
      Assert.LessOrEqual(point.y, bounds.max.y);
      Assert.GreaterOrEqual(point.z, bounds.min.z);
      Assert.LessOrEqual(point.z, bounds.max.z);
    }
  }

  [Test]
  public void RandomPoint_WithRng_ReturnsPointInsideBounds()
  {
    System.Random rng = new System.Random(42);

    for (int i = 0; i < 100; i++)
    {
      Vector3 point = bounds.RandomPoint(rng);

      Assert.GreaterOrEqual(point.x, bounds.min.x);
      Assert.LessOrEqual(point.x, bounds.max.x);
      Assert.GreaterOrEqual(point.y, bounds.min.y);
      Assert.LessOrEqual(point.y, bounds.max.y);
      Assert.GreaterOrEqual(point.z, bounds.min.z);
      Assert.LessOrEqual(point.z, bounds.max.z);
    }
  }

  [Test]
  public void ClosestPoint_PointInside_ReturnsSamePoint()
  {
    Vector3 inside = new Vector3(3f, 4f, 6f);

    Assert.AreEqual(inside, bounds.ClosestPoint(inside));
  }

  [Test]
  public void ClosestPoint_PointOutside_ReturnsClampedPoint()
  {
    Vector3 outside = new Vector3(20f, 20f, 20f);
    Vector3 closest = bounds.ClosestPoint(outside);

    Assert.AreEqual(bounds.max.x, closest.x);
    Assert.AreEqual(bounds.max.y, closest.y);
    Assert.AreEqual(bounds.max.z, closest.z);
  }

  [Test]
  public void ClosestPoint_PointBelow_ReturnsMinEdge()
  {
    Vector3 below = new Vector3(5f, -10f, 5f);
    Vector3 closest = bounds.ClosestPoint(below);

    Assert.AreEqual(5f, closest.x);
    Assert.AreEqual(bounds.min.y, closest.y);
    Assert.AreEqual(5f, closest.z);
  }

  [Test]
  public void Expanded_ExpandsCorrectly()
  {
    Bounds expanded = bounds.Expanded(5f);

    Assert.AreEqual(bounds.center, expanded.center);
    Assert.AreEqual(20f, expanded.size.x, 0.001f);
    Assert.AreEqual(20f, expanded.size.y, 0.001f);
    Assert.AreEqual(20f, expanded.size.z, 0.001f);
  }

  [Test]
  public void Expanded_NegativeValue_ShrinksCorrectly()
  {
    Bounds expanded = bounds.Expanded(-2f);

    Assert.AreEqual(bounds.center, expanded.center);
    Assert.AreEqual(6f, expanded.size.x, 0.001f);
    Assert.AreEqual(6f, expanded.size.y, 0.001f);
    Assert.AreEqual(6f, expanded.size.z, 0.001f);
  }

  [Test]
  public void Scaled_ScalesCorrectly()
  {
    Bounds scaled = bounds.Scaled(new Vector3(2f, 3f, 0.5f));

    Assert.AreEqual(bounds.center, scaled.center);
    Assert.AreEqual(20f, scaled.size.x, 0.001f);
    Assert.AreEqual(30f, scaled.size.y, 0.001f);
    Assert.AreEqual(5f, scaled.size.z, 0.001f);
  }

  [Test]
  public void Scaled_One_NoChange()
  {
    Bounds scaled = bounds.Scaled(Vector3.one);

    Assert.AreEqual(bounds.size, scaled.size);
  }

  [Test]
  public void Intersects_OverlappingBounds_ReturnsTrue()
  {
    Bounds other = new Bounds(new Vector3(8f, 8f, 8f), new Vector3(10f, 10f, 10f));

    Assert.IsTrue(bounds.Intersects(other));
  }

  [Test]
  public void Intersects_NonOverlappingBounds_ReturnsFalse()
  {
    Bounds other = new Bounds(new Vector3(50f, 50f, 50f), new Vector3(10f, 10f, 10f));

    Assert.IsFalse(bounds.Intersects(other));
  }

  [Test]
  public void Intersects_TouchingEdge_ReturnsTrue()
  {
    Bounds other = new Bounds(new Vector3(15f, 5f, 5f), new Vector3(10f, 10f, 10f));

    Assert.IsTrue(bounds.Intersects(other));
  }

  [Test]
  public void Intersects_ContainedBounds_ReturnsTrue()
  {
    Bounds other = new Bounds(new Vector3(5f, 5f, 5f), new Vector3(2f, 2f, 2f));

    Assert.IsTrue(bounds.Intersects(other));
  }
}
