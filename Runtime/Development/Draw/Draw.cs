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
using System.Diagnostics;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Drawing of objects for development.
  /// </summary>
  /// <remarks>Only available in the Editor</remarks>
  public static partial class Draw
  {
    [Conditional("UNITY_EDITOR")]
    public static void Point(Vector3 p, Color color, float size = 0.5f)
    {
      Line(p + Vector3.right * (size * 0.5f), -Vector3.right * size, color);
      Line(p + Vector3.up * (size * 0.5f), -Vector3.up * size, color);
      Line(p + Vector3.forward * (size * 0.5f), -Vector3.forward * size, color);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Point(Vector3 p, float size = 0.5f)
    {
      Line(p + Vector3.right * (size * 0.5f), -Vector3.right * size, ColorX);
      Line(p + Vector3.up * (size * 0.5f), -Vector3.up * size, ColorY);
      Line(p + Vector3.forward * (size * 0.5f), -Vector3.forward * size, ColorZ);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Line(Vector3 start, Vector3 end, Color color) => solidLines.Add(new LineGL(start, end, color));

    [Conditional("UNITY_EDITOR")]
    public static void Line(Vector3 start, Vector3 end) => solidLines.Add(new LineGL(start, end, LineColor));

    [Conditional("UNITY_EDITOR")]
    private static void Lines(IReadOnlyList<Vector3> segments, Color color)
    {
      if (segments.Count > 1)
      {
        for (int i = 0; i < segments.Count - 1; ++i)
          Line(segments[i], segments[i + 1], color);
      }
    }
    
    [Conditional("UNITY_EDITOR")]
    public static void DottedLine(Vector3 start, Vector3 end, Color color) => dottedLines.Add(new LineGL(start, end, color));

    [Conditional("UNITY_EDITOR")]
    public static void DottedLine(Vector3 start, Vector3 end) => dottedLines.Add(new LineGL(start, end, LineColor));

    [Conditional("UNITY_EDITOR")]
    public static void DottedLines(IReadOnlyList<Vector3> segments, Color color)
    {
      if (segments.Count > 1)
      {
        for (int i = 0; i < segments.Count - 1; ++i)
          DottedLine(segments[i], segments[i + 1], color);
      }
    }

    [Conditional("UNITY_EDITOR")]
    public static void Arrow(Vector3 start, Quaternion rotation, float size, Color color)
    {
      Vector3 direction = rotation * Vector3.forward;
      Vector3 end = start + direction * size;
      Vector3 stepBack = direction.normalized * (size * -ArrowHeadLength);
      Vector3 stepSide = Vector3.Cross(end - start, Vector3.up).normalized * size * ArrowHeadWidth;

      Line(start, start + direction * size * (1.0f - ArrowHeadLength), color);
      Triangle(end, end + stepBack + stepSide, start + direction * size * (1.0f - ArrowHeadLength), color);
      Triangle(end, end + stepBack - stepSide, start + direction * size * (1.0f - ArrowHeadLength), color);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Arrow(Vector3 start, Quaternion rotation, float size) => Arrow(start, rotation, size, ArrowColor);
    
    [Conditional("UNITY_EDITOR")]
    public static void Triangle(Vector3 a, Vector3 b, Vector3 c, Color color) => triangles.Add(new TriangleGL(a, b, c, color));
    
    [Conditional("UNITY_EDITOR")]
    public static void Disc(Vector3 center, float radius, Quaternion rotation, Color color)
    {
      float current = 0.0f;
      float grad = MathConstants.Pi2 / Segments;

      for (int i = 0; i < Segments; ++i)
      {
        Line(rotation * new Vector3(Mathf.Sin(current) * radius, 0.0f, Mathf.Cos(current) * radius) + center,
          i == Segments - 1 ? rotation * new Vector3(0f, 0f, radius) + center
            : rotation * new Vector3(Mathf.Sin(current + grad) * radius, 0.0f, Mathf.Cos(current + grad) * radius) + center,
          color);
        current += grad;
      }
    }

    [Conditional("UNITY_EDITOR")]
    public static void Disc(Vector3 center, float radius, Quaternion rotation = default) =>
      Disc(center, radius, rotation, DiscColor);

    [Conditional("UNITY_EDITOR")]
    public static void SolidDisc(Vector3 center, float radius, Quaternion rotation, Color color)
    {
      float current = 0.0f;
      float grad = MathConstants.Pi2 / Segments;

      for (int i = 0; i < Segments; ++i)
      {
        Triangle(center, rotation * new Vector3(Mathf.Sin(current) * radius, 0.0f, Mathf.Cos(current) * radius) + center,
          i == Segments - 1 ? rotation * new Vector3(0.0f, 0.0f, radius) + center
            : rotation * new Vector3(Mathf.Sin(current + grad) * radius, 0.0f, Mathf.Cos(current + grad) * radius) + center,
          color);
        current += grad;
      }
    }

    [Conditional("UNITY_EDITOR")]
    public static void SolidDisc(Vector3 center, float radius, Quaternion rotation = default) =>
      SolidDisc(center, radius, rotation, DiscColor);

    [Conditional("UNITY_EDITOR")]
    public static void Sphere(Vector3 position, float radius, Color color) => spheres.Add(new SphereGL(position, radius, color));
/*    
    [Conditional("UNITY_EDITOR")]
    public static void Arc(Vector3 center, Vector3 normal, Vector3 from, float radius, float angle, Color color) => DrawArc(center, normal, from, radius, angle, color);

    [Conditional("UNITY_EDITOR")]
    public static void Arrow(Vector3 position, Vector3 direction, Color color, float duration = 0.0f, float arrowheadScale = 1.0f)
    {
      rayDelegate(position, direction, color, duration);
      DrawArrowHead(position, direction, color, arrowheadScale);
    }

    [Conditional("UNITY_EDITOR")]
    public static void ArrowLine(Vector3 origin, Vector3 destination, Color color, float duration = 0.0f, float arrowheadScale = 1.0f)
    {
      lineDelegate(origin, destination, color, duration);
      Vector3 direction = destination - origin;
      DrawArrowHead(origin, direction, color, arrowheadScale);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Axis(Vector3 point, bool arrowHeads = false, float scale = 1.0f)
      => Axis(point, Quaternion.identity, arrowHeads, scale);

    [Conditional("UNITY_EDITOR")]
    public static void Axis(Vector3 point, Quaternion rotation, bool arrowHeads = false, float scale = 1.0f)
    {
      Vector3 right = rotation * new Vector3(scale, 0, 0);
      Vector3 up = rotation * new Vector3(0, scale, 0);
      Vector3 forward = rotation * new Vector3(0, 0, scale);
      Color colorRight = ColorX;
      rayDelegate(point, right, colorRight);
      Color colorUp = ColorY;
      rayDelegate(point, up, colorUp);
      Color colorForward = ColorZ;
      rayDelegate(point, forward, colorForward);

      if (arrowHeads == false)
        return;

      DrawArrowHead(point, right, colorRight, scale: scale);
      DrawArrowHead(point, up, colorUp, scale: scale);
      DrawArrowHead(point, forward, colorForward, scale: scale);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Box(Vector3 center, Vector3 halfExtents, Quaternion orientation, Color color)
    {
      DrawBox(center, halfExtents, orientation, DrawLine);

      void DrawLine(Vector3 a, Vector3 b) => lineDelegate(a, b, color);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Box(Vector3 center, Vector3 halfExtents, Color color) => Box(center, halfExtents, Quaternion.identity, color);
    
    [Conditional("UNITY_EDITOR")]
    public static void Box(Vector3 center, Vector3 halfExtents) => Box(center, halfExtents, Quaternion.identity, LineColor);

    [Conditional("UNITY_EDITOR")]
    public static void Bounds(Bounds b, Color color, float duration = 0.0f)
    {
      Vector3 lbf = new Vector3(b.min.x, b.min.y, b.max.z);
      Vector3 ltb = new Vector3(b.min.x, b.max.y, b.min.z);
      Vector3 rbb = new Vector3(b.max.x, b.min.y, b.min.z);
      lineDelegate(b.min, lbf, color, duration);
      lineDelegate(b.min, ltb, color, duration);
      lineDelegate(b.min, rbb, color, duration);

      Vector3 rtb = new Vector3(b.max.x, b.max.y, b.min.z);
      Vector3 rbf = new Vector3(b.max.x, b.min.y, b.max.z);
      Vector3 ltf = new Vector3(b.min.x, b.max.y, b.max.z);
      lineDelegate(b.max, rtb, color, duration);
      lineDelegate(b.max, rbf, color, duration);
      lineDelegate(b.max, ltf, color, duration);

      lineDelegate(rbb, rbf, color, duration);
      lineDelegate(rbb, rtb, color, duration);

      lineDelegate(lbf, rbf, color, duration);
      lineDelegate(lbf, ltf, color, duration);

      lineDelegate(ltb, rtb, color, duration);
      lineDelegate(ltb, ltf, color, duration);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Bounds(BoundsInt b, Color color, float duration = 0.0f) => Bounds(new Bounds(b.center, b.size), color, duration);

    [Conditional("UNITY_EDITOR")]
    public static void Capsule(Vector3 start, Vector3 end, float radius, Color color)
    {
      Vector3 alignment = (start - end).normalized;
      Vector3 crossA = GetAxisAlignedPerpendicular(alignment);
      Vector3 crossB = Vector3.Cross(crossA, alignment);

      DrawCapsuleFast(start, end, radius, alignment, crossA, crossB, DrawLine);

      void DrawLine(Vector3 a, Vector3 b, float f) => lineDelegate(a, b, color);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Text(Vector3 position, object t, Camera camera = null) => Text(position, t, TextColor, camera);

    [Conditional("UNITY_EDITOR")]
    public static void Text(Vector3 position, object t, Color color, Camera camera = null)
    {
#if UNITY_EDITOR
      if (!Application.isPlaying)
        return;

      DrawManager drawManager = DrawManager.Instance;
      DebugText debugText = new(position, t, color, camera);

      if (Time.deltaTime == Time.fixedDeltaTime)
      {
        if (!subscribedFixed)
        {
          subscribedFixed = true;
          UnityEditor.SceneView.duringSceneGui += SceneViewGUIFixed;
          drawManager.RegisterFixedUpdateAction(WaitForNextFixed);
          RegisterGUI();
        }

        debugTextFixed.Add(debugText);
      }
      else
      {
        if (!subscribedUpdate)
        {
          subscribedUpdate = true;
          UnityEditor.SceneView.duringSceneGui += SceneViewGUIUpdate;
          drawManager.RegisterUpdateAction(WaitForNextUpdate);
          RegisterGUI();
        }

        debugTextUpdate.Add(debugText);
      }

      void RegisterGUI()
      {
        drawManager.RegisterOnGUIAction(() =>
        {
          if (!GameViewGizmosEnabled) return;
          foreach (DebugText t in debugTextUpdate)
          {
            if (t.Camera == null)
              continue;

            DoDrawText(t.Position, t.Text, t.Color, t.Camera);
          }

          foreach (DebugText t in debugTextFixed)
          {
            if (t.Camera == null)
              continue;

            DoDrawText(t.Position, t.Text, t.Color, t.Camera);
          }
        });
      }
#else
      throw new NotSupportedException();
#endif
    }

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
