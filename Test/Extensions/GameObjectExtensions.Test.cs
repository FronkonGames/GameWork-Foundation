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
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> Extensions test. </summary>
public partial class ExtensionsTests
{
  /// <summary> GameObject extensions test. </summary>
  [UnityTest]
  public IEnumerator GameObject()
  {
    GameObject go = new("TestObject");
    GameObject child1 = new("Child1");
    GameObject child2 = new("Child2");
    child1.transform.SetParent(go.transform);
    child2.transform.SetParent(go.transform);

    Assert.IsNotNull(go.GetOrAddComponent<Transform>());
    Assert.AreEqual(go.transform, go.GetOrAddComponent<Transform>());

    List<GameObject> children = go.GetAllChildren();
    Assert.AreEqual(2, children.Count);

    List<GameObject> allIncludingSelf = go.GetAllChildrenAndSelf();
    Assert.AreEqual(3, allIncludingSelf.Count);

    go.SafeDestroy();
    child1.SafeDestroy();
    child2.SafeDestroy();

    yield return null;
  }
}
