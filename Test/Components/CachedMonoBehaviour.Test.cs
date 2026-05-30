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

/// <summary> Test component for CachedMonoBehaviour tests. </summary>
public class TestCachedComponent : CachedMonoBehaviour
{
}

/// <summary> CachedMonoBehaviour tests. </summary>
public class CachedMonoBehaviourTests
{
  private GameObject root;
  private TestCachedComponent cached;

  [SetUp]
  public void SetUp()
  {
    root = new GameObject("TestCachedMonoBehaviour");
    cached = root.AddComponent<TestCachedComponent>();
  }

  [TearDown]
  public void TearDown()
  {
    Object.DestroyImmediate(root);
  }

  /// <summary> Get returns component on same GameObject. </summary>
  [Test]
  public void Get_ReturnsComponentOnSameGameObject()
  {
    Transform result = cached.Get<Transform>();
    Assert.AreEqual(root.transform, result);
  }

  /// <summary> Gets returns all components on same GameObject. </summary>
  [Test]
  public void Gets_ReturnsAllComponentsOnSameGameObject()
  {
    Collider[] results = cached.Gets<Collider>();
    Assert.IsNotNull(results);
  }

  /// <summary> ChildrenGet returns component on child. </summary>
  [Test]
  public void ChildrenGet_ReturnsComponentOnChild()
  {
    GameObject child = new("Child");
    child.transform.SetParent(root.transform);
    child.AddComponent<BoxCollider>();

    BoxCollider result = cached.ChildrenGet<BoxCollider>();
    Assert.IsNotNull(result);
    Assert.AreEqual(child, result.gameObject);

    Object.DestroyImmediate(child);
  }

  /// <summary> ChildrenGet returns null when no child has component. </summary>
  [Test]
  public void ChildrenGet_ReturnsNullWhenNotFound()
  {
    GameObject child = new("Child");
    child.transform.SetParent(root.transform);

    BoxCollider result = cached.ChildrenGet<BoxCollider>();
    Assert.IsNull(result);

    Object.DestroyImmediate(child);
  }

  /// <summary> ChildrenGets returns all matching components in children. </summary>
  [Test]
  public void ChildrenGets_ReturnsAllMatchingInChildren()
  {
    GameObject child1 = new("Child1");
    child1.transform.SetParent(root.transform);
    child1.AddComponent<BoxCollider>();

    GameObject child2 = new("Child2");
    child2.transform.SetParent(root.transform);
    child2.AddComponent<BoxCollider>();

    BoxCollider[] results = cached.ChildrenGets<BoxCollider>();
    Assert.IsNotNull(results);
    Assert.AreEqual(2, results.Length);

    Object.DestroyImmediate(child1);
    Object.DestroyImmediate(child2);
  }

  /// <summary> ParentGet returns component on parent. </summary>
  [Test]
  public void ParentGet_ReturnsComponentOnParent()
  {
    GameObject child = new("Child");
    child.transform.SetParent(root.transform);
    TestCachedComponent childCached = child.AddComponent<TestCachedComponent>();

    TestCachedComponent result = childCached.ParentGet<TestCachedComponent>();
    Assert.IsNotNull(result);
    Assert.AreEqual(cached, result);

    Object.DestroyImmediate(child);
  }

  /// <summary> ParentGet returns null when no parent has component. </summary>
  [Test]
  public void ParentGet_ReturnsNullWhenNotFound()
  {
    GameObject child = new("Child");
    child.transform.SetParent(root.transform);
    TestCachedComponent childCached = child.AddComponent<TestCachedComponent>();

    BoxCollider result = childCached.ParentGet<BoxCollider>();
    Assert.IsNull(result);

    Object.DestroyImmediate(child);
  }

  /// <summary> ParentGets returns all matching components in parents. </summary>
  [Test]
  public void ParentGets_ReturnsAllMatchingInParents()
  {
    GameObject child = new("Child");
    child.transform.SetParent(root.transform);
    TestCachedComponent childCached = child.AddComponent<TestCachedComponent>();

    TestCachedComponent[] results = childCached.ParentGets<TestCachedComponent>();
    Assert.IsNotNull(results);
    Assert.AreEqual(1, results.Length);
    Assert.AreEqual(cached, results[0]);

    Object.DestroyImmediate(child);
  }

  /// <summary> ClearCachedComponents does not throw. </summary>
  [Test]
  public void ClearCachedComponents_DoesNotThrow()
  {
    Assert.DoesNotThrow(() => cached.ClearCachedComponents());
  }

  /// <summary> Cached transform returns same reference. </summary>
  [Test]
  public void CachedTransform_ReturnsSameReference()
  {
    Transform first = cached.transform;
    Transform second = cached.transform;
    Assert.AreEqual(first, second);
  }

  /// <summary> Cached rectTransform returns same reference. </summary>
  [Test]
  public void CachedRectTransform_ReturnsSameReference()
  {
    GameObject uiRoot = new("TestUI");
    Canvas canvas = uiRoot.AddComponent<Canvas>();
    TestCachedComponent uiCached = uiRoot.AddComponent<TestCachedComponent>();

    RectTransform first = uiCached.rectTransform;
    RectTransform second = uiCached.rectTransform;
    Assert.IsNotNull(first);
    Assert.AreEqual(first, second);

    Object.DestroyImmediate(uiRoot);
  }
}
