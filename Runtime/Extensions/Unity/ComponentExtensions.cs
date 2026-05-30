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
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Component extensions. </summary>
  public static class ComponentExtension
  {
    /// <summary> Set the parent GameObject active state. </summary>
    /// <param name="component">Component</param>
    /// <param name="isActive">New active state</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetParentGameObjectActive(this Component component, bool isActive)
    {
      if (component.transform.parent != null)
        component.transform.parent.gameObject.SetActive(isActive);
    }

    /// <summary> Set this GameObject active state. </summary>
    /// <param name="component">Component</param>
    /// <param name="isActive">New active state</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetGameObjectActive(this Component component, bool isActive) => component.gameObject.SetActive(isActive);

    /// <summary> Try to get a component from this Component. </summary>
    /// <param name="component">Component</param>
    /// <param name="result">Result component or null</param>
    /// <returns>True if the component was found</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetComponent<T>(this Component component, out T result) where T : Component
    {
      result = component.GetComponent<T>();
      return result != null;
    }

    /// <summary> Try to get a component from this GameObject. </summary>
    /// <param name="go">GameObject</param>
    /// <param name="result">Result component or null</param>
    /// <returns>True if the component was found</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetComponent<T>(this GameObject go, out T result) where T : Component
    {
      result = go.GetComponent<T>();
      return result != null;
    }

    /// <summary> Check if this Component has a component of type T. </summary>
    /// <param name="component">Component</param>
    /// <returns>True if the component exists</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasComponent<T>(this Component component) where T : Component => component.GetComponent<T>() != null;

    /// <summary> Check if this GameObject has a component of type T. </summary>
    /// <param name="go">GameObject</param>
    /// <returns>True if the component exists</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasComponent<T>(this GameObject go) where T : Component => go.GetComponent<T>() != null;
  }
}
