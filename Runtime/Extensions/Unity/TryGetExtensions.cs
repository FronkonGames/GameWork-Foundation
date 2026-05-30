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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> TryGet extensions for Component and GameObject. </summary>
  public static class TryGetExtensions
  {
    /// <summary> Try to get a component in the children. </summary>
    public static bool TryGetComponentInChildren<T>(this Component self, out T component, bool includeInactive = false) where T : Component
    {
      component = self.GetComponentInChildren<T>(includeInactive);
      return component != null;
    }

    /// <summary> Try to get a component in the children. </summary>
    public static bool TryGetComponentInChildren<T>(this GameObject self, out T component, bool includeInactive = false) where T : Component
    {
      component = self.GetComponentInChildren<T>(includeInactive);
      return component != null;
    }

    /// <summary> Try to get a component in the parent. </summary>
    public static bool TryGetComponentInParent<T>(this Component self, out T component, bool includeInactive = false) where T : Component
    {
      component = self.GetComponentInParent<T>(includeInactive);
      return component != null;
    }

    /// <summary> Try to get a component in the parent. </summary>
    public static bool TryGetComponentInParent<T>(this GameObject self, out T component, bool includeInactive = false) where T : Component
    {
      component = self.GetComponentInParent<T>(includeInactive);
      return component != null;
    }

    /// <summary> Try to get all components in the children. </summary>
    public static bool TryGetComponentsInChildren<T>(this Component self, out T[] components, bool includeInactive = false) where T : Component
    {
      components = self.GetComponentsInChildren<T>(includeInactive);
      return components.Length > 0;
    }

    /// <summary> Try to get all components in the children. </summary>
    public static bool TryGetComponentsInChildren<T>(this GameObject self, out T[] components, bool includeInactive = false) where T : Component
    {
      components = self.GetComponentsInChildren<T>(includeInactive);
      return components.Length > 0;
    }
  }
}
