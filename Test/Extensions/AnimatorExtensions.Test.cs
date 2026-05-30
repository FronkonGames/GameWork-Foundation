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

/// <summary> AnimatorExtensions tests. </summary>
public class AnimatorExtensionsTests
{
  private GameObject gameObject;
  private Animator animator;

  [SetUp]
  public void SetUp()
  {
    gameObject = new GameObject("TestAnimator");
    animator = gameObject.AddComponent<Animator>();
  }

  [TearDown]
  public void TearDown()
  {
    Object.DestroyImmediate(gameObject);
  }

  [Test]
  public void IsAvailable_Null_ReturnsFalse()
  {
    Animator nullAnimator = null;

    Assert.IsFalse(nullAnimator.IsAvailable());
  }

  [Test]
  public void IsAvailable_DisabledAnimator_ReturnsFalse()
  {
    animator.enabled = false;

    Assert.IsFalse(animator.IsAvailable());
  }

  [Test]
  public void IsAvailable_ActiveAnimator_ReturnsTrue()
  {
    animator.enabled = true;

    Assert.IsTrue(animator.IsAvailable());
  }

  [Test]
  public void SetBoolSafe_ValidParameter_DoesNotThrow()
  {
    int id = Animator.StringToHash("TestBool");

    Assert.DoesNotThrow(() => animator.SetBoolSafe(id, true));
  }

  [Test]
  public void SetBoolSafe_InvalidParameter_DoesNotThrow()
  {
    int id = Animator.StringToHash("NonExistentBool");

    Assert.DoesNotThrow(() => animator.SetBoolSafe(id, false));
  }

  [Test]
  public void SetFloatSafe_ValidParameter_DoesNotThrow()
  {
    int id = Animator.StringToHash("TestFloat");

    Assert.DoesNotThrow(() => animator.SetFloatSafe(id, 1.5f));
  }

  [Test]
  public void SetTriggerSafe_ValidParameter_DoesNotThrow()
  {
    int id = Animator.StringToHash("TestTrigger");

    Assert.DoesNotThrow(() => animator.SetTriggerSafe(id));
  }

  [Test]
  public void HasParameter_ReturnsFalseWhenMissing()
  {
    Assert.IsFalse(animator.HasParameter("NonExistentParameter"));
  }
}
