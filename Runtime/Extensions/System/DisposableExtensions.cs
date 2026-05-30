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

namespace FronkonGames.GameWork.Foundation
{
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
  }
}
