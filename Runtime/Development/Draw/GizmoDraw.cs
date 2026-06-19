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
  /// <summary> Gizmo drawing helpers for <c>OnDrawGizmos</c>. </summary>
  public static class GizmoDraw
  {
    private const float InvSqrt2 = 0.7071067811865475f;
    private const int DefaultSegments = 32;

    /// <summary> Draws a 3D arrow from origin to tip. </summary>
    /// <param name="origin"> Arrow start. </param>
    /// <param name="tip"> Arrow end. </param>
    /// <param name="normal"> Normal used to orient the arrow head. </param>
    /// <param name="arrowHeadLength"> Arrow head size. </param>
    public static void Arrow(Vector3 origin, Vector3 tip, Vector3 normal, float arrowHeadLength)
    {
      Vector3 arrowDir = (tip - origin).normalized;
      Vector3 perpendicularArrowDir = Vector3.Cross(arrowDir, normal).normalized;
      float tipMultiplier = arrowHeadLength * InvSqrt2;

      Gizmos.DrawLine(origin, tip);
      Gizmos.DrawLine(tip, tip - (arrowDir + perpendicularArrowDir) * tipMultiplier);
      Gizmos.DrawLine(tip, tip - (arrowDir - perpendicularArrowDir) * tipMultiplier);
    }

    /// <summary> Draws a 2D arrow from origin to tip. </summary>
    /// <param name="origin"> Arrow start. </param>
    /// <param name="tip"> Arrow end. </param>
    /// <param name="arrowHeadLength"> Arrow head size. </param>
    public static void Arrow2D(Vector2 origin, Vector2 tip, float arrowHeadLength)
      => Arrow(origin, tip, Vector3.forward, arrowHeadLength);

    /// <summary> Draws an arrow along a direction vector. </summary>
    /// <param name="origin"> Arrow start. </param>
    /// <param name="direction"> Arrow direction. </param>
    /// <param name="length"> Arrow length. </param>
    /// <param name="normal"> Normal used to orient the arrow head. </param>
    /// <param name="arrowHeadLength"> Arrow head size. </param>
    public static void Direction(Vector3 origin, Vector3 direction, float length, Vector3 normal, float arrowHeadLength = 0.25f)
    {
      if (direction.sqrMagnitude == 0.0f)
        return;

      Vector3 tip = origin + direction.normalized * length;
      Arrow(origin, tip, normal, arrowHeadLength);
    }

    /// <summary> Draws a 2D arrow along a direction vector. </summary>
    /// <param name="origin"> Arrow start. </param>
    /// <param name="direction"> Arrow direction. </param>
    /// <param name="length"> Arrow length. </param>
    /// <param name="arrowHeadLength"> Arrow head size. </param>
    public static void Direction2D(Vector2 origin, Vector2 direction, float length, float arrowHeadLength = 0.25f)
      => Direction(origin, direction, length, Vector3.forward, arrowHeadLength);

    /// <summary> Draws a cross centered at the given position. </summary>
    /// <param name="center"> Cross center. </param>
    /// <param name="normal"> Cross plane normal. </param>
    /// <param name="up"> Up direction for orientation. </param>
    /// <param name="length"> Half-length of each cross arm. </param>
    /// <param name="angle"> Rotation around the normal in degrees. </param>
    public static void Cross(Vector3 center, Vector3 normal, Vector3 up, float length, float angle = 0.0f)
    {
      Vector3 centerVector = Vector3.Cross(normal, up).normalized;
      Vector3 crossVector1 = Quaternion.AngleAxis(45.0f + angle, normal) * centerVector * length;
      Vector3 crossVector2 = Quaternion.AngleAxis(-45.0f + angle, normal) * centerVector * length;

      Gizmos.DrawLine(center - crossVector1, center + crossVector1);
      Gizmos.DrawLine(center - crossVector2, center + crossVector2);
    }

    /// <summary> Draws a 2D cross centered at the given position. </summary>
    /// <param name="center"> Cross center. </param>
    /// <param name="length"> Half-length of each cross arm. </param>
    /// <param name="angle"> Rotation in degrees. </param>
    public static void Cross2D(Vector3 center, float length, float angle = 0.0f)
      => Cross(center, Vector3.forward, Vector3.up, length, angle);

    /// <summary> Draws a wire circle in the plane defined by the normal. </summary>
    /// <param name="center"> Circle center. </param>
    /// <param name="normal"> Plane normal. </param>
    /// <param name="radius"> Circle radius. </param>
    /// <param name="segments"> Number of line segments. </param>
    public static void WireCircle(Vector3 center, Vector3 normal, float radius, int segments = DefaultSegments)
    {
      if (radius <= 0.0f || segments < 3)
        return;

      GetPlaneVectors(normal, out Vector3 tangent, out Vector3 bitangent);
      DrawCircleSegmentLoop(center, tangent, bitangent, radius, 0.0f, MathConstants.Tau, segments);
    }

    /// <summary> Draws a wire circle on the XY plane. </summary>
    /// <param name="center"> Circle center. </param>
    /// <param name="radius"> Circle radius. </param>
    /// <param name="segments"> Number of line segments. </param>
    public static void WireCircle2D(Vector2 center, float radius, int segments = DefaultSegments)
      => WireCircle(center, Vector3.forward, radius, segments);

    /// <summary> Draws a wire arc in the plane defined by the normal. </summary>
    /// <param name="center"> Arc center. </param>
    /// <param name="normal"> Plane normal. </param>
    /// <param name="radius"> Arc radius. </param>
    /// <param name="startAngleDegrees"> Start angle in degrees. </param>
    /// <param name="endAngleDegrees"> End angle in degrees. </param>
    /// <param name="segments"> Number of line segments. </param>
    public static void WireArc(
      Vector3 center,
      Vector3 normal,
      float radius,
      float startAngleDegrees,
      float endAngleDegrees,
      int segments = DefaultSegments / 2)
    {
      if (radius <= 0.0f || segments < 2)
        return;

      GetPlaneVectors(normal, out Vector3 tangent, out Vector3 bitangent);
      float startAngle = startAngleDegrees * MathConstants.Deg2Rad;
      float endAngle = endAngleDegrees * MathConstants.Deg2Rad;
      DrawCircleSegmentLoop(center, tangent, bitangent, radius, startAngle, endAngle, segments, closedLoop: false);
    }

    /// <summary> Draws a wire arc on the XY plane. </summary>
    /// <param name="center"> Arc center. </param>
    /// <param name="radius"> Arc radius. </param>
    /// <param name="startAngleDegrees"> Start angle in degrees. </param>
    /// <param name="endAngleDegrees"> End angle in degrees. </param>
    /// <param name="segments"> Number of line segments. </param>
    public static void WireArc2D(
      Vector2 center,
      float radius,
      float startAngleDegrees,
      float endAngleDegrees,
      int segments = DefaultSegments / 2)
      => WireArc(center, Vector3.forward, radius, startAngleDegrees, endAngleDegrees, segments);

    /// <summary> Draws a wireframe axis-aligned bounds box. </summary>
    /// <param name="bounds"> Bounds to draw. </param>
    public static void WireBounds(Bounds bounds)
    {
      Vector3 min = bounds.min;
      Vector3 max = bounds.max;

      Vector3 p000 = new(min.x, min.y, min.z);
      Vector3 p001 = new(min.x, min.y, max.z);
      Vector3 p010 = new(min.x, max.y, min.z);
      Vector3 p011 = new(min.x, max.y, max.z);
      Vector3 p100 = new(max.x, min.y, min.z);
      Vector3 p101 = new(max.x, min.y, max.z);
      Vector3 p110 = new(max.x, max.y, min.z);
      Vector3 p111 = new(max.x, max.y, max.z);

      Gizmos.DrawLine(p000, p001);
      Gizmos.DrawLine(p001, p101);
      Gizmos.DrawLine(p101, p100);
      Gizmos.DrawLine(p100, p000);

      Gizmos.DrawLine(p010, p011);
      Gizmos.DrawLine(p011, p111);
      Gizmos.DrawLine(p111, p110);
      Gizmos.DrawLine(p110, p010);

      Gizmos.DrawLine(p000, p010);
      Gizmos.DrawLine(p001, p011);
      Gizmos.DrawLine(p100, p110);
      Gizmos.DrawLine(p101, p111);
    }

    /// <summary> Draws a wire rectangle on the XY plane. </summary>
    /// <param name="rect"> Rectangle to draw. </param>
    /// <param name="z"> Z coordinate. </param>
    public static void WireRect2D(Rect rect, float z = 0.0f)
    {
      Vector3 bottomLeft = new(rect.xMin, rect.yMin, z);
      Vector3 bottomRight = new(rect.xMax, rect.yMin, z);
      Vector3 topRight = new(rect.xMax, rect.yMax, z);
      Vector3 topLeft = new(rect.xMin, rect.yMax, z);

      Gizmos.DrawLine(bottomLeft, bottomRight);
      Gizmos.DrawLine(bottomRight, topRight);
      Gizmos.DrawLine(topRight, topLeft);
      Gizmos.DrawLine(topLeft, bottomLeft);
    }

    /// <summary> Draws a closed wire polygon through the given points. </summary>
    /// <param name="points"> Polygon vertices. </param>
    public static void WirePolygon(IReadOnlyList<Vector3> points)
    {
      if (points == null || points.Count < 2)
        return;

      for (int i = 0; i < points.Count; i++)
        Gizmos.DrawLine(points[i], points[(i + 1) % points.Count]);
    }

    /// <summary> Draws a closed wire polygon on the XY plane. </summary>
    /// <param name="points"> Polygon vertices. </param>
    /// <param name="z"> Z coordinate. </param>
    public static void WirePolygon2D(IReadOnlyList<Vector2> points, float z = 0.0f)
    {
      if (points == null || points.Count < 2)
        return;

      for (int i = 0; i < points.Count; i++)
      {
        Vector2 current = points[i];
        Vector2 next = points[(i + 1) % points.Count];
        Gizmos.DrawLine(new Vector3(current.x, current.y, z), new Vector3(next.x, next.y, z));
      }
    }

    /// <summary> Draws a wire Bezier curve. </summary>
    /// <param name="start"> Start point. </param>
    /// <param name="end"> End point. </param>
    /// <param name="controlPoints"> Optional control points. </param>
    /// <param name="segments"> Number of line segments. </param>
    public static void WireBezier(Vector3 start, Vector3 end, Vector3[] controlPoints = null, int segments = 24)
    {
      if (segments < 1)
        return;

      Vector3 previous = start;

      for (int i = 1; i <= segments; i++)
      {
        float t = i / (float)segments;
        Vector3 point = MathUtils.Bezier(start, end, t, controlPoints);
        Gizmos.DrawLine(previous, point);
        previous = point;
      }
    }

    /// <summary> Draws a wire Bezier curve on the XY plane. </summary>
    /// <param name="start"> Start point. </param>
    /// <param name="end"> End point. </param>
    /// <param name="controlPoints"> Optional control points. </param>
    /// <param name="segments"> Number of line segments. </param>
    public static void WireBezier2D(Vector2 start, Vector2 end, Vector2[] controlPoints = null, int segments = 24)
    {
      if (segments < 1)
        return;

      Vector2 previous = start;

      for (int i = 1; i <= segments; i++)
      {
        float t = i / (float)segments;
        Vector2 point = MathUtils.Bezier(start, end, t, controlPoints);
        Gizmos.DrawLine(previous, point);
        previous = point;
      }
    }

    /// <summary> Draws a flat wire grid centered on a point. </summary>
    /// <param name="center"> Grid center. </param>
    /// <param name="normal"> Plane normal. </param>
    /// <param name="size"> Total grid size in local X and Y. </param>
    /// <param name="divisions"> Number of cells along each axis. </param>
    public static void WireGrid(Vector3 center, Vector3 normal, Vector2 size, int divisions = 8)
    {
      if (divisions < 1 || size.x <= 0.0f || size.y <= 0.0f)
        return;

      GetPlaneVectors(normal, out Vector3 tangent, out Vector3 bitangent);
      Vector2 halfSize = size * 0.5f;
      float stepX = size.x / divisions;
      float stepY = size.y / divisions;

      for (int x = 0; x <= divisions; x++)
      {
        float offsetX = -halfSize.x + stepX * x;
        Vector3 lineStart = center + tangent * offsetX - bitangent * halfSize.y;
        Vector3 lineEnd = center + tangent * offsetX + bitangent * halfSize.y;
        Gizmos.DrawLine(lineStart, lineEnd);
      }

      for (int y = 0; y <= divisions; y++)
      {
        float offsetY = -halfSize.y + stepY * y;
        Vector3 lineStart = center - tangent * halfSize.x + bitangent * offsetY;
        Vector3 lineEnd = center + tangent * halfSize.x + bitangent * offsetY;
        Gizmos.DrawLine(lineStart, lineEnd);
      }
    }

    /// <summary> Draws a wire cone useful for field-of-view visualization. </summary>
    /// <param name="apex"> Cone tip. </param>
    /// <param name="direction"> Cone axis direction. </param>
    /// <param name="angleDegrees"> Half-angle at the apex in degrees. </param>
    /// <param name="length"> Cone length. </param>
    /// <param name="segments"> Number of base segments. </param>
    public static void WireCone(Vector3 apex, Vector3 direction, float angleDegrees, float length, int segments = DefaultSegments / 2)
    {
      if (direction.sqrMagnitude == 0.0f || length <= 0.0f || segments < 3)
        return;

      Vector3 axis = direction.normalized;
      GetPlaneVectors(axis, out Vector3 tangent, out Vector3 bitangent);
      float radius = Mathf.Tan(angleDegrees * MathConstants.Deg2Rad) * length;
      Vector3 baseCenter = apex + axis * length;
      Vector3 previousBasePoint = baseCenter + tangent * radius;

      for (int i = 1; i <= segments; i++)
      {
        float angle = MathConstants.Tau * i / segments;
        Vector3 basePoint = baseCenter + (tangent * Mathf.Cos(angle) + bitangent * Mathf.Sin(angle)) * radius;
        Gizmos.DrawLine(previousBasePoint, basePoint);
        Gizmos.DrawLine(apex, basePoint);
        previousBasePoint = basePoint;
      }
    }

    /// <summary> Draws a wire diamond centered on the XY plane. </summary>
    /// <param name="center"> Diamond center. </param>
    /// <param name="width"> Total width. </param>
    /// <param name="height"> Total height. </param>
    /// <param name="z"> Z coordinate. </param>
    public static void WireDiamond2D(Vector2 center, float width, float height, float z = 0.0f)
    {
      Vector2 halfSize = new(width * 0.5f, height * 0.5f);
      Vector3 top = new(center.x, center.y + halfSize.y, z);
      Vector3 right = new(center.x + halfSize.x, center.y, z);
      Vector3 bottom = new(center.x, center.y - halfSize.y, z);
      Vector3 left = new(center.x - halfSize.x, center.y, z);

      Gizmos.DrawLine(top, right);
      Gizmos.DrawLine(right, bottom);
      Gizmos.DrawLine(bottom, left);
      Gizmos.DrawLine(left, top);
    }

    private static void GetPlaneVectors(Vector3 normal, out Vector3 tangent, out Vector3 bitangent)
    {
      Vector3 upHint = Mathf.Abs(Vector3.Dot(normal, Vector3.up)) > 0.99f ? Vector3.forward : Vector3.up;
      tangent = Vector3.Cross(normal, upHint).normalized;
      bitangent = Vector3.Cross(normal, tangent).normalized;
    }

    private static void DrawCircleSegmentLoop(
      Vector3 center,
      Vector3 tangent,
      Vector3 bitangent,
      float radius,
      float startAngle,
      float endAngle,
      int segments,
      bool closedLoop = true)
    {
      Vector3 previous = center + (tangent * Mathf.Cos(startAngle) + bitangent * Mathf.Sin(startAngle)) * radius;

      for (int i = 1; i <= segments; i++)
      {
        float t = i / (float)segments;
        float angle = Mathf.Lerp(startAngle, endAngle, t);
        Vector3 point = center + (tangent * Mathf.Cos(angle) + bitangent * Mathf.Sin(angle)) * radius;
        Gizmos.DrawLine(previous, point);
        previous = point;
      }

      if (closedLoop == true && segments >= 3)
      {
        Vector3 first = center + (tangent * Mathf.Cos(startAngle) + bitangent * Mathf.Sin(startAngle)) * radius;
        Gizmos.DrawLine(previous, first);
      }
    }
  }
}
