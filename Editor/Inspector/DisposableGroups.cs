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
using UnityEditor;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Horizontal scope group. </summary>
  public class HorizontalGroup : GUI.Scope
  {
    private Rect Rect { get; }

    private readonly bool disabled;

    public HorizontalGroup() : this(false, GUIStyle.none, Array.Empty<GUILayoutOption>()) { }

    public HorizontalGroup(GUIStyle style) : this(false, style, Array.Empty<GUILayoutOption>()) { }

    public HorizontalGroup(params GUILayoutOption[] options) : this(false, GUIStyle.none, options) { }

    public HorizontalGroup(bool disabled, params GUILayoutOption[] options) : this(disabled, GUIStyle.none, options) { }

    public HorizontalGroup(bool disabled, GUIStyle style, params GUILayoutOption[] options)
    {
      this.disabled = disabled;
      Rect = EditorGUILayout.BeginHorizontal(style, options);

      if (this.disabled == true)
        EditorGUI.BeginDisabledGroup(disabled);
    }

    protected override void CloseScope()
    {
      if (disabled == true)
        EditorGUI.EndDisabledGroup();

      EditorGUILayout.EndHorizontal();
    }
  }

  /// <summary> Vertical scope group. </summary>
  public class VerticalGroup : GUI.Scope
  {
    private Rect rect;

    private readonly bool disabled;

    public VerticalGroup() : this(false, GUIStyle.none, Array.Empty<GUILayoutOption>()) { }

    public VerticalGroup(GUIStyle style) : this(false, style, Array.Empty<GUILayoutOption>()) { }

    public VerticalGroup(params GUILayoutOption[] options) : this(false, GUIStyle.none, options) { }

    public VerticalGroup(bool disabled, params GUILayoutOption[] options) : this(disabled, GUIStyle.none, options) { }

    public VerticalGroup(bool disabled, GUIStyle style, params GUILayoutOption[] options)
    {
      this.disabled = disabled;
      rect = EditorGUILayout.BeginVertical(style, options);

      if (this.disabled == true)
        EditorGUI.BeginDisabledGroup(disabled);
    }

    protected override void CloseScope()
    {
      if (disabled == true)
        EditorGUI.EndDisabledGroup();

      EditorGUILayout.EndVertical();
    }
  }

  /// <summary> </summary>
  public class IndentGroup : IDisposable
  {
    private readonly int level;

    public IndentGroup(int increment = 1)
    {
      level = EditorGUI.indentLevel;
      EditorGUI.indentLevel += increment;
    }

    public void Dispose() => EditorGUI.indentLevel = level;
  }
}
