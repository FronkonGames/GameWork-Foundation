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
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Drawing of objects for development.
  /// </summary>
  /// <remarks>Only available in the Editor</remarks>
  public static partial class DebugDraw
  {
    [Conditional("UNITY_EDITOR")]
    public static void Point(Vector3 position, float size = PointSize, Color? color = null, Quaternion? rotation = null)
    {
      float halfSize = size * 0.5f;

      Line(position + Vector3.right * halfSize, position - Vector3.right * halfSize, color ?? AxisX, rotation);
      Line(position + Vector3.up * halfSize, position - Vector3.up * halfSize, color ?? AxisY, rotation);
      Line(position + Vector3.forward * halfSize, position - Vector3.forward * halfSize, color ?? AxisZ, rotation);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Points(Vector3[] points, float size = PointSize, Color? color = null, Quaternion? rotation = null)
    {
      for (int i = 0; i < points.Length; ++i)
        Point(points[i], size, color, rotation);
    }

    [Conditional("UNITY_EDITOR")]
    private static void Line(Vector3 a, Vector3 b, Color? color = null, Quaternion? rotation = null) =>
      DrawHandle(new LineHandle
      {
        a = rotation == null ? a : (Quaternion)rotation * a,
        b = rotation == null ? b : (Quaternion)rotation * b,
        color = color ?? LineColor,
        solid = true
      });

    [Conditional("UNITY_EDITOR")]
    private static void Lines(Vector3[] lines, Color? color = null, Quaternion? rotation = null)
    {
      for (int i = 0; i < lines.Length - 1; i += 1)
        Line(lines[i], lines[i + 1], color, rotation);
    }

    [Conditional("UNITY_EDITOR")]
    public static void DottedLine(Vector3 a, Vector3 b, Color? color = null, Quaternion? rotation = null) =>
      DrawHandle(new LineHandle
      {
        a = rotation == null ? a : (Quaternion)rotation * a,
        b = rotation == null ? b : (Quaternion)rotation * b,
        color = color ?? LineColor,
        solid = false
      });

    [Conditional("UNITY_EDITOR")]
    public static void DottedLines(Vector3[] lines, Color? color = null, Quaternion? rotation = null)
    {
      for (int i = 0; i < lines.Length - 1; i += 1)
        DottedLine(lines[i], lines[i + 1], color, rotation);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Arrow(Vector3 position, Quaternion rotation, float length = 1.0f, float size = ArrowTipSize, float width = ArrowWidth, Color? color = null)
    {
      Vector3 direction = rotation * Vector3.forward;
      float sideLen = length - length * size;
      Vector3 widthOffset = Vector3.Cross(direction, Vector3.up) * width;
      Vector3 tip = position + direction * length;
      Vector3 upCornerInRight = position - widthOffset * 0.3f + direction * sideLen;
      Vector3 upCornerInLeft = position + widthOffset * 0.3f + direction * sideLen;
      Vector3 upCornerOutRight = position - widthOffset * 0.5f + direction * sideLen;
      Vector3 upCornerOutLeft = position + widthOffset * 0.5f + direction * sideLen;

      Line(position, upCornerInRight, color ?? ArrowColor);
      Line(upCornerInRight, upCornerOutRight, color ?? ArrowColor);
      Line(upCornerOutRight, tip, color ?? ArrowColor);
      Line(tip, upCornerOutLeft, color ?? ArrowColor);
      Line(upCornerOutLeft, upCornerInLeft, color ?? ArrowColor);
      Line(upCornerInLeft, position, color ?? ArrowColor);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Circle(Vector3 center, float radius, Color? color = null, Quaternion? rotation = null) =>
      DrawHandle(new CircleHandle
      {
        center = center,
        normal = rotation == null ? Vector3.up : (Quaternion)rotation * Vector3.up,
        radius = radius,
        color = color ?? CircleColor
      });
    
    [Conditional("UNITY_EDITOR")]
    public static void SolidCircle(Vector3 center, float radius, Color? color = null, Quaternion? rotation = null) =>
      DrawHandle(new CircleHandle
      {
        center = center,
        normal = rotation == null ? Vector3.up : (Quaternion)rotation * Vector3.up,
        radius = radius,
        color = color ?? CircleColor,
        solid = true
      });

    [Conditional("UNITY_EDITOR")]
    public static void Sphere(Vector3 center, float radius, Color? color = null, Quaternion? rotation = null)
    {
      Circle(center, radius, color ?? AxisY, rotation);
      Circle(center, radius, color ?? AxisX, (rotation ?? Quaternion.identity) * Quaternion.Euler(0.0f, 0.0f, 90.0f));
      Circle(center, radius, color ?? AxisZ, (rotation ?? Quaternion.identity) * Quaternion.Euler(0.0f, 90.0f, 90.0f));
    }

    [Conditional("UNITY_EDITOR")]
    public static void SolidSphere(Vector3 center, float radius, Color? color = null, Quaternion? rotation = null) =>
      DrawHandle(new SphereHandle
      {
        center = center,
        radius = radius,
        color = color ?? SphereColor,
        rotation = rotation ?? Quaternion.identity
      });

    [Conditional("UNITY_EDITOR")]
    public static void Arc(Vector3 center, Quaternion rotation, float radius, float angle, Color? color = null) =>
      DrawHandle(new ArcHandle
      {
        center = center,
        normal = rotation * Vector3.up,
        from = (rotation * Quaternion.Euler(-angle * 0.5f, 0.0f, 0.0f)) * Vector3.forward,
        angle = angle,
        radius = radius,
        color = color ?? ArcColor
      });

    [Conditional("UNITY_EDITOR")]
    public static void SolidArc(Vector3 center, Quaternion rotation, float radius, float angle, Color? color = null) =>
      DrawHandle(new ArcHandle
      {
        center = center,
        normal = rotation * Vector3.up,
        from = (rotation * Quaternion.Euler(0.0f, -angle * 0.5f, 0.0f)) * Vector3.forward,
        angle = angle,
        radius = radius,
        color = color ?? ArcColor,
        solid = true
      });

    [Conditional("UNITY_EDITOR")]
    public static void Cube(Vector3 center, Vector3 size, Color? color = null) =>
      DrawHandle(new CubeHandle
      {
        center = center,
        size = size,
        color = color ?? CubeColor
      });

    [Conditional("UNITY_EDITOR")]
    public static void Cube(Vector3 center, float size, Color? color = null) =>
      Cube(center, Vector3.one * size, color ?? CubeColor);

    [Conditional("UNITY_EDITOR")]
    public static void Diamond(Vector3 center, float size = DiamondSize, Color? color = null, Quaternion? rotation = null)
    {
      Vector3 u = center + Vector3.up * size;
      Vector3 d = center + Vector3.down * size;
      Vector3 r = center + Vector3.right * size;
      Vector3 l = center + Vector3.left * size;
      Vector3 f = center + Vector3.forward * size;
      Vector3 b = center + Vector3.back * size;
      
      Lines(new[]
      {
        u, r, f,
        u, f, l,
        u, l, b,
        u, b, r
      }, color ?? DiamondColor, rotation);

      Lines(new[]
      {
        d, f, r,
        d, r, b,
        d, b, l,
        d, l, f
      }, color ?? DiamondColor, rotation);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Cone(Vector3 position, Quaternion rotation, float angle, Color? color = null)
    {
      Vector3 direction = rotation * Vector3.forward;
      float length = direction.magnitude;

      Vector3 forward = direction;
      Vector3 up = Vector3.Slerp(forward, -forward, 0.5f);
      Vector3 right = Vector3.Cross(forward, up).normalized * length;

      direction = direction.normalized;

      Vector3 slerpedVector = Vector3.Slerp(forward, up, angle / 90.0f);

      float dist;
      var farPlane = new UnityEngine.Plane(-direction, position + forward);
      var distRay = new Ray(position, slerpedVector);

      farPlane.Raycast(distRay, out dist);

      Debug.DrawRay(position, slerpedVector.normalized * dist, color ?? ConeColor);
      Debug.DrawRay(position, Vector3.Slerp(forward, -up, angle / 90.0f).normalized * dist, color ?? ConeColor);
      Debug.DrawRay(position, Vector3.Slerp(forward, right, angle / 90.0f).normalized * dist, color ?? ConeColor);
      Debug.DrawRay(position, Vector3.Slerp(forward, -right, angle / 90.0f).normalized * dist, color ?? ConeColor);

      Circle(position + forward, (forward - (slerpedVector.normalized * dist)).magnitude, color ?? ConeColor, rotation * Quaternion.Euler(0.0f, 90.0f, 90.0f));
      Circle(position + (forward * 0.5f), ((forward * 0.5f) - (slerpedVector.normalized * (dist * 0.5f))).magnitude, color ?? ConeColor, rotation * Quaternion.Euler(0.0f, 90.0f, 90.0f));      
    }
    
    [Conditional("UNITY_EDITOR")]
    public static void Bounds(Bounds b, Color? color)
    {
      Vector3 lbf = new Vector3(b.min.x, b.min.y, b.max.z);
      Vector3 ltb = new Vector3(b.min.x, b.max.y, b.min.z);
      Vector3 rbb = new Vector3(b.max.x, b.min.y, b.min.z);
      Line(b.min, lbf, color);
      Line(b.min, ltb, color);
      Line(b.min, rbb, color);
      
      Vector3 rtb = new Vector3(b.max.x, b.max.y, b.min.z);
      Vector3 rbf = new Vector3(b.max.x, b.min.y, b.max.z);
      Vector3 ltf = new Vector3(b.min.x, b.max.y, b.max.z);
      Line(b.max, rtb, color);
      Line(b.max, rbf, color);
      Line(b.max, ltf, color);

      Line(rbb, rbf, color);
      Line(rbb, rtb, color);

      Line(lbf, rbf, color);
      Line(lbf, ltf, color);

      Line(ltb, rtb, color);
      Line(ltb, ltf, color);
    }
    
    [Conditional("UNITY_EDITOR")]
    public static void Bounds(BoundsInt b, Color color) => Bounds(new Bounds(b.center, b.size), color);

    [Conditional("UNITY_EDITOR")]
    public static void Text(Vector3 position, string text, Color? color = null) =>
      DrawHandle(new TextHandle
      {
        position = position,
        text = text,
        color = color ?? TextColor
      });
/*
    [Conditional("UNITY_EDITOR")]
    public static void Raycast(Ray ray, float distance, Color rayColor, float duration = 0.0f)
    {
      if (float.IsInfinity(distance) == true)
        distance = 10000000.0f;

      rayDelegate(ray.origin, ray.direction * distance, rayColor, duration);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Raycast(Ray ray, float duration = 0.0f) => rayDelegate(ray.origin, ray.direction, RayColor, duration);

    [Conditional("UNITY_EDITOR")]
    public static void SphereCast(Ray ray, float radius, float distance, Color colorStart, Color colorEnd, int iterationCount = 10)
      => SphereCast(ray.origin, radius, ray.direction, distance, colorStart, colorEnd, iterationCount);

    [Conditional("UNITY_EDITOR")]
    public static void SphereCast(Vector3 origin, float radius, Vector3 direction, float distance, Color colorStart, Color colorEnd, int iterationCount = 10)
    {
      direction.EnsureNormalized();
      Vector3 crossA = GetAxisAlignedPerpendicular(direction);
      Vector3 crossB = Vector3.Cross(crossA, direction);
      Color color = colorStart;
      DrawCircleFast(origin, crossA, crossB, radius, DrawLine);
      DrawCircleFast(origin, crossB, crossA, radius, DrawLine);

      Vector3 scaledDirection = direction * distance;
      iterationCount += 2; // Caps
      for (int i = 0; i < iterationCount; ++i)
      {
        float t = i / ((float)iterationCount - 1);
        color = Color.Lerp(colorStart, colorEnd, t);
        DrawCircleFast(origin + scaledDirection * t, direction, crossA, radius, DrawLine);
      }

      Vector3 end = origin + scaledDirection;
      color = colorEnd;
      DrawCircleFast(end, crossA, crossB, radius, DrawLine);
      DrawCircleFast(end, crossB, crossA, radius, DrawLine);

      void DrawLine(Vector3 a, Vector3 b, float f) => lineDelegate(a, b, color);
    }

    [Conditional("UNITY_EDITOR")]
    public static void RaycastHits(RaycastHit[] hits, Color color, int maxCount = -1, float rayLength = 1, float duration = 0.0f)
    {
      if (maxCount < 0)
        maxCount = hits.Length;

      for (int i = 0; i < maxCount; ++i)
        rayDelegate(hits[i].point, hits[i].normal * rayLength, color, duration);
    }

    [Conditional("UNITY_EDITOR")]
    public static void RaycastHits(RaycastHit[] hits, int maxCount = -1, float rayLength = 1.0f, float duration = 0.0f)
      => RaycastHits(hits, RayColor, maxCount, rayLength, duration);

    [Conditional("UNITY_EDITOR")]
    public static void SphereCastHits(RaycastHit[] hits, Ray ray, float radius, Color color, int maxCount = -1)
      => SphereCastHits(hits, ray.origin, radius, ray.direction, color, maxCount);

    [Conditional("UNITY_EDITOR")]
    public static void SphereCastHits(RaycastHit[] hits, Vector3 origin, float radius, Vector3 direction, Color color, int maxCount = -1)
    {
      if (maxCount < 0)
        maxCount = hits.Length;

      if (maxCount == 0)
        return;

      direction.EnsureNormalized();

      Vector3 zero = Vector3.zero;
      for (int i = 0; i < maxCount; ++i)
      {
        RaycastHit hit = hits[i];

        if (hit.point == zero)
        {
          hit.point = origin;
          Vector3 crossA = GetAxisAlignedPerpendicular(direction);
          Vector3 crossB = Vector3.Cross(crossA, direction);
          DrawCircleFast(origin, crossA, crossB, radius, DrawLineSolid);
          DrawCircleFast(origin, crossB, crossA, radius, DrawLineSolid);
          DrawCircleFast(origin, direction, crossA, radius, DrawLineSolid);

          void DrawLineSolid(Vector3 a, Vector3 b, float f) => lineDelegate(a, b, color);
          continue;
        }

        Vector3 localDirection = GetAxisAlignedAlternateWhereRequired(hit.normal, direction);
        Vector3 cross = Vector3.Cross(localDirection, hit.normal);

        Vector3 point = hit.point + hit.normal * radius;
        DrawCircleFast(point, cross, hit.normal, radius, DrawLine);
        Vector3 secondCross = Vector3.Cross(cross, hit.normal);
        DrawCircleFast(point, secondCross, hit.normal, radius, DrawLine);
      }

      void DrawLine(Vector3 a, Vector3 b, float f) =>
        lineDelegate(a, b, new Color(color.r, color.g, color.b, Mathf.Pow(1.0f - Mathf.Abs(f - 0.5f) * 2.0f, 2.0f) * color.a));
    }
*/
  }
}
