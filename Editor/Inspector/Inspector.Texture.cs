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
    /// <summary> Texture. </summary>
    public Texture Texture(GUIContent label, Texture value, bool allowSceneTextures = false, bool multiLine = false)
    {
      if (multiLine == true)
        value = EditorGUILayout.ObjectField(label, value, typeof(Texture), allowSceneTextures) as Texture;
      else
        value = EditorGUILayout.ObjectField(label, value, typeof(Texture), allowSceneTextures, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture;

      return value;
    }

    /// <summary> Texture field. </summary>
    public Texture Texture(string fieldName, bool allowSceneTextures = false, bool multiLine = false)
    {
      Texture value = default;
      FieldInfo fieldInfo = target.GetField(fieldName);
      if (fieldInfo != null)
      {
        GUIContent label = GetFieldLabel(fieldName, fieldInfo);

        value = Texture(label, (Texture)fieldInfo.GetValue(target), allowSceneTextures, multiLine);

        fieldInfo.SetValue(target, value);
      }
      else
        Log.Warning($"Field '{fieldName}' not found");

      return value;
    }
  }
}
