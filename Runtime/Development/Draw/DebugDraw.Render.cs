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
  public static partial class DebugDraw
  {
    private static readonly List<RenderGL> jobsGL;
    private static readonly List<RenderText> jobsText;

    private static readonly Material material;

    private static GUIStyle TextStyle
    {
      get
      {
        if (textStyle == null)
          textStyle = new(UnityEditor.EditorStyles.whiteMiniLabel)
          {
            richText = true,
            fontSize = TextSize,
            alignment = TextAnchor.MiddleCenter
          };

        return textStyle;
      }
    }

    private static GUIStyle textStyle;
    
    static DebugDraw()
    {
      jobsGL = new(Capacity);
      jobsText = new(Capacity / 4);
      
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
          Render();
        else
        {
          jobsText.Clear();
          jobsGL.Clear();
        }
      };
    }
    
    private static void Render()
    {
      SubmitText();

      material.SetInt("_ZTest",  OcclusionColorFactor < 1.0f ? (int)CompareFunction.Less : (int)CompareFunction.Always);
      material.SetPass(0);
      
      SubmitGL();

      if (OcclusionColorFactor < 1.0f)
      {
        material.SetInt("_ZTest",  (int)CompareFunction.Greater);
        material.SetPass(0);

        SubmitGL(OcclusionColorFactor);
      }
      
      jobsGL.Clear();
      jobsText.Clear();
    }

    private static void SubmitText()
    {
      UnityEditor.Handles.BeginGUI();

      for (int i = 0; i < jobsText.Count; ++i)
      {
        RenderText render = jobsText[i];
        
        GUI.Label(render.textArea, render.content, TextStyle);        
      }
      
      UnityEditor.Handles.EndGUI();
    }

    private static void SubmitGL(float colorFactor = 1.0f)
    {
      for (int i = 0; i < jobsGL.Count; ++i)
      {
        RenderGL render = jobsGL[i];

        bool identity = render.matrix.Equals(Matrix4x4.identity); 
        if (identity == false)
        {
          GL.PushMatrix();
          GL.MultMatrix(render.matrix);
        }

        GL.Begin(render.mode);

        GL.Color(render.color * colorFactor);

        if (render.dotted == true)
        {
          float length = Vector3.Distance(render.vertices[0], render.vertices[1]);
    
          int count = Mathf.CeilToInt(length / LineDashSize);
          for (int j = 0; j < count; j += 2)
          {
            GL.Vertex(Vector3.Lerp(render.vertices[0], render.vertices[1], j * LineDashSize / length));
            GL.Vertex(Vector3.Lerp(render.vertices[0], render.vertices[1], (j + 1) * LineDashSize / length));
          }
        }
        else
        {
          for (int j = 0; j < jobsGL[i].vertices.Length; ++j)
            GL.Vertex(render.vertices[j]);
        }
        
        GL.End();
        
        if (identity == false)
          GL.PopMatrix();
      }
    }
#endif
  }
}
