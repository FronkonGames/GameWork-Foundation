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
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Drawing of objects for development.
  /// </summary>
  /// <remarks>Only available in the Editor</remarks>
  public static partial class Draw
  {
    private static MethodInfo applyWireMaterial = null;

    private static void DrawPoint(Vector3 a, Color color, float size)
    {
      if (OccludeColorFactor <= 0.0f)
      {
        ApplyWireMaterial();

        GLPoint(a, color, size);
      }
      else
      {
        ApplyWireMaterial(CompareFunction.Less);

        GLPoint(a, color, size);

        ApplyWireMaterial(CompareFunction.Greater);

        GLPoint(a, color * OccludeColorFactor, size);
      }
    }
    
    private static void DrawTriangle(Vector3 a, Vector3 b, Vector3 c, Color color)
    {
      if (OccludeColorFactor <= 0.0f)
      {
        ApplyWireMaterial();

        GLTriangle(a, b, c, color);
      }
      else
      {
        ApplyWireMaterial(CompareFunction.Less);

        GLTriangle(a, b, c, color);

        ApplyWireMaterial(CompareFunction.Greater);

        GLTriangle(a, b, c, color * OccludeColorFactor);
      }
    }

    private static void DrawLine(Vector3 a, Vector3 b, Color color)
    {
      if (OccludeColorFactor <= 0.0f)
      {
        ApplyWireMaterial();

        GLLine(a, b, color);
      }
      else
      {
        ApplyWireMaterial(CompareFunction.Less);

        GLLine(a, b, color);

        ApplyWireMaterial(CompareFunction.Greater);

        GLLine(a, b, color * OccludeColorFactor);
      }
    }

    private static void DrawDottedLine(Vector3 a, Vector3 b, Color color)
    {
      if (OccludeColorFactor <= 0.0f)
      {
        ApplyWireMaterial();

        GLDottedLine(a, b, color);
      }
      else
      {
        ApplyWireMaterial(CompareFunction.Less);

        GLDottedLine(a, b, color);

        ApplyWireMaterial(CompareFunction.Greater);

        GLDottedLine(a, b, color * OccludeColorFactor);
      }
    }

    private static void DrawLines(IReadOnlyList<Vector3> segments, Color color)
    {
      if (segments.Count > 1)
      {
        for (int i = 0; i < segments.Count - 1; ++i)
          DrawLine(segments[i], segments[i + 1], color);
      }
    }

    private static void DrawDottedLines(IReadOnlyList<Vector3> segments, Color color)
    {
      if (segments.Count > 1)
      {
        for (int i = 0; i < segments.Count - 1; ++i)
          DrawDottedLine(segments[i], segments[i + 1], color);
      }
    }

    private static void DrawDisc(Vector3 center, float radius, Quaternion rotation, Color color)
    {
      float current = 0.0f;
      float grad = MathConstants.Pi2 / Segments;

      for (int i = 0; i < Segments; ++i)
      {
        DrawLine(rotation * new Vector3(Mathf.Sin(current) * radius, 0.0f, Mathf.Cos(current) * radius) + center,
          i == Segments - 1 ? rotation * new Vector3(0f, 0f, radius) + center
                            : rotation * new Vector3(Mathf.Sin(current + grad) * radius, 0.0f, Mathf.Cos(current + grad) * radius) + center,
          color);
        current += grad;
      }
    }

    private static void DrawSolidDisc(Vector3 center, float radius, Quaternion rotation, Color color)
    {
      float current = 0.0f;
      float grad = MathConstants.Pi2 / Segments;

      for (int i = 0; i < Segments; ++i)
      {
        DrawTriangle(center, rotation * new Vector3(Mathf.Sin(current) * radius, 0.0f, Mathf.Cos(current) * radius) + center,
          i == Segments - 1 ? rotation * new Vector3(0.0f, 0.0f, radius) + center
                              : rotation * new Vector3(Mathf.Sin(current + grad) * radius, 0.0f, Mathf.Cos(current + grad) * radius) + center,
          color);
        current += grad;
      }
    }
    
    private static void DrawArc(Vector3 center, Vector3 normal, Vector3 from, float radius, float angle, Color color)
    {
    }
/*
    private static void DrawArrowHead(Vector3 point, Vector3 dir, Color color, float scale = 1.0f)
    {
      const float arrowLength = 0.075f;
      const float arrowWidth = 0.05f;
      const int segments = 3;

      Vector3 arrowPoint = point + dir;
      dir.EnsureNormalized();
      InternalDisc(arrowPoint - dir * (arrowLength * scale), dir, arrowWidth * scale, (a, b, f) =>
      {
        lineDelegate(a, b, color, f);
        lineDelegate(a, arrowPoint, color, f);
      }, segments);
    }

    private static void DrawBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, LineDelegateSimple lineDelegate)
    {
      DrawBoxStructure box = new DrawBoxStructure(halfExtents, orientation);
      DrawBox(center, box, lineDelegate);
    }

    private static void DrawBox(Vector3 center, DrawBoxStructure structure, LineDelegateSimple lineDelegate)
    {
      Vector3 posUFL = structure.UFL + center,
        posUFR = structure.UFR + center,
        posUBL = structure.UBL + center,
        posUBR = structure.UBR + center,
        posDFL = structure.DFL + center,
        posDFR = structure.DFR + center,
        posDBL = structure.DBL + center,
        posDBR = structure.DBR + center;

      // Up.
      lineDelegate(posUFL, posUFR);
      lineDelegate(posUFR, posUBR);
      lineDelegate(posUBR, posUBL);
      lineDelegate(posUBL, posUFL);

      // Down.
      lineDelegate(posDFL, posDFR);
      lineDelegate(posDFR, posDBR);
      lineDelegate(posDBR, posDBL);
      lineDelegate(posDBL, posDFL);
      
      // Down to up.
      lineDelegate(posDFL, posUFL);
      lineDelegate(posDFR, posUFR);
      lineDelegate(posDBR, posUBR);
      lineDelegate(posDBL, posUBL);
    }

    private static void DrawCapsuleFast(Vector3 point1, Vector3 point2, float radius, Vector3 axis, Vector3 crossA, Vector3 crossB, LineDelegate lineDelegate)
    {
      // Circles.
      DrawCircleFast(point1, axis, crossB, radius, lineDelegate);
      DrawCircleFast(point2, axis, crossB, radius, lineDelegate);

      // Caps.
      DrawArc(point1, crossB, crossA, radius, 180, lineDelegate, 25);
      DrawArc(point1, crossA, crossB, radius, -180, lineDelegate, 25);

      DrawArc(point2, crossB, crossA, radius, -180, lineDelegate, 25);
      DrawArc(point2, crossA, crossB, radius, 180, lineDelegate, 25);

      // Joining lines.
      Vector3 a = crossA * radius;
      Vector3 b = crossB * radius;
      lineDelegate.Invoke(point1 + a, point2 + a, 0);
      lineDelegate.Invoke(point1 - a, point2 - a, 0);
      lineDelegate.Invoke(point1 + b, point2 + b, 0);
      lineDelegate.Invoke(point1 - b, point2 - b, 0);
    }
*/
    private static void EnsureNormalized(this ref Vector3 vector3)
    {
      float sqrMag = vector3.sqrMagnitude;
      if (Mathf.Approximately(sqrMag, 1.0f))
        return;

      vector3 /= Mathf.Sqrt(sqrMag);
    }

    private static void EnsureNormalized(this ref Vector2 vector2)
    {
      float sqrMag = vector2.sqrMagnitude;
      if (Mathf.Approximately(sqrMag, 1.0f))
        return;

      vector2 /= Mathf.Sqrt(sqrMag);
    }

    private static Vector3 GetAxisAlignedAlternateWhereRequired(Vector3 normal, Vector3 alternate)
    {
      if (Mathf.Abs(Vector3.Dot(normal, alternate)) > 0.999f)
        alternate = GetAxisAlignedAlternate(normal);

      return alternate;
    }

    private static Vector3 GetAxisAlignedAlternate(Vector3 normal)
    {
      Vector3 alternate = new Vector3(0.0f, 0.0f, 1.0f);
      if (Mathf.Abs(Vector3.Dot(normal, alternate)) > 0.707f)
        alternate = new Vector3(0.0f, 1.0f, 0.0f);

      return alternate;
    }

    private static Vector3 GetAxisAlignedPerpendicular(Vector3 normal)
    {
      Vector3 cross = Vector3.Cross(normal, GetAxisAlignedAlternate(normal));
      cross.EnsureNormalized();

      return cross;
    }

    private static void GLPoint(Vector3 p, Color color, float size)
    {
      GL.PushMatrix();
      GL.Begin(GL.QUADS);
      GL.Color(color);

      GL.Vertex3(p.x + size, p.y + size, p.z);
      GL.Vertex3(p.x + size, p.y - size, p.z);
      GL.Vertex3(p.x - size, p.y - size, p.z);
      GL.Vertex3(p.x - size, p.y + size, p.z);

      GL.Vertex3(p.x, p.y + size, p.z + size);
      GL.Vertex3(p.x, p.y - size, p.z + size);
      GL.Vertex3(p.x, p.y - size, p.z - size);
      GL.Vertex3(p.x, p.y + size, p.z - size);

      GL.Vertex3(p.x + size, p.y, p.z + size);
      GL.Vertex3(p.x + size, p.y, p.z - size);
      GL.Vertex3(p.x - size, p.y, p.z - size);
      GL.Vertex3(p.x - size, p.y, p.z + size);
      
      GL.End();
      GL.PopMatrix();
    }

    private static void GLLine(Vector3 a, Vector3 b, Color color)
    {
      GL.PushMatrix();
      GL.Begin(GL.LINES);
      GL.Color(color);
      GL.Vertex(a);
      GL.Vertex(b);      
      GL.End();
      GL.PopMatrix();
    }

    private static void GLDottedLine(Vector3 a, Vector3 b, Color color)
    {
      GL.PushMatrix();
      GL.Begin(GL.LINES);
      GL.Color(color);
      
      float length = Vector3.Distance(a, b);
      
      int count = Mathf.CeilToInt(length / DashSize);
      for (int i = 0; i < count; i += 2)
      {
        GL.Vertex((Vector3.Lerp(a, b, i * DashSize / length)));

        GL.Vertex((Vector3.Lerp(a, b, (i + 1) * DashSize / length)));
      }

      GL.End();
      GL.PopMatrix();
    }

    private static void GLTriangle(Vector3 a, Vector3 b, Vector3 c, Color color)
    {
      GL.PushMatrix();
      GL.Begin(GL.TRIANGLES);
      GL.Color(color);
      GL.Vertex(a);
      GL.Vertex(b);      
      GL.Vertex(c);      
      GL.End();
      GL.PopMatrix();
    }

    private static void ApplyWireMaterial(CompareFunction zTest = CompareFunction.Always)
    {
      if (applyWireMaterial == null)
      {
        MethodInfo[] methodInfos = typeof(UnityEditor.HandleUtility).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
        for (int i = 0; i < methodInfos.Length; ++i)
        {
          if (methodInfos[i].Name == "ApplyWireMaterial" && methodInfos[i].GetParameters().Length == 1)
          {
            applyWireMaterial = methodInfos[i];
            break;
          }
        }
      }

      applyWireMaterial?.Invoke(null, new object[] { zTest });
    }
  }
}
