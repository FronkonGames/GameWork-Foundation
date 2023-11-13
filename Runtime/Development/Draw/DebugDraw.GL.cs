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
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Drawing of objects for development. </summary>
  /// <remarks>Only available in the Editor</remarks>
  public partial class DebugDraw : CachedMonoBehaviour
  {
    private int currentProgram = -1;

    private void DrawLineGL(Vector3 a, Vector3 b, Color color)
    {
      SetProgramGL(GL.LINES);
      GL.Color(color.SetA(Settings.DebugDraw.Transparency * occludedTransparency));

      GL.Vertex(a);
      GL.Vertex(b);
    }

    private void DrawLineGLDotted(Vector3 a, Vector3 b, Color color)
    {
      SetProgramGL(GL.LINES);
      GL.Color(color.SetA(Settings.DebugDraw.Transparency * occludedTransparency));

      float dashSize = Settings.DebugDraw.LineGapSize * EditorGUIUtility.pixelsPerPoint;

      float length = Vector3.Distance(a, b);
      int count = Mathf.CeilToInt(length / dashSize);
      for (int i = 0; i < count; i += 2)
      {
        GL.Vertex(Vector3.Lerp(a, b, i * dashSize / length));
        GL.Vertex(Vector3.Lerp(a, b, (i + 1) * dashSize / length));
      }
    }

    private void DrawTriangleGL(Vector3 a, Vector3 b, Vector3 c, Color color)
    {
      SetProgramGL(GL.LINES);
      GL.Color(color.SetA(Settings.DebugDraw.Transparency * occludedTransparency));

      DrawLineGL(a, b, color);
      DrawLineGL(b, c, color);
      DrawLineGL(c, a, color);
    }

    private void DrawTriangleGLSolid(Vector3 a, Vector3 b, Vector3 c, Color color)
    {
      SetProgramGL(GL.TRIANGLES);
      GL.Color(color.SetA(Settings.DebugDraw.Transparency * occludedTransparency));

      GL.Vertex(a);
      GL.Vertex(b);
      GL.Vertex(c);
    }

    private void DrawMeshGL(List<Vector3> vertices, Color color, Matrix4x4 matrix)
    {
      GL.PushMatrix();
      GL.MultMatrix(matrix);

      GL.Begin(GL.LINES);
      GL.Color(color.SetA(Settings.DebugDraw.Transparency * occludedTransparency));

      for (int i = 0; i < vertices.Count; ++i)
        GL.Vertex(vertices[i]);

      GL.End();

      GL.PopMatrix();
    }

    private void SetProgramGL(int program)
    {
      if (currentProgram != program)
      {
        currentProgram = program;
        GL.End();

        switch (currentProgram)
        {
          case 1:  GL.Begin(GL.LINES); break;
          case 4:  GL.Begin(GL.TRIANGLES); break;
          case 5:  GL.Begin(GL.TRIANGLE_STRIP); break;
          case 7:  GL.Begin(GL.QUADS); break;
          default: GL.Begin(GL.LINES); break;
        }
      }
    }

    private void ResetProgramGL()
    {
      currentProgram = -1;

      GL.End();
    }
  }
}
#endif