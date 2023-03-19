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
using FronkonGames.GameWork.Foundation;

/// <summary> Attributes test. </summary>
public sealed class AttributesDemo : MonoBehaviour
{
  [MessageBox("This inspector is an example of all the extra Game:Work Foundation attributes", MessageBoxAttribute.MessageType.Info)]
  [Space]

  [Title("Variables")]

  [Indent, Label("Int field"), Field(50)]
  public int intField;

  [Indent, Label("Int less than 0"), FieldLess(0, -1)]
  public int intLess = -1;

  [Indent, Label("Int less equal than 0"), FieldLessEqual(0, 0)]
  public int intLessEqual;
  
  [Indent, Label("Int greater than 0"), FieldGreat(0, 10)]
  public int intGreater;

  [Indent, Label("Int greater equal than 10"), FieldGreat(10, 10)]
  public int intGreaterEqual;
  
  [Indent, Label("Int"), Slider(0, 10, 10)]
  public int intSlider;

  [Indent, Label("Int snap 10"), Slider(0, 100, 50, 10)]
  public int intSnap;

  [Indent, Label("Ints min/max"), MinMaxSlider(0, 100, 0, 100)]
  public int intMin = 0;

  [HideInInspector]
  public int intMax = 100;
  
  [Indent, Label("Float field"), Field(1.0f)]
  public float floatField;

  [Indent, Label("Float less than 0"), FieldLess(0.0f, -1.0f)]
  public float floatLess = 1.0f;

  [Indent, Label("Float less equal than 0"), FieldLessEqual(0.0f, 0.0f)]
  public float floatLessEqual;
  
  [Indent, Label("Float greater than 0"), FieldGreat(0.0f, 1.0f)]
  public float floatGreater;

  [Indent, Label("Float greater equal than 0"), FieldGreatEqual(0.0f, 0.0f)]
  public float floatGreaterEqual;
  
  [Indent, Label("Float"), Slider(0.0f, 1.0f, 1.0f)]
  public float floatSlider;

  [Indent, Label("Float snap 0.5"), Slider(0.0f, 1.0f, 0.5f, 0.1f)]
  public float floatSnap;
  
  [Indent, Label("Floats min/max"), MinMaxSlider(0.0f, 1.0f)]
  public float floatMin = 0.0f;

  [HideInInspector]
  public float floatMax = 1.0f;
  
  [Title("Text")]
  
  [Indent, Label("Nice name")]
  public string badName;

  [Indent, Password]
  public string password;
  
  [Title("Files")]

  [Indent, Scene]
  public int sceneIndex;

  [Indent, File]
  public string filePath;

  [Indent, Folder]
  public string folderPath;
  
  [Title("Editable")]
  
  [Indent, NotEditable]
  public string notEditable;
  
  [Indent, OnlyEditableInEditor]
  public string editableInEdit;
  
  [Indent, OnlyEditableInPlay]
  public string editableInPlay;

  [Title("Toogle")]

  [Indent]
  public bool toggle;

  [Indent, EnableIf(nameof(toggle))]
  public string enableIf;

  [Indent, DisableIf(nameof(toggle))]
  public string disableIf;

  [Indent, ShowIf(nameof(toggle))]
  public string showIf;

  [Indent, HideIf(nameof(toggle))]
  public string hideIf;

  [Title("Check")]
  
  [Indent, NotNull]
  public GameObject cantBeNull;
  
  [Title("Misc")]
  
  [Indent, NotEditable]
  public int counter;
  
  [Button(nameof(Increase))]
  public string buttonInc;

  [Button(nameof(Reset))]
  public string buttonReset;

  public void Increase() => counter++;
  
  public void Reset() => counter = 0;
}
