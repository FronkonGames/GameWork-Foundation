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
  /// <summary> Slider drawer. </summary>
  [CustomPropertyDrawer(typeof(SliderAttribute), true)]
  public sealed class SliderPropertyDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      SliderAttribute sliderAttribute = (SliderAttribute)attribute;

      if (property.propertyType == SerializedPropertyType.Float)
      {
        Rect rectSlider = position;
        rectSlider.xMax -= 18.0f;
        float value = EditorGUI.Slider(rectSlider, label, property.floatValue, sliderAttribute.min, sliderAttribute.max);
        value = value.Snap(sliderAttribute.snap);
        property.floatValue = Mathf.Clamp(value, sliderAttribute.min, sliderAttribute.max);

        Rect rectReset = position;
        rectReset.xMin = rectSlider.xMax + 1.0f;
        if (GUI.Button(rectReset, Styles.RefreshIcon, EditorStyles.iconButton) == true)
          property.floatValue = sliderAttribute.reset;
      }
      else if (property.propertyType == SerializedPropertyType.Integer)
      {
        Rect rectSlider = position;
        rectSlider.xMax -= 18.0f;
        int value = EditorGUI.IntSlider(rectSlider, label, property.intValue, Mathf.RoundToInt(sliderAttribute.min), Mathf.RoundToInt(sliderAttribute.max));
        value = value.Snap(Mathf.RoundToInt(sliderAttribute.snap));
        property.intValue = Mathf.Clamp(value, Mathf.RoundToInt(sliderAttribute.min), Mathf.RoundToInt(sliderAttribute.max));

        Rect rectReset = position;
        rectReset.xMin = rectSlider.xMax + 1.0f;
        if (GUI.Button(rectReset, Styles.RefreshIcon, EditorStyles.iconButton) == true)
          property.intValue = Mathf.RoundToInt(sliderAttribute.reset);
      }
      else
      {
        Color original = GUI.color;
        GUI.color = Settings.Editor.ErrorColor;
        EditorGUI.LabelField(position, label.text, $"Field '{property.propertyPath}' can only be applied to a float or int fields");
        GUI.color = original;
      }
    }
  }
}
