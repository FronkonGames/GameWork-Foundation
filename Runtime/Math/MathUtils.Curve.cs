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
  /// <summary> Math utilities. </summary>
  public static partial class MathUtils
  {
    /// <summary> Evaluates a Bezier curve at t using De Casteljau's algorithm. </summary>
    /// <param name="start"> Start point. </param>
    /// <param name="end"> End point. </param>
    /// <param name="t"> Interpolation factor in [0, 1]. </param>
    /// <param name="controlPoints"> Optional control points between start and end. </param>
    /// <returns> Point on the curve. </returns>
    public static Vector3 Bezier(Vector3 start, Vector3 end, float t, Vector3[] controlPoints = null)
    {
      t = Mathf.Clamp01(t);

      if (controlPoints == null || controlPoints.Length == 0)
        return Vector3.Lerp(start, end, t);

      Vector3[] points = new Vector3[controlPoints.Length + 2];
      points[0] = start;
      points[^1] = end;

      for (int i = 0; i < controlPoints.Length; i++)
        points[i + 1] = controlPoints[i];

      int maxIterCount = points.Length - 1;

      for (int iteration = 0; iteration < maxIterCount; iteration++)
      {
        Vector3[] subPoints = new Vector3[points.Length - 1];

        for (int i = 0; i < points.Length - 1; i++)
          subPoints[i] = Vector3.Lerp(points[i], points[i + 1], t);

        points = subPoints;
      }

      return points[0];
    }

    /// <summary> Evaluates a Bezier curve at t using De Casteljau's algorithm. </summary>
    /// <param name="start"> Start point. </param>
    /// <param name="end"> End point. </param>
    /// <param name="t"> Interpolation factor in [0, 1]. </param>
    /// <param name="controlPoints"> Optional control points between start and end. </param>
    /// <returns> Point on the curve. </returns>
    public static Vector2 Bezier(Vector2 start, Vector2 end, float t, Vector2[] controlPoints = null)
    {
      t = Mathf.Clamp01(t);

      if (controlPoints == null || controlPoints.Length == 0)
        return Vector2.Lerp(start, end, t);

      Vector2[] points = new Vector2[controlPoints.Length + 2];
      points[0] = start;
      points[^1] = end;

      for (int i = 0; i < controlPoints.Length; i++)
        points[i + 1] = controlPoints[i];

      int maxIterCount = points.Length - 1;

      for (int iteration = 0; iteration < maxIterCount; iteration++)
      {
        Vector2[] subPoints = new Vector2[points.Length - 1];

        for (int i = 0; i < points.Length - 1; i++)
          subPoints[i] = Vector2.Lerp(points[i], points[i + 1], t);

        points = subPoints;
      }

      return points[0];
    }
  }
}
