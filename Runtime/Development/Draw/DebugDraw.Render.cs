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
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Drawing of objects for development.
  /// </summary>
  /// <remarks>Only available in the Editor</remarks>
#if UNITY_EDITOR
  [UnityEditor.InitializeOnLoad]
#endif
  public static partial class DebugDraw
  {
    private static readonly List<JobGL> jobs;

    private static bool playing;
    private static readonly Material material;

#if UNITY_EDITOR
    static DebugDraw()
    {
      jobs = new(Capacity);
      
      material = new Material(Shader.Find("Hidden/Internal-Colored"))
      {
        hideFlags = HideFlags.HideAndDontSave
      };      
      material.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
      material.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
      material.SetInt("_Cull", (int)CullMode.Off);
      material.SetInt("_ZWrite", (int)CompareFunction.Always);

      UnityEditor.SceneView.duringSceneGui += (_) =>
      {
        if (Event.current.type == EventType.Repaint)
          Render(UnityEditor.SceneView.currentDrawingSceneView.camera);
        else
          jobs.Clear();
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

      //GL.PushMatrix();

      SubmitJobs();

      if (OcclusionColorFactor < 1.0f)
      {
        material.SetInt("_ZTest",  (int)CompareFunction.Greater);
        material.SetPass(0);

        SubmitJobs(OcclusionColorFactor);
      }
      
      //GL.PopMatrix();
      
      jobs.Clear();
    }

    private static void SubmitJobs(float colorFactor = 1.0f)
    {
      // Line.
      GL.Begin(GL.LINES);
      {
        for (int i = 0; i < jobs.Count; ++i)
        {
          if (jobs[i].matrix != Matrix4x4.identity)
          {
            GL.PushMatrix();
            GL.MultMatrix(jobs[i].matrix);
          }
        
          GL.Color(jobs[i].color * colorFactor);

          if (jobs[i].mode == JobGLMode.Line)
          {
            GL.Vertex(jobs[i].vertices[0]);
            GL.Vertex(jobs[i].vertices[1]);
          }
          else if (jobs[i].mode == JobGLMode.DottedLine)
          {
            float length = Vector3.Distance(jobs[i].vertices[0], jobs[i].vertices[1]);
      
            int count = Mathf.CeilToInt(length / DashSize);
            for (int j = 0; j < count; j += 2)
            {
              GL.Vertex(Vector3.Lerp(jobs[i].vertices[0], jobs[i].vertices[1], j * DashSize / length));
              GL.Vertex(Vector3.Lerp(jobs[i].vertices[0], jobs[i].vertices[1], (j + 1) * DashSize / length));
            }
          }
          
          if (jobs[i].matrix != Matrix4x4.identity)
            GL.PopMatrix();
        }
      }
      GL.End();

      // Lines.
      GL.Begin(GL.LINE_STRIP);
      {
        for (int i = 0; i < jobs.Count; ++i)
        {
          if (jobs[i].matrix != Matrix4x4.identity)
          {
            GL.PushMatrix();
            GL.MultMatrix(jobs[i].matrix);
          }
        
          GL.Color(jobs[i].color * colorFactor);

          if (jobs[i].mode == JobGLMode.Lines)
          {
            for (int j = 0; j < jobs[i].vertices.Length; ++j)
              GL.Vertex(jobs[i].vertices[j]);
          }
          
          if (jobs[i].matrix != Matrix4x4.identity)
            GL.PopMatrix();
        }
      }
      GL.End();
      
      // Triangles.
      GL.Begin(GL.TRIANGLES);
      {
        for (int i = 0; i < jobs.Count; ++i)
        {
          if (jobs[i].matrix != Matrix4x4.identity)
          {
            GL.PushMatrix();
            GL.MultMatrix(jobs[i].matrix);
          }
        
          GL.Color(jobs[i].color * colorFactor);

          if (jobs[i].mode == JobGLMode.Triangle)
          {
            GL.Vertex(jobs[i].vertices[0]);
            GL.Vertex(jobs[i].vertices[1]);
            GL.Vertex(jobs[i].vertices[2]);
          }
          
          if (jobs[i].matrix != Matrix4x4.identity)
            GL.PopMatrix();
        }
      }
      GL.End();
    }
  }
}
