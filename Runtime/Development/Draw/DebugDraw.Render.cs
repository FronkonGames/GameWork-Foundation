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
using Matrix4x4 = UnityEngine.Matrix4x4;
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
      
      CreateMeshes();
      
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

      Submit();

      if (OcclusionColorFactor < 1.0f)
      {
        material.SetInt("_ZTest",  (int)CompareFunction.Greater);
        material.SetPass(0);

        Submit(OcclusionColorFactor);
      }
      
      jobs.Clear();
    }

    private static void Submit(float colorFactor = 1.0f)
    {
      for (int i = 0; i < jobs.Count; ++i)
      {
        JobGL job = jobs[i];

        bool identity = job.matrix.Equals(Matrix4x4.identity); 
        if (identity == false)
        {
          GL.PushMatrix();
          GL.MultMatrix(job.matrix);
        }

        GL.Begin(job.mode);

        GL.Color(job.color * colorFactor);

        if (job.dotted == true)
        {
          float length = Vector3.Distance(job.vertices[0], job.vertices[1]);
    
          int count = Mathf.CeilToInt(length / LineDashSize);
          for (int j = 0; j < count; j += 2)
          {
            GL.Vertex(Vector3.Lerp(job.vertices[0], job.vertices[1], j * LineDashSize / length));
            GL.Vertex(Vector3.Lerp(job.vertices[0], job.vertices[1], (j + 1) * LineDashSize / length));
          }
        }
        else
        {
          for (int j = 0; j < jobs[i].vertices.Length; ++j)
            GL.Vertex(job.vertices[j]);
        }
        
        GL.End();
        
        if (identity == false)
          GL.PopMatrix();
      }
    }
  }
}
