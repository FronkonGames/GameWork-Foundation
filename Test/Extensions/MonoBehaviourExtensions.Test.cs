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
using UnityEngine;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> MonoBehaviour extensions tests. </summary>
public class MonoBehaviourExtensionsTests
{
  private GameObject gameObject;
  private MonoBehaviour monoBehaviour;

  [SetUp]
  public void SetUp()
  {
    gameObject = new GameObject("TestMonoBehaviour");
    monoBehaviour = gameObject.AddComponent<TestMonoBehaviour>();
  }

  [TearDown]
  public void TearDown()
  {
    Object.DestroyImmediate(gameObject);
  }

  /// <summary> SafeStartCoroutine returns null when MonoBehaviour is null. </summary>
  [Test]
  public void SafeStartCoroutine_NullMonoBehaviour_ReturnsNull()
  {
    MonoBehaviour nullMono = null;
    Coroutine result = nullMono.SafeStartCoroutine(WaitOneFrame());

    Assert.IsNull(result);
  }

  /// <summary> SafeStartCoroutine returns null when routine is null. </summary>
  [Test]
  public void SafeStartCoroutine_NullRoutine_ReturnsNull()
  {
    Coroutine result = monoBehaviour.SafeStartCoroutine(null);

    Assert.IsNull(result);
  }

  /// <summary> SafeStartCoroutine returns a Coroutine when valid. </summary>
  [UnityTest]
  public IEnumerator SafeStartCoroutine_ValidInput_ReturnsCoroutine()
  {
    Coroutine result = monoBehaviour.SafeStartCoroutine(WaitOneFrame());

    Assert.IsNotNull(result);

    yield return null;
  }

  /// <summary> SafeStopCoroutine with null coroutine does not throw. </summary>
  [Test]
  public void SafeStopCoroutine_NullCoroutine_DoesNotThrow()
  {
    Assert.DoesNotThrow(() => monoBehaviour.SafeStopCoroutine(null));
  }

  /// <summary> SafeStopCoroutine with null MonoBehaviour does not throw. </summary>
  [Test]
  public void SafeStopCoroutine_NullMonoBehaviour_DoesNotThrow()
  {
    MonoBehaviour nullMono = null;

    Assert.DoesNotThrow(() => nullMono.SafeStopCoroutine(null));
  }

  /// <summary> SafeStopCoroutine stops a running coroutine without throwing. </summary>
  [UnityTest]
  public IEnumerator SafeStopCoroutine_RunningCoroutine_DoesNotThrow()
  {
    Coroutine coroutine = monoBehaviour.SafeStartCoroutine(WaitOneFrame());

    Assert.DoesNotThrow(() => monoBehaviour.SafeStopCoroutine(coroutine));

    yield return null;
  }

  /// <summary> ResolveComponentFromChildrenIfNull resolves component from child. </summary>
  [Test]
  public void ResolveComponentFromChildrenIfNull_ResolvesFromChild()
  {
    GameObject child = new("Child");
    child.transform.SetParent(gameObject.transform);
    BoxCollider expected = child.AddComponent<BoxCollider>();

    BoxCollider component = null;
    monoBehaviour.ResolveComponentFromChildrenIfNull(ref component);

    Assert.IsNotNull(component);
    Assert.AreEqual(expected, component);

    Object.DestroyImmediate(child);
  }

  /// <summary> ResolveComponentFromChildrenIfNull does not override existing reference. </summary>
  [Test]
  public void ResolveComponentFromChildrenIfNull_DoesNotOverrideExisting()
  {
    GameObject child = new("Child");
    child.transform.SetParent(gameObject.transform);
    child.AddComponent<BoxCollider>();

    BoxCollider existing = new GameObject("Other").AddComponent<BoxCollider>();
    BoxCollider component = existing;
    monoBehaviour.ResolveComponentFromChildrenIfNull(ref component);

    Assert.AreEqual(existing, component);

    Object.DestroyImmediate(child);
    Object.DestroyImmediate(existing.gameObject);
  }

  /// <summary> ResolveComponentFromChildrenOrParentIfNull resolves from child. </summary>
  [Test]
  public void ResolveComponentFromChildrenOrParentIfNull_ResolvesFromChild()
  {
    GameObject child = new("Child");
    child.transform.SetParent(gameObject.transform);
    BoxCollider expected = child.AddComponent<BoxCollider>();

    BoxCollider component = null;
    monoBehaviour.ResolveComponentFromChildrenOrParentIfNull(ref component);

    Assert.IsNotNull(component);
    Assert.AreEqual(expected, component);

    Object.DestroyImmediate(child);
  }

  /// <summary> ResolveComponentFromChildrenOrParentIfNull resolves from parent when no child. </summary>
  [Test]
  public void ResolveComponentFromChildrenOrParentIfNull_ResolvesFromParent()
  {
    GameObject parent = new("Parent");
    parent.AddComponent<BoxCollider>();
    gameObject.transform.SetParent(parent.transform);

    BoxCollider component = null;
    monoBehaviour.ResolveComponentFromChildrenOrParentIfNull(ref component);

    Assert.IsNotNull(component);
    Assert.AreEqual(parent.GetComponent<BoxCollider>(), component);

    Object.DestroyImmediate(parent);
  }

  /// <summary> ResolveComponentFromChildrenOrParentIfNull does not override existing reference. </summary>
  [Test]
  public void ResolveComponentFromChildrenOrParentIfNull_DoesNotOverrideExisting()
  {
    GameObject child = new("Child");
    child.transform.SetParent(gameObject.transform);
    child.AddComponent<BoxCollider>();

    BoxCollider existing = new GameObject("Other").AddComponent<BoxCollider>();
    BoxCollider component = existing;
    monoBehaviour.ResolveComponentFromChildrenOrParentIfNull(ref component);

    Assert.AreEqual(existing, component);

    Object.DestroyImmediate(child);
    Object.DestroyImmediate(existing.gameObject);
  }

  private static IEnumerator WaitOneFrame()
  {
    yield return null;
  }
}

/// <summary> Test MonoBehaviour component. </summary>
public class TestMonoBehaviour : MonoBehaviour
{
}
