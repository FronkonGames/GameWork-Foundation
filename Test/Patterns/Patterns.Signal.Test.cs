////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary>
/// Signal pattern tests.
/// </summary>
public partial class PatternsTests
{
  private class TestSignal : ScriptableSignal { }

  private class TestSignalInt : ScriptableSignal<int> { }

  private class TestSignalFloatBool : ScriptableSignal<float, bool> { }

  /// <summary> Subscribe and emit parameterless signal. </summary>
  [UnityTest]
  public IEnumerator Signal_SubscribeAndEmit()
  {
    TestSignal signal = ScriptableObject.CreateInstance<TestSignal>();

    int counter = 0;
    Action callback = () => counter++;

    signal.Subscribe(callback);

    signal.Emit();
    Assert.AreEqual(1, counter);

    signal.Emit();
    Assert.AreEqual(2, counter);

    signal.Unsubscribe(callback);

    signal.Emit();
    Assert.AreEqual(2, counter);

    yield return null;
  }

  /// <summary> Multiple subscribers on parameterless signal. </summary>
  [UnityTest]
  public IEnumerator Signal_MultipleSubscribers()
  {
    TestSignal signal = ScriptableObject.CreateInstance<TestSignal>();

    int a = 0, b = 0, c = 0;
    Action cbA = () => a++;
    Action cbB = () => b++;
    Action cbC = () => c++;

    signal.Subscribe(cbA);
    signal.Subscribe(cbB);
    signal.Subscribe(cbC);

    signal.Emit();

    Assert.AreEqual(1, a);
    Assert.AreEqual(1, b);
    Assert.AreEqual(1, c);

    signal.Unsubscribe(cbB);

    signal.Emit();

    Assert.AreEqual(2, a);
    Assert.AreEqual(1, b);
    Assert.AreEqual(2, c);

    yield return null;
  }

  /// <summary> Duplicate subscribe is ignored. </summary>
  [UnityTest]
  public IEnumerator Signal_DuplicateSubscribeIgnored()
  {
    TestSignal signal = ScriptableObject.CreateInstance<TestSignal>();

    int counter = 0;
    Action callback = () => counter++;

    signal.Subscribe(callback);
    signal.Subscribe(callback);
    signal.Subscribe(callback);

    signal.Emit();

    Assert.AreEqual(1, counter);

    yield return null;
  }

  /// <summary> Unsubscribe null throws. </summary>
  [UnityTest]
  public IEnumerator Signal_UnsubscribeNullThrows()
  {
    TestSignal signal = ScriptableObject.CreateInstance<TestSignal>();

    Assert.Throws<ArgumentNullException>(() => signal.Unsubscribe(null));

    yield return null;
  }

  /// <summary> Subscribe null throws. </summary>
  [UnityTest]
  public IEnumerator Signal_SubscribeNullThrows()
  {
    TestSignal signal = ScriptableObject.CreateInstance<TestSignal>();

    Assert.Throws<ArgumentNullException>(() => signal.Subscribe(null));

    yield return null;
  }

  /// <summary> Clear removes all subscribers. </summary>
  [UnityTest]
  public IEnumerator Signal_ClearRemovesAll()
  {
    TestSignal signal = ScriptableObject.CreateInstance<TestSignal>();

    int counter = 0;
    Action callback = () => counter++;

    signal.Subscribe(callback);
    signal.Subscribe(() => counter++);

    signal.Emit();
    Assert.AreEqual(2, counter);

    signal.Clear();

    signal.Emit();
    Assert.AreEqual(2, counter);

    yield return null;
  }

  /// <summary> Generic signal passes value to subscribers. </summary>
  [UnityTest]
  public IEnumerator Signal_Generic_PassesValue()
  {
    TestSignalInt signal = ScriptableObject.CreateInstance<TestSignalInt>();

    int received = 0;
    signal.Subscribe((int value) => received = value);

    signal.Emit(42);
    Assert.AreEqual(42, received);

    signal.Emit(100);
    Assert.AreEqual(100, received);

    yield return null;
  }

  /// <summary> Generic signal with multiple subscribers. </summary>
  [UnityTest]
  public IEnumerator Signal_Generic_MultipleSubscribers()
  {
    TestSignalInt signal = ScriptableObject.CreateInstance<TestSignalInt>();

    int sum = 0;
    signal.Subscribe((int value) => sum += value);
    signal.Subscribe((int value) => sum += value * 10);

    signal.Emit(3);

    Assert.AreEqual(33, sum);

    yield return null;
  }

  /// <summary> Two-parameter signal passes both values. </summary>
  [UnityTest]
  public IEnumerator Signal_TwoParam_PassesValues()
  {
    TestSignalFloatBool signal = ScriptableObject.CreateInstance<TestSignalFloatBool>();

    float receivedFloat = 0f;
    bool receivedBool = false;

    signal.Subscribe((float f, bool b) =>
    {
      receivedFloat = f;
      receivedBool = b;
    });

    signal.Emit(3.14f, true);

    Assert.AreEqual(3.14f, receivedFloat, 0.001f);
    Assert.AreEqual(true, receivedBool);

    yield return null;
  }

  /// <summary> Unsubscribe from generic signal stops notifications. </summary>
  [UnityTest]
  public IEnumerator Signal_Generic_UnsubscribeStopsNotifications()
  {
    TestSignalInt signal = ScriptableObject.CreateInstance<TestSignalInt>();

    int counter = 0;
    Action<int> callback = (int value) => counter++;

    signal.Subscribe(callback);
    signal.Emit(1);
    Assert.AreEqual(1, counter);

    signal.Unsubscribe(callback);
    signal.Emit(2);
    Assert.AreEqual(1, counter);

    yield return null;
  }

  /// <summary> SignalRegistry register and lookup. </summary>
  [UnityTest]
  public IEnumerator SignalRegistry_RegisterAndLookup()
  {
    SignalRegistry registry = new SignalRegistry();
    TestSignal signal = ScriptableObject.CreateInstance<TestSignal>();

    registry.Register(signal);

    Assert.IsTrue(registry.TryGet(typeof(TestSignal), out ScriptableObject found));
    Assert.AreEqual(signal, found);

    yield return null;
  }

  /// <summary> SignalRegistry TryGet returns false for unregistered type. </summary>
  [UnityTest]
  public IEnumerator SignalRegistry_UnregisteredReturnsFalse()
  {
    SignalRegistry registry = new SignalRegistry();

    Assert.IsFalse(registry.TryGet(typeof(TestSignal), out _));

    yield return null;
  }

  /// <summary> SignalRegistry ignore null registration. </summary>
  [UnityTest]
  public IEnumerator SignalRegistry_NullIgnored()
  {
    SignalRegistry registry = new SignalRegistry();

    registry.Register<TestSignal>(null);

    Assert.IsFalse(registry.TryGet(typeof(TestSignal), out _));

    yield return null;
  }

  /// <summary> SignalRegistry Clear removes all. </summary>
  [UnityTest]
  public IEnumerator SignalRegistry_ClearRemovesAll()
  {
    SignalRegistry registry = new SignalRegistry();
    TestSignal signal = ScriptableObject.CreateInstance<TestSignal>();

    registry.Register(signal);
    Assert.IsTrue(registry.TryGet(typeof(TestSignal), out _));

    registry.Clear();
    Assert.IsFalse(registry.TryGet(typeof(TestSignal), out _));

    yield return null;
  }
}
