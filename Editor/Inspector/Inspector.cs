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
    public override void OnInspectorGUI()
    {
      Reset();

      serializedObject.Update();

      InspectorGUI();

      serializedObject.ApplyModifiedProperties();

      if (Changed == true)
        SetDirty(this.target);
    }

    /// <summary>
    /// 
    /// </summary>
    protected abstract void InspectorGUI();

    protected virtual void OnEnable()
    {
      productID = GetType().ToString().Replace("Editor", string.Empty);
    }

    public static GUIContent NewGUIContent(string label, string name, string tooltip) =>
      new GUIContent(string.IsNullOrEmpty(label) == false ? label : name.FromCamelCase(), tooltip);

    private static void Reset(int indentLevel = 0, float labelWidth = 0.0f, float fieldWidth = 0.0f, bool guiEnabled = true)
    {
      EditorGUI.indentLevel = 0;
      EditorGUIUtility.labelWidth = 0.0f;
      EditorGUIUtility.fieldWidth = 0.0f;
      GUI.enabled = true;
    }

    private static GUIContent GetContent(string textAndTooltip)
    {
      if (string.IsNullOrEmpty(textAndTooltip))
        return GUIContent.none;

      GUIContent content;

      if (GUIContentCache.TryGetValue(textAndTooltip, out content) == false)
      {
        string[] s = textAndTooltip.Split('|');
        content = new GUIContent(s[0]);

        if (s.Length > 1 && !string.IsNullOrEmpty(s[1]))
          content.tooltip = s[1];

        GUIContentCache.Add(textAndTooltip, content);
      }

      return content;
    }

    /// <summary>
    /// Creates a texture for use in the Editor.
    /// </summary>
    public static Texture2D MakeTexture(int width, int height, Color color)
    {
      Color[] pixels = new Color[width * height];

      for (int i = 0; i < pixels.Length; ++i)
        pixels[i] = color;

      Texture2D result = new Texture2D(width, height);
      result.SetPixels(pixels);
      result.Apply();

      return result;
    }

    private static void SetDirty(UnityEngine.Object target) => EditorUtility.SetDirty(target);

    private static readonly Dictionary<string, GUIContent> GUIContentCache = new Dictionary<string, GUIContent>();
    private static Dictionary<string, bool> statesCache = new Dictionary<string, bool>();

    private string productID;
  }
}
