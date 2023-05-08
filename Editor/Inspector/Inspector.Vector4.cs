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
using UnityEditor;
using System.Reflection;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Custom inspector. </summary>
  public abstract partial class Inspector : Editor
  {
    /// <summary> Vector4 with reset. </summary>
    public Vector4 Vector4(GUIContent label, Vector4 value, Vector4 reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));

        value = EditorGUILayout.Vector4Field(string.Empty, value, GUILayout.ExpandWidth(true));

        if (ResetButton() == true)
          value = reset;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary> Vector4 with reset. </summary>
    public Vector4 Vector4(string label, Vector4 value, Vector4 reset = default) => Vector4(new GUIContent(label), value, reset);

    /// <summary> Vector4 field with reset. </summary>
    public Vector4 Vector4(string fieldName, Vector4 reset = default)
    {
      Vector4 value = default;
      FieldInfo fieldInfo = target.GetField(fieldName);
      if (fieldInfo != null)
      {
        GUIContent label = GetFieldLabel(fieldName, fieldInfo);

        value = Vector4(label, (Vector4)fieldInfo.GetValue(target), reset);

        fieldInfo.SetValue(target, value);
      }
      else
        Log.Warning($"Field '{fieldName}' not found");

      return value;
    }

    /// <summary> Vector4 field multi with reset. </summary>
    public Vector4 Vector4(string fieldName, string labelX, string labelY, string labelZ, string labelW, Vector4 reset = default)
    {
      Vector4 value = default;
      FieldInfo fieldInfo = target.GetField(fieldName);
      if (fieldInfo != null)
      {
        GUIContent label = GetFieldLabel(fieldName, fieldInfo);

        value = Vector4(label, (Vector4)fieldInfo.GetValue(target), new GUIContent(labelX), new GUIContent(labelY), new GUIContent(labelZ), new GUIContent(labelW), reset);

        fieldInfo.SetValue(target, value);
      }
      else
        Log.Warning($"Field '{fieldName}' not found");

      return value;
    }

    /// <summary> Vector4 multiline with reset. </summary>
    public Vector4 Vector4(GUIContent label, Vector4 value, GUIContent labelX, GUIContent labelY, GUIContent labelZ, GUIContent labelW, Vector4 reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));

        Rect rect = GUILayoutUtility.GetLastRect();
        rect.xMin += rect.width - GUI.skin.label.CalcSize(labelX).x;

        EditorGUI.LabelField(rect, labelX);

        value.x = EditorGUILayout.FloatField(value.x);

        if (ResetButton() == true)
          value.x = reset.x;
      }
      EditorGUILayout.EndHorizontal();

      EditorGUILayout.BeginHorizontal();
      {
        EditorGUILayout.LabelField(string.Empty, GUILayout.Width(LabelWidth));

        Rect rect = GUILayoutUtility.GetLastRect();
        rect.xMin += rect.width - GUI.skin.label.CalcSize(labelY).x;

        EditorGUI.LabelField(rect, labelY);

        value.y = EditorGUILayout.FloatField(value.y);

        if (ResetButton() == true)
          value.y = reset.y;
      }
      EditorGUILayout.EndHorizontal();

      EditorGUILayout.BeginHorizontal();
      {
        EditorGUILayout.LabelField(string.Empty, GUILayout.Width(LabelWidth));

        Rect rect = GUILayoutUtility.GetLastRect();
        rect.xMin += rect.width - GUI.skin.label.CalcSize(labelZ).x;

        EditorGUI.LabelField(rect, labelZ);

        value.z = EditorGUILayout.FloatField(value.z);

        if (ResetButton() == true)
          value.z = reset.z;
      }
      EditorGUILayout.EndHorizontal();

      EditorGUILayout.BeginHorizontal();
      {
        EditorGUILayout.LabelField(string.Empty, GUILayout.Width(LabelWidth));

        Rect rect = GUILayoutUtility.GetLastRect();
        rect.xMin += rect.width - GUI.skin.label.CalcSize(labelW).x;

        EditorGUI.LabelField(rect, labelW);

        value.w = EditorGUILayout.FloatField(value.w);

        if (ResetButton() == true)
          value.w = reset.w;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }
  }
}
