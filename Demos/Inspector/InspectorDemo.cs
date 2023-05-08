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
#if UNITY_EDITOR
using UnityEditor;
#endif
using FronkonGames.GameWork.Foundation;

/// <summary> Inspector test. </summary>
public sealed class InspectorDemo : MonoBehaviour
{
  [SerializeField]
  private bool toggle;

  [SerializeField, Label("EnumPopup")]
  private MessageType messageType;

  [SerializeField, Label("Int Value")]
  private int intValue;

  [SerializeField, Range(0, 10), Label("Int Range")]
  private int intRange;

  [SerializeField, Label("Int Popup")]
  private int intPopup;

  [SerializeField, Label("Float Value")]
  private float floatValue;

  [SerializeField, Range(0.0f, 10.0f), Label("Float Range")]
  private float floatRange;

  [SerializeField, Label("One line text")]
  private string text;

  [SerializeField, Label("Vector2 Int")]
  private Vector2Int vector2IntValue;

  [SerializeField, MinMaxSlider(0, 10), Label("Min/Max Int")]
  private Vector2Int vector2IntRange = new(2, 8);

  [SerializeField, Label("Vector2")]
  private Vector2 vector2Value;

  [SerializeField, Label("Vector2 Multi")]
  private Vector2 vector2Multi;

  [SerializeField, MinMaxSlider(0.0f, 10.0f), Label("Min/Max")]
  private Vector2 vector2Range = new(2.0f, 8.0f);

  [SerializeField, Label("Vector3")]
  private Vector3 vector3Value;

  [SerializeField, Label("Vector3 Multi")]
  private Vector3 vector3Multi;

  [SerializeField, Label("Vector4")]
  private Vector4 vector4Value;

  [SerializeField, Label("Vector4 Multi")]
  private Vector4 vector4Multi;

  [SerializeField, Label("Color")]
  private Color colorValue;

  [SerializeField, ColorUsage(true, true), Label("Color HDR")]
  private Color colorHDRValue;

  [SerializeField, Label("Texture")]
  private Texture textureValue;

  [SerializeField, Label("Texture Multi")]
  private Texture textureMulti;

  [SerializeField, Label("Object")]
  private Object objectValue;
}

#if UNITY_EDITOR
/// <summary> Inspector test. </summary>
[CustomEditor(typeof(InspectorDemo))]
public sealed class InspectorDemoEditor : Inspector
{
  protected override void InspectorGUI()
  {
    Title("This is an example of Custom Inspector");

    Toggle("toggle");

    Separator();

    EnumPopup("messageType");
    Int("intValue");
    Int("intRange");
    IntPopup("intPopup", new[] { "Klaatu", "Barada", "Nictu" },
                         new[] { 0, 1, 2 });
    Separator();

    Float("floatValue");
    Float("floatRange");

    Separator();

    String("text");

    Separator();

    Vector2Int("vector2IntValue");
    Vector2Int("vector2IntRange");

    Separator();

    Vector2("vector2Value");
    Vector2("vector2Multi", "A", "B");
    Vector2("vector2Range");

    Vector3("vector3Value");
    Vector3("vector3Multi", "A", "B", "C");

    Vector4("vector4Value");
    Vector4("vector4Multi", "A", "B", "C", "D");

    Color("colorValue");
    Color("colorHDRValue");

    Texture("textureValue");
    Texture("textureMulti", false, true);

    Object<Object>("objectValue");

    Separator();

    using (new HorizontalGroup("box"))
    {
      Button("Button A");
      Button("Button B");
      Button("Button C");
    }
  }
}
#endif
