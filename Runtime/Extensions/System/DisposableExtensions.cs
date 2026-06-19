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
using UnityEngine;
using Object = UnityEngine.Object;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Runs an action when disposed. Useful with <c>using</c> statements. </summary>
  public sealed class ActionDisposable : IDisposable
  {
    private Action onDispose;
    private bool disposed;

    /// <summary> Creates a disposable that runs the given action once. </summary>
    /// <param name="onDispose"> Cleanup action. </param>
    public ActionDisposable(Action onDispose)
    {
      this.onDispose = onDispose;
    }

    /// <summary> Runs the cleanup action. </summary>
    public void Dispose()
    {
      if (disposed == true)
        return;

      onDispose?.Invoke();
      disposed = true;
    }
  }

  /// <summary> Disposable extensions. </summary>
  public static class DisposableExtensions
  {
    /// <summary> Disposes all items in the array. </summary>
    public static void DisposeAll<T>(this T[] disposables) where T : IDisposable
    {
      if (disposables == null)
        return;

      for (int i = 0; i < disposables.Length; i++)
        disposables[i]?.Dispose();
    }

    /// <summary> Disposes all items in the list. </summary>
    public static void DisposeAll<T>(this List<T> disposables) where T : IDisposable
    {
      if (disposables == null)
        return;

      for (int i = 0; i < disposables.Count; i++)
        disposables[i]?.Dispose();
    }

    /// <summary> Disposes all values in the dictionary. </summary>
    public static void DisposeValues<TKey, TValue>(this Dictionary<TKey, TValue> disposables) where TValue : IDisposable
    {
      if (disposables == null)
        return;

      foreach (TValue disposable in disposables.Values)
        disposable?.Dispose();
    }

    /// <summary> Returns a disposable that destroys the object when disposed. </summary>
    /// <param name="obj"> Object to destroy. </param>
    /// <param name="immediate"> Use DestroyImmediate when true. </param>
    /// <returns> Disposable handle, or null if the object is null. </returns>
    public static IDisposable DestroyOnDispose(this Object obj, bool immediate = false)
    {
      if (obj == null)
        return null;

      return new ActionDisposable(() =>
      {
        if (obj == null)
          return;

#if UNITY_EDITOR
        if (immediate == true || Application.isEditor == true)
        {
          Object.DestroyImmediate(obj);
          return;
        }
#endif
        Object.Destroy(obj);
      });
    }

    /// <summary> Activates the game object and returns a disposable that deactivates it. </summary>
    /// <param name="gameObject"> Game object to toggle. </param>
    /// <returns> Disposable handle. </returns>
    public static IDisposable EnableThenDisable(this GameObject gameObject)
    {
      gameObject.SetActive(true);

      return new ActionDisposable(() => gameObject.SetActive(false));
    }

    /// <summary> Returns a disposable that releases a temporary render texture. </summary>
    /// <param name="renderTexture"> Temporary render texture. </param>
    /// <returns> Disposable handle. </returns>
    public static IDisposable DisposeTemporaryTexture(this RenderTexture renderTexture)
    {
      return new ActionDisposable(() => RenderTexture.ReleaseTemporary(renderTexture));
    }

    /// <summary> Returns a disposable that destroys every object in the collection. </summary>
    /// <param name="objects"> Objects to destroy. </param>
    /// <returns> Disposable handle, or null if the collection is null. </returns>
    public static IDisposable DestroyOnDispose(this IEnumerable<Object> objects)
    {
      if (objects == null)
        return null;

      return new ActionDisposable(() =>
      {
        foreach (Object obj in objects)
        {
          if (obj == null)
            continue;

#if UNITY_EDITOR
          if (Application.isEditor == true)
          {
            Object.DestroyImmediate(obj);
            continue;
          }
#endif
          Object.Destroy(obj);
        }
      });
    }

    /// <summary> Returns a disposable that runs the given action. </summary>
    /// <param name="onDispose"> Cleanup action. </param>
    /// <returns> Disposable handle. </returns>
    public static IDisposable AsDisposable(this Action onDispose) => new ActionDisposable(onDispose);
  }
}
