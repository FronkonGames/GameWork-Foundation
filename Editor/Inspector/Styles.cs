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
  public static class Styles
  {
    /// <summary>
    /// .
    /// </summary>
    public static Texture2D TransparentTexture
    {
      get
      {
        if (transparentTexture == null)
        {
          transparentTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false) { name = "Transparent Texture" };
          transparentTexture.SetPixel(0, 0, Color.clear);
          transparentTexture.Apply();
        }

        return transparentTexture;
      }
    }

    public static readonly GUIStyle MiniLabelButton;

    private static Texture2D transparentTexture;

    static Styles()
    {
      MiniLabelButton = new GUIStyle(EditorStyles.miniLabel)
      {
        normal = new GUIStyleState
        {
          background = TransparentTexture,
          scaledBackgrounds = null,
          textColor = Color.grey
        },
        active = new GUIStyleState
        {
          background = TransparentTexture,
          scaledBackgrounds = null,
          textColor = Color.white
        }
      };
      MiniLabelButton.onNormal = MiniLabelButton.active;
      MiniLabelButton.onActive = MiniLabelButton.active;
    }
  }
}
