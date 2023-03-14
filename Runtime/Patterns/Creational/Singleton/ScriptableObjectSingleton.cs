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
  /// <summary> Generic lazy ScriptableObject singleton thread-safe. </summary>
  /// <remarks>
  /// The scriptable object must be placed in the Resources folder, and must have the same name as its type.
  /// </remarks>
  /// <typeparam name="T">Singleton type</typeparam>
  public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
  {
    /// <summary>Instance.</summary>
    public static T Instance => lazy.Value;

    [NonSerialized]
    private static readonly Lazy<T> lazy = new Lazy<T>(() =>
    {
      string name = typeof(T).Name;
      
      T instance = Resources.Load<T>(name);
      if (instance == null)
        Log.Exception($"No instance of '{name}' found in the Resources folder. Create one inside the Resources folder, and name the file '{name}'");
      
      return instance;
    });
  }
}
