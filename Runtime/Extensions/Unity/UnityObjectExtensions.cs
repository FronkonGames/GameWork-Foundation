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
  /// <summary> Unity Object extensions. </summary>
  public static class UnityObjectExtension
  {
    /// <summary> Destroys the object safely, using Destroy in play mode and DestroyImmediate otherwise. </summary>
    /// <param name="obj">Object to destroy.</param>
    public static void SmartDestroy(this UnityEngine.Object obj)
    {
      if (obj == null)
        return;

#if UNITY_EDITOR
      if (Application.isEditor == true)
        UnityEngine.Object.DestroyImmediate(obj);
      else
#endif
      UnityEngine.Object.Destroy(obj);
    }

    /// <summary> Returns true if the object is not null (handles Unity Object destruction). </summary>
    /// <param name="obj">Object to check.</param>
    /// <returns>True if the object is not null.</returns>
    public static bool IsNull(this UnityEngine.Object obj) => obj == null;

    /// <summary> Returns true if the object is not null and not destroyed. </summary>
    /// <param name="obj">Object to check.</param>
    /// <returns>True if the object is valid and alive.</returns>
    public static bool IsValid(this UnityEngine.Object obj) => obj != null;

    /// <summary> Marks the object dirty in the editor when not playing. </summary>
    public static void TrySetDirty<T>(this T obj) where T : UnityEngine.Object
    {
#if UNITY_EDITOR
      if (obj != null)
        UnityEditor.EditorUtility.SetDirty(obj);
#endif
    }

    /// <summary> Loads a resource when the current reference is null. </summary>
    public static T LoadWhenNull<T>(this T current, Func<T> load) where T : UnityEngine.Object
    {
      if (current == null)
        return load.Invoke();

      return current;
    }
  }
}
