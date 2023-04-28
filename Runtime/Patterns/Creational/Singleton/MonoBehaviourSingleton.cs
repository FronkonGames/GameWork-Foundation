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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Generic lazy non-persistent between scenes MonoBehaviour singleton thread-safe. </summary>
  /// <remarks>
  /// FindObjectOfType is executed the first time you call Instance, so it is not recommended to do it in Update() or similar.
  /// </remarks>
  /// <typeparam name="T">Singleton type</typeparam>
  [DisallowMultipleComponent]
  public abstract class MonoBehaviourSingleton<T> : BaseMonoBehaviour where T : BaseMonoBehaviour
  {
    /// <summary> Instance. </summary>
    public static T Instance => Lazy.Value;

    /// <summary> Instance created? </summary>
    public static bool IsCreated => lazy?.IsValueCreated == true;

    private static Lazy<T> Lazy
    {
      get => lazy ??= new(LazyCreate);
      set => lazy = value;
    }

    private static Lazy<T> lazy;

    private static T LazyCreate() => FindObjectOfType<T>(true) ?? new GameObject(typeof(T).Name).AddComponent<T>();

    /// <remarks> Don't forget to call 'base.OnDestroy()' in the overloaded method. </remarks>
    protected virtual void OnDestroy() => Lazy = null;
  }
}
