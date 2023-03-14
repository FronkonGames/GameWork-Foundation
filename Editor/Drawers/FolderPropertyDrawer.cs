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
using FronkonGames.GameWork.Foundation;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Folder button drawer. </summary>
  [CustomPropertyDrawer(typeof(FolderAttribute), true)]
  public sealed class FolderPropertyDrawer : PropertyDrawer
  {
    private readonly Texture2D folderButtonTexture;
    private const float buttonWidth = 20.0f;
    private const float padding = 4.0f;

    public FolderPropertyDrawer()
    {
      folderButtonTexture = EditorGUIUtility.FindTexture("Project");
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      FolderAttribute folderAttribute = (FolderAttribute)attribute;
      if (property.propertyType == SerializedPropertyType.String)
      {
        position.width -= buttonWidth + padding;
        
        label = EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(position, property, label, true);
       
        position.x += position.width + padding;
        position.width = buttonWidth;

        if (GUI.Button(position, folderButtonTexture, GUIStyle.none) == true)
          SetFolderPath(property, folderAttribute);
        
        EditorGUI.EndProperty();
      }
      else
        EditorGUI.PropertyField(position, property, label);
    }
    
    private static void SetFolderPath(SerializedProperty property, FolderAttribute folderAttribute)
    {
      string path = property.stringValue;
      path = EditorUtility.OpenFolderPanel("Select Folder",folderAttribute.relativeToProject == true ? path.ToAbsolutePath() : path, String.Empty);

      if (string.IsNullOrEmpty(path) == false)
        property.stringValue = folderAttribute.relativeToProject == true ? path.ToRelativePath() : path;

      property.stringValue = path;
      property.serializedObject.ApplyModifiedProperties();
      GUIUtility.ExitGUI();      
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
      Mathf.Max(base.GetPropertyHeight(property, label), folderButtonTexture.height);
  }
}
