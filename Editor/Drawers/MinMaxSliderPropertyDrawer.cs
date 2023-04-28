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
  /// <summary> Min-max slider drawer. </summary>
  [CustomPropertyDrawer(typeof(MinMaxSliderAttribute), true)]
  public sealed class MinMaxSliderPropertyDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      MinMaxSliderAttribute sliderAttribute = (MinMaxSliderAttribute)attribute;

      SerializedProperty minProperty = property;
      SerializedProperty maxProperty = property.GetEndProperty(true);

      if (minProperty.propertyType != maxProperty.propertyType)
      {
        Color original = GUI.color;
        GUI.color = Settings.Editor.ErrorColor;
        EditorGUI.LabelField(position, label.text, $"The types don't match in field '{property.propertyPath}'");
        GUI.color = original;
      }
      else if (minProperty.propertyType != SerializedPropertyType.Float &&
               minProperty.propertyType != SerializedPropertyType.Integer)
      {
        Color original = GUI.color;
        GUI.color = Settings.Editor.ErrorColor;
        EditorGUI.LabelField(position, label.text, $"Field '{property.propertyPath}' can only be applied to a float or int fields");
        GUI.color = original;
      }
      else
      {
        Rect rect = position;
        if (label != null)
        {
          rect = EditorGUI.PrefixLabel(position, label);

          float indent = EditorGUI.indentLevel * 15.0f;
          rect = new Rect(rect.x - indent, rect.y, rect.width + indent, rect.height); 
        }

        float width = rect.width - 20.0f;
        Rect minRect = new Rect(rect.x, rect.y, width * 0.15f, EditorGUIUtility.singleLineHeight);
        Rect sliderRect = new Rect(minRect.xMax, rect.y, width * 0.7f, EditorGUIUtility.singleLineHeight);
        Rect maxRect = new Rect(sliderRect.xMax, rect.y, width * 0.15f, EditorGUIUtility.singleLineHeight);
        Rect resetRect = new Rect(maxRect.xMax + 1.0f, rect.y, 18.0f, EditorGUIUtility.singleLineHeight);

        if (property.propertyType == SerializedPropertyType.Float)
        {
          float min = EditorGUI.FloatField(minRect, minProperty.floatValue);
          float max = EditorGUI.FloatField(maxRect, maxProperty.floatValue);

          EditorGUI.MinMaxSlider(sliderRect, ref min, ref max, sliderAttribute.min, sliderAttribute.max);

          min = min.Snap(sliderAttribute.snap);
          max = max.Snap(sliderAttribute.snap);

          minProperty.floatValue = Mathf.Clamp(min, sliderAttribute.min, max);
          maxProperty.floatValue = Mathf.Clamp(max, min, sliderAttribute.max);

          if (GUI.Button(resetRect, Styles.RefreshIcon, EditorStyles.iconButton) == true)
          {
            minProperty.floatValue = sliderAttribute.resetMin;
            maxProperty.floatValue = sliderAttribute.resetMax;
          }
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
          float minimum = EditorGUI.IntField(minRect, minProperty.intValue);
          float maximum = EditorGUI.IntField(maxRect, maxProperty.intValue);

          EditorGUI.MinMaxSlider(sliderRect, ref minimum, ref maximum, sliderAttribute.min, sliderAttribute.max);

          int min = Mathf.RoundToInt(minimum.Snap(sliderAttribute.snap));
          int max = Mathf.RoundToInt(maximum.Snap(sliderAttribute.snap));

          minProperty.intValue = Mathf.Clamp(min, Mathf.RoundToInt(sliderAttribute.min), max);
          maxProperty.intValue = Mathf.Clamp(max, min, Mathf.RoundToInt(sliderAttribute.max));

          if (GUI.Button(resetRect, Styles.RefreshIcon, EditorStyles.iconButton) == true)
          {
            minProperty.intValue = Mathf.RoundToInt(sliderAttribute.resetMin);
            maxProperty.intValue = Mathf.RoundToInt(sliderAttribute.resetMax);
          }
        }
      }
    }
  }
}
