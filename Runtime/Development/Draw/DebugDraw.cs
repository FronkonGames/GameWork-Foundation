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
      jobsGL.Add(new RenderGL(GL.LINES, new[] {a, b}, color ?? LineColor, rotation == null
        ? Matrix4x4.identity
        : Matrix4x4.TRS(Vector3.zero, (Quaternion)rotation, Vector3.one)));
    
    [Conditional("UNITY_EDITOR")]
    private static void Lines(Vector3[] lines, Color? color = null, Quaternion? rotation = null)
    {
      if (lines.Length > 2)
        jobsGL.Add(new RenderGL(GL.LINES, lines, color ?? LineColor, rotation == null
            ? Matrix4x4.identity
            : Matrix4x4.TRS(Vector3.zero, (Quaternion)rotation, Vector3.one)));
      else if (lines.Length == 2)
        Line(lines[0], lines[1], color ?? LineColor, rotation);
    }

    [Conditional("UNITY_EDITOR")]
    public static void DottedLine(Vector3 a, Vector3 b, Color? color = null, Quaternion? rotation = null) =>
      jobsGL.Add(new RenderGL(GL.LINES, new[] {a, b}, color ?? LineColor, rotation == null
        ? Matrix4x4.identity
        : Matrix4x4.TRS(Vector3.zero, (Quaternion)rotation, Vector3.one), true));

    [Conditional("UNITY_EDITOR")]
    public static void DottedLines(Vector3[] lines, Color? color = null, Quaternion? rotation = null)
    {
      if (lines.Length > 1)
      {
        for (int i = 0; i < lines.Length - 1; ++i)
          DottedLine(lines[i], lines[i + 1], color, rotation);
      }
    }

    [Conditional("UNITY_EDITOR")]
    public static void Arrow(Vector3 start, Quaternion rotation, float size = ArrowSize, Color? color = null)
    {
      Vector3 direction = rotation * Vector3.forward;
      Vector3 end = start + direction * size;
      Vector3 stepBack = direction.normalized * (size * -ArrowHeadLength);
      Vector3 stepSide = Vector3.Cross(end - start, Vector3.up).normalized * size * ArrowHeadWidth;

      Line(start, start + direction * size * (1.0f - ArrowHeadLength), color ?? ArrowColor);
      SolidTriangle(end, end + stepBack - stepSide, end + stepBack + stepSide, color ?? ArrowColor);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Triangle(Vector3 a, Vector3 b, Vector3 c, Color? color = null, Quaternion? rotation = null) =>
      Lines(new[] { a, b, c, a }, color ?? TriangleColor, rotation);

    [Conditional("UNITY_EDITOR")]
    public static void SolidTriangle(Vector3 a, Vector3 b, Vector3 c, Color? color = null, Quaternion? rotation = null) =>
      jobsGL.Add(new RenderGL(GL.TRIANGLES, new[] { a, b, c }, color ?? TriangleColor, rotation == null
        ? Matrix4x4.identity
        : Matrix4x4.TRS(Vector3.zero, (Quaternion)rotation, Vector3.one)));
    
    [Conditional("UNITY_EDITOR")]
    public static void Circle(Vector3 center, float radius, Color? color = null, Quaternion? rotation = null) =>
      jobsGL.Add(new RenderGL(GL.LINE_STRIP, circle, color ?? CircleColor, Matrix4x4.TRS(center, rotation ?? Quaternion.identity, Vector3.one * radius)));

    [Conditional("UNITY_EDITOR")]
    public static void SolidCircle(Vector3 center, float radius, Color? color = null, Quaternion? rotation = null) =>
      jobsGL.Add(new RenderGL(GL.TRIANGLE_STRIP, solidCircle, color ?? CircleColor, Matrix4x4.TRS(center, rotation ?? Quaternion.identity, Vector3.one * radius)));

    [Conditional("UNITY_EDITOR")]
    public static void Sphere(Vector3 center, float radius, Color? color = null, Quaternion? rotation = null)
    {
      Circle(center, radius, color ?? SphereColor, rotation);
      Circle(center, radius, color ?? SphereColor, Quaternion.Euler(0.0f, 0.0f, 90.0f) * (rotation ?? Quaternion.identity));
      Circle(center, radius, color ?? SphereColor, Quaternion.Euler(0.0f, 90.0f, 90.0f) * (rotation ?? Quaternion.identity));
    }

    [Conditional("UNITY_EDITOR")]
    public static void Arc(Vector3 center, Vector3 forward, float radius, float angle, Color? color = null, Quaternion? rotation = null)
    {
      Vector3[] vertices = new Vector3[Segments];
      Quaternion rot = Quaternion.AngleAxis(angle / (Segments - 1), Vector3.up) * (rotation ?? Quaternion.identity);
      Vector3 surfacePoint = forward.normalized * radius;
      surfacePoint = Quaternion.Euler(0.0f, angle * -0.5f, 0.0f) * surfacePoint;

      for (int i = 0; i < Segments; ++i)
      {
        vertices[i] = center + surfacePoint;
        surfacePoint = rot * surfacePoint;
      }      

      for (int i = 1; i < Segments; ++i)
        Line(vertices[i - 1], vertices[i], color ?? ArcColor, rotation);
      
      Line(center, vertices[0], color ?? ArcColor, rotation);
      Line(center, vertices[Segments - 1], color ?? ArcColor, rotation);
    }

    [Conditional("UNITY_EDITOR")]
    public static void SolidArc(Vector3 center, Vector3 forward, float radius, float angle, Color? color = null, Quaternion? rotation = null)
    {
      Vector3[] vertices = new Vector3[Segments];
      Quaternion rot = Quaternion.AngleAxis(angle / (Segments - 1), Vector3.up) * (rotation ?? Quaternion.identity);
      Vector3 surfacePoint = forward.normalized * radius;
      surfacePoint = Quaternion.Euler(0.0f, angle * -0.5f, 0.0f) * surfacePoint;

      for (int i = 0; i < Segments; ++i)
      {
        vertices[i] = center + surfacePoint;
        surfacePoint = rot * surfacePoint;
      }      

      for (int i = 1; i < Segments; ++i)
        SolidTriangle(center, vertices[i - 1], vertices[i], color ?? ArcColor, rotation);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Cube(Vector3 center, Vector3 size, Color? color = null, Quaternion? rotation = null) =>
      jobsGL.Add(new RenderGL(GL.LINES, cube, color ?? CubeColor, Matrix4x4.TRS(center, rotation ?? Quaternion.identity, size)));

    [Conditional("UNITY_EDITOR")]
    public static void Cube(Vector3 center, float size, Color? color = null, Quaternion? rotation = null) =>
      Cube(center, Vector3.one * size, color ?? CubeColor, rotation);
    
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
    public static void Text(Vector3 position, string text, Color? color = null, Vector2 offset = default) =>
      RenderText.Add(position, text, color ?? TextColor, offset);
/*
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
