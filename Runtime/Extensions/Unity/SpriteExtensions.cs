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
using System.Collections.Generic;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Sprite extensions. </summary>
  public static class SpriteExtensions
  {
    /// <summary> Returns the sprite's rect as RectInt, optionally subtracting border. </summary>
    /// <param name="sprite">Source sprite.</param>
    /// <param name="subtractBorder">If true, subtracts the sprite border from the rect.</param>
    /// <returns>RectInt representing the sprite rect.</returns>
    public static RectInt GetRect(this Sprite sprite, bool subtractBorder = false)
    {
      Rect rect = sprite.rect;

      if (subtractBorder)
      {
        Vector4 border = sprite.border;
        rect.x += border.x;
        rect.y += border.w;
        rect.width -= border.x + border.z;
        rect.height -= border.y + border.w;
      }

      return new RectInt(
        Mathf.RoundToInt(rect.x),
        Mathf.RoundToInt(rect.y),
        Mathf.RoundToInt(rect.width),
        Mathf.RoundToInt(rect.height));
    }

    /// <summary> Returns the sprite's pixels as Color32 array. </summary>
    /// <param name="sprite">Source sprite.</param>
    /// <param name="clearColor">Color assigned to pixels outside the texture bounds.</param>
    /// <returns>Array of Color32 pixels from the sprite texture.</returns>
    public static Color32[] GetPixels32(this Sprite sprite, Color32 clearColor = default)
    {
      RectInt rect = GetRect(sprite);
      Texture2D texture = sprite.texture;
      int texWidth = texture.width;
      int texHeight = texture.height;

      Color32[] texturePixels = texture.GetPixels32();
      Color32[] result = new Color32[rect.width * rect.height];

      for (int y = 0; y < rect.height; y++)
      {
        for (int x = 0; x < rect.width; x++)
        {
          int texX = rect.x + x;
          int texY = rect.y + y;

          if (texX >= 0 && texX < texWidth && texY >= 0 && texY < texHeight)
          {
            result[y * rect.width + x] = texturePixels[texY * texWidth + texX];
          }
          else
          {
            result[y * rect.width + x] = clearColor;
          }
        }
      }

      return result;
    }

    /// <summary> Returns pixels with their world positions. </summary>
    /// <param name="sprite">Source sprite.</param>
    /// <returns>Enumerable of (position, color) pairs for each pixel.</returns>
    public static IEnumerable<(Vector2 position, Color32 color)> GetPixelsWithPosition(this Sprite sprite)
    {
      RectInt rect = GetRect(sprite);
      Texture2D texture = sprite.texture;
      int texWidth = texture.width;
      int texHeight = texture.height;

      Color32[] texturePixels = texture.GetPixels32();

      Vector2 pivot = sprite.pivot;
      Vector2 localOffset = new Vector2(rect.x, rect.y) + pivot;

      for (int y = 0; y < rect.height; y++)
      {
        for (int x = 0; x < rect.width; x++)
        {
          int texX = rect.x + x;
          int texY = rect.y + y;

          if (texX >= 0 && texX < texWidth && texY >= 0 && texY < texHeight)
          {
            Vector2 localPos = new Vector2(x, y) - localOffset;
            yield return (localPos, texturePixels[texY * texWidth + texX]);
          }
        }
      }
    }

    /// <summary> Returns true if all pixels are fully transparent. </summary>
    /// <param name="sprite">Source sprite.</param>
    /// <returns>True if every pixel alpha is zero.</returns>
    public static bool IsFullyTransparent(this Sprite sprite)
    {
      RectInt rect = GetRect(sprite);
      Texture2D texture = sprite.texture;
      int texWidth = texture.width;
      int texHeight = texture.height;

      Color32[] texturePixels = texture.GetPixels32();

      for (int y = 0; y < rect.height; y++)
      {
        for (int x = 0; x < rect.width; x++)
        {
          int texX = rect.x + x;
          int texY = rect.y + y;

          if (texX >= 0 && texX < texWidth && texY >= 0 && texY < texHeight)
          {
            if (texturePixels[texY * texWidth + texX].a != 0)
            {
              return false;
            }
          }
        }
      }

      return true;
    }
  }
}
