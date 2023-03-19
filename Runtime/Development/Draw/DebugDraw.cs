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
  /// <summary> Drawing of objects for development. </summary>
  /// <remarks>Only available in the Editor</remarks>
  public static partial class DebugDraw
  {
    /// <summary> Draw a point with a three-axis cross. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="position">Position</param>
    /// <param name="size">Cross size</param>
    /// <param name="color">Color</param>
    /// <param name="rotation">Rotation</param>
    [Conditional("UNITY_EDITOR")]
    public static void Point(Vector3 position, float? size = null, Color? color = null, Quaternion? rotation = null)
    {
      float halfSize = (size ?? Settings.DebugDrawPointSize.Value) * 0.5f;

      Line(position + Vector3.right * halfSize, position - Vector3.right * halfSize, color ?? Settings.DebugDrawAxisXColor.Value, rotation);
      Line(position + Vector3.up * halfSize, position - Vector3.up * halfSize, color ?? Settings.DebugDrawAxisYColor.Value, rotation);
      Line(position + Vector3.forward * halfSize, position - Vector3.forward * halfSize, color ?? Settings.DebugDrawAxisZColor.Value, rotation);
    }

    /// <summary> Draw an array of points using three-axis crosshairs. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="points">Point array</param>
    /// <param name="size">Cross size</param>
    /// <param name="color">Color</param>
    /// <param name="rotation">Rotation</param>
    [Conditional("UNITY_EDITOR")]
    public static void Points(Vector3[] points, float? size = null, Color? color = null, Quaternion? rotation = null)
    {
      for (int i = 0; i < points.Length; ++i)
        Point(points[i], size, color, rotation);
    }

    /// <summary> Draw a solid line. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="a">Start</param>
    /// <param name="b">End</param>
    /// <param name="color">Color</param>
    /// <param name="rotation">Rotation</param>
    [Conditional("UNITY_EDITOR")]
    private static void Line(Vector3 a, Vector3 b, Color? color = null, Quaternion? rotation = null) =>
      DrawHandle(new LineHandle
      {
        a = rotation == null ? a : (Quaternion)rotation * a,
        b = rotation == null ? b : (Quaternion)rotation * b,
        color = color ?? Settings.DebugDrawLineColor.Value,
        solid = true
      });

    /// <summary> Draw an array of solid lines. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="lines">Lines array</param>
    /// <param name="color">Color</param>
    /// <param name="rotation">Rotation</param>
    [Conditional("UNITY_EDITOR")]
    private static void Lines(Vector3[] lines, Color? color = null, Quaternion? rotation = null)
    {
      for (int i = 0; i < lines.Length - 1; i += 1)
        Line(lines[i], lines[i + 1], color, rotation);
    }

    /// <summary>
    /// Draw a dashed line.
    /// </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="a">Start</param>
    /// <param name="b">End</param>
    /// <param name="color">Color</param>
    /// <param name="rotation">Rotation</param>
    [Conditional("UNITY_EDITOR")]
    public static void DottedLine(Vector3 a, Vector3 b, Color? color = null, Quaternion? rotation = null) =>
      DrawHandle(new LineHandle
      {
        a = rotation == null ? a : (Quaternion)rotation * a,
        b = rotation == null ? b : (Quaternion)rotation * b,
        color = color ?? Settings.DebugDrawLineColor.Value,
        solid = false
      });

    /// <summary> Draw an array of dashed lines. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="lines">Lines array</param>
    /// <param name="color">Color</param>
    /// <param name="rotation">Rotation</param>
    [Conditional("UNITY_EDITOR")]
    public static void DottedLines(Vector3[] lines, Color? color = null, Quaternion? rotation = null)
    {
      for (int i = 0; i < lines.Length - 1; i += 1)
        DottedLine(lines[i], lines[i + 1], color, rotation);
    }

    /// <summary> Draw a line using an arrow. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="position">Position</param>
    /// <param name="direction">Direction</param>
    /// <param name="length">Line length</param>
    /// <param name="size">Arrow tip size</param>
    /// <param name="width">Arrow width</param>
    /// <param name="color">Color</param>
    [Conditional("UNITY_EDITOR")]
    public static void Arrow(Vector3 position, Vector3 direction, float length = 1.0f, float? size = null, float? width = null, Color? color = null)
    {
      float sideLen = length - length * (size ?? Settings.DebugDrawArrowTipSize.Value);
      Vector3 widthOffset = Vector3.Cross(direction, Vector3.up) * (width ?? Settings.DebugDrawArrowWidth.Value);
      Vector3 tip = position + direction * length;
      Vector3 upCornerInRight = position - widthOffset * 0.3f + direction * sideLen;
      Vector3 upCornerInLeft = position + widthOffset * 0.3f + direction * sideLen;
      Vector3 upCornerOutRight = position - widthOffset * 0.5f + direction * sideLen;
      Vector3 upCornerOutLeft = position + widthOffset * 0.5f + direction * sideLen;

      Line(position, upCornerInRight, color ?? Settings.DebugDrawArrowColor.Value);
      Line(upCornerInRight, upCornerOutRight, color ?? Settings.DebugDrawArrowColor.Value);
      Line(upCornerOutRight, tip, color ?? Settings.DebugDrawArrowColor.Value);
      Line(tip, upCornerOutLeft, color ?? Settings.DebugDrawArrowColor.Value);
      Line(upCornerOutLeft, upCornerInLeft, color ?? Settings.DebugDrawArrowColor.Value);
      Line(upCornerInLeft, position, color ?? Settings.DebugDrawArrowColor.Value);
    }

    /// <summary> Draw a line using an arrow. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="position">Position</param>
    /// <param name="rotation">Rotation</param>
    /// <param name="length">Line length</param>
    /// <param name="size">Arrow tip size</param>
    /// <param name="width">Arrow width</param>
    /// <param name="color">Color</param>
    [Conditional("UNITY_EDITOR")]
    public static void Arrow(Vector3 position, Quaternion rotation, float length = 1.0f, float? size = null, float? width = null, Color? color = null)
      => Arrow(position, rotation * Vector3.forward, length, size, width, color);

    /// <summary> Draw a line using an arrow. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="color"></param>
    [Conditional("UNITY_EDITOR")]
    public static void Ray(Vector3 position, Quaternion rotation, Color? color = null) =>
      Line(position, (rotation * Vector3.forward) * Settings.DebugDrawRayLength.Value, color ?? Settings.DebugDrawRayColor.Value);

    /// <summary> Draw a ray.
    /// </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="position">Position</param>
    /// <param name="direction">Direction</param>
    /// <param name="color">Color</param>
    [Conditional("UNITY_EDITOR")]
    public static void Ray(Vector3 position, Vector3 direction, Color? color = null) =>
      Line(position, direction * Settings.DebugDrawRayLength.Value, color ?? Settings.DebugDrawRayColor.Value);
    
    /// <summary> Draw a wire circle. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="center">Center</param>
    /// <param name="radius">Radius</param>
    /// <param name="color">Color</param>
    /// <param name="rotation">Rotation</param>
    [Conditional("UNITY_EDITOR")]
    public static void Circle(Vector3 center, float radius, Color? color = null, Quaternion? rotation = null) =>
      DrawHandle(new CircleHandle
      {
        center = center,
        normal = rotation == null ? Vector3.up : (Quaternion)rotation * Vector3.forward,
        radius = radius,
        color = color ?? Settings.DebugDrawCircleColor.Value
      });

    /// <summary> Draw a wire circle. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="center">Center</param>
    /// <param name="radius">Radius</param>
    /// <param name="color">Color</param>
    /// <param name="normal">Normal</param>
    [Conditional("UNITY_EDITOR")]
    public static void Circle(Vector3 center, float radius, Color? color = null, Vector3? normal = null) =>
      DrawHandle(new CircleHandle
      {
        center = center,
        normal = normal ?? Vector3.up,
        radius = radius,
        color = color ?? Settings.DebugDrawCircleColor.Value
      });

    /// <summary> Draw a solid circle. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="center">Center</param>
    /// <param name="radius">Radius</param>
    /// <param name="color">Color</param>
    /// <param name="rotation">Rotation</param>
    [Conditional("UNITY_EDITOR")]
    public static void SolidCircle(Vector3 center, float radius, Color? color = null, Quaternion? rotation = null) =>
      DrawHandle(new CircleHandle
      {
        center = center,
        normal = rotation == null ? Vector3.up : (Quaternion)rotation * Vector3.forward,
        radius = radius,
        color = color ?? Settings.DebugDrawCircleColor.Value,
        solid = true
      });

    /// <summary> Draw a solid circle. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="center">Center</param>
    /// <param name="radius">Radius</param>
    /// <param name="color">Color</param>
    /// <param name="normal">Normal</param>
    [Conditional("UNITY_EDITOR")]
    public static void SolidCircle(Vector3 center, float radius, Color? color = null, Vector3? normal = null) =>
      DrawHandle(new CircleHandle
      {
        center = center,
        normal = normal ?? Vector3.up,
        radius = radius,
        color = color ?? Settings.DebugDrawCircleColor.Value,
        solid = true
      });

    /// <summary> Draw a wire sphere formed by three circles. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="center">Center</param>
    /// <param name="radius">Radius</param>
    /// <param name="color">Color</param>
    /// <param name="rotation">Rotation</param>
    [Conditional("UNITY_EDITOR")]
    public static void Sphere(Vector3 center, float radius, Color? color = null, Quaternion? rotation = null)
    {
      Circle(center, radius, color ?? Settings.DebugDrawAxisYColor.Value, rotation);
      Circle(center, radius, color ?? Settings.DebugDrawAxisXColor.Value, (rotation ?? Quaternion.identity) * Quaternion.Euler(0.0f, 0.0f, 90.0f));
      Circle(center, radius, color ?? Settings.DebugDrawAxisZColor.Value, (rotation ?? Quaternion.identity) * Quaternion.Euler(0.0f, 90.0f, 90.0f));
    }

    /// <summary> Draw a solid sphere. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="center">Center</param>
    /// <param name="radius">Radius</param>
    /// <param name="color">Color</param>
    /// <param name="rotation">Rotation</param>
    [Conditional("UNITY_EDITOR")]
    public static void SolidSphere(Vector3 center, float radius, Color? color = null, Quaternion? rotation = null) =>
      DrawHandle(new SphereHandle
      {
        center = center,
        radius = radius,
        color = color ?? Settings.DebugDrawCircleColor.Value,
        rotation = rotation ?? Quaternion.identity
      });

    /// <summary> Draw an wire arc centered on the forward vector. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="center">Center</param>
    /// <param name="rotation">Rotation</param>
    /// <param name="radius">Radius</param>
    /// <param name="angle">Angle</param>
    /// <param name="color">Color</param>
    [Conditional("UNITY_EDITOR")]
    public static void Arc(Vector3 center, Quaternion rotation, float radius, float angle, Color? color = null) =>
      DrawHandle(new ArcHandle
      {
        center = center,
        normal = rotation * Vector3.up,
        from = (rotation * Quaternion.Euler(-angle * 0.5f, 0.0f, 0.0f)) * Vector3.forward,
        angle = angle,
        radius = radius,
        color = color ?? Settings.DebugDrawArcColor.Value
      });

    /// <summary> Draw an solid arc centered on the forward vector. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="center">Center</param>
    /// <param name="rotation">Rotation</param>
    /// <param name="radius">Radius</param>
    /// <param name="angle">Angle</param>
    /// <param name="color">Color</param>
    [Conditional("UNITY_EDITOR")]
    public static void SolidArc(Vector3 center, Quaternion rotation, float radius, float angle, Color? color = null) =>
      DrawHandle(new ArcHandle
      {
        center = center,
        normal = rotation * Vector3.up,
        from = (rotation * Quaternion.Euler(0.0f, -angle * 0.5f, 0.0f)) * Vector3.forward,
        angle = angle,
        radius = radius,
        color = color ?? Settings.DebugDrawArcColor.Value,
        solid = true
      });

    /// <summary> Draw a wire cube. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="center">Center</param>
    /// <param name="size">Size</param>
    /// <param name="color">Color</param>
    [Conditional("UNITY_EDITOR")]
    public static void Cube(Vector3 center, Vector3 size, Color? color = null) =>
      DrawHandle(new CubeHandle
      {
        center = center,
        size = size,
        color = color ?? Settings.DebugDrawCubeColor.Value
      });

    /// <summary> Draw a wire cube. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="center">Center</param>
    /// <param name="size">Size</param>
    /// <param name="color">Color</param>
    [Conditional("UNITY_EDITOR")]
    public static void Cube(Vector3 center, float size, Color? color = null) =>
      Cube(center, Vector3.one * size, color ?? Settings.DebugDrawCubeColor.Value);

    /// <summary> Draw a wire diamond. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="center">Center</param>
    /// <param name="size">Size</param>
    /// <param name="color">Color</param>
    /// <param name="rotation">Rotation</param>
    [Conditional("UNITY_EDITOR")]
    public static void Diamond(Vector3 center, float? size = null, Color? color = null, Quaternion? rotation = null)
    {
      float diamondSize = size ?? Settings.DebugDrawDiamondSize.Value;
      Vector3 u = center + Vector3.up * diamondSize;
      Vector3 d = center + Vector3.down * diamondSize;
      Vector3 r = center + Vector3.right * diamondSize;
      Vector3 l = center + Vector3.left * diamondSize;
      Vector3 f = center + Vector3.forward * diamondSize;
      Vector3 b = center + Vector3.back * diamondSize;
      
      Lines(new[]
      {
        u, r, f,
        u, f, l,
        u, l, b,
        u, b, r
      }, color ?? Settings.DebugDrawDiamondColor.Value, rotation);

      Lines(new[]
      {
        d, f, r,
        d, r, b,
        d, b, l,
        d, l, f
      }, color ?? Settings.DebugDrawDiamondColor.Value, rotation);
    }

    /// <summary> Draw a wire cone. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="position">Position</param>
    /// <param name="rotation">Rotation</param>
    /// <param name="angle">Angle</param>
    /// <param name="color">Color</param>
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
      var farPlane = new Plane(-direction, position + forward);
      var distRay = new Ray(position, slerpedVector);

      farPlane.Raycast(distRay, out dist);

      Color coneColor = color ?? Settings.DebugDrawConeColor.Value;
      Debug.DrawRay(position, slerpedVector.normalized * dist, coneColor);
      Debug.DrawRay(position, Vector3.Slerp(forward, -up, angle / 90.0f).normalized * dist, coneColor);
      Debug.DrawRay(position, Vector3.Slerp(forward, right, angle / 90.0f).normalized * dist, coneColor);
      Debug.DrawRay(position, Vector3.Slerp(forward, -right, angle / 90.0f).normalized * dist, coneColor);

      Circle(position + forward, (forward - (slerpedVector.normalized * dist)).magnitude, coneColor, rotation);
      Circle(position + (forward * 0.5f), ((forward * 0.5f) - (slerpedVector.normalized * (dist * 0.5f))).magnitude, coneColor, rotation);      
    }

    /// <summary> Draw bounds. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="b">Bounds</param>
    /// <param name="color">Color</param>
    [Conditional("UNITY_EDITOR")]
    public static void Bounds(Bounds b, Color? color = null)
    {
      Color boundsColor = color ?? Settings.DebugDrawBoundsColor.Value;
      
      Vector3 lbf = new Vector3(b.min.x, b.min.y, b.max.z);
      Vector3 ltb = new Vector3(b.min.x, b.max.y, b.min.z);
      Vector3 rbb = new Vector3(b.max.x, b.min.y, b.min.z);
      Line(b.min, lbf, boundsColor);
      Line(b.min, ltb, boundsColor);
      Line(b.min, rbb, boundsColor);
      
      Vector3 rtb = new Vector3(b.max.x, b.max.y, b.min.z);
      Vector3 rbf = new Vector3(b.max.x, b.min.y, b.max.z);
      Vector3 ltf = new Vector3(b.min.x, b.max.y, b.max.z);
      Line(b.max, rtb, boundsColor);
      Line(b.max, rbf, boundsColor);
      Line(b.max, ltf, boundsColor);

      Line(rbb, rbf, boundsColor);
      Line(rbb, rtb, boundsColor);

      Line(lbf, rbf, boundsColor);
      Line(lbf, ltf, boundsColor);

      Line(ltb, rtb, boundsColor);
      Line(ltb, ltf, boundsColor);
    }

    /// <summary> Draw bounds. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="b"></param>
    /// <param name="color"></param>
    [Conditional("UNITY_EDITOR")]
    public static void Bounds(BoundsInt b, Color color) => Bounds(new Bounds(b.center, b.size), color);

    /// <summary> Draw text. </summary>
    /// <remarks>Only available in the Editor</remarks>
    /// <param name="position">Position</param>
    /// <param name="text">Text</param>
    /// <param name="color">Color</param>
    [Conditional("UNITY_EDITOR")]
    public static void Text(Vector3 position, string text, Color? color = null) =>
      DrawHandle(new TextHandle
      {
        position = position,
        text = text,
        color = color ?? Settings.DebugDrawTextColor.Value
      });
  }
}
