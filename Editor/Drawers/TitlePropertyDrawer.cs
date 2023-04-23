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
  /// <summary> Nice title drawer. </summary>
  [CustomPropertyDrawer(typeof(TitleAttribute), true)]
  public sealed class TitlePropertyDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      TitleAttribute titleAttribute = (TitleAttribute)attribute;

      Rect rectTitle = new(position)
      {
        y = position.y + Settings.Editor.TitleSpaceBeforeTitle,
        height = EditorGUIUtility.singleLineHeight
      };
      GUI.Label(rectTitle, titleAttribute.label, EditorStyles.boldLabel);

      Rect rectLine = new(position)
      {
        y = rectTitle.yMax + Settings.Editor.TitleSpaceBeforeLine,
        height = Settings.Editor.TitleLineHeight
      };
      EditorGUI.DrawRect(rectLine, Color.gray);

      Rect rectContent = new(position)
      {
        yMin = rectLine.yMax + Settings.Editor.TitleSpaceBeforeContent
      };
      label = EditorGUI.BeginProperty(rectContent, label, property);
      EditorGUI.PropertyField(rectContent, property, label, true);
      EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
      EditorGUI.GetPropertyHeight(property, label) +
      Settings.Editor.TitleSpaceBeforeTitle +
      EditorGUIUtility.singleLineHeight +
      Settings.Editor.TitleSpaceBeforeLine +
      Settings.Editor.TitleLineHeight +
      Settings.Editor.TitleSpaceBeforeContent;
  }
}
