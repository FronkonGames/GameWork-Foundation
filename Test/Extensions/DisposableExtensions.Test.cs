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
using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

namespace FronkonGames.GameWork.Foundation.Tests
{
  /// <summary> Disposable extensions tests. </summary>
  [TestFixture]
  public class DisposableExtensionsTests
  {
    /// <summary> ActionDisposable runs cleanup once. </summary>
    [Test]
    public void ActionDisposable_RunsCleanupOnce()
    {
      int count = 0;
      ActionDisposable disposable = new(() => count++);

      disposable.Dispose();
      disposable.Dispose();

      Assert.AreEqual(1, count);
    }

    /// <summary> AsDisposable wraps an action. </summary>
    [Test]
    public void AsDisposable_WrapsAction()
    {
      int count = 0;

      using (new ActionDisposable(() => count++))
      {
      }

      Assert.AreEqual(1, count);
    }

    /// <summary> DestroyOnDispose destroys a game object. </summary>
    [UnityTest]
    public IEnumerator DestroyOnDispose_DestroysGameObject()
    {
      GameObject gameObject = new("DisposableTest");

      using (gameObject.DestroyOnDispose())
      {
        Assert.IsNotNull(gameObject);
      }

      yield return null;

      Assert.IsTrue(gameObject == null);
    }

    /// <summary> EnableThenDisable toggles active state. </summary>
    [UnityTest]
    public IEnumerator EnableThenDisable_TogglesActiveState()
    {
      GameObject gameObject = new("EnableDisableTest");
      gameObject.SetActive(false);

      using (gameObject.EnableThenDisable())
      {
        Assert.IsTrue(gameObject.activeSelf);
      }

      Assert.IsFalse(gameObject.activeSelf);

      UnityEngine.Object.DestroyImmediate(gameObject);
      yield return null;
    }

    /// <summary> DisposeTemporaryTexture releases a temporary render texture. </summary>
    [UnityTest]
    public IEnumerator DisposeTemporaryTexture_ReleasesTemporary()
    {
      RenderTexture renderTexture = RenderTexture.GetTemporary(16, 16);

      using (renderTexture.DisposeTemporaryTexture())
      {
        Assert.IsNotNull(renderTexture);
      }

      yield return null;
    }
  }
}
