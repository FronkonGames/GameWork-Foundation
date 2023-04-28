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
  /// <summary> Password drawer. </summary>
  [CustomPropertyDrawer(typeof(PasswordAttribute), true)]
  public sealed class PasswordPropertyDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      if (property.propertyType == SerializedPropertyType.String)
      {
        PasswordAttribute passwordAttribute = (PasswordAttribute)attribute;
        string password = property.stringValue;

        if (property.stringValue.Length > passwordAttribute.maxLength)
          password = password[..passwordAttribute.maxLength];

        Color color = GUI.color;

        GUI.color = IsValid(password) ? Color.white : Settings.Editor.ErrorColor;

        property.stringValue = EditorGUI.PasswordField(position, label, password);

        GUI.color = color;
      }
      else
      {
        Color original = GUI.color;
        GUI.color = Settings.Editor.ErrorColor;
        EditorGUI.LabelField(position, label.text, $"Field '{property.propertyPath}' can only be applied to a string fields");
        GUI.color = original;
      }
    }

    private bool IsValid(string password) => password.Length >= ((PasswordAttribute)attribute).minLength &&
                                             password.Length <= ((PasswordAttribute)attribute).maxLength;
  }
}
