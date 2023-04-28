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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Drawing of objects for development. </summary>
  /// <remarks>Only available in the Editor</remarks>
  [UnityEditor.InitializeOnLoad]
  public static partial class DebugDraw
  {
    private static readonly List<IHandleDraw> handles;

    private static GUIStyle TextStyle
    {
      get
      {
        if (textStyle == null)
          textStyle = new(UnityEditor.EditorStyles.whiteMiniLabel)
          {
            richText = true,
            fontSize = Settings.Draw.TextSize,
            alignment = TextAnchor.MiddleCenter
          };

        return textStyle;
      }
    }

    private static GUIStyle textStyle;
    private static GUIContent guiContent;

    private static int lastFrame;

    static DebugDraw()
    {
      handles = new(Settings.Draw.Capacity);

      UnityEditor.SceneView.duringSceneGui += (_) =>
      {
        using (new UnityEditor.Handles.DrawingScope())
        {
          for (int i = 0; i < handles.Count; ++i)
            handles[i].Draw();
        }

        CheckFrameChange();
      };
    }

    private static void CheckFrameChange()
    {
      int currentFrame = Time.frameCount;
      if (lastFrame != currentFrame)
      {
        handles.Clear();

        lastFrame = currentFrame;
      }
    }

    private static void DrawHandle(IHandleDraw handle)
    {
      CheckFrameChange();

      handles.Add(handle);
    }
  }
}
#endif
