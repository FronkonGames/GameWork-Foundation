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
using FronkonGames.GameWork.Foundation;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// .
  /// </summary>
  [CustomPropertyDrawer(typeof(HideIfAttribute), true)]
  public sealed class HideIfPropertyDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      HideIfAttribute hideIf = (HideIfAttribute)attribute;
      if (NeedsToHide(hideIf, property) == false)
      {
        label = EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(position, property, label, true);
        EditorGUI.EndProperty();
      }
    }
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      HideIfAttribute hideIf = (HideIfAttribute)attribute;

      return NeedsToHide(hideIf, property) == true ? -EditorGUIUtility.standardVerticalSpacing :
                                                     EditorGUI.GetPropertyHeight(property, label);
    }

    private bool NeedsToHide(HideIfAttribute hideIf, SerializedProperty property)
    {
      bool enabled = true;
      string propertyPath = property.propertyPath;
      string conditionPath = propertyPath.Replace(property.name, hideIf.conditional);
      SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);
 
      if (sourcePropertyValue != null)
        enabled = sourcePropertyValue.boolValue;
      else
        Log.Warning($"Condition '{hideIf.conditional}' not found.");
 
      return enabled;
    }
  }
}
