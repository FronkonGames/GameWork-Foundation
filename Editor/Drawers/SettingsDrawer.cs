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
using UnityEditor;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation.Editor
{
  /// <summary> Foundation settings drawer. </summary>
  public static class SettingsDrawer
  {
    [SettingsProvider]
    public static SettingsProvider CreateSettingsProvider() =>
      new("Preferences/Game:Work/", SettingsScope.User)
      {
        label = "Foundation",
        guiHandler = context =>
        {
          EditorGUI.BeginChangeCheck();

          EditorGUI.indentLevel++;
          
          EditorGUILayout.LabelField("Log", EditorStyles.boldLabel);
          {
            SettingColor("Info", Settings.LogInfoColor);
            SettingColor("Warning", Settings.LogWarningColor);
            SettingColor("Error", Settings.LogErrorColor);
          }
          
          EditorGUILayout.Separator();

          EditorGUILayout.LabelField("Draw", EditorStyles.boldLabel);
          {
            SettingSlider("Capacity", Settings.DebugDrawCapacity, 10, 10000);
            SettingSlider("Transparency", Settings.DebugDrawTransparency, 0.0f, 1.0f);
            SettingSlider("Point size", Settings.DebugDrawPointSize, 0.0f, 1.0f);
            SettingColor("Axis X", Settings.DebugDrawAxisXColor);
            SettingColor("Axis Y", Settings.DebugDrawAxisYColor);
            SettingColor("Axis Z", Settings.DebugDrawAxisZColor);
            SettingColor("Line", Settings.DebugDrawLineColor);
            EditorGUI.indentLevel++;
            SettingSlider("Gap size", Settings.DebugDrawLineGapSize, 0.0f, 10.0f);
            SettingSlider("Thickness", Settings.DebugDrawLineThickness, 0.0f, 2.0f);
            EditorGUI.indentLevel--;
            SettingColor("Arrow", Settings.DebugDrawArrowColor);
            EditorGUI.indentLevel++;
            SettingSlider("Tip size", Settings.DebugDrawArrowTipSize, 0.0f, 2.0f);
            SettingSlider("Width", Settings.DebugDrawArrowWidth, 0.0f, 2.0f);
            EditorGUI.indentLevel--;
            SettingColor("Ray", Settings.DebugDrawRayColor);
            EditorGUI.indentLevel++;
            SettingFloat("Length", Settings.DebugDrawRayLength);
            EditorGUI.indentLevel--;
            SettingColor("Hit", Settings.DebugDrawHitColor);
            EditorGUI.indentLevel++;
            SettingSlider("Radius", Settings.DebugDrawHitRadius, 0.0f, 2.0f);
            SettingSlider("Length", Settings.DebugDrawHitLength, 0.0f, 2.0f);
            EditorGUI.indentLevel--;
            SettingColor("Circle", Settings.DebugDrawCircleColor);
            SettingColor("Cube", Settings.DebugDrawCubeColor);
            SettingColor("Sphere", Settings.DebugDrawSphereColor);
            SettingColor("Arc", Settings.DebugDrawArcColor);
            SettingColor("Diamond", Settings.DebugDrawDiamondColor);
            EditorGUI.indentLevel++;
            SettingSlider("Size", Settings.DebugDrawDiamondSize, 0.0f, 2.0f);
            EditorGUI.indentLevel--;
            SettingColor("Cone", Settings.DebugDrawConeColor);
            SettingColor("Bounds", Settings.DebugDrawBoundsColor);
            SettingColor("Text", Settings.DebugDrawTextColor);
            EditorGUI.indentLevel++;
            SettingSlider("Size", Settings.DebugDrawTextSize, 1, 64);
            EditorGUI.indentLevel--;
            
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Changes in variables are automatically saved.", MessageType.Info);
            EditorGUILayout.Space();
          }

          EditorGUI.indentLevel--;
          
          if (EditorGUI.EndChangeCheck() == true)
          {
            SceneView.RepaintAll();
          }
        },
        keywords = new HashSet<string>(new[] {"Fronkon", "Fronkon Games", "Game:Work", "Foundation"})
      };

    private const string EditorPrefPrefix = "FronkonGames.GameWork.Foundation";
    
    private static readonly HashSet<string> SettingsInitialized = new();

    private static void SettingColor(string label, SettingColor setting)
    {
      if (SettingsInitialized.Contains(setting.Key) == false)
      {
        if (EditorPrefs.HasKey($"{EditorPrefPrefix}.{setting.Key}") == true)
          setting.Value = EditorPrefs.GetString($"{EditorPrefPrefix}.{setting.Key}").FromHex();

        SettingsInitialized.Add(setting.Key);
      }
      
      EditorGUILayout.BeginHorizontal();
      {
        Color value = EditorGUILayout.ColorField(label, setting.Value);
        if (value != setting.Value)
        {
          setting.Value = value;
          EditorPrefs.SetString($"{EditorPrefPrefix}.{setting.Key}", setting.Value.ToHex());
        }

        if (GUILayout.Button(EditorGUIUtility.IconContent("d_Refresh"), EditorStyles.iconButton) == true)
          setting.Reset();
      }
      EditorGUILayout.EndHorizontal();
    }
    
    private static void SettingSlider(string label, SettingInt setting, int min, int max)
    {
      if (SettingsInitialized.Contains(setting.Key) == false)
      {
        if (EditorPrefs.HasKey($"{EditorPrefPrefix}.{setting.Key}") == true)
          setting.Value = EditorPrefs.GetInt($"{EditorPrefPrefix}.{setting.Key}");

        SettingsInitialized.Add(setting.Key);
      }
      
      EditorGUILayout.BeginHorizontal();
      {
        int value = EditorGUILayout.IntSlider(label, setting.Value, min, max);
        if (value != setting.Value)
        {
          setting.Value = value;
          EditorPrefs.SetInt($"{EditorPrefPrefix}.{setting.Key}", setting.Value);
        }

        if (GUILayout.Button(EditorGUIUtility.IconContent("d_Refresh"), EditorStyles.iconButton) == true)
          setting.Reset();
      }
      EditorGUILayout.EndHorizontal();
    }
    
    private static void SettingSlider(string label, SettingFloat setting, float min, float max)
    {
      if (SettingsInitialized.Contains(setting.Key) == false)
      {
        if (EditorPrefs.HasKey($"{EditorPrefPrefix}.{setting.Key}") == true)
          setting.Value = EditorPrefs.GetFloat($"{EditorPrefPrefix}.{setting.Key}");

        SettingsInitialized.Add(setting.Key);
      }
      
      EditorGUILayout.BeginHorizontal();
      {
        float value = EditorGUILayout.Slider(label, setting.Value, min, max);
        if (value != setting.Value)
        {
          setting.Value = value;
          EditorPrefs.SetFloat($"{EditorPrefPrefix}.{setting.Key}", setting.Value);
        }

        if (GUILayout.Button(EditorGUIUtility.IconContent("d_Refresh"), EditorStyles.iconButton) == true)
          setting.Reset();
      }
      EditorGUILayout.EndHorizontal();
    }
    
    private static void SettingFloat(string label, SettingFloat setting)
    {
      if (SettingsInitialized.Contains(setting.Key) == false)
      {
        if (EditorPrefs.HasKey($"{EditorPrefPrefix}.{setting.Key}") == true)
          setting.Value = EditorPrefs.GetFloat($"{EditorPrefPrefix}.{setting.Key}");

        SettingsInitialized.Add(setting.Key);
      }
      
      EditorGUILayout.BeginHorizontal();
      {
        float value = EditorGUILayout.FloatField(label, setting.Value);
        if (value != setting.Value)
        {
          setting.Value = value;
          EditorPrefs.SetFloat($"{EditorPrefPrefix}.{setting.Key}", setting.Value);
        }

        if (GUILayout.Button(EditorGUIUtility.IconContent("d_Refresh"), EditorStyles.iconButton) == true)
          setting.Reset();
      }
      EditorGUILayout.EndHorizontal();
    }
  }
}
