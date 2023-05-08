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
    /// <summary> Vector2Int with reset. </summary>
    public Vector2Int Vector2Int(GUIContent label, Vector2Int value, Vector2Int reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));

        value = EditorGUILayout.Vector2IntField(string.Empty, value, GUILayout.ExpandWidth(true));

        if (ResetButton() == true)
          value = reset;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary> Vector2Int with reset. </summary>
    public Vector2Int Vector2Int(string label, Vector2Int value, Vector2Int reset = default) => Vector2Int(new GUIContent(label), value, reset);

    /// <summary> Vector2Int field with reset. </summary>
    public Vector2Int Vector2Int(string fieldName, Vector2Int reset = default)
    {
      Vector2Int value = default;
      FieldInfo fieldInfo = target.GetField(fieldName);
      if (fieldInfo != null)
      {
        GUIContent label = GetFieldLabel(fieldName, fieldInfo);

        if (fieldInfo.HasAttribute<MinMaxSliderAttribute>() == true)
        {
          MinMaxSliderAttribute attribute = fieldInfo.GetAttribute<MinMaxSliderAttribute>();
          value = MinMax(label, (Vector2Int)fieldInfo.GetValue(target), (int)attribute.min, (int)attribute.max, reset);
        }
        else
          value = Vector2Int(label, (Vector2Int)fieldInfo.GetValue(target), reset);

        fieldInfo.SetValue(target, value);
      }
      else
        Log.Warning($"Field '{fieldName}' not found");

      return value;
    }

    /// <summary> Vector2Int min/max slider with reset. </summary>
    public Vector2Int MinMax(GUIContent label, Vector2Int value, int minLimit, int maxLimit, Vector2Int reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        float min = value.x;
        float max = value.y;

        EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));

        min = EditorGUILayout.IntField((int)min, GUILayout.Width(Settings.Editor.MinMaxFieldWidth));

        EditorGUILayout.MinMaxSlider(ref min, ref max, minLimit, maxLimit, GUILayout.ExpandWidth(true));

        max = EditorGUILayout.IntField((int)max, GUILayout.Width(Settings.Editor.MinMaxFieldWidth));

        value.x = Mathf.RoundToInt(min);
        value.y = Mathf.RoundToInt(max);

        if (ResetButton() == true)
          value = reset == default ? new Vector2Int(minLimit, maxLimit) : reset;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }
  }
}
