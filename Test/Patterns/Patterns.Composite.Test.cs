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
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> Patterns tests. </summary>
public partial class PatternsTests
{
  private class TestComponent : Composite<TestComponent>
  {
    public string Name;

    public TestComponent(string name) => Name = name;
  }

  /// <summary> Composite test. </summary>
  [UnityTest]
  public IEnumerator Composite()
  {
    TestComponent root = new("Root");
    TestComponent child1 = new("Child1");
    TestComponent child2 = new("Child2");
    TestComponent grandchild = new("Grandchild");

    root.Add(child1);
    root.Add(child2);
    child1.Add(grandchild);

    Assert.AreEqual(2, root.Count);
    Assert.IsTrue(root.HasChildren);
    Assert.AreEqual("Child1", root.GetChild(0).Name);
    Assert.AreEqual("Child2", root.GetChild(1).Name);

    Assert.AreEqual(1, child1.Count);
    Assert.AreEqual("Grandchild", child1.GetChild(0).Name);

    Assert.AreEqual(0, grandchild.Count);
    Assert.IsFalse(grandchild.HasChildren);

    Assert.IsTrue(root.Remove(child1));
    Assert.AreEqual(1, root.Count);
    Assert.IsFalse(root.Remove(child1));

    IReadOnlyList<TestComponent> children = root.GetChildren();
    Assert.AreEqual(1, children.Count);

    yield return null;
  }
}
