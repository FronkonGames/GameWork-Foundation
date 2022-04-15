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
  /// Drawing of objects for development.
  /// </summary>
  /// <remarks>Only available in the Editor</remarks>
  public static partial class Draw
  {
    private static readonly ColouredLineDelegate rayDelegate = (a, b, c, d) => lineDelegate(a, a + b, c, d);

    private static void DrawCircle(Vector3 center, Vector3 normal, float radius, LineDelegate lineDelegate, int segments = 100)
    {
      Vector3 cross = GetAxisAlignedPerpendicular(normal);
      Vector3 direction = cross * radius;
      Vector3 lastPos = center + direction;
      Quaternion rotation = Quaternion.AngleAxis(1.0f / (float)segments * 360, normal);
      Quaternion currentRotation = rotation;
      
      for (int i = 1; i <= segments; ++i)
      {
        Vector3 nextPos = center + currentRotation * direction;
        lineDelegate(lastPos, nextPos, (i - 1) / (float)segments);
        currentRotation = rotation * currentRotation;
        lastPos = nextPos;
      }
    }

    private static void DrawCircleFast(Vector3 center, Vector3 normal, Vector3 cross, float radius, LineDelegate lineDelegate, int segments = 100)
    {
      Vector3 direction = cross * radius;
      Vector3 lastPos = center + direction;
      Quaternion rotation = Quaternion.AngleAxis(1 / (float)segments * 360, normal);
      Quaternion currentRotation = rotation;

      for (int i = 1; i <= segments; ++i)
      {
        Vector3 nextPos = center + currentRotation * direction;
        lineDelegate(lastPos, nextPos, (i - 1) / (float)segments);
        currentRotation = rotation * currentRotation;
        lastPos = nextPos;
      }
    }

    private static void DrawArc(Vector3 center, Vector3 normal, Vector3 startDirection, float radius, float totalAngle, LineDelegate lineDelegate, int segments = 100)
    {
      Vector3 direction = startDirection * radius;
      Vector3 lastPos = center + direction;
      Quaternion rotation = Quaternion.AngleAxis(1.0f / (float)segments * totalAngle, normal);
      Quaternion currentRotation = rotation;

      for (int i = 1; i <= segments * 0.5f; ++i)
      {
        Vector3 nextPos = center + currentRotation * direction;
        lineDelegate(lastPos, nextPos, (i - 1) / (float)segments - 0.5f);
        currentRotation = rotation * currentRotation;
        lastPos = nextPos;
      }
    }

    private static void DrawArrowHead(Vector3 point, Vector3 dir, Color color, float duration = 0.0f, float scale = 1.0f)
    {
      const float arrowLength = 0.075f;
      const float arrowWidth = 0.05f;
      const int segments = 3;

      Vector3 arrowPoint = point + dir;
      dir.EnsureNormalized();
      DrawCircle(arrowPoint - dir * (arrowLength * scale), dir, arrowWidth * scale, (a, b, f) =>
      {
        lineDelegate(a, b, color, duration);
        lineDelegate(a, arrowPoint, color, duration);
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
  }
}
