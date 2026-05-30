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
  /// <summary> Camera extensions. </summary>
  public static class CameraExt
  {
    /// <summary> Calculates viewport extents with an optional margin. Useful for frustum culling. </summary>
    /// <param name="camera">Camera.</param>
    /// <param name="viewportMargin">Optional margin added to each side of the viewport.</param>
    /// <returns>Viewport extents in world units.</returns>
    public static Vector2 GetViewportExtentsWithMargin(this Camera camera, Vector2? viewportMargin = null)
    {
      float halfHeight = camera.orthographic ? camera.orthographicSize : camera.WorldToScreenPoint(camera.transform.position + camera.transform.forward * camera.nearClipPlane).y / camera.pixelHeight * camera.nearClipPlane;
      float halfWidth = halfHeight * camera.aspect;

      Vector2 margin = viewportMargin ?? Vector2.zero;

      return new Vector2(halfWidth + margin.x, halfHeight + margin.y);
    }

    /// <summary> Returns the world position at the center of the camera's view. </summary>
    /// <param name="camera">Camera.</param>
    /// <returns>World position at the center of the viewport.</returns>
    public static Vector3 GetCenterWorldPosition(this Camera camera)
    {
      return camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.nearClipPlane));
    }

    /// <summary> Returns the world position at a screen point. </summary>
    /// <param name="camera">Camera.</param>
    /// <param name="screenPosition">Screen position in pixels.</param>
    /// <param name="depth">Distance from the camera along its forward axis.</param>
    /// <returns>World position corresponding to the screen point.</returns>
    public static Vector3 ScreenToWorldPosition(this Camera camera, Vector3 screenPosition, float depth)
    {
      screenPosition.z = depth;

      return camera.ScreenToWorldPoint(screenPosition);
    }
  }
}
