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
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary>
/// AwaitableExtensions tests.
/// </summary>
public class AwaitableExtensionsTests
{
  /// <summary>
  /// AsTask on Awaitable completes successfully.
  /// </summary>
  [UnityTest]
  public IEnumerator AsTask_CompletesSuccessfully()
  {
    Awaitable awaitable = Awaitable.WaitForSecondsAsync(0.01f);

    Task task = awaitable.AsTask();

    yield return new WaitUntil(() => task.IsCompleted);

    Assert.IsTrue(task.IsCompletedSuccessfully);
  }

  /// <summary>
  /// AsTask on Awaitable&lt;int&gt; returns correct value.
  /// </summary>
  [UnityTest]
  public IEnumerator AsTaskGeneric_ReturnsCorrectValue()
  {
    Awaitable<int> awaitable = AwaitableMethod();
    Task<int> task = awaitable.AsTask();

    yield return new WaitUntil(() => task.IsCompleted);

    Assert.IsTrue(task.IsCompletedSuccessfully);
    Assert.AreEqual(42, task.Result);
  }

  private static async Awaitable<int> AwaitableMethod()
  {
    await Awaitable.WaitForSecondsAsync(0.01f);
    return 42;
  }
}
