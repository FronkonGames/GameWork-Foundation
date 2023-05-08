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
    /// <summary> Color with reset. </summary>
    public Color Color(GUIContent label, Color value, bool showAlpha = false, bool hdr = false, Color reset = default)
    {
      EditorGUILayout.BeginHorizontal();
      {
        EditorGUILayout.LabelField(label, GUILayout.Width(LabelWidth));

        value = EditorGUILayout.ColorField(new GUIContent(), value, true, showAlpha, hdr, GUILayout.ExpandWidth(true));

        if (ResetButton() == true)
          value = reset;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary> Color with reset. </summary>
    public Color Color(string label, Color value, Color reset = default) => Color(new GUIContent(label), value, false, false, reset);

    /// <summary> Color field with reset. </summary>
    public Color Color(string fieldName, Color reset = default)
    {
      Color value = default;
      FieldInfo fieldInfo = target.GetField(fieldName);
      if (fieldInfo != null)
      {
        GUIContent label = GetFieldLabel(fieldName, fieldInfo);

        if (fieldInfo.HasAttribute<ColorUsageAttribute>() == true)
        {
          ColorUsageAttribute attribute = fieldInfo.GetAttribute<ColorUsageAttribute>();
          value = Color(label, (Color)fieldInfo.GetValue(target), attribute.showAlpha, attribute.hdr, reset);
        }
        else
          value = Color(label, (Color)fieldInfo.GetValue(target), false, false, reset);

        fieldInfo.SetValue(target, value);
      }
      else
        Log.Warning($"Field '{fieldName}' not found");

      return value;
    }
  }
}
