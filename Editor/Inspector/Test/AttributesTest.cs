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
  public class AttributesTest : MonoBehaviour
  {
    public enum EnumTest
    {
      One, Two, Three, Four,
    }

    [SerializeField, Bool("A boolean value.")]
    private bool boolValue;

    [SerializeField, Int("A int value.")]
    private int intValue;

    [SerializeField, Int(0, 10, 0, "A int ranged value.")]
    private int intRangedValue;

    [SerializeField, Slider(0, 10, 0, "A int slider value.")]
    private int intSliderValue;

    [SerializeField, Float("A float value.")]
    private float floatValue;

    [SerializeField, Float(0.0f, 10.0f, 0.0f, "A float ranged value.")]
    private float floatRangedValue;

    [SerializeField, Slider(0.0f, 10.0f, 0.0f, "A float slider value.")]
    private float floatSliderValue;

    [SerializeField, Enum(0, "A enum value.")]
    private EnumTest enumValue;

    [SerializeField, Color(1.0f, 0.0f, 1.0f, 1.0f, "A color value.")]
    private Color colorValue;

    [SerializeField, Vector2("A Vector2 value.")]
    private Vector2 vector2Value;

    [SerializeField, Vector3("A Vector3 value.")]
    private Vector3 vector3Value;

    [SerializeField, ObjectReference(typeof(GameObject), true, "A GameObject value.")]
    private GameObject objectValue;

    [SerializeField, ObjectReference(typeof(GameObject), true, "A GameObject value."), NotNone]
    private GameObject requiredValue;
  }

  [CustomEditor(typeof(AttributesTest))]
  public class AttributesTestEditor : Inspector
  {
    protected override void InspectorGUI()
    {
      Header("Boolean");

      BoolField("boolValue", "A bool");

      Header("Int");

      IntField("intValue", "A int");
      IntField("intRangedValue", "A int");
      SliderField("intSliderValue", "A int");

      Header("Float");

      FloatField("floatValue", "A float");
      FloatField("floatRangedValue", "A float ranged");
      SliderField("floatSliderValue", "A float");

      Header("Enum");

      EnumField("enumValue", "A enum");

      Header("Color");

      ColorField("colorValue", "A Color");

      Header("Vectors");

      Vector2Field("vector2Value", "A Vector2");
      Vector3Field("vector3Value", "A Vector3");

      Header("Object");

      ObjectField("objectValue", "A GameObject");
      ObjectField("requiredValue", "A not null GameObject");

      Line();
    }
  }  
}
