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
    /// <summary> Vector2 with reset. </summary>
    public Vector2 Vector2(GUIContent label, Vector2 value, Vector2 reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));

        value = EditorGUILayout.Vector2Field(string.Empty, value, GUILayout.ExpandWidth(true));

        if (ResetButton() == true)
          value = reset;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary> Vector2 multiline with reset. </summary>
    public Vector2 Vector2(GUIContent label, Vector2 value, GUIContent labelX, GUIContent labelY, Vector2 reset = default)
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

      return value;
    }

    /// <summary> Vector2 with reset. </summary>
    public Vector2 Vector2(string label, Vector2 value, Vector2 reset = default) => Vector2(new GUIContent(label), value, reset);

    /// <summary> Vector2 field with reset. </summary>
    public Vector2 Vector2(string fieldName, Vector2 reset = default)
    {
      Vector2 value = default;
      FieldInfo fieldInfo = target.GetField(fieldName);
      if (fieldInfo != null)
      {
        GUIContent label = GetFieldLabel(fieldName, fieldInfo);

        if (fieldInfo.HasAttribute<MinMaxSliderAttribute>() == true)
        {
          MinMaxSliderAttribute attribute = fieldInfo.GetAttribute<MinMaxSliderAttribute>();
          value = MinMax(label, (Vector2)fieldInfo.GetValue(target), attribute.min, attribute.max, reset);
        }
        else
          value = Vector2(label, (Vector2)fieldInfo.GetValue(target), reset);

        fieldInfo.SetValue(target, value);
      }
      else
        Log.Warning($"Field '{fieldName}' not found");

      return value;
    }

    /// <summary> Vector2 field multi with reset. </summary>
    public Vector2 Vector2(string fieldName, string labelX, string labelY, Vector2 reset = default)
    {
      Vector2 value = default;
      FieldInfo fieldInfo = target.GetField(fieldName);
      if (fieldInfo != null)
      {
        GUIContent label = GetFieldLabel(fieldName, fieldInfo);

        value = Vector2(label, (Vector2)fieldInfo.GetValue(target), new GUIContent(labelX), new GUIContent(labelY), reset);

        fieldInfo.SetValue(target, value);
      }
      else
        Log.Warning($"Field '{fieldName}' not found");

      return value;
    }

    /// <summary> Vector2 min/max slider with reset. </summary>
    public Vector2 MinMax(GUIContent label, Vector2 value, float minLimit, float maxLimit, Vector2 reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        float min = value.x;
        float max = value.y;

        EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));

        min = EditorGUILayout.FloatField(min, GUILayout.Width(Settings.Editor.MinMaxFieldWidth));

        EditorGUILayout.MinMaxSlider(ref min, ref max, minLimit, maxLimit, GUILayout.ExpandWidth(true));

        max = EditorGUILayout.FloatField(max, GUILayout.Width(Settings.Editor.MinMaxFieldWidth));

        value.x = min;
        value.y = max;

        if (ResetButton() == true)
          value = reset == default ? new Vector2(minLimit, maxLimit) : reset;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary> Vector2 min/max slider with reset. </summary>
    public Vector2 MinMax(string label, Vector2 value, float minValue, float maxValue, Vector2 reset = default) => MinMax(new GUIContent(label), value, minValue, maxValue, reset);
  }
}
