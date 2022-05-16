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
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// SerializedObject extensions.
  /// </summary>
  public static class SerializedObjectExtensions
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static bool BoolField(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      BoolAttribute attribute = self.targetObject.GetAttribute<BoolAttribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          EditorGUILayout.PropertyField(property, Inspector.NewGUIContent(label, name, attribute.tooltip));

          if (Inspector.ResetButton() == true)
            property.boolValue = attribute.defaultValue;
        }
        EditorGUILayout.EndHorizontal();

        return property.boolValue;
      }

      return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static int IntField(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      IntAttribute attribute = self.targetObject.GetAttribute<IntAttribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          EditorGUILayout.PropertyField(property, Inspector.NewGUIContent(label, name, attribute.tooltip));

          if (Inspector.ResetButton() == true)
            property.intValue = attribute.defaultValue;

          if (attribute.min < attribute.max)
            property.intValue = property.intValue.Clamp(attribute.min, attribute.max);
        }
        EditorGUILayout.EndHorizontal();

        return property.intValue;
      }

      return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static float FloatField(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      FloatAttribute attribute = self.targetObject.GetAttribute<FloatAttribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          EditorGUILayout.PropertyField(property, Inspector.NewGUIContent(label, name, attribute.tooltip));

          if (Inspector.ResetButton() == true)
            property.floatValue = attribute.defaultValue;

          if (attribute.min < attribute.max)
            property.floatValue = property.floatValue.Clamp(attribute.min, attribute.max);
        }
        EditorGUILayout.EndHorizontal();

        return property.floatValue;
      }

      return 0.0f;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static float SliderField(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      SliderAttribute attribute = self.targetObject.GetAttribute<SliderAttribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          if (attribute.floatSlider == true)
          {
            property.floatValue = EditorGUILayout.Slider(Inspector.NewGUIContent(label, name, attribute.tooltip), property.floatValue, attribute.min, attribute.max);

            if (Inspector.ResetButton() == true)
              property.floatValue = attribute.defaultValue;
          }
          else
          {
            property.intValue = EditorGUILayout.IntSlider(Inspector.NewGUIContent(label, name, attribute.tooltip), property.intValue, (int)attribute.min, (int)attribute.max);

            if (Inspector.ResetButton() == true)
              property.intValue = (int)attribute.defaultValue;
          }
        }
        EditorGUILayout.EndHorizontal();

        return attribute.floatSlider == true ? property.floatValue : property.intValue;
      }

      return 0.0f;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static string StringField(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      StringAttribute attribute = self.targetObject.GetAttribute<StringAttribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          EditorGUILayout.PropertyField(property, Inspector.NewGUIContent(label, name, attribute.tooltip));

          if (Inspector.ResetButton() == true)
            property.stringValue = attribute.defaultValue;
        }
        EditorGUILayout.EndHorizontal();

        return property.stringValue;
      }

      return string.Empty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static int EnumField(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      EnumAttribute attribute = self.targetObject.GetAttribute<EnumAttribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          EditorGUILayout.PropertyField(property, Inspector.NewGUIContent(label, name, attribute.tooltip));

          if (Inspector.ResetButton() == true)
            property.enumValueIndex = attribute.defaultValue;
        }
        EditorGUILayout.EndHorizontal();

        return property.enumValueIndex;
      }

      return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static Color ColorField(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      ColorAttribute attribute = self.targetObject.GetAttribute<ColorAttribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          EditorGUILayout.PropertyField(property, Inspector.NewGUIContent(label, name, attribute.tooltip));

          if (Inspector.ResetButton() == true)
            property.colorValue = attribute.defaultValue;
        }
        EditorGUILayout.EndHorizontal();

        return property.colorValue;
      }

      return Color.black;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static Vector2 Vector2Field(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      Vector2Attribute attribute = self.targetObject.GetAttribute<Vector2Attribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          EditorGUILayout.PropertyField(property, Inspector.NewGUIContent(label, name, attribute.tooltip));

          if (Inspector.ResetButton() == true)
            property.vector2Value = attribute.defaultValue;
        }
        EditorGUILayout.EndHorizontal();

        return property.vector2Value;
      }

      return default(Vector2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static Vector3 Vector3Field(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      Vector3Attribute attribute = self.targetObject.GetAttribute<Vector3Attribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          EditorGUILayout.PropertyField(property, Inspector.NewGUIContent(label, name, attribute.tooltip));

          if (Inspector.ResetButton() == true)
            property.vector3Value = attribute.defaultValue;
        }
        EditorGUILayout.EndHorizontal();

        return property.vector3Value;
      }

      return default(Vector3);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static UnityEngine.Object ObjectReferenceField(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      ObjectReferenceAttribute attribute = self.targetObject.GetAttribute<ObjectReferenceAttribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          property.objectReferenceValue = EditorGUILayout.ObjectField(Inspector.NewGUIContent(label, name, attribute.tooltip),
                                                                      property.objectReferenceValue,
                                                                      attribute.type,
                                                                      attribute.allowSceneObjects);          
        }
        EditorGUILayout.EndHorizontal();

        return property.objectReferenceValue;
      }

      return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static int SceneField(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      SceneBuildIndexAttribute attribute = self.targetObject.GetAttribute<SceneBuildIndexAttribute>(name);
      if (property != null && attribute != null)
      {
        int count = SceneManager.sceneCountInBuildSettings;
        GUIContent[] displayedOptions = new GUIContent[count];
        for (int i = 0; i < count; ++i)
          displayedOptions[i] = new GUIContent(Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[i].path));

        property.intValue = EditorGUILayout.Popup(Inspector.NewGUIContent(label, name, attribute.tooltip), property.intValue, displayedOptions);

        return property.intValue;
      }

      return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static CanvasGroup CanvasGroupField(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      CanvasGroupAttribute attribute = self.targetObject.GetAttribute<CanvasGroupAttribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          EditorGUILayout.PropertyField(property, Inspector.NewGUIContent(label, name, attribute.tooltip));
        }
        EditorGUILayout.EndHorizontal();

        return property.objectReferenceValue as CanvasGroup;
      }

      return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static Image ImageField(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      ImageAttribute attribute = self.targetObject.GetAttribute<ImageAttribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          EditorGUILayout.PropertyField(property, Inspector.NewGUIContent(label, name, attribute.tooltip));
        }
        EditorGUILayout.EndHorizontal();

        return property.objectReferenceValue as Image;
      }

      return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static Text TextField(this SerializedObject self, string name, string label = "")
    {
      SerializedProperty property = self.FindProperty(name);
      TextAttribute attribute = self.targetObject.GetAttribute<TextAttribute>(name);
      if (property != null && attribute != null)
      {
        EditorGUILayout.BeginHorizontal();
        {
          EditorGUILayout.PropertyField(property, Inspector.NewGUIContent(label, name, attribute.tooltip));
        }
        EditorGUILayout.EndHorizontal();

        return property.objectReferenceValue as Text;
      }

      return null;
    }
  }
}
