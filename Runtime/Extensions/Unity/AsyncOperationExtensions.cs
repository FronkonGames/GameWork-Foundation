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
using UnityEngine;
using System.Threading.Tasks;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Extension methods for Unity's AsyncOperation type. </summary>
  public static class AsyncOperationExtensions
  {
    /// <summary> Converts an AsyncOperation to a Task. </summary>
    /// <param name="asyncOperation"> The AsyncOperation to convert. </param>
    /// <returns> A Task that completes when the AsyncOperation completes. </returns>
    public static Task AsTask(this AsyncOperation asyncOperation)
    {
      if (asyncOperation == null)
        return Task.CompletedTask;

      if (asyncOperation.isDone)
        return Task.CompletedTask;

      var tcs = new TaskCompletionSource<bool>();
      AsyncOperationHelper.Watch(asyncOperation, () => tcs.TrySetResult(true));
      return tcs.Task;
    }
  }

  internal sealed class AsyncOperationHelper : MonoBehaviour
  {
    private static AsyncOperationHelper instance;

    private readonly System.Collections.Generic.List<(AsyncOperation operation, System.Action onComplete)> pending = new();

    public static void Watch(AsyncOperation operation, System.Action onComplete)
    {
      if (instance == null)
      {
        var go = new GameObject("[AsyncOperationHelper]");
        go.hideFlags = HideFlags.HideAndDontSave;
        DontDestroyOnLoad(go);
        instance = go.AddComponent<AsyncOperationHelper>();
      }

      instance.pending.Add((operation, onComplete));
    }

    private void Update()
    {
      for (int i = pending.Count - 1; i >= 0; i--)
      {
        if (pending[i].operation.isDone)
        {
          pending[i].onComplete.Invoke();
          int last = pending.Count - 1;
          if (i != last)
            pending[i] = pending[last];
          pending.RemoveAt(last);
        }
      }
    }
  }
}
