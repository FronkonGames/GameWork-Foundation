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

/// <summary>
/// Attributes test.
/// </summary>
public sealed class AttributesDemo : MonoBehaviour
{
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

  [Title("Visibility")]

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
}
