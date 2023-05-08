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
    /// <summary> Int with reset. </summary>
    public int Int(GUIContent label, int value, int reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.IntField(label, value);

        if (ResetButton() == true)
          value = reset;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary> Int field with reset. </summary>
    public int Int(string fieldName, int reset = default)
    {
      int value = default;
      FieldInfo fieldInfo = target.GetField(fieldName);
      if (fieldInfo != null)
      {
        GUIContent label = GetFieldLabel(fieldName, fieldInfo);

        if (fieldInfo.HasAttribute<RangeAttribute>() == true)
        {
          RangeAttribute attribute = fieldInfo.GetAttribute<RangeAttribute>();
          value = Slider(label, (int)fieldInfo.GetValue(target), (int)attribute.min, (int)attribute.max, reset);
        }
        else
          value = Int(label, (int)fieldInfo.GetValue(target), reset);

        fieldInfo.SetValue(target, value);
      }
      else
        Log.Warning($"Field '{fieldName}' not found");

      return value;
    }

    /// <summary> Int slider with reset. </summary>
    public int Slider(GUIContent label, int value, int minValue, int maxValue, int reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.IntSlider(label, value, minValue, maxValue);

        if (ResetButton() == true)
          value = reset;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary> Int popup with reset. </summary>
    public int IntPopup(GUIContent label, int value, GUIContent[] options, int[] optionsValues, int reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.IntPopup(label, value, options, optionsValues);

        if (ResetButton() == true)
          value = reset;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary> Int popup with reset. </summary>
    public int IntPopup(string label, int value, string[] options, int[] optionsValues, int reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.IntPopup(label, value, options, optionsValues);

        if (ResetButton() == true)
          value = reset;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary> Int popup field with reset. </summary>
    public int IntPopup(string fieldName, string[] options, int[] optionsValues, int reset = default)
    {
      int value = default;
      FieldInfo fieldInfo = target.GetField(fieldName);
      if (fieldInfo != null)
      {
        GUIContent label = GetFieldLabel(fieldName, fieldInfo);

        value = IntPopup(label.text, (int)fieldInfo.GetValue(target), options, optionsValues, reset);

        fieldInfo.SetValue(target, value);
      }
      else
        Log.Warning($"Field '{fieldName}' not found");

      return value;
    }
  }
}
