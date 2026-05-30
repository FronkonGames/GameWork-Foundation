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

/// <summary> ComponentExtensions tests. </summary>
public class ComponentExtensionsTests
{
  private GameObject gameObject;
  private GameObject parentGameObject;
  private Transform component;

  [SetUp]
  public void SetUp()
  {
    parentGameObject = new GameObject("Parent");
    gameObject = new GameObject("Child");
    gameObject.transform.SetParent(parentGameObject.transform);
    component = gameObject.transform;
  }

  [TearDown]
  public void TearDown()
  {
    Object.DestroyImmediate(gameObject);
    Object.DestroyImmediate(parentGameObject);
  }

  [Test]
  public void SetGameObjectActive_Deactivate()
  {
    gameObject.SetActive(true);

    component.SetGameObjectActive(false);

    Assert.IsFalse(gameObject.activeSelf);
  }

  [Test]
  public void SetGameObjectActive_Activate()
  {
    gameObject.SetActive(false);

    component.SetGameObjectActive(true);

    Assert.IsTrue(gameObject.activeSelf);
  }

  [Test]
  public void SetParentGameObjectActive_Deactivate()
  {
    parentGameObject.SetActive(true);

    component.SetParentGameObjectActive(false);

    Assert.IsFalse(parentGameObject.activeSelf);
  }

  [Test]
  public void SetParentGameObjectActive_Activate()
  {
    parentGameObject.SetActive(false);

    component.SetParentGameObjectActive(true);

    Assert.IsTrue(parentGameObject.activeSelf);
  }

  [Test]
  public void HasComponent_OnComponent_True()
  {
    Assert.IsTrue(component.HasComponent<Transform>());
  }

  [Test]
  public void HasComponent_OnGameObject_True()
  {
    Assert.IsTrue(gameObject.HasComponent<Transform>());
  }

  [Test]
  public void HasComponent_OnComponent_False()
  {
    Assert.IsFalse(component.HasComponent<BoxCollider>());
  }

  [Test]
  public void HasComponent_OnGameObject_False()
  {
    Assert.IsFalse(gameObject.HasComponent<BoxCollider>());
  }

  [Test]
  public void TryGetComponent_OnComponent_True()
  {
    bool found = component.TryGetComponent(out Transform result);

    Assert.IsTrue(found);
    Assert.IsNotNull(result);
    Assert.AreEqual(component, result);
  }

  [Test]
  public void TryGetComponent_OnGameObject_True()
  {
    bool found = gameObject.TryGetComponent(out Transform result);

    Assert.IsTrue(found);
    Assert.IsNotNull(result);
    Assert.AreEqual(gameObject.transform, result);
  }

  [Test]
  public void TryGetComponent_OnComponent_False()
  {
    bool found = component.TryGetComponent(out BoxCollider result);

    Assert.IsFalse(found);
    Assert.IsNull(result);
  }

  [Test]
  public void TryGetComponent_OnGameObject_False()
  {
    bool found = gameObject.TryGetComponent(out BoxCollider result);

    Assert.IsFalse(found);
    Assert.IsNull(result);
  }
}
