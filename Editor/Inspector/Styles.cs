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
  /// <summary> Styles for the Editor. </summary>
  public static class Styles
  {
    public static Color Splitter => EditorGUIUtility.isProSkin == true ? SplitterDark : SplitterLight;

    public static Color HeaderBackground => EditorGUIUtility.isProSkin == true ? HeaderBackgroundDark : HeaderBackgroundLight;

    /// <summary> 1x1 white texture. </summary>
    public static Texture2D WhiteTexture
    {
      get
      {
        if (whiteTexture == null)
        {
          whiteTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false) { name = "White Texture" };
          whiteTexture.SetPixel(0, 0, Color.white);
          whiteTexture.Apply();
        }

        return whiteTexture;
      }
    }

    /// <summary> 1x1 black texture. </summary>
    public static Texture2D BlackTexture
    {
      get
      {
        if (blackTexture == null)
        {
          blackTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false) { name = "Black Texture" };
          blackTexture.SetPixel(0, 0, Color.black);
          blackTexture.Apply();
        }

        return blackTexture;
      }
    }

    /// <summary> 1x1 transparent texture. </summary>
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

    /// <summary> Pane options texture. </summary>
    public static Texture2D PaneOptionsIcon
    {
      get
      {
        if (paneOptionsIcon == null)
          paneOptionsIcon = (Texture2D)EditorGUIUtility.Load(EditorGUIUtility.isProSkin == true
            ? "Builtin Skins/DarkSkin/Images/pane options.png"
            : "Builtin Skins/LightSkin/Images/pane options.png");

        return paneOptionsIcon;
      }
    }

    public static readonly GUIStyle SmallTickBox;
    public static readonly GUIStyle MiniLabelButton;
    public static readonly GUIStyle MiniLabel;

    public static readonly Color NormalColor;
    public static readonly Color BackgroundColorEnabled;
    public static readonly Color BackgroundColorDisabled;
    public static readonly Color SelectedFocusedColor;
    public static readonly Color SelectedUnfocusedColor;

    private static readonly Color SplitterDark;
    private static readonly Color SplitterLight;

    private static readonly Color HeaderBackgroundDark;
    private static readonly Color HeaderBackgroundLight;

    private static Texture2D whiteTexture;
    private static Texture2D blackTexture;
    private static Texture2D transparentTexture;

    private static Texture2D paneOptionsIcon;

    static Styles()
    {
      if (EditorGUIUtility.isProSkin == true)
      {
        BackgroundColorEnabled = new Color32(155, 155, 155, 255);
        BackgroundColorDisabled = new Color32(155, 155, 155, 100);
        NormalColor = new Color32(56, 56, 56, 255);
        SelectedFocusedColor = new Color32(62, 95, 150, 255);
        SelectedUnfocusedColor = new Color32(72, 72, 72, 255);
      }
      else
      {
        BackgroundColorEnabled = new Color32(65, 65, 65, 255);
        BackgroundColorDisabled = new Color32(65, 65, 65, 120);
        NormalColor = new Color32(194, 194, 194, 255);
        SelectedFocusedColor = new Color32(62, 125, 231, 255);
        SelectedUnfocusedColor = new Color32(143, 143, 143, 255);
      }

      SmallTickBox = new GUIStyle("ShurikenToggle");

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

      MiniLabel = new GUIStyle(EditorStyles.miniLabel)
      {
        normal = new GUIStyleState
        {
          background = TransparentTexture,
          scaledBackgrounds = null,
          textColor = Color.grey
        }
      };

      SplitterDark = new Color(0.12f, 0.12f, 0.12f, 1.333f);
      SplitterLight = new Color(0.6f, 0.6f, 0.6f, 1.333f);

      HeaderBackgroundDark = new Color(0.05f, 0.05f, 0.05f, 0.2f);
      HeaderBackgroundLight = new Color(1.0f, 1.0f, 1.0f, 0.2f);
    }
  }
}
