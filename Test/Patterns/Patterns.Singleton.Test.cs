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
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;
using UnityEngine;

/// <summary> Patterns tests. </summary>
public partial class PatternsTests
{
  [ExecuteInEditMode]
  private class MonoBehaviourSingletonTest : MonoBehaviourSingleton<MonoBehaviourSingletonTest> { };
  
  /// <summary> Singleton test. </summary>
  [UnityTest]
  public IEnumerator Singleton()
  {
    Assert.IsFalse(MonoBehaviourSingletonTest.IsCreated);

    GameObject gameObject = new();
    MonoBehaviourSingletonTest monoBehaviourSingletonTest = gameObject.AddComponent<MonoBehaviourSingletonTest>();

    Assert.NotNull(MonoBehaviourSingletonTest.Instance);
    Assert.AreEqual(monoBehaviourSingletonTest.GetInstanceID(), MonoBehaviourSingletonTest.Instance.GetInstanceID());
    Assert.IsTrue(MonoBehaviourSingletonTest.IsCreated);

    gameObject.SafeDestroy();

    Assert.IsFalse(MonoBehaviourSingletonTest.IsCreated);

    monoBehaviourSingletonTest = MonoBehaviourSingletonTest.Instance;

    Assert.NotNull(monoBehaviourSingletonTest);
    Assert.IsTrue(MonoBehaviourSingletonTest.IsCreated);
    Assert.AreEqual(monoBehaviourSingletonTest.GetInstanceID(), MonoBehaviourSingletonTest.Instance.GetInstanceID());
    
    yield return null;
  }
}

