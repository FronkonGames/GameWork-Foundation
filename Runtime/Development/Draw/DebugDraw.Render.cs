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
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Drawing of objects for development. </summary>
  /// <remarks> Only available in the Editor. </remarks>
  public partial class DebugDraw : CachedMonoBehaviour
  {
    private struct LineJob
    {
      public Vector3 start, end;
      public Color color;
      public bool continuous;

      public int frame;

      public readonly bool Alive => Time.frameCount - frame <= 0;
    }

    private struct TriangleJob
    {
      public Vector3 a, b, c;
      public Color color;
      public bool continuous;

      public int frame;

      public readonly bool Alive => Time.frameCount - frame <= 0;
    }

    private struct MeshJob
    {
      public List<Vector3> vertices;
      public Color color;
      public Matrix4x4 matrix;

      public int frame;

      public readonly bool Alive => Time.frameCount - frame <= 0;
    }

    private struct TextJob
    {
      public Vector3 position;
      public string text;
      public GUIStyle style;

      public int frame;

      public readonly bool Alive => Time.frameCount - frame <= 0;
    }

    private static LineJob[] lineJobs;
    private static TriangleJob[] triangleJobs;
    private static MeshJob[] meshJobs;
    private static TextJob[] textJobs;
#if UNITY_EDITOR
    private Material materialVisible, materialOccluded;

    private float occludedTransparency = 1.0f;

    private void OnDebugRender(Camera camera)
    {
      if (Event.current.type != EventType.Repaint)
        return;

      Profiler.BeginSample(Settings.DebugDraw.Profiler);

      CheckMaterials();
      CheckGeometry();

      GL.PushMatrix();
      GL.MultMatrix(Matrix4x4.identity);

      occludedTransparency = Settings.DebugDraw.OccludedColor;
      materialOccluded.SetPass(0);
      SubmitLines();
      SubmitTriangles();
      ResetProgramGL();
      SubmitMeshes();

      occludedTransparency = 1.0f;
      materialVisible.SetPass(0);
      SubmitLines();
      SubmitTriangles();
      ResetProgramGL();
      SubmitMeshes();

      GL.PopMatrix();

      SubmitTexts();

      Profiler.EndSample();
    }

    private void CheckMaterials()
    {
      if (materialVisible == null || materialOccluded)
      {
        Shader shader = Shader.Find("Hidden/Internal-Colored");
        materialVisible = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };
        materialVisible.shader.hideFlags = HideFlags.HideAndDontSave;
        materialVisible.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
        materialVisible.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
        materialVisible.SetInt("_Cull", (int)CullMode.Off);
        materialVisible.SetInt("_ZWrite", 1);
        materialVisible.SetInt("_ZTest", (int)CompareFunction.Less);

        materialOccluded = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };
        materialOccluded.shader.hideFlags = HideFlags.HideAndDontSave;
        materialOccluded.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
        materialOccluded.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
        materialOccluded.SetInt("_Cull", (int)CullMode.Off);
        materialOccluded.SetInt("_ZWrite", 1);
        materialOccluded.SetInt("_ZTest", (int)CompareFunction.GreaterEqual);
      }
    }

    private void CheckGeometry()
    {
      lineJobs ??= new LineJob[Settings.DebugDraw.Capacity];
      triangleJobs ??= new TriangleJob[Settings.DebugDraw.Capacity];
      meshJobs ??= new MeshJob[Settings.DebugDraw.Capacity];
      textJobs ??= new TextJob[Settings.DebugDraw.TextCapacity];
    }

    private void SubmitLines()
    {
      for (int i = 0; i < Settings.DebugDraw.Capacity; ++i)
      {
        if (lineJobs[i].Alive == true)
        {
          if (lineJobs[i].continuous == true)
            DrawLineGL(lineJobs[i].start, lineJobs[i].end, lineJobs[i].color);
          else
            DrawLineGLDotted(lineJobs[i].start, lineJobs[i].end, lineJobs[i].color);
        }
      }
    }
    
    private void SubmitTriangles()
    {
      for (int i = 0; i < Settings.DebugDraw.Capacity; ++i)
      {
        if (triangleJobs[i].Alive == true)
        {
          if (triangleJobs[i].continuous == true)
            DrawTriangleGLSolid(triangleJobs[i].a, triangleJobs[i].b, triangleJobs[i].c, triangleJobs[i].color);
          else
            DrawTriangleGL(triangleJobs[i].a, triangleJobs[i].b, triangleJobs[i].c, triangleJobs[i].color);
        }
      }
    }

    private void SubmitMeshes()
    {
      for (int i = 0; i < Settings.DebugDraw.Capacity; ++i)
      {
        if (meshJobs[i].Alive == true)
          DrawMeshGL(meshJobs[i].vertices, meshJobs[i].color, meshJobs[i].matrix);
      }
    }

    private void SubmitTexts()
    {
      UnityEditor.Handles.BeginGUI();

      for (int i = 0; i < Settings.DebugDraw.TextCapacity; ++i)
      {
        if (textJobs[i].Alive == true)
        {
          Vector3 position = UnityEditor.HandleUtility.WorldToGUIPointWithDepth(textJobs[i].position);
          if (position.z >= 0.0f)
          {
            GUIContent content = UnityEditor.EditorGUIUtility.TrTempContent(textJobs[i].text);
            GUI.Label(UnityEditor.HandleUtility.WorldPointToSizedRect(textJobs[i].position, content, textJobs[i].style ?? GUI.skin.label), content, textJobs[i].style ?? GUI.skin.label);
          }
        }
      }

      UnityEditor.Handles.EndGUI();
    }

    private int GetLineIndex()
    {
      int length = lineJobs != null ? lineJobs.Length : 0;
      for (int i = 0; i < length; ++i)
      {
        if (lineJobs[i].Alive == false)
          return i;
      }

      return -1;
    }

    private int GetTriangleIndex()
    {
      int length = triangleJobs != null ? triangleJobs.Length : 0;
      for (int i = 0; i < length; ++i)
      {
        if (triangleJobs[i].Alive == false)
          return i;
      }

      return -1;
    }

    private int GetMeshIndex()
    {
      int length = meshJobs != null ? meshJobs.Length : 0;
      for (int i = 0; i < length; ++i)
      {
        if (meshJobs[i].Alive == false)
          return i;
      }

      return -1;
    }

    private int GetTextIndex()
    {
      int length = textJobs != null ? textJobs.Length : 0;
      for (int i = 0; i < length; ++i)
      {
        if (textJobs[i].Alive == false)
          return i;
      }

      return -1;
    }

    private void Clear()
    {
      for (int i = 0; lineJobs != null && i < lineJobs.Length; ++i)
        lineJobs[i].frame = -1;

      for (int i = 0; triangleJobs != null && i < triangleJobs.Length; ++i)
        triangleJobs[i].frame = -1;

      for (int i = 0; meshJobs != null && i < meshJobs.Length; ++i)
        meshJobs[i].frame = -1;

      for (int i = 0; textJobs != null && i < textJobs.Length; ++i)
        textJobs[i].frame = -1;
    }
#else
    private int GetLineIndex() => 0;

    private int GetTriangleIndex() => 0;

    private int GetMeshIndex() => 0;

    private int GetTextIndex() => 0;
#endif
  }
}