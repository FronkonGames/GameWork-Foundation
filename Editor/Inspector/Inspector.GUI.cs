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
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// .
  /// </summary>
  public abstract partial class Inspector : Editor
  {
    /// <summary>
    /// Indent level.
    /// </summary>
    public static int IndentLevel
    {
      get => EditorGUI.indentLevel;
      set => EditorGUI.indentLevel = value;
    }

    /// <summary>
    /// Label width.
    /// </summary>
    public static float LabelWidth
    {
      get => EditorGUIUtility.labelWidth;
      set => EditorGUIUtility.labelWidth = value;
    }

    /// <summary>
    /// Field width.
    /// </summary>
    public static float FieldWidth
    {
      get => EditorGUIUtility.fieldWidth;
      set => EditorGUIUtility.fieldWidth = value;
    }

    /// <summary>
    /// GUI enabled?
    /// </summary>
    public static bool Enabled
    {
      get => GUI.enabled;
      set => GUI.enabled = value;
    }

    /// <summary>
    /// GUI changed?
    /// </summary>
    public static bool Changed
    {
      get => GUI.changed;
      set => GUI.changed = value;
    }

    /// <summary>
    /// Begin vertical.
    /// </summary>
    public static void BeginVertical() => EditorGUILayout.BeginVertical();

    /// <summary>
    /// End vertical.
    /// </summary>
    public static void EndVertical() => EditorGUILayout.EndVertical();

    /// <summary>
    /// Begin horizontal.
    /// </summary>
    public static void BeginHorizontal() => EditorGUILayout.BeginHorizontal();

    /// <summary>
    /// End horizontal.
    /// </summary>
    public static void EndHorizontal() => EditorGUILayout.EndHorizontal();

    /// <summary>
    /// Flexible space.
    /// </summary>
    public static void FlexibleSpace() => GUILayout.FlexibleSpace();

    /// <summary>
    /// Expand width.
    /// </summary>
    public static void ExpandWidth(bool expand = true) => GUILayout.ExpandWidth(expand);

    /// <summary>
    /// Expand width.
    /// </summary>
    public static void ExpandHeight(bool expand = true) => GUILayout.ExpandHeight(expand);

    /// <summary>
    /// Label.
    /// </summary>
    public static void Label(string label) => Label(EditorGUIUtility.TrTextContent(label));

    /// <summary>
    /// Label.
    /// </summary>
    public static void Label(GUIContent label) => EditorGUILayout.LabelField(label);

    /// <summary>
    /// Button.
    /// </summary>
    public static bool Button(string title, GUIStyle style = null) => Button(EditorGUIUtility.TrTextContent(title), style);

    /// <summary>
    /// Button.
    /// </summary>
    public static bool Button(GUIContent title, GUIStyle style = null) => GUILayout.Button(title, style ?? GUI.skin.button);

    /// <summary>
    /// Toggle with reset.
    /// </summary>
    public static bool Toggle(string title, bool value, bool resetValue) => Toggle(EditorGUIUtility.TrTextContent(title), value, resetValue);

    /// <summary>
    /// Toggle.
    /// </summary>
    public static bool Toggle(string title, bool value) => Toggle(EditorGUIUtility.TrTextContent(title), value);

    /// <summary>
    /// Toggle.
    /// </summary>
    public static bool Toggle(GUIContent title, bool value) => EditorGUILayout.Toggle(title, value);

    /// <summary>
    /// Line separator.
    /// </summary>
    public static void Line()
    {
      EditorGUILayout.Separator();

      GUILayout.Box(string.Empty, GUILayout.ExpandWidth(true), GUILayout.Height(1.0f));

      EditorGUILayout.Separator();
    }

    /// <summary>
    /// Separator.
    /// </summary>
    public static void Separator(float space = 0.0f)
    {
      if (space <= 0.0f)
        EditorGUILayout.Separator();
      else
        GUILayout.Space(space);
    }

    /// <summary>
    /// Draws a horizontal split line.
    /// </summary>
    public static void Splitter(bool isBoxed = false)
    {
      Rect rect = GUILayoutUtility.GetRect(1.0f, 1.0f);

      rect.xMin = 0.0f;
      rect.width += 4.0f;

      if (isBoxed == true)
      {
        rect.xMin = EditorGUIUtility.singleLineHeight - 2;
        rect.width -= 1;
      }

      if (UnityEngine.Event.current.type != EventType.Repaint)
        return;

      EditorGUI.DrawRect(rect, EditorGUIUtility.isProSkin == true ? new Color(0.12f, 0.12f, 0.12f, 1.333f) : new Color(0.6f, 0.6f, 0.6f, 1.333f));
    }

    /// <summary>
    /// Toggle with reset.
    /// </summary>
    public static bool Toggle(GUIContent title, bool value, bool resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.Toggle(title, value);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Enum popup with reset.
    /// </summary>
    public static Enum EnumPopup(string title, Enum selected, Enum resetValue) => EnumPopup(EditorGUIUtility.TrTextContent(title), selected, resetValue);

    /// <summary>
    /// Enum popup with reset.
    /// </summary>
    public static Enum EnumPopup(GUIContent title, Enum selected, Enum resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        selected = EditorGUILayout.EnumPopup(title, selected);

        if (ResetButton(resetValue) == true)
          selected = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return selected;
    }

    /// <summary>
    /// Slider with reset.
    /// </summary>
    public static float Slider(string title, float value, float minValue, float maxValue, float resetValue) =>
      Slider(EditorGUIUtility.TrTextContent(title), value, minValue, maxValue, resetValue);

    /// <summary>
    /// Slider with reset.
    /// </summary>
    public static float Slider(GUIContent title, float value, float minValue, float maxValue, float resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.Slider(title, value, minValue, maxValue);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Float field with reset.
    /// </summary>
    public static float Float(string title, float value, float resetValue) => Float(EditorGUIUtility.TrTextContent(title), value, resetValue);

    /// <summary>
    /// Float field with reset.
    /// </summary>
    public static float Float(GUIContent title, float value, float resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.FloatField(title, value);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Float field with reset.
    /// </summary>
    public static float Float(GUIContent title, float value, float min, float max, float resetValue)
    {
      return Float(title, Mathf.Clamp(value, min, max), resetValue);
    }

    /// <summary>
    /// Int field with reset.
    /// </summary>
    public static int IntSlider(string title, int value, int minValue, int maxValue, int resetValue) =>
      IntSlider(EditorGUIUtility.TrTextContent(title), value, minValue, maxValue, resetValue);

    /// <summary>
    /// Int field with reset.
    /// </summary>
    public static int IntSlider(GUIContent title, int value, int minValue, int maxValue, int resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.IntSlider(title, value, minValue, maxValue);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Int field with reset.
    /// </summary>
    public static int Int(string title, int value, int resetValue) => Int(EditorGUIUtility.TrTextContent(title), value, resetValue);

    /// <summary>
    /// Int field with reset.
    /// </summary>
    public static int Int(GUIContent title, int value, int resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.IntField(title, value);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Int popup field with reset.
    /// </summary>
    public static int IntPopup(string title, int value, string[] names, int[] values, int resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.IntPopup(title, value, names, values);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Int popup field with reset.
    /// </summary>
    public static int IntPopup(GUIContent title, int value, GUIContent[] names, int[] values, int resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.IntPopup(title, value, names, values);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Min-max slider with reset.
    /// </summary>
    public static void MinMaxSlider(string title, ref float minValue, ref float maxValue, float minLimit, float maxLimit, float defaultMinLimit, float defaultMaxLimit) =>
      MinMaxSlider(EditorGUIUtility.TrTextContent(title), ref minValue, ref maxValue, minLimit, maxLimit, defaultMinLimit, defaultMaxLimit);

    /// <summary>
    /// Min-max slider with reset.
    /// </summary>
    public static void MinMaxSlider(GUIContent title, ref float minValue, ref float maxValue, float minLimit, float maxLimit, float defaultMinLimit, float defaultMaxLimit)
    {
      EditorGUILayout.BeginHorizontal();
      {
        EditorGUILayout.MinMaxSlider(title, ref minValue, ref maxValue, minLimit, maxLimit);

        if (ResetButton() == true)
        {
          minValue = defaultMinLimit;
          maxValue = defaultMaxLimit;
        }
      }
      EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Color field with reset.
    /// </summary>
    public static Color Color(string title, Color value, Color resetValue) => Color(EditorGUIUtility.TrTextContent(title), value, resetValue);

    /// <summary>
    /// Color field with reset.
    /// </summary>
    public static Color Color(GUIContent title, Color value, Color resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.ColorField(title, value);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Animation curve.
    /// </summary>
    public static AnimationCurve Curve(string title, AnimationCurve value) => Curve(EditorGUIUtility.TrTextContent(title), value);

    /// <summary>
    /// Animation curve.
    /// </summary>
    public static AnimationCurve Curve(GUIContent title, AnimationCurve value)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.CurveField(title, value);

        if (ResetButton() == true)
          value = new AnimationCurve(new Keyframe(1.0f, 0.0f, 0.0f, 0.0f), new Keyframe(0.0f, 1.0f, 0.0f, 0.0f));
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Vector2 field with reset.
    /// </summary>
    public static Vector2 Vector2(string title, Vector2 value, Vector2 resetValue) => Vector2(EditorGUIUtility.TrTextContent(title), value, resetValue);

    /// <summary>
    /// Vector2 field with reset.
    /// </summary>
    public static Vector2 Vector2(GUIContent title, Vector2 value, Vector2 resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.Vector2Field(title, value);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Vector3 field with reset.
    /// </summary>
    public static Vector3 Vector3(string title, Vector3 value, Vector3 resetValue) => Vector3(EditorGUIUtility.TrTextContent(title), value, resetValue);

    /// <summary>
    /// Vector3 field with reset.
    /// </summary>
    public static Vector3 Vector3(GUIContent title, Vector3 value, Vector3 resetValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.Vector3Field(title, value);

        if (ResetButton(resetValue) == true)
          value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// Texture field.
    /// </summary>
    public static Texture Texture(string title, Texture value, bool allowSceneObjects) => Texture(EditorGUIUtility.TrTextContent(title), value, allowSceneObjects);

    /// <summary>
    /// Texture field.
    /// </summary>
    public static Texture Texture(GUIContent title, Texture value, bool allowSceneObjects)
    {
      return EditorGUILayout.ObjectField(title, value, typeof(Texture), allowSceneObjects) as Texture;
    }

    /// <summary>
    /// Layermask field with reset.
    /// </summary>
    public static LayerMask LayerMask(string title, LayerMask layerMask, int resetValue) => LayerMask(EditorGUIUtility.TrTextContent(title), layerMask, resetValue);

    /// <summary>
    /// Layermask field with reset.
    /// </summary>
    public static LayerMask LayerMask(GUIContent title, LayerMask layerMask, int resetValue)
    {
      List<string> layers = new List<string>();
      List<int> layerNumbers = new List<int>();

      for (int i = 0; i < 32; ++i)
      {
        string layerName = UnityEngine.LayerMask.LayerToName(i);
        if (string.IsNullOrEmpty(layerName) == false)
        {
          layers.Add(layerName);
          layerNumbers.Add(i);
        }
      }

      int maskWithoutEmpty = 0;
      for (int i = 0; i < layerNumbers.Count; ++i)
      {
        if (((1 << layerNumbers[i]) & layerMask.value) > 0)
          maskWithoutEmpty |= (1 << i);
      }

      EditorGUILayout.BeginHorizontal();
      {
        maskWithoutEmpty = EditorGUILayout.MaskField(title, maskWithoutEmpty, layers.ToArray());
        int mask = 0;
        for (int i = 0; i < layerNumbers.Count; ++i)
        {
          if ((maskWithoutEmpty & (1 << i)) > 0)
            mask |= (1 << layerNumbers[i]);
        }

        layerMask.value = mask;

        if (ResetButton(resetValue) == true)
          layerMask.value = resetValue;
      }
      EditorGUILayout.EndHorizontal();

      return layerMask;
    }

    /// <summary>
    /// Draws a UI box with a description and a "Fix Me" button next to it.
    /// </summary>
    /// <param name="text">The description</param>
    /// <param name="action">The action to execute when the button is clicked</param>
    public static void FixMeBox(string text, Action action)
    {
      EditorGUILayout.HelpBox(text, MessageType.Warning);

      GUILayout.Space(-32);
      using (new EditorGUILayout.HorizontalScope())
      {
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Fix", GUILayout.Width(60)) == true)
          action();

        GUILayout.Space(8);
      }
      GUILayout.Space(11);
    }

    /// <summary>
    /// .
    /// </summary>
    public static void Header(string title)
    {
      Separator();

      Rect rect = GUILayoutUtility.GetRect(1.0f, 17.0f);
      rect.xMin = 0.0f;
      rect.width += 4.0f;

      EditorGUI.DrawRect(rect, Styles.HeaderBackground);

      rect.xMin += 4.0f;

      EditorGUI.LabelField(rect, GetContent(title), EditorStyles.boldLabel);
    }

    /// <summary>
    /// .
    /// </summary>
    public static bool HeaderFoldout(string title, bool expanded)
    {
      Separator();

      Rect backgroundRect = GUILayoutUtility.GetRect(1.0f, 17.0f);
      backgroundRect.xMin = 0.0f;
      backgroundRect.width += 4.0f;

      float backgroundTint = EditorGUIUtility.isProSkin == true ? 0.1f : 1.0f;
      EditorGUI.DrawRect(backgroundRect, new Color(backgroundTint, backgroundTint, backgroundTint, 0.2f));

      // Title.
      Rect labelRect = backgroundRect;
      labelRect.xMin += 15.0f;
      labelRect.xMax -= 20.0f;

      EditorGUI.LabelField(labelRect, title, EditorStyles.boldLabel);

      // Foldout.
      Rect foldoutRect = backgroundRect;
      foldoutRect.y += 1.0f;
      foldoutRect.width = 130.0f;
      foldoutRect.height = 13.0f;

      expanded = GUI.Toggle(foldoutRect, expanded, GUIContent.none, EditorStyles.foldout);

      return expanded;
    }

    /// <summary>
    /// .
    /// </summary>
    public static bool HeaderActive(string title, bool active)
    {
      Separator();

      Rect backgroundRect = GUILayoutUtility.GetRect(1.0f, 17.0f);

      Rect labelRect = backgroundRect;
      labelRect.xMin += 32.0f;
      labelRect.xMax -= 20.0f;

      Rect foldoutRect = backgroundRect;
      foldoutRect.y += 1.0f;
      foldoutRect.width = 13.0f;
      foldoutRect.height = 13.0f;

      Rect toggleRect = backgroundRect;
      toggleRect.x += 16.0f;
      toggleRect.y += 2.0f;
      toggleRect.width = 13.0f;
      toggleRect.height = 13.0f;

      backgroundRect.xMin = 0.0f;
      backgroundRect.width += 4.0f;

      float backgroundTint = EditorGUIUtility.isProSkin == true ? 0.1f : 1.0f;
      EditorGUI.DrawRect(backgroundRect, new Color(backgroundTint, backgroundTint, backgroundTint, 0.2f));

      // Title.
      using (new EditorGUI.DisabledScope(!active))
        EditorGUI.LabelField(labelRect, title, EditorStyles.boldLabel);

      // Active checkbox.
      return GUI.Toggle(toggleRect, active, GUIContent.none, Styles.SmallTickBox);
    }

    /// <summary>
    /// .
    /// </summary>
    public static bool HeaderFoldoutActive(string title, bool expanded, ref bool active)
    {
      Separator();

      Rect backgroundRect = GUILayoutUtility.GetRect(1.0f, 17.0f);

      Rect labelRect = backgroundRect;
      labelRect.xMin += 32.0f;
      labelRect.xMax -= 20.0f;

      Rect foldoutRect = backgroundRect;
      foldoutRect.y += 1.0f;
      foldoutRect.width = 13.0f;
      foldoutRect.height = 13.0f;

      Rect toggleRect = backgroundRect;
      toggleRect.x += 16.0f;
      toggleRect.y += 2.0f;
      toggleRect.width = 13.0f;
      toggleRect.height = 13.0f;

      backgroundRect.xMin = 0.0f;
      backgroundRect.width += 4.0f;

      float backgroundTint = EditorGUIUtility.isProSkin == true ? 0.1f : 1.0f;
      EditorGUI.DrawRect(backgroundRect, new Color(backgroundTint, backgroundTint, backgroundTint, 0.2f));

      // Title.
      using (new EditorGUI.DisabledScope(!active))
        EditorGUI.LabelField(labelRect, title, EditorStyles.boldLabel);

      // Foldout.
      expanded = GUI.Toggle(foldoutRect, expanded, GUIContent.none, EditorStyles.foldout);

      // Active checkbox.
      active = GUI.Toggle(toggleRect, active, GUIContent.none, Styles.SmallTickBox);

      return expanded;
    }
  }
}
