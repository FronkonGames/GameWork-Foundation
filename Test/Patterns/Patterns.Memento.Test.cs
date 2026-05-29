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
  private class TestMementoOriginator : IOriginator<int>
  {
    public int Value;

    public IMemento<int> CreateMemento() => new Memento<int>(Value);
    public void SetMemento(IMemento<int> memento) => Value = memento.State;
  }

  /// <summary> Memento test. </summary>
  [UnityTest]
  public IEnumerator Memento()
  {
    TestMementoOriginator originator = new();
    originator.Value = 42;

    IMemento<int> memento = originator.CreateMemento();
    Assert.AreEqual(42, memento.State);

    originator.Value = 100;
    Assert.AreEqual(100, originator.Value);

    originator.SetMemento(memento);
    Assert.AreEqual(42, originator.Value);

    List<IMemento<int>> history = new();
    originator.Value = 10;
    history.Add(originator.CreateMemento());
    originator.Value = 20;
    history.Add(originator.CreateMemento());
    originator.Value = 30;

    originator.SetMemento(history[0]);
    Assert.AreEqual(10, originator.Value);
    originator.SetMemento(history[1]);
    Assert.AreEqual(20, originator.Value);

    yield return null;
  }
}
