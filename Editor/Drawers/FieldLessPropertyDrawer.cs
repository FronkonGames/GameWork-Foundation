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
  /// <summary> Int/float drawer. </summary>
  [CustomPropertyDrawer(typeof(FieldLessAttribute), true)]
  public sealed class FieldLessAttributeDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      FieldLessAttribute fieldAttribute = (FieldLessAttribute)attribute;

      if (property.propertyType == SerializedPropertyType.Float)
      {
        Rect rectSlider = position;
        rectSlider.xMax -= 18.0f;
        float value = EditorGUI.FloatField(rectSlider, label, property.floatValue);
        if (value < fieldAttribute.less)
          property.floatValue = value;

        Rect rectReset = position;
        rectReset.xMin = rectSlider.xMax + 1.0f;
        if (GUI.Button(rectReset, EditorGUIUtility.IconContent("d_Refresh"), EditorStyles.iconButton) == true)
          property.floatValue = fieldAttribute.reset;
      }
      else if (property.propertyType == SerializedPropertyType.Integer)
      {
        Rect rectSlider = position;
        rectSlider.xMax -= 18.0f;
        int value = EditorGUI.IntField(rectSlider, label, property.intValue);
        if (value < fieldAttribute.less)
          property.intValue = value;

        Rect rectReset = position;
        rectReset.xMin = rectSlider.xMax + 1.0f;
        if (GUI.Button(rectReset, EditorGUIUtility.IconContent("d_Refresh"), EditorStyles.iconButton) == true)
          property.intValue = Mathf.RoundToInt(fieldAttribute.reset);
      }
    }
  }
}
