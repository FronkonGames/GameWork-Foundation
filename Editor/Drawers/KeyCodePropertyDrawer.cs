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
  /// <summary> KeyCode drawer. </summary>
  [CustomPropertyDrawer(typeof(KeyCodeAttribute), true)]
  public sealed class KeyCodePropertyDrawer : PropertyDrawer
  {
    private bool detectKeyDown;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

      if (property.propertyType == SerializedPropertyType.Enum)
      {
        if (detectKeyDown == true)
        {
          position = EditorGUI.PrefixLabel(position, label);
          if (GUI.Button(position, "Press any key or this button to cancel"))
            detectKeyDown = false;
        }
        else
        {
          Rect basePosition = position;
          position.width -= 20.0f;
          property.intValue = (int)(KeyCode)EditorGUI.EnumPopup(position, label, (KeyCode)property.intValue);
          position.x += position.width;
          position.width = basePosition.width - position.width;

          if (GUI.Button(position, EditorGUIUtility.IconContent("d_Font Icon"), EditorStyles.iconButton) == true)
            detectKeyDown = true;
        }
      }
      else
      {
        Color original = GUI.color;
        GUI.color = Color.red;
        EditorGUI.LabelField(position, label.text, $"Field '{property.propertyPath}' can only be applied to a KeyCode fields");
        GUI.color = original;      
      }

      KeyCode key = (KeyCode)property.intValue;
      if (detectKeyDown == true)
      {
        Event e = Event.current;
        if (e.type != EventType.KeyDown)
          return;

        key = e.keyCode;
        detectKeyDown = false;        
      }

      property.intValue = (int)key;
    }
  }
}
