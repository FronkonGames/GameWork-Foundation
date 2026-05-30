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
using System.Collections.Generic;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Renderer extensions. </summary>
  public static class RendererExt
  {
    /// <summary> Enables ZWrite for materials with a _Color property. </summary>
    /// <param name="renderer">Renderer.</param>
    public static void EnableZWrite(this Renderer renderer)
    {
      if (renderer == null)
        return;

      Material[] materials = renderer.materials;

      for (int i = 0; i < materials.Length; i++)
      {
        if (materials[i].HasProperty("_Color"))
          materials[i].SetFloat("_ZWrite", 1.0f);
      }
    }

    /// <summary> Disables ZWrite for materials with a _Color property. </summary>
    /// <param name="renderer">Renderer.</param>
    public static void DisableZWrite(this Renderer renderer)
    {
      if (renderer == null)
        return;

      Material[] materials = renderer.materials;

      for (int i = 0; i < materials.Length; i++)
      {
        if (materials[i].HasProperty("_Color"))
          materials[i].SetFloat("_ZWrite", 0.0f);
      }
    }

    /// <summary> Returns true if the renderer is visible from the given camera. </summary>
    /// <param name="renderer">Renderer.</param>
    /// <param name="camera">Camera to check visibility against.</param>
    /// <returns>True if the renderer is visible from the camera.</returns>
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
      if (renderer == null || camera == null)
        return false;

      Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
      Bounds bounds = renderer.bounds;

      return GeometryUtility.TestPlanesAABB(planes, bounds);
    }

    /// <summary> Sets enabled on all renderers in the array. </summary>
    public static T[] SetEnabled<T>(this T[] renderers, bool value) where T : Renderer
    {
      for (int i = 0; i < renderers.Length; i++)
        renderers[i].enabled = value;

      return renderers;
    }

    /// <summary> Sets enabled on all renderers in the list. </summary>
    public static List<T> SetEnabled<T>(this List<T> renderers, bool value) where T : Renderer
    {
      for (int i = 0; i < renderers.Count; i++)
        renderers[i].enabled = value;

      return renderers;
    }

    /// <summary> Sets material on all renderers in the array. </summary>
    public static T[] SetMaterial<T>(this T[] renderers, Material value) where T : Renderer
    {
      for (int i = 0; i < renderers.Length; i++)
        renderers[i].material = value;

      return renderers;
    }

    /// <summary> Sets material on all renderers in the list. </summary>
    public static List<T> SetMaterial<T>(this List<T> renderers, Material value) where T : Renderer
    {
      for (int i = 0; i < renderers.Count; i++)
        renderers[i].material = value;

      return renderers;
    }
  }
}
