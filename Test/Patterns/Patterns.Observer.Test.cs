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
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary>
/// Patterns tests.
/// </summary>
public partial class PatternsTests
{
  private class TestPublisher : Publisher<int>
  {
    public void Fire(int i) => Notify(i);
  }

  private class TestObserver : IObserver<int>
  {
    public int Value { get; set; }

    public void OnNotify(int value) => Value += value;
  }

  /// <summary> Observer test. </summary>
  [UnityTest]
  public IEnumerator Observer()
  {
    TestPublisher publisher = new TestPublisher();
    TestObserver[] observers = new TestObserver[10];

    for (int i = 0; i < 10; ++i)
      observers[i] = new TestObserver();

    publisher.AddObservers(observers);

    for (int i = 0; i < 10; ++i)
      Assert.AreEqual(observers[i].Value, 0);

    publisher.Fire(42);

    for (int i = 0; i < 10; ++i)
      Assert.AreEqual(observers[i].Value, 42);

    publisher.RemoveObservers(observers);

    publisher.Fire(0);
    for (int i = 0; i < 10; ++i)
      Assert.AreEqual(observers[i].Value, 42);

    yield return null;
  }
}
