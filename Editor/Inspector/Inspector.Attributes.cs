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
  /// <summary>
  /// .
  /// </summary>
  public abstract partial class Inspector : Editor
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="valueName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    protected bool BoolField(string valueName, string label = "") => serializedObject.BoolField(valueName, label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="valueName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    protected int IntField(string valueName, string label = "") => serializedObject.IntField(valueName, label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="valueName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    protected float FloatField(string valueName, string label = "") => serializedObject.FloatField(valueName, label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="valueName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    protected float SliderField(string valueName, string label = "") => serializedObject.SliderField(valueName, label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="valueName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    protected float EnumField(string valueName, string label = "") => serializedObject.EnumField(valueName, label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="valueName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    protected Color ColorField(string valueName, string label = "") => serializedObject.ColorField(valueName, label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="valueName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    protected Vector2 Vector2Field(string valueName, string label = "") => serializedObject.Vector2Field(valueName, label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="valueName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    protected Vector3 Vector3Field(string valueName, string label = "") => serializedObject.Vector3Field(valueName, label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="valueName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    protected UnityEngine.Object ObjectField(string valueName, string label = "") => serializedObject.ObjectReferenceField(valueName, label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="valueName"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    protected int SceneField(string valueName, string label = "") => serializedObject.SceneField(valueName, label);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resetValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool ResetButton<T>(T resetValue) => GUILayout.Button(new GUIContent("R", $"Reset to '{resetValue}'."), Styles.MiniLabelButton, GUILayout.Width(10.0f), GUILayout.Height(14.0f));

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static bool ResetButton() => GUILayout.Button("R", Styles.MiniLabelButton, GUILayout.Width(18.0f));
  }
}
