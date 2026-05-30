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

/// <summary> ColliderExtensions tests. </summary>
public class ColliderExtensionsTests
{
  private GameObject gameObject;
  private BoxCollider boxCollider;

  [SetUp]
  public void SetUp()
  {
    gameObject = new GameObject("TestCollider");
    boxCollider = gameObject.AddComponent<BoxCollider>();
    boxCollider.size = Vector3.one * 2.0f;
  }

  [TearDown]
  public void TearDown()
  {
    Object.DestroyImmediate(gameObject);
  }

  [Test]
  public void ClosestPointTo_ReturnsClosestPoint()
  {
    Vector3 outside = new Vector3(5.0f, 0.0f, 0.0f);

    Vector3 closest = boxCollider.ClosestPointTo(outside);

    Vector3 expected = new Vector3(1.0f, 0.0f, 0.0f);
    Assert.AreEqual(expected, closest);
  }

  [Test]
  public void ContainsPoint_PointInside_ReturnsTrue()
  {
    Vector3 inside = Vector3.zero;

    Assert.IsTrue(boxCollider.ContainsPoint(inside));
  }

  [Test]
  public void ContainsPoint_PointOutside_ReturnsFalse()
  {
    Vector3 outside = new Vector3(5.0f, 0.0f, 0.0f);

    Assert.IsFalse(boxCollider.ContainsPoint(outside));
  }

  [Test]
  public void CloneColliderAsChild_CreatesChildWithCollider()
  {
    boxCollider.CloneColliderAsChild(LayerMask.NameToLayer("Default"));

    Transform child = gameObject.transform.Find($"Clone_{gameObject.name}");
    Assert.IsNotNull(child);
    Assert.IsTrue(child.gameObject != gameObject);
    Assert.AreEqual(gameObject.transform, child.parent);

    BoxCollider clonedBox = child.GetComponent<BoxCollider>();
    Assert.IsNotNull(clonedBox);
    Assert.AreEqual(boxCollider.center, clonedBox.center);
    Assert.AreEqual(boxCollider.size, clonedBox.size);
  }
}
