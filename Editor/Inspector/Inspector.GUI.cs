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
using System.Reflection;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Custom inspector. </summary>
  public abstract partial class Inspector : Editor
  {
    /// <summary> Indent level. </summary>
    public static int IndentLevel
    {
      get { return EditorGUI.indentLevel; }
      set { EditorGUI.indentLevel = value; }
    }

    /// <summary> Label width. </summary>
    public static float LabelWidth
    {
      get { return EditorGUIUtility.labelWidth; }
      set { EditorGUIUtility.labelWidth = value; }
    }

    /// <summary> Field width. </summary>
    public static float FieldWidth
    {
      get { return EditorGUIUtility.fieldWidth; }
      set { EditorGUIUtility.fieldWidth = value; }
    }

    /// <summary> GUI enabled? </summary>
    public static bool EnableGUI
    {
      get { return GUI.enabled; }
      set { GUI.enabled = value; }
    }

    /// <summary> GUI changed? </summary>
    public static bool Changed
    {
      get { return GUI.changed; }
      set { GUI.changed = value; }
    }

    /// <summary> Separator. </summary>
    public void Separator() => EditorGUILayout.Separator();

    /// <summary> Space. </summary>
    public void Space(float space = Settings.Editor.SpaceSeparation) => GUILayout.Space(space);

    /// <summary> Flexible space. </summary>
    public void FlexibleSpace() => GUILayout.FlexibleSpace();

    /// <summary> Line separator. </summary>
    public void Line()
    {
      EditorGUILayout.Separator();

      GUILayout.Box(string.Empty, GUILayout.ExpandWidth(true), GUILayout.Height(1.0f));
    }

    /// <summary> Begin vertical. </summary>
    public void BeginVertical() => EditorGUILayout.BeginVertical();

    /// <summary> End vertical. </summary>
    public void EndVertical() => EditorGUILayout.EndVertical();

    /// <summary> Begin horizontal. </summary>
    public void BeginHorizontal() => EditorGUILayout.BeginHorizontal();

    /// <summary> End horizontal. </summary>
    public void EndHorizontal() => EditorGUILayout.EndHorizontal();

    /// <summary> Reset some GUI variables. </summary>
    public void ResetGUI(int indentLevel = 0, float labelWidth = 0.0f, float fieldWidth = 0.0f, bool guiEnabled = true)
    {
      EditorGUI.indentLevel = indentLevel;
      EditorGUIUtility.labelWidth = labelWidth;
      EditorGUIUtility.fieldWidth = fieldWidth;
      GUI.enabled = guiEnabled;
    }

    /// <summary> Marks as dirty. </summary>
    public void DirtyGUI() => EditorUtility.SetDirty(target);

    /// <summary> Nice foldout. </summary>
    public bool Foldout(string title)
    {
      bool display = GetFoldoutDisplay(title);

      Rect rect = GUILayoutUtility.GetRect(16.0f, 22.0f, Styles.Header);
      GUI.Box(rect, title, Styles.Header);

      Rect toggleRect = new(rect.x + 4.0f, rect.y + 2.0f, 13.0f, 13.0f);
      if (Event.current.type == EventType.Repaint)
        EditorStyles.foldout.Draw(toggleRect, false, false, display, false);

      Event e = Event.current;
      if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition) == true)
      {
        display = !display;
        e.Use();
      }

      SetFoldoutDisplay(title, display);

      return display;
    }

    /// <summary> Label. </summary>
    public void Label(string label, string tooltip = default) => EditorGUILayout.LabelField(new GUIContent(label, tooltip));

    /// <summary> Button. </summary>
    public bool Button(string label, string tooltip = default, GUIStyle style = null) => GUILayout.Button(new GUIContent(label, tooltip), style ?? GUI.skin.button);

    /// <summary> Button. </summary>
    public bool Button(string label, params GUILayoutOption[] options) => GUILayout.Button(label, GUI.skin.button, options);

    /// <summary> Reset button. </summary>
    public bool ResetButton() => GUILayout.Button(Styles.RefreshIcon, EditorStyles.iconButton);

    /// <summary> Button with confirmation. </summary>
    public bool ConfirmationButton(string buttonText, Color buttonColor, string dialogTitle, string dialogMessage)
    {
      bool confirmation = false;

      GUI.color = buttonColor;

      if (GUILayout.Button(buttonText) == true)
        confirmation = EditorUtility.DisplayDialog(dialogTitle, dialogMessage, "OK", "Cancel");

      GUI.color = UnityEngine.Color.white;

      return confirmation;
    }

    /// <summary> Title. </summary>
    public void Title(string label)
    {
      GUILayout.Label(label, EditorStyles.boldLabel);

      GUILayout.Box(string.Empty, GUILayout.ExpandWidth(true), GUILayout.Height(Settings.Editor.TitleLineHeight));
      EditorGUI.DrawRect(GUILayoutUtility.GetLastRect(), UnityEngine.Color.gray);
    }

    private bool GetFoldoutDisplay(string foldoutName)
    {
      string key = string.Format("{0}.display{1}", productID, foldoutName);
      bool value = true;

      if (foldoutDisplay.ContainsKey(key) == false)
      {
        value = PlayerPrefs.GetInt(key, 0) == 1;

        foldoutDisplay.Add(key, value);
      }
      else
        value = foldoutDisplay[key];

      return value;
    }

    private void SetFoldoutDisplay(string foldoutName, bool value)
    {
      string key = string.Format("{0}.display{1}", productID, foldoutName);

      if (foldoutDisplay.ContainsKey(key) == false)
        foldoutDisplay.Add(key, value);
      else
        foldoutDisplay[key] = value;

      PlayerPrefs.SetInt(key, value == true ? 1 : 0);
    }

    private static GUIContent GetFieldLabel(string fieldName, FieldInfo fieldInfo)
    {
      GUIContent label;
      if (fieldInfo.HasAttribute<LabelAttribute>() == true)
      {
        LabelAttribute attribute = fieldInfo.GetAttribute<LabelAttribute>();

        label = new GUIContent(attribute.label, attribute.tooltip);
      }
      else
        label = new(fieldName.ToWords());

      return label;
    }
  }
}
