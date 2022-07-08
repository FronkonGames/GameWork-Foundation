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

namespace FronkonGames.GameWork.Foundation
{
  public class DrawersTest : MonoBehaviour
  {
    [Title("Edit only in Play/Editor modes")]

    [SerializeField, OnlyEnableInEdit, Indent]
    private string onlyEditInEditor;

    [SerializeField, OnlyEnableInPlay, Indent]
    private string onlyEditInPlay;

    [Title("Show/Hide Enable/Disable Ifs")]

    [SerializeField, NotEditable, Indent]
    private string notEditableText;

    [SerializeField, Indent]
    private bool enable;

    [SerializeField, EnableIf(nameof(enable)), Indent(2)]
    private string enableText;

    [SerializeField, Indent]
    private bool disable;

    [SerializeField, EnableIf(nameof(disable)), Indent(2)]
    private string disableText;

    [SerializeField, Indent]
    private bool show;

    [SerializeField, ShowIf(nameof(show)), Indent(2)]
    private string showText;

    [SerializeField, Indent]
    private bool hide;

    [SerializeField, HideIf(nameof(hide)), Indent(2)]
    private string hideText;

    [Title("Required")]
    
    [SerializeField, NotNull, Indent]
    private GameObject cantBeNull;
    
    [Title("Scenes")]

    [SerializeField, Scene, Indent]
    private int scene;

    [Title("Files")]

    [SerializeField, Folder, Indent]
    private string relativeFolder;

    [SerializeField, Folder(false), Indent]
    private string absoluteFolder;
    
    [SerializeField, File, Indent]
    private string relativeFile;
    
    [SerializeField, File(false), Indent]
    private string absoluteFile;
  }
}
