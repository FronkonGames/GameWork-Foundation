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
  /// <summary> Texture2D extensions. </summary>
  public static class Texture2DExtensions
  {
    /// <summary> Applies a box blur to the texture in-place. </summary>
    /// <param name="texture">Texture to blur.</param>
    /// <param name="radius">Blur kernel radius.</param>
    /// <param name="iterations">Number of blur passes.</param>
    public static void Blur(this Texture2D texture, int radius, int iterations)
    {
      int width = texture.width;
      int height = texture.height;
      Color[] pixels = texture.GetPixels();
      Color[] result = new Color[pixels.Length];

      for (int iter = 0; iter < iterations; iter++)
      {
        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            float r = 0f;
            float g = 0f;
            float b = 0f;
            float a = 0f;
            int count = 0;

            for (int ky = -radius; ky <= radius; ky++)
            {
              for (int kx = -radius; kx <= radius; kx++)
              {
                int px = Mathf.Clamp(x + kx, 0, width - 1);
                int py = Mathf.Clamp(y + ky, 0, height - 1);
                Color c = pixels[py * width + px];
                r += c.r;
                g += c.g;
                b += c.b;
                a += c.a;
                count++;
              }
            }

            result[y * width + x] = new Color(r / count, g / count, b / count, a / count);
          }
        }

        System.Array.Copy(result, pixels, pixels.Length);
      }

      texture.SetPixels(pixels);
      texture.Apply();
    }

    /// <summary> Returns a new blurred copy of the texture. </summary>
    /// <param name="texture">Source texture.</param>
    /// <param name="radius">Blur kernel radius.</param>
    /// <param name="iterations">Number of blur passes.</param>
    /// <returns>New blurred texture.</returns>
    public static Texture2D Blurred(this Texture2D texture, int radius, int iterations)
    {
      Texture2D copy = new Texture2D(texture.width, texture.height, texture.format, texture.mipmapCount > 1);
      CopyPixels(texture, copy);
      copy.Blur(radius, iterations);

      return copy;
    }

    /// <summary> Flips the texture horizontally. </summary>
    /// <param name="texture">Texture to flip.</param>
    public static void FlipHorizontal(this Texture2D texture)
    {
      int width = texture.width;
      int height = texture.height;
      Color[] pixels = texture.GetPixels();
      Color[] result = new Color[pixels.Length];

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          result[y * width + x] = pixels[y * width + (width - 1 - x)];
        }
      }

      texture.SetPixels(result);
      texture.Apply();
    }

    /// <summary> Flips the texture vertically. </summary>
    /// <param name="texture">Texture to flip.</param>
    public static void FlipVertical(this Texture2D texture)
    {
      int width = texture.width;
      int height = texture.height;
      Color[] pixels = texture.GetPixels();
      Color[] result = new Color[pixels.Length];

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          result[y * width + x] = pixels[(height - 1 - y) * width + x];
        }
      }

      texture.SetPixels(result);
      texture.Apply();
    }

    /// <summary> Returns a resized copy of the texture. </summary>
    /// <param name="texture">Source texture.</param>
    /// <param name="width">New width.</param>
    /// <param name="height">New height.</param>
    /// <returns>New resized texture.</returns>
    public static Texture2D Resized(this Texture2D texture, int width, int height)
    {
      Color[] srcPixels = texture.GetPixels();
      int srcWidth = texture.width;
      int srcHeight = texture.height;
      Color[] dstPixels = new Color[width * height];

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          float u = (float)x / width * srcWidth;
          float v = (float)y / height * srcHeight;
          int sx = Mathf.Clamp(Mathf.FloorToInt(u), 0, srcWidth - 1);
          int sy = Mathf.Clamp(Mathf.FloorToInt(v), 0, srcHeight - 1);
          dstPixels[y * width + x] = srcPixels[sy * srcWidth + sx];
        }
      }

      Texture2D result = new Texture2D(width, height, texture.format, texture.mipmapCount > 1);
      result.SetPixels(dstPixels);
      result.Apply();

      return result;
    }

    /// <summary> Returns a cropped copy of the texture. </summary>
    /// <param name="texture">Source texture.</param>
    /// <param name="x">Left edge of crop rectangle.</param>
    /// <param name="y">Bottom edge of crop rectangle.</param>
    /// <param name="width">Width of crop rectangle.</param>
    /// <param name="height">Height of crop rectangle.</param>
    /// <returns>New cropped texture.</returns>
    public static Texture2D Crop(this Texture2D texture, int x, int y, int width, int height)
    {
      int srcWidth = texture.width;
      Color[] srcPixels = texture.GetPixels();
      Color[] dstPixels = new Color[width * height];

      for (int dy = 0; dy < height; dy++)
      {
        for (int dx = 0; dx < width; dx++)
        {
          int sx = Mathf.Clamp(x + dx, 0, srcWidth - 1);
          int sy = Mathf.Clamp(y + dy, 0, texture.height - 1);
          dstPixels[dy * width + dx] = srcPixels[sy * srcWidth + sx];
        }
      }

      Texture2D result = new Texture2D(width, height, texture.format, texture.mipmapCount > 1);
      result.SetPixels(dstPixels);
      result.Apply();

      return result;
    }

    /// <summary> Fills the texture with a single color. </summary>
    /// <param name="texture">Texture to fill.</param>
    /// <param name="color">Fill color.</param>
    public static void Fill(this Texture2D texture, Color color)
    {
      Color[] pixels = new Color[texture.width * texture.height];

      for (int i = 0; i < pixels.Length; i++)
      {
        pixels[i] = color;
      }

      texture.SetPixels(pixels);
      texture.Apply();
    }

    /// <summary> Sets a pixel with bounds checking. </summary>
    /// <param name="texture">Texture to modify.</param>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    /// <param name="color">Color to set.</param>
    public static void SetPixelSafe(this Texture2D texture, int x, int y, Color color)
    {
      if (x >= 0 && x < texture.width && y >= 0 && y < texture.height)
      {
        texture.SetPixel(x, y, color);
      }
    }

    /// <summary> Gets a pixel with bounds checking (clamps to edges). </summary>
    /// <param name="texture">Texture to sample.</param>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    /// <returns>Pixel color, clamped to edge coordinates.</returns>
    public static Color GetPixelClamped(this Texture2D texture, int x, int y)
    {
      x = Mathf.Clamp(x, 0, texture.width - 1);
      y = Mathf.Clamp(y, 0, texture.height - 1);

      return texture.GetPixel(x, y);
    }

    /// <summary> Copies pixels from source to destination texture. </summary>
    /// <param name="source">Source texture.</param>
    /// <param name="destination">Destination texture.</param>
    private static void CopyPixels(Texture2D source, Texture2D destination)
    {
      int width = Mathf.Min(source.width, destination.width);
      int height = Mathf.Min(source.height, destination.height);
      Color[] srcPixels = source.GetPixels(0, 0, width, height);
      destination.SetPixels(0, 0, width, height, srcPixels);
      destination.Apply();
    }

    /// <summary> Crops the center square and resizes to the target size. </summary>
    public static Texture2D CropAndResizeToSquare(this Texture2D texture, int squareSize)
    {
      int cropSize = Mathf.Min(texture.width, texture.height);
      int startX = (texture.width - cropSize) / 2;
      int startY = (texture.height - cropSize) / 2;

      Color[] croppedPixels = texture.GetPixels(startX, startY, cropSize, cropSize);
      Texture2D croppedTexture = new(cropSize, cropSize);
      croppedTexture.SetPixels(croppedPixels);
      croppedTexture.Apply();

      return croppedTexture.Resized(squareSize, squareSize);
    }
  }
}
