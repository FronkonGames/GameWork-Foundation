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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> File button drawer. </summary>
  [CustomPropertyDrawer(typeof(FileAttribute), true)]
  public sealed class FilePropertyDrawer : PropertyDrawer
  {
    private readonly Texture2D folderButtonTexture;

    public FilePropertyDrawer()
    {
      folderButtonTexture = EditorGUIUtility.FindTexture("Project");
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      FileAttribute fileAttribute = (FileAttribute)attribute;
      if (property.propertyType == SerializedPropertyType.String)
      {
        position.width -= Settings.Editor.FileButtonWidth + Settings.Editor.FileButtonPadding;
        
        label = EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(position, property, label, true);
       
        position.x += position.width + Settings.Editor.FileButtonPadding;
        position.width = Settings.Editor.FileButtonWidth;

        if (GUI.Button(position, folderButtonTexture, GUIStyle.none) == true)
          SetFilePath(property, fileAttribute);
        
        EditorGUI.EndProperty();
      }
      else
      {
        Color original = GUI.color;
        GUI.color = Settings.Editor.ErrorColor;
        EditorGUI.LabelField(position, label.text, $"Field '{property.propertyPath}' can only be applied to a string fields");
        GUI.color = original;      
      }
    }

    private static void SetFilePath(SerializedProperty property, FileAttribute fileAttribute)
    {
      string path = property.stringValue;
      path = EditorUtility.OpenFilePanel("Select file", fileAttribute.relativeToProject == true ? path.ToAbsolutePath() : path, string.Empty);
      
      if (string.IsNullOrEmpty(path) == false)
        path = fileAttribute.relativeToProject == true ? path.ToRelativePath() : path;

      property.stringValue = path;
      property.serializedObject.ApplyModifiedProperties();
      GUIUtility.ExitGUI();      
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
      Mathf.Max(base.GetPropertyHeight(property, label), folderButtonTexture.height);
  }
}