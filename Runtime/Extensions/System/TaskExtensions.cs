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
using System.Threading.Tasks;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Extension methods for <see cref="Task"/> and <see cref="Task{T}"/>.
  /// </summary>
  public static class TaskExtensions
  {
    /// <summary>
    /// Wraps an object into a completed Task.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object to wrap.</param>
    /// <returns>A completed Task containing the object.</returns>
    public static Task<T> AsCompletedTask<T>(this T obj) => Task.FromResult(obj);

    /// <summary>
    /// Converts a Task to an IEnumerator for coroutine usage.
    /// </summary>
    /// <param name="task">The Task to convert.</param>
    /// <returns>An IEnumerator that can be yielded in a coroutine.</returns>
    public static IEnumerator AsCoroutine(this Task task)
    {
      while (!task.IsCompleted)
      {
        yield return null;
      }

      if (task.IsFaulted)
      {
        throw task.Exception;
      }
    }

    /// <summary>
    /// Fire-and-forget with optional exception handling.
    /// </summary>
    /// <param name="task">The Task to run without awaiting.</param>
    /// <param name="onException">Optional callback invoked when an exception occurs.</param>
    public static async void Forget(this Task task, Action<Exception> onException = null)
    {
      try
      {
        await task;
      }
      catch (Exception ex)
      {
        onException?.Invoke(ex);
      }
    }

    /// <summary>
    /// Polls a condition until true or timeout.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="timeoutMs">Maximum time to wait in milliseconds. -1 for infinite.</param>
    /// <param name="pollIntervalMs">Interval between polls in milliseconds.</param>
    /// <returns>True if the condition was met, false if timed out.</returns>
    public static async Task<bool> WaitUntil(this Func<bool> condition, int timeoutMs = -1, int pollIntervalMs = 33)
    {
      if (condition())
      {
        return true;
      }

      var stopwatch = System.Diagnostics.Stopwatch.StartNew();

      while (true)
      {
        await Task.Delay(pollIntervalMs);

        if (condition())
        {
          return true;
        }

        if (timeoutMs >= 0 && stopwatch.ElapsedMilliseconds >= timeoutMs)
        {
          return false;
        }
      }
    }
  }
}
