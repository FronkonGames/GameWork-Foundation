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
  public static partial class DebugDraw
  {
    private readonly struct RenderText
    {
      public readonly GUIContent content;
      public readonly Rect textArea;

      private RenderText(Vector3 position, string text, Color color, Vector2 offset = default)
      {
#if UNITY_EDITOR
        content = new($"<color=#{color.ToHex()}{(int)(Transparency * 255.0f):X}>{text}</color>");
        textArea = UnityEditor.HandleUtility.WorldPointToSizedRect(position, content, TextStyle);
        textArea.x += 1.0f;
        textArea.position += offset;
#endif
      }

      public static void Add(Vector3 position, string text, Color? color, Vector2 offset = default)
      {
#if UNITY_EDITOR
        if (UnityEditor.HandleUtility.WorldToGUIPointWithDepth(position).z >= 0.0f)
          jobsText.Add(new RenderText(position, text, color ?? TextColor, offset));
#endif
      }
    }
  }
}
