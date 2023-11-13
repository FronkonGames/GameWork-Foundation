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
  /// <summary> Drawing of objects for development. </summary>
  /// <remarks>Only available in the Editor</remarks>
  [ExecuteInEditMode, HideInInspector, DefaultExecutionOrder(int.MaxValue)]
  public partial class DebugDraw : CachedMonoBehaviour
  {
    /// <summary> Draw a three-axis cross. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Axis(Vector3 position, float size, Quaternion rotation)
    {
      float halfSize = size * 0.5f;

      Line(position + rotation * Vector3.right * halfSize, position - rotation * Vector3.right * halfSize, null, Color.red);
      Line(position + rotation * Vector3.up * halfSize, position - rotation * Vector3.up * halfSize, null, Color.green);
      Line(position + rotation * Vector3.forward * halfSize, position - rotation * Vector3.forward * halfSize, null, Color.blue);
    }

    /// <summary> Draw a three-axis cross. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Axis(Vector3 position, float? size = null, Quaternion? rotation = null) =>
      Axis(position, size ?? Settings.DebugDraw.PointSize, rotation ?? Quaternion.identity);

    /// <summary> Draw a point with a three-axis cross. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Point(Vector3 position, float size, Quaternion rotation, Color color, bool continuous)
    {
      float halfSize = size * 0.5f;

      Line(position + rotation * Vector3.right * halfSize, position - rotation * Vector3.right * halfSize, null, color, continuous);
      Line(position + rotation * Vector3.up * halfSize, position - rotation * Vector3.up * halfSize, null, color, continuous);
      Line(position + rotation * Vector3.forward * halfSize, position - rotation * Vector3.forward * halfSize, null, color, continuous);
    }

    /// <summary> Draw a point with a three-axis cross. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Point(Vector3 position, float? size = null, Quaternion? rotation = null, Color ? color = null, bool continuous = true) =>
      Point(position,
            size ?? Settings.DebugDraw.PointSize,
            rotation ?? Quaternion.identity,
            color ?? Settings.DebugDraw.AxisXColor,
            continuous);

    /// <summary> Draw an array of points using three-axis crosshairs. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Points(Vector3[] points, float size, Quaternion rotation, Color color, bool continuous)
    {
      for (int i = 0; i < points.Length; ++i)
        Point(points[i], size, rotation, color, continuous);
    }

    /// <summary> Draw an array of points using three-axis crosshairs. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Points(Vector3[] points, float? size = null, Quaternion? rotation = null, Color ? color = null, bool continuous = true) =>
      Points(points,
             size ?? Settings.DebugDraw.PointSize,
             rotation ?? Quaternion.identity,
             color ?? Settings.DebugDraw.AxisXColor,
             continuous);

    /// <summary> Draw a line. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Line(Vector3 a, Vector3 b, Quaternion? rotation = null, Color ? color = null, bool continuous = true)
    {
      int index = Instance.GetLineIndex();
      if (index != -1)
      {
        lineJobs[index].start = rotation != null ? (Quaternion)rotation * a : a;
        lineJobs[index].end = rotation != null ? (Quaternion)rotation * b : b;
        lineJobs[index].color = color ?? Settings.DebugDraw.LineColor;
        lineJobs[index].continuous = continuous;
        lineJobs[index].frame = Time.frameCount;
      }
    }

    /// <summary> Draw an array of lines. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Lines(Vector3[] lines, Quaternion? rotation = null, Color ? color = null, bool continuous = true)
    {
      for (int i = 0; i < lines.Length - 1; ++i)
        Line(lines[i], lines[i + 1], rotation, color, continuous);
    }

    /// <summary> Draw a triangle. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Triangle(Vector3 a, Vector3 b, Vector3 c, Quaternion? rotation = null, Color? color = null, bool continuous = true)
    {
      int index = Instance.GetTriangleIndex();
      if (index != -1)
      {
        triangleJobs[index].a = rotation != null ? (Quaternion)rotation * a : a;
        triangleJobs[index].b = rotation != null ? (Quaternion)rotation * b : b;
        triangleJobs[index].c = rotation != null ? (Quaternion)rotation * c : c;
        triangleJobs[index].color = color ?? Settings.DebugDraw.LineColor;
        triangleJobs[index].continuous = continuous;
        triangleJobs[index].frame = Time.frameCount;
      }
    }

    /// <summary> Draw a line using an arrow. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Arrow(Vector3 position, Vector3 direction, float length = 1.0f, float? size = null, float? width = null, Color? color = null)
    {
      float sideLen = length - length * (size ?? Settings.DebugDraw.ArrowTipSize);
      Vector3 widthOffset = Vector3.Cross(direction, Vector3.up) * (width ?? Settings.DebugDraw.ArrowWidth);
      Vector3 tip = position + direction * length;
      Vector3 upCornerInRight = position - widthOffset * 0.3f + direction * sideLen;
      Vector3 upCornerInLeft = position + widthOffset * 0.3f + direction * sideLen;
      Vector3 upCornerOutRight = position - widthOffset * 0.5f + direction * sideLen;
      Vector3 upCornerOutLeft = position + widthOffset * 0.5f + direction * sideLen;

      Line(position, upCornerInRight, Quaternion.identity, color ?? Settings.DebugDraw.ArrowColor);
      Line(upCornerInRight, upCornerOutRight, Quaternion.identity, color ?? Settings.DebugDraw.ArrowColor);
      Line(upCornerOutRight, tip, Quaternion.identity, color ?? Settings.DebugDraw.ArrowColor);
      Line(tip, upCornerOutLeft, Quaternion.identity, color ?? Settings.DebugDraw.ArrowColor);
      Line(upCornerOutLeft, upCornerInLeft, Quaternion.identity, color ?? Settings.DebugDraw.ArrowColor);
      Line(upCornerInLeft, position, Quaternion.identity, color ?? Settings.DebugDraw.ArrowColor);
    }

    /// <summary> Draw a line using an arrow. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Arrow(Vector3 position, Quaternion rotation, float length = 1.0f, float? size = null, float? width = null, Color? color = null)
      => Arrow(position, rotation * Vector3.forward, length, size, width, color);

    /// <summary> Draw a ray. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Ray(Vector3 position, Quaternion rotation, Color? color = null) =>
      Line(position, rotation * Vector3.forward * Settings.DebugDraw.RayLength, Quaternion.identity, color ?? Settings.DebugDraw.RayColor);

    /// <summary> Draw a ray. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Ray(Vector3 position, Vector3 direction, Color? color = null) =>
      Line(position, position + (direction * direction.magnitude), Quaternion.identity, color ?? Settings.DebugDraw.RayColor);

    /// <summary> Draw a wire circle. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Circle(Vector3 center, float radius, Quaternion rotation, Color color, bool continuous)
    {
      Vector3 forward = (rotation * Vector3.forward).normalized;
      Vector3 right = (rotation * Vector3.right).normalized;

      Vector3 b = center + (forward * radius);
      float angleStep = Mathf.PI * 2.0f / Settings.DebugDraw.Divisions;

      for (int i = 0; i < Settings.DebugDraw.Divisions; ++i)
      {
        float angle = (i == Settings.DebugDraw.Divisions - 1) ? 0.0f : (i + 1) * angleStep;

        Vector3 next = new Vector3(Mathf.Sin(angle), 0.0f, Mathf.Cos(angle)) * radius;
        Vector3 a = center + (right * next.x) + (forward * next.z);

        Line(a, b, Quaternion.identity, color, continuous);

        b = a;
      }
    }

    /// <summary> Draw a wire circle. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Circle(Vector3 center, float radius, Quaternion? rotation = null, Color ? color = null, bool continuous = true) =>
      Circle(center, radius, rotation ?? Quaternion.identity, color ?? Settings.DebugDraw.CircleColor, continuous);

    /// <summary> Draw a solid circle. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void CircleColid(Vector3 center, float radius, Quaternion rotation, Color color)
    {
      Vector3 normal = rotation == null ? Vector3.up : rotation * Vector3.up;
      Vector3 forward = Vector3.Cross(normal, normal.x < normal.z ? Vector3.right : Vector3.forward).normalized;
      Vector3 right = Vector3.Cross(forward, normal).normalized;

      Vector3 b = center + (forward * radius);
      float angleStep = Mathf.PI * 2.0f / Settings.DebugDraw.Divisions;

      for (int i = 0; i < Settings.DebugDraw.Divisions; ++i)
      {
        float angle = (i == Settings.DebugDraw.Divisions - 1) ? 0.0f : (i + 1) * angleStep;

        Vector3 next = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * radius;
        Vector3 a = center + (right * next.x) + (forward * next.z);

        Triangle(a, b, center, null, color);

        b = a;
      }
    }

    /// <summary> Draw a solid circle. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void CircleSolid(Vector3 center, float radius, Quaternion? rotation = null, Color ? color = null) =>
      CircleColid(center, radius, rotation ?? Quaternion.identity, color ?? Settings.DebugDraw.CircleColor);

    /// <summary> Draw a wire sphere. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Sphere(Vector3 center, float radius, Quaternion? rotation = null, Color ? color = null)
    {
      if (SphereWire == null)
        CreateSphereWireMesh();

      int index = Instance.GetMeshIndex();
      if (index != -1)
      {
        meshJobs[index].vertices = SphereWire;
        meshJobs[index].color = color ?? Settings.DebugDraw.SphereColor;
        meshJobs[index].matrix = Matrix4x4.TRS(center, rotation ?? Quaternion.identity, Vector3.one * radius);
        meshJobs[index].frame = Time.frameCount;
      }
    }

    /// <summary> Draw an arc centered on the forward vector. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Arc(Vector3 center, float radius, float angle, Quaternion rotation, Color color, bool continuous)
    {
      Vector3 from = rotation * Quaternion.Euler(0.0f, -angle * 0.5f, 0.0f) * Vector3.forward;
      from.Normalize();
      Quaternion rot = Quaternion.AngleAxis(angle / (Settings.DebugDraw.Divisions / 4 - 1), rotation * Vector3.up);
      Vector3 vector = from;

      Line(center, center + vector * radius, Quaternion.identity, color);

      int num = Settings.DebugDraw.Divisions / 4;
      for (int i = 1; i < num; ++i)
      {
        Vector3 a = center + vector * radius;
        vector = rot * vector;
        Vector3 b = center + vector * radius;

        Line(a, b, Quaternion.identity, color);
      }

      Line(center, center + vector * radius, Quaternion.identity, color);
    }

    /// <summary> Draw an arc centered on the forward vector. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Arc(Vector3 center, float radius, float angle, Quaternion? rotation = null, Color ? color = null, bool continuous = true) =>
      Arc(center, radius, angle, rotation ?? Quaternion.identity, color ?? Settings.DebugDraw.ArcColor, continuous);

    /// <summary> Draw an solid arc centered on the forward vector. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void ArcSolid(Vector3 center, float radius, float angle, Quaternion rotation, Color color)
    {
      Vector3 from = rotation * Quaternion.Euler(0.0f, -angle * 0.5f, 0.0f) * Vector3.forward;
      from.Normalize();
      Quaternion rot = Quaternion.AngleAxis(angle / (Settings.DebugDraw.Divisions / 4 - 1), rotation * Vector3.up);
      Vector3 vector = from;

      int num = Settings.DebugDraw.Divisions / 4;
      for (int i = 1; i < num; ++i)
      {
        Vector3 b = center + vector * radius;
        vector = rot * vector;
        Vector3 c = center + vector * radius;

        Triangle(center, b, c, null, color);
      }
    }

    /// <summary> Draw an solid arc centered on the forward vector. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void ArcSolid(Vector3 center, float radius, float angle, Quaternion? rotation = null, Color ? color = null) =>
      ArcSolid(center, radius, angle, rotation ?? Quaternion.identity, color ?? Settings.DebugDraw.ArcColor);

    /// <summary> Draw a wire cube. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Cube(Vector3 center, Vector3 size, Quaternion? rotation = null, Color ? color = null)
    {
      if (CubeWire == null)
        CreateCubeWireMesh();

      int index = Instance.GetMeshIndex();
      if (index != -1)
      {
        meshJobs[index].vertices = CubeWire;
        meshJobs[index].color = color ?? Settings.DebugDraw.CubeColor;
        meshJobs[index].matrix = Matrix4x4.TRS(center, rotation ?? Quaternion.identity, size);
        meshJobs[index].frame = Time.frameCount;
      }
    }

    /// <summary> Draw a wire cube. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Cube(Vector3 center, float size, Quaternion? rotation = null, Color ? color = null) =>
      Cube(center, Vector3.one * size, rotation, color ?? Settings.DebugDraw.CubeColor);

    /// <summary> Draw a wire diamond. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Diamond(Vector3 center, float size, Quaternion rotation, Color color, bool continuous)
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
      }, rotation, color, continuous);

      Lines(new[]
      {
        d, f, r,
        d, r, b,
        d, b, l,
        d, l, f
      }, rotation, color, continuous);
    }

    /// <summary> Draw a wire diamond. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Diamond(Vector3 center, float? size = null, Quaternion? rotation = null, Color ? color = null, bool continuous = true) =>
      Diamond(center, size ?? Settings.DebugDraw.DiamondSize, rotation ?? Quaternion.identity, color ?? Settings.DebugDraw.DiamondColor, continuous);

    /// <summary> Draw a wire cone. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Cone(Vector3 position, float angle, float length, Quaternion rotation, Color color, bool continuous)
    {
      Vector3 direction = rotation * Vector3.forward;

      Vector3 forward = direction;
      Vector3 up = Vector3.Slerp(forward, -forward, 0.5f);
      Vector3 right = Vector3.Cross(forward, up).normalized * length;

      direction = direction.normalized;

      Vector3 slerpedVector = Vector3.Slerp(forward, up, angle / 90.0f);

      Plane farPlane = new(-direction, position + forward);
      Ray distRay = new(position, slerpedVector);

      farPlane.Raycast(distRay, out float dist);

      Ray(position, slerpedVector.normalized * dist, color);
      Ray(position, Vector3.Slerp(forward, -up, angle / 90.0f).normalized * dist, color);
      Ray(position, Vector3.Slerp(forward, right, angle / 90.0f).normalized * dist, color);
      Ray(position, Vector3.Slerp(forward, -right, angle / 90.0f).normalized * dist, color);

      Circle(position + forward, (forward - (slerpedVector.normalized * dist)).magnitude, rotation * Quaternion.Euler(90.0f, 0.0f, 0.0f), color, continuous);
    }

    /// <summary> Draw a wire cone. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Cone(Vector3 position, float angle, float length, Quaternion? rotation = null, Color? color = null, bool continuous = true) =>
      Cone(position, angle, length, rotation ?? Quaternion.identity, color ?? Settings.DebugDraw.ConeColor, continuous);

    /// <summary> Draw bounds. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Bounds(Bounds b, Color? color = null)
    {
      Color boundsColor = color ?? Settings.DebugDraw.BoundsColor;

      Vector3 lbf = new(b.min.x, b.min.y, b.max.z);
      Vector3 ltb = new(b.min.x, b.max.y, b.min.z);
      Vector3 rbb = new(b.max.x, b.min.y, b.min.z);
      Line(b.min, lbf, Quaternion.identity, boundsColor);
      Line(b.min, ltb, Quaternion.identity, boundsColor);
      Line(b.min, rbb, Quaternion.identity, boundsColor);

      Vector3 rtb = new(b.max.x, b.max.y, b.min.z);
      Vector3 rbf = new(b.max.x, b.min.y, b.max.z);
      Vector3 ltf = new(b.min.x, b.max.y, b.max.z);
      Line(b.max, rtb, Quaternion.identity, boundsColor);
      Line(b.max, rbf, Quaternion.identity, boundsColor);
      Line(b.max, ltf, Quaternion.identity, boundsColor);

      Line(rbb, rbf, Quaternion.identity, boundsColor);
      Line(rbb, rtb, Quaternion.identity, boundsColor);

      Line(lbf, rbf, Quaternion.identity, boundsColor);
      Line(lbf, ltf, Quaternion.identity, boundsColor);

      Line(ltb, rtb, Quaternion.identity, boundsColor);
      Line(ltb, ltf, Quaternion.identity, boundsColor);
    }

    /// <summary> Draw bounds. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Bounds(BoundsInt b, Color color) => Bounds(new Bounds(b.center, b.size), color);

    /// <summary> Draw text. </summary>
    [Conditional("UNITY_EDITOR")]
    public static void Text(Vector3 position, string text, GUIStyle style = null)
    {
      int index = Instance.GetTextIndex();
      if (index != -1)
      {
        textJobs[index].text = text;
        textJobs[index].position = position;
        textJobs[index].style = style;
        textJobs[index].frame = Time.frameCount;
      }
    }
  }
}