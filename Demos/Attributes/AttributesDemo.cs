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
#pragma warning disable CS0414 

  [MessageBox("This inspector is an example of all the extra Game:Work Foundation attributes", MessageBoxAttribute.MessageType.Info)]
  [Space]

  [Title("Variables")]

  [Indent, Label("Int field"), Field(50), SerializeField]
  private int intField;

  [Indent, Label("Int less than 0"), FieldLess(0, -1), SerializeField]
  private int intLess = -1;

  [Indent, Label("Int less equal than 0"), FieldLessEqual(0, 0), SerializeField]
  private int intLessEqual;
  
  [Indent, Label("Int greater than 0"), FieldGreat(0, 10), SerializeField]
  private int intGreater;

  [Indent, Label("Int greater equal than 10"), FieldGreat(10, 10), SerializeField]
  private int intGreaterEqual;
  
  [Indent, Label("Int"), Slider(0, 10, 10), SerializeField]
  private int intSlider;

  [Indent, Label("Int snap 10"), Slider(0, 100, 50, 10), SerializeField]
  private int intSnap;

  [Indent, Label("Ints min/max"), MinMaxSlider(0, 100, 0, 100), SerializeField]
  private int intMin = 0;

  [HideInInspector, SerializeField]
  private int intMax = 100;
  
  [Indent, Label("Float field"), Field(1.0f), SerializeField]
  private float floatField;

  [Indent, Label("Float less than 0"), FieldLess(0.0f, -1.0f), SerializeField]
  private float floatLess = 1.0f;

  [Indent, Label("Float less equal than 0"), FieldLessEqual(0.0f, 0.0f), SerializeField]
  private float floatLessEqual;

  [Indent, Label("Float greater than 0"), FieldGreat(0.0f, 1.0f), SerializeField]
  private float floatGreater;

  [Indent, Label("Float greater equal than 0"), FieldGreatEqual(0.0f, 0.0f), SerializeField]
  private float floatGreaterEqual;

  [Indent, Label("Float"), Slider(0.0f, 1.0f, 1.0f), SerializeField]
  private float floatSlider;

  [Indent, Label("Float snap 0.5"), Slider(0.0f, 1.0f, 0.5f, 0.1f), SerializeField]
  private float floatSnap;

  [Indent, Label("Floats min/max"), MinMaxSlider(0.0f, 1.0f), SerializeField]
  private float floatMin = 0.0f;

  [HideInInspector, SerializeField]
  private float floatMax = 1.0f;

  [Indent, KeyCode, SerializeField]
  private KeyCode keyCode = KeyCode.Space;

  [Title("Text")]

  [Indent, Label("Nice name"), SerializeField]
  private string badName;

  [Indent, Password, SerializeField]
  private string password;

  [Title("Files")]

  [Indent, Scene, SerializeField]
  private int sceneIndex;

  [Indent, File, SerializeField]
  private string filePath;

  [Indent, Folder, SerializeField]
  private string folderPath;

  [Title("Editable")]

  [Indent, NotEditable, SerializeField]
  private string notEditable;

  [Indent, OnlyEditableInEditor, SerializeField]
  private string editableInEdit;

  [Indent, OnlyEditableInPlay, SerializeField]
  private string editableInPlay;

  [Title("Toggle")]

  [Indent, SerializeField]
  private bool toggle;

  [Indent, EnableIf(nameof(toggle)), SerializeField]
  private string enableIf;

  [Indent, DisableIf(nameof(toggle)), SerializeField]
  private string disableIf;

  [Indent, ShowIf(nameof(toggle)), SerializeField]
  private string showIf;

  [Indent, HideIf(nameof(toggle)), SerializeField]
  private string hideIf;

  [Title("Check")]

  [Indent, NotNull, SerializeField]
  private GameObject cantBeNull;

  [Title("Misc")]

  [Indent, NotEditable, SerializeField]
  private int counter;

  [Button(nameof(Increase)), SerializeField]
  private string buttonInc;

  [Button(nameof(Reset)), SerializeField]
  private string buttonReset;

  private void Increase() => counter++;

  private void Reset() => counter = 0;

#pragma warning restore CS0414  
}
