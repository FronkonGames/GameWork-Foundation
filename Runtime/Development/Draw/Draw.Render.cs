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
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Drawing of objects for development.
  /// </summary>
  /// <remarks>Only available in the Editor</remarks>
#if UNITY_EDITOR
  [UnityEditor.InitializeOnLoad]
#endif
  public static partial class Draw
  {
    private struct PointGL
    {
      public Vector3 a;
      public Color color;
      public float duration;
    }
    
    private readonly struct LineGL
    {
      public readonly Vector3 a;
      public readonly Vector3 b;
      public readonly Color color;
      public readonly float duration;

      public LineGL(Vector3 a, Vector3 b, Color color, float duration = 0.0f)
      {
        this.a = a;
        this.b = b;
        this.color = color;
        this.duration = Mathf.Max(0.0f, duration);
      }
    }
    
    private readonly struct TriangleGL
    {
      public readonly Vector3 a;
      public readonly Vector3 b;
      public readonly Vector3 c;
      public readonly Color color;
      public readonly float duration;
      
      public TriangleGL(Vector3 a, Vector3 b, Vector3 c, Color color, float duration = 0.0f)
      {
        this.a = a;
        this.b = b;
        this.c = c;
        this.color = color;
        this.duration = Mathf.Max(0.0f, duration);
      }
    }

    private static List<PointGL> points;
    private static readonly List<LineGL> solidLines;
    private static readonly List<LineGL> dottedLines;
    private static readonly List<TriangleGL> triangles;

    private static bool playing;
    private static readonly Material material;

#if UNITY_EDITOR
    static Draw()
    {
      points = new(Capacity);
      solidLines = new(Capacity);
      dottedLines = new(Capacity);
      triangles = new(Capacity);
      
      material = new Material(Shader.Find("Hidden/Internal-Colored"))
      {
        hideFlags = HideFlags.HideAndDontSave
      };      
      material.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
      material.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
      material.SetInt("_Cull", (int)CullMode.Off);
      material.SetInt("_ZWrite", 0);

      UnityEditor.SceneView.duringSceneGui += (_) =>
      {
        if (Event.current.type == EventType.Repaint)
          Render(UnityEditor.SceneView.currentDrawingSceneView.camera);
        else
          Clear();
      };
      UnityEditor.EditorApplication.playModeStateChanged += playMode => (playMode switch
      {
        UnityEditor.PlayModeStateChange.EnteredPlayMode => () => playing = true,
        UnityEditor.PlayModeStateChange.ExitingPlayMode => () => playing = false,
        _ => (Action)(() => { })
      })();      
    }
#endif
    
    private static void Render(Camera camera)
    {
      material.SetInt("_ZTest",  OcclusionColorFactor < 1.0f ? (int)CompareFunction.Less : (int)CompareFunction.Always);
      material.SetPass(0);

      GL.PushMatrix();

      GL.Begin(GL.TRIANGLES);
      SendTriangles();
      GL.End();
      
      GL.Begin(GL.LINES);
      SendSolidLines();
      SendDottedLines();
      GL.End();

      if (OcclusionColorFactor < 1.0f)
      {
        material.SetInt("_ZTest",  (int)CompareFunction.Greater);
        material.SetPass(0);
        
        GL.Begin(GL.TRIANGLES);
        SendTriangles(OcclusionColorFactor);
        GL.End();
      
        GL.Begin(GL.LINES);
        SendSolidLines(OcclusionColorFactor);
        SendDottedLines(OcclusionColorFactor);
        GL.End();
      }
      
      GL.PopMatrix();
      
      Clear();
    }

    private static void SendSolidLines(float colorFactor = 1.0f)
    {
      int i = 0;
      for (; i < solidLines.Count; ++i)
      {
        GL.Color(solidLines[i].color * colorFactor);
        GL.Vertex(solidLines[i].a);
        GL.Vertex(solidLines[i].b);
      }
    }

    private static void SendDottedLines(float colorFactor = 1.0f)
    {
      int i = 0;
      for (; i < dottedLines.Count; ++i)
      {
        float length = Vector3.Distance(dottedLines[i].a, dottedLines[i].b);
      
        int count = Mathf.CeilToInt(length / DashSize);
        for (int j = 0; j < count; j += 2)
        {
          GL.Color(dottedLines[i].color * colorFactor);
          GL.Vertex((Vector3.Lerp(dottedLines[i].a, dottedLines[i].b, j * DashSize / length)));
          GL.Vertex((Vector3.Lerp(dottedLines[i].a, dottedLines[i].b, (j + 1) * DashSize / length)));
        }
      }
    }

    private static void SendTriangles(float colorFactor = 1.0f)
    {
      int i = 0;
      for (; i < triangles.Count; ++i)
      {
        GL.Color(triangles[i].color * colorFactor);
        GL.Vertex(triangles[i].a);
        GL.Vertex(triangles[i].b);
        GL.Vertex(triangles[i].c);
      }
    }
    
    private static void Clear()
    {
      solidLines.Clear();
      dottedLines.Clear();
      triangles.Clear();
    }
  }
}