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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Drawing of objects for development. </summary>
  /// <remarks>Only available in the Editor</remarks>
  public static partial class DebugDraw
  {
    private interface IHandleDraw
    {
      void Draw();

      void DrawGL();
    }

    private struct LineHandle : IHandleDraw
    {
      public Vector3 a;
      public Vector3 b;
      public Color color;
      public bool solid;

      public void Draw()
      {
#if UNITY_EDITOR
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.color = UnityEditor.Handles.color.SetA(Settings.Draw.Transparency);

        if (solid == true)
          UnityEditor.Handles.DrawLine(a, b, Settings.Draw.LineThickness);
        else
          UnityEditor.Handles.DrawDottedLine(a, b, Settings.Draw.LineGapSize);
#else
        throw new NotSupportedException("Only available in Editor version.");
#endif
      }

      public void DrawGL()
      {
#if UNITY_EDITOR
        //wireMaterial.color = color;
        GL.Color(color);
        GL.Vertex(a);
        GL.Vertex(b);
#else
        throw new NotSupportedException("Only available in Editor version.");
#endif
      }
    }

    private struct CircleHandle : IHandleDraw
    {
      public Vector3 center;
      public Vector3 normal;
      public float radius;
      public Color color;
      public bool solid;

      public void Draw()
      {
#if UNITY_EDITOR
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.color = UnityEditor.Handles.color.SetA(Settings.Draw.Transparency);

        if (solid == true)
          UnityEditor.Handles.DrawSolidDisc(center, normal, radius);
        else
          UnityEditor.Handles.DrawWireDisc(center, normal, radius);
#else
        throw new NotSupportedException("Only available in Editor version.");
#endif
      }

      public void DrawGL()
      {
#if UNITY_EDITOR
#else
        throw new NotSupportedException("Only available in Editor version.");
#endif
      }
    }

    private struct ArcHandle : IHandleDraw
    {
      public Vector3 center;
      public Vector3 normal;
      public Vector3 from;
      public float angle;
      public float radius;
      public Color color;
      public bool solid;

      public void Draw()
      {
#if UNITY_EDITOR
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.color = UnityEditor.Handles.color.SetA(Settings.Draw.Transparency);

        if (solid == true)
          UnityEditor.Handles.DrawSolidArc(center, normal, from, angle, radius);
        else
          UnityEditor.Handles.DrawWireArc(center, normal, from, angle, radius);
#else
        throw new NotSupportedException("Only available in Editor version.");
#endif
      }

      public void DrawGL()
      {
#if UNITY_EDITOR
#else
        throw new NotSupportedException("Only available in Editor version.");
#endif
      }
    }

    private struct SphereHandle : IHandleDraw
    {
      public Vector3 center;
      public float radius;
      public Color color;
      public Quaternion rotation;

      public void Draw()
      {
#if UNITY_EDITOR
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.color = UnityEditor.Handles.color.SetA(Settings.Draw.Transparency);
        UnityEditor.Handles.SphereHandleCap(0, center, rotation, radius, EventType.Repaint);
#else
        throw new NotSupportedException("Only available in Editor version.");
#endif
      }

      public void DrawGL()
      {
#if UNITY_EDITOR
#else
        throw new NotSupportedException("Only available in Editor version.");
#endif
      }
    }

    private struct CubeHandle : IHandleDraw
    {
      public Vector3 center;
      public Vector3 size;
      public Color color;

      public void Draw()
      {
#if UNITY_EDITOR
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.color = UnityEditor.Handles.color.SetA(Settings.Draw.Transparency);
        UnityEditor.Handles.DrawWireCube(center, size);
#else
        throw new NotSupportedException("Only available in Editor version.");
#endif
      }

      public void DrawGL()
      {
#if UNITY_EDITOR
#else
        throw new NotSupportedException("Only available in Editor version.");
#endif
      }
    }

    private struct TextHandle : IHandleDraw
    {
      public Vector3 position;
      public string text;
      public Color color;

      public void Draw()
      {
#if UNITY_EDITOR
        if (UnityEditor.HandleUtility.WorldToGUIPointWithDepth(position).z >= 0.0)
        {
          guiContent ??= new GUIContent();

          guiContent.text = $"<color=#{color.ToHex()}{(byte)(Settings.Draw.Transparency * 255.0):X}>{text}</color>";

          Vector2 size = TextStyle.CalcSize(guiContent) * 0.5f;
          UnityEditor.Handles.BeginGUI();
          Rect screenPos = UnityEditor.HandleUtility.WorldPointToSizedRect(position, guiContent, TextStyle);
          screenPos.x -= size.x;
          screenPos.y -= size.y;
          GUI.Label(screenPos, guiContent, TextStyle);
          UnityEditor.Handles.EndGUI();
        }
#else
        throw new NotSupportedException("Only available in Editor version.");
#endif
      }

      public void DrawGL()
      {
#if UNITY_EDITOR
#else
        throw new NotSupportedException("Only available in Editor version.");
#endif
      }
    }
  }
}
