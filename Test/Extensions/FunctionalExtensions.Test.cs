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
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using FronkonGames.GameWork.Foundation;

/// <summary>
/// Functional extensions test.
/// </summary>
public class FunctionalExtensionsTests
{
  /// <summary>
  /// Do executes action and returns the same value.
  /// </summary>
  [Test]
  public void DoExecutesActionAndReturnsSameValue()
  {
    int result = 42.Do(v => Assert.AreEqual(42, v));

    Assert.AreEqual(42, result);
  }

  /// <summary>
  /// Do with null action does not throw.
  /// </summary>
  [Test]
  public void DoWithNullActionDoesNotThrow()
  {
    Assert.DoesNotThrow(() => 42.Do(null));
  }

  /// <summary>
  /// Map transforms value correctly.
  /// </summary>
  [Test]
  public void MapTransformsValueCorrectly()
  {
    int result = 5.Map(v => v * 2);

    Assert.AreEqual(10, result);
  }

  /// <summary>
  /// Map with identity function returns the same value.
  /// </summary>
  [Test]
  public void MapWithIdentityFunctionReturnsSameValue()
  {
    string result = "hello".Map(v => v);

    Assert.AreEqual("hello", result);
  }

  /// <summary>
  /// Do chained with Map creates a fluent pipeline.
  /// </summary>
  [Test]
  public void DoChainedWithMap_FluentPipelineWorks()
  {
    int result = 5
      .Do(v => Assert.AreEqual(5, v))
      .Map(v => v + 10)
      .Do(v => Assert.AreEqual(15, v))
      .Map(v => v * 2);

    Assert.AreEqual(30, result);
  }

  [Flags]
  private enum SampleFlags
  {
    None = 0,
    A = 1,
    B = 2,
    C = 4
  }

  [Test]
  public void HasAnyFlag_DetectsMatchingFlags()
  {
    SampleFlags value = SampleFlags.A | SampleFlags.B;

    Assert.IsTrue(value.HasAnyFlag(SampleFlags.A));
    Assert.IsTrue(value.HasAnyFlag(SampleFlags.B, SampleFlags.C));
    Assert.IsTrue(value.HasNoneOfFlags(SampleFlags.C));
    Assert.IsFalse(value.HasNoneOfFlags(SampleFlags.A));
  }

  [Test]
  public void InvokeAllSafe_AllowsListModificationDuringInvoke()
  {
    List<Action> actions = new();
    int count = 0;
    actions.Add(() =>
    {
      count++;
      actions.Clear();
    });

    actions.InvokeAllSafe();

    Assert.AreEqual(1, count);
  }

  [Test]
  public void InvokeAny_StopsAtFirstMatch()
  {
    int calls = 0;
    Func<bool>[] actions =
    {
      () => { calls++; return false; },
      () => { calls++; return true; },
      () => { calls++; return true; }
    };

    actions.InvokeAny();

    Assert.AreEqual(2, calls);
  }

  [Test]
  public void DisposeAll_DisposesAllItems()
  {
    TestDisposable[] items = { new(), new() };

    items.DisposeAll();

    Assert.IsTrue(items[0].Disposed);
    Assert.IsTrue(items[1].Disposed);
  }

  [Test]
  public void TimeSpanExtensions_ComputeWholeUnits()
  {
    TimeSpan span = new(1, 2, 3, 4);

    Assert.AreEqual(26, span.TotalWholeHours());
    Assert.AreEqual(1563, span.TotalWholeMinutes());
    Assert.AreEqual(TimeSpan.FromSeconds(58), TimeSpan.FromMinutes(1).SubtractSeconds(2));
  }

  [Test]
  public void CancellationTokenSource_RecreateCreatesFreshToken()
  {
    CancellationTokenSource source = new();
    source.Cancel();

    CancellationTokenSource recreated = source.Recreate();

    Assert.IsFalse(recreated.IsCancellationRequested);
    recreated.Dispose();
  }

  private sealed class TestDisposable : IDisposable
  {
    public bool Disposed { get; private set; }

    public void Dispose() => Disposed = true;
  }
}
