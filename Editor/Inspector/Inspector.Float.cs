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
    /// <summary> Float with reset. </summary>
    public float Float(GUIContent label, float value, float reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.FloatField(label, value);

        if (ResetButton() == true)
          value = reset;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary> Float with reset. </summary>
    public float Float(string label, float value, float reset = default) => Float(new GUIContent(label), value, reset);

    /// <summary> Float field with reset. </summary>
    public float Float(string fieldName, float reset = default)
    {
      float value = default;
      FieldInfo fieldInfo = target.GetField(fieldName);
      if (fieldInfo != null)
      {
        GUIContent label = GetFieldLabel(fieldName, fieldInfo);

        if (fieldInfo.HasAttribute<RangeAttribute>() == true)
        {
          RangeAttribute attribute = fieldInfo.GetAttribute<RangeAttribute>();
          value = Slider(label, (float)fieldInfo.GetValue(target), attribute.min, attribute.max, reset);
        }
        else
          value = Float(label, (float)fieldInfo.GetValue(target), reset);

        fieldInfo.SetValue(target, value);
      }
      else
        Log.Warning($"Field '{fieldName}' not found");

      return value;
    }

    /// <summary> Float slider with reset. </summary>
    public float Slider(GUIContent label, float value, float minValue, float maxValue, float reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.Slider(label, value, minValue, maxValue);

        if (ResetButton() == true)
          value = reset;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }
  }
}
