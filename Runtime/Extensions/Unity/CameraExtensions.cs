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
  /// <summary>
  /// Camera extensions.
  /// </summary>
  public static class CameraExtensions
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="guiPosition"></param>
    /// <returns></returns>
    public static Vector3 GUIPositionToWorldPosition(this Camera self, Vector2 guiPosition) =>
      self.ScreenPointToRay(guiPosition).GetPoint(0.0f);

    public static Vector3 GUIDeltaToWorldDelta(this Camera self, Vector2 guiDelta)
    {
      Vector3 screenDelta = GUIUtility.GUIToScreenPoint(guiDelta);
      Ray worldRay = self.ScreenPointToRay(screenDelta);

      Vector3 worldDelta = worldRay.GetPoint(0.0f);
      worldDelta -= self.ScreenPointToRay(Vector3.zero).GetPoint(0.0f);

      return worldDelta;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="renderer"></param>
    /// <returns></returns>
    public static bool IsObjectVisible(this Camera self, Renderer renderer) =>
      GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(self), renderer.bounds);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static bool IsPointVisible(this Camera self, Vector3 point)
    {
      Vector3 p = self.WorldToViewportPoint(point);

      return p.x >= 0.0f && p.x <= 1.0f && p.y >= 0.0f && p.y <= 1.0f;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Rect OrhographicVisibleRect(this Camera self)
    {
      Check.True(self.orthographic);

      return new Rect(self.transform.position - new Vector3(self.aspect * self.orthographicSize, self.orthographicSize),
        new Vector2
        (
          self.aspect * self.orthographicSize * 2.0f,
          self.orthographicSize * 2.0f
        ));
    }    
  }
}
