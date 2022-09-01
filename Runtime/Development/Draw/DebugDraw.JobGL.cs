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
using System.Linq;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Drawing of objects for development.
  /// </summary>
  /// <remarks>Only available in the Editor</remarks>
  public static partial class DebugDraw
  {
    private readonly struct JobGL
    {
      public readonly int mode;
      public readonly Vector3[] vertices;
      public readonly Color color;
      public readonly Matrix4x4 matrix;
      public readonly bool dotted;

      public JobGL(int mode, Vector3[] vertices, Color color, Matrix4x4 matrix, bool dotted = false)
      {
        this.mode = mode;
        this.vertices = vertices;
        this.color = color;
        this.color.a = Transparency;
        this.matrix = matrix;
        this.dotted = dotted;
      }

      public static void AddLine(Vector3 a, Vector3 b, Color color, Quaternion? rotation = null, float scale = 1.0f, bool dotted = false)
      {
        JobGL job = new(GL.LINES, new[] {a, b}, color,
          rotation == null && scale == 1.0f
            ? Matrix4x4.identity
            : Matrix4x4.TRS(Vector3.zero, rotation ?? Quaternion.identity, scale * Vector3.one),
          dotted);

        jobs.Add(job);
      }

      public static void AddLines(IEnumerable<Vector3> points, Color color, Quaternion? rotation = null, float scale = 1.0f)
      {
        JobGL job = new(GL.LINE_STRIP, points.ToArray(), color,
          rotation == null && scale == 1.0f
            ? Matrix4x4.identity
            : Matrix4x4.TRS(Vector3.zero, rotation ?? Quaternion.identity, scale * Vector3.one));
          
        jobs.Add(job);
      }
      
      public static void AddTriangle(Vector3 a, Vector3 b, Vector3 c, Color color, Quaternion? rotation = null, float scale = 1.0f)
      {
        JobGL job = new(GL.TRIANGLES, new[] { a, b, c }, color,
          rotation == null && scale == 1.0f
            ? Matrix4x4.identity
            : Matrix4x4.TRS(Vector3.zero, rotation ?? Quaternion.identity, scale * Vector3.one));

        jobs.Add(job);
      }
    }
  }
}
