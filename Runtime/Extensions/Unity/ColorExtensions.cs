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
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Color extensions. </summary>
  public static class ColorExtensions
  {
    /// <summary> Color with the R component changed. </summary>
    /// <param name="r">component</param>
    /// <returns>Color</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color SetR(this Color self, float r) => new(r, self.g, self.b, self.a);

    /// <summary> Color with the G component changed. </summary>
    /// <param name="r">component</param>
    /// <returns>Color</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color SetG(this Color self, float g) => new(self.r, g, self.b, self.a);

    /// <summary> Color with the B component changed. </summary>
    /// <param name="r">component</param>
    /// <returns>Color</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color SetB(this Color self, float b) => new(self.r, self.g, b, self.a);

    /// <summary> Color with the A component changed. </summary>
    /// <param name="r">component</param>
    /// <returns>Color</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color SetA(this Color self, float a) => new(self.r, self.g, self.b, a);

    /// <summary> Color with the hue component displaced. </summary>
    /// <param name="hue">Hue</param>
    /// <param name="hdr">HDR color?</param>
    /// <returns>Color</returns>
    public static Color SetHue(this Color self, float hue, bool hdr = false)
    {
      Color.RGBToHSV(self, out _, out var saturation, out var value);

      return Color.HSVToRGB(hue, saturation, value, hdr);
    }

    /// <summary> Color with the saturation changed. </summary>
    /// <param name="saturation">Saturation</param>
    /// <param name="hdr">HDR color?</param>
    /// <returns>Color</returns>
    public static Color SetSaturation(this Color self, float saturation, bool hdr = false)
    {
      Color.RGBToHSV(self, out var hue, out _, out var value);

      return Color.HSVToRGB(hue, saturation, value, hdr);
    }

    /// <summary> Color with the value (HSV space) changed. </summary>
    /// <param name="value">Value</param>
    /// <param name="hdr">HDR color?</param>
    /// <returns>Color</returns>
    public static Color SetValue(this Color self, float value, bool hdr = false)
    {
      Color.RGBToHSV(self, out var hue, out var saturation, out _);

      return Color.HSVToRGB(hue, saturation, value, hdr);
    }

    /// <summary> From hex string. </summary>
    /// <param name="text">HTML color, ie '#FF00FF'</param>
    /// <returns>String</returns>
    public static Color FromHex(this string text)
    {
      ColorUtility.TryParseHtmlString(text, out Color color);

      return color;
    }

    /// <summary> To hex string. </summary>
    /// <returns>String</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToHex(this Color self) => $"#{ColorUtility.ToHtmlStringRGB(self)}";

    /// <summary> Color to string. </summary>
    /// <param name="self">Value</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString(this Color self) => $"{self.r},{self.g},{self.b},{self.a}";

    /// <summary> Brightness correction. </summary>
    /// <param name="correctionFactor">Brightness correction factor [-1 - 1]</param>
    /// <returns>Color with corrected brightness.</returns>
    public static Color ChangeBrightness(this Color self, float correctionFactor)
    {
      float red = (float)self.r;
      float green = (float)self.g;
      float blue = (float)self.b;

      if (correctionFactor < 0.0f)
      {
        correctionFactor = 1.0f + correctionFactor;
        red *= correctionFactor;
        green *= correctionFactor;
        blue *= correctionFactor;
      }
      else
      {
        red = (255 - red) * correctionFactor + red;
        green = (255 - green) * correctionFactor + green;
        blue = (255 - blue) * correctionFactor + blue;
      }

      return new Color(red / 255, green / 255, blue / 255, self.a);
    }

    /// <summary> Is it almost black? </summary>
    /// <returns>True if it is almost black.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsApproximatelyBlack(this Color self) => self.r <= MathConstants.Epsilon &&
                                                                self.g <= MathConstants.Epsilon &&
                                                                self.b <= MathConstants.Epsilon;

    /// <summary> Is it almost white? </summary>
    /// <returns>True if it is almost white.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsApproximatelyWhite(this Color self) => self.r >= 1.0f - MathConstants.Epsilon &&
                                                                self.g >= 1.0f - MathConstants.Epsilon &&
                                                                self.b >= 1.0f - MathConstants.Epsilon;

    /// <summary> Opaque version of the color. </summary>
    /// <returns>New opaque color.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color Opaque(this Color self) => new(self.r, self.g, self.b);

    /// <summary> Inverted color. </summary>
    /// <returns>New inverted color.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color Invert(this Color self) => new(1.0f - self.r, 1.0f - self.g, 1.0f - self.b, self.a);

    /// <summary> Same color, different alpha. </summary>
    /// <param name="alpha">Alpha</param>
    /// <returns>New color.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithAlpha(this Color self, float alpha) => new(self.r, self.g, self.b, alpha);

    /// <summary> Random color. </summary>
    /// <returns>New color.</returns>
    public static Color Random()
    {
      return new Color
      {
        r = UnityEngine.Random.Range(0.0f, 1.0f),
        g = UnityEngine.Random.Range(0.0f, 1.0f),
        b = UnityEngine.Random.Range(0.0f, 1.0f),
        a = 1.0f
      };
    }

    /// <summary> Random color. </summary>
    /// <returns>New random color.</returns>
    public static Color Random(this Color self)
    {
      self.r = UnityEngine.Random.Range(0.0f, 1.0f);
      self.g = UnityEngine.Random.Range(0.0f, 1.0f);
      self.b = UnityEngine.Random.Range(0.0f, 1.0f);
      self.a = 1.0f;

      return self;
    }

    /// <summary> Converts Color to hex string without #. </summary>
    /// <param name="includeAlpha">If true, includes the alpha component.</param>
    /// <returns>Hex string without # prefix.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToHexRaw(this Color color, bool includeAlpha = false) =>
      includeAlpha ? ColorUtility.ToHtmlStringRGBA(color) : ColorUtility.ToHtmlStringRGB(color);

    /// <summary> Converts Color to RGB values (0-255). </summary>
    /// <returns>Array of [r, g, b] in 0-255 range.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int[] ToRGB255(this Color color) =>
      new[] { (int)(color.r * 255f), (int)(color.g * 255f), (int)(color.b * 255f) };

    /// <summary> Converts Color to RGB string format "rgb(255, 255, 255)". </summary>
    /// <returns>RGB string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToRGBString(this Color color) =>
      $"rgb({(int)(color.r * 255f)}, {(int)(color.g * 255f)}, {(int)(color.b * 255f)})";

    /// <summary> Converts Color to RGBA string format "rgba(255, 255, 255, 255)". </summary>
    /// <returns>RGBA string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToRGBAString(this Color color) =>
      $"rgba({(int)(color.r * 255f)}, {(int)(color.g * 255f)}, {(int)(color.b * 255f)}, {(int)(color.a * 255f)})";

    /// <summary> Converts Color to HSV values (H: 0-360, S: 0-1, V: 0-1). </summary>
    /// <returns>Vector3 with H, S, V components.</returns>
    public static Vector3 ToHSV(this Color color)
    {
      Color.RGBToHSV(color, out float h, out float s, out float v);

      return new Vector3(h * 360f, s, v);
    }

    /// <summary> Creates Color from HSV values (H: 0-360, S: 0-1, V: 0-1). </summary>
    /// <param name="h">Hue (0-360).</param>
    /// <param name="s">Saturation (0-1).</param>
    /// <param name="v">Value (0-1).</param>
    /// <param name="a">Alpha (0-1).</param>
    /// <returns>Color from HSV.</returns>
    public static Color FromHSV(float h, float s, float v, float a = 1f) =>
      Color.HSVToRGB(h / 360f, s, v);

    /// <summary> Gets the brightness/luminance of the color. </summary>
    /// <returns>Brightness value (0-1).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float GetBrightness(this Color color) =>
      (color.r * 0.299f) + (color.g * 0.587f) + (color.b * 0.114f);

    /// <summary> Returns whether the color is considered "dark". </summary>
    /// <returns>True if brightness is below 0.5.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsDark(this Color color) => color.GetBrightness() < 0.5f;

    /// <summary> Returns whether the color is considered "light". </summary>
    /// <returns>True if brightness is 0.5 or above.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLight(this Color color) => color.GetBrightness() >= 0.5f;

    /// <summary> Returns the complementary color. </summary>
    /// <returns>Complementary color.</returns>
    public static Color GetComplement(this Color color)
    {
      Color.RGBToHSV(color, out float h, out float s, out float v);

      return Color.HSVToRGB((h + 0.5f) % 1.0f, s, v);
    }

    /// <summary> Returns a contrasting color (black or white). </summary>
    /// <returns>Black or white depending on luminance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color GetContrastingColor(this Color color) =>
      color.GetBrightness() >= 0.5f ? Color.black : Color.white;

    /// <summary> Adjusts the saturation of the color. </summary>
    /// <param name="saturationMultiplier">Multiplier for saturation (1 = unchanged).</param>
    /// <returns>Color with adjusted saturation.</returns>
    public static Color AdjustSaturation(this Color color, float saturationMultiplier)
    {
      Color.RGBToHSV(color, out float h, out float s, out float v);

      return Color.HSVToRGB(h, Mathf.Clamp01(s * saturationMultiplier), v);
    }

    /// <summary> Adjusts the brightness/value of the color. </summary>
    /// <param name="brightnessMultiplier">Multiplier for brightness (1 = unchanged).</param>
    /// <returns>Color with adjusted brightness.</returns>
    public static Color AdjustBrightness(this Color color, float brightnessMultiplier)
    {
      Color.RGBToHSV(color, out float h, out float s, out float v);

      return Color.HSVToRGB(h, s, Mathf.Clamp01(v * brightnessMultiplier));
    }

    /// <summary> Shifts the hue of the color by the specified degrees. </summary>
    /// <param name="hueDegrees">Degrees to shift hue (-360 to 360).</param>
    /// <returns>Color with shifted hue.</returns>
    public static Color ShiftHue(this Color color, float hueDegrees)
    {
      Color.RGBToHSV(color, out float h, out float s, out float v);
      h = (h + hueDegrees / 360f) % 1.0f;

      if (h < 0.0f)
      {
        h += 1.0f;
      }

      return Color.HSVToRGB(h, s, v);
    }

    /// <summary> Returns the grayscale version of the color. </summary>
    /// <returns>Grayscale color.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color ToGrayscale(this Color color)
    {
      float luminance = color.GetBrightness();

      return new Color(luminance, luminance, luminance, color.a);
    }

    /// <summary> Creates a tinted version (mix with white). </summary>
    /// <param name="amount">Amount to tint (0 = original, 1 = white).</param>
    /// <returns>Tinted color.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color Tint(this Color color, float amount) =>
      Color.Lerp(color, Color.white, Mathf.Clamp01(amount));

    /// <summary> Creates a shaded version (mix with black). </summary>
    /// <param name="amount">Amount to shade (0 = original, 1 = black).</param>
    /// <returns>Shaded color.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color Shade(this Color color, float amount) =>
      Color.Lerp(color, Color.black, Mathf.Clamp01(amount));

    /// <summary> Creates a toned version (mix with gray). </summary>
    /// <param name="amount">Amount to tone (0 = original, 1 = gray).</param>
    /// <returns>Toned color.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color Tone(this Color color, float amount) =>
      Color.Lerp(color, new Color(0.5f, 0.5f, 0.5f, color.a), Mathf.Clamp01(amount));

    /// <summary> Returns the brightness/luminance of the color (0-1). </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Brightness(this Color color) =>
      (color.r * 0.299f) + (color.g * 0.587f) + (color.b * 0.114f);

    /// <summary> Returns a grayscale version of the color. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color Mono(this Color color)
    {
      float luminance = color.Brightness();

      return new Color(luminance, luminance, luminance, color.a);
    }

    /// <summary> Resizes a Color array to new dimensions using bilinear interpolation. </summary>
    public static Color[] ResizePixels(this Color[] pixels, int originalWidth, int originalHeight, int newWidth, int newHeight)
    {
      Color[] result = new Color[newWidth * newHeight];
      float ratioX = (float)(originalWidth - 1) / (newWidth - 1);
      float ratioY = (float)(originalHeight - 1) / (newHeight - 1);

      for (int y = 0; y < newHeight; y++)
      {
        for (int x = 0; x < newWidth; x++)
        {
          float srcX = x * ratioX;
          float srcY = y * ratioY;

          int x0 = (int)srcX;
          int y0 = (int)srcY;
          int x1 = Math.Min(x0 + 1, originalWidth - 1);
          int y1 = Math.Min(y0 + 1, originalHeight - 1);

          float tx = srcX - x0;
          float ty = srcY - y0;

          Color c00 = pixels[y0 * originalWidth + x0];
          Color c10 = pixels[y0 * originalWidth + x1];
          Color c01 = pixels[y1 * originalWidth + x0];
          Color c11 = pixels[y1 * originalWidth + x1];

          Color top = Color.Lerp(c00, c10, tx);
          Color bottom = Color.Lerp(c01, c11, tx);

          result[y * newWidth + x] = Color.Lerp(top, bottom, ty);
        }
      }

      return result;
    }

    /// <summary> Returns a new color with the red channel replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithR(this Color color, float r) => new(r, color.g, color.b, color.a);

    /// <summary> Returns a new color with the green channel replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithG(this Color color, float g) => new(color.r, g, color.b, color.a);

    /// <summary> Returns a new color with the blue channel replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithB(this Color color, float b) => new(color.r, color.g, b, color.a);

    /// <summary> Returns a new color with the alpha channel replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithA(this Color color, float a) => new(color.r, color.g, color.b, a);

    /// <summary> Returns a new color with red and green channels replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithRG(this Color color, float r, float g) => new(r, g, color.b, color.a);

    /// <summary> Returns a new color with red and blue channels replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithRB(this Color color, float r, float b) => new(r, color.g, b, color.a);

    /// <summary> Returns a new color with green and blue channels replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color WithGB(this Color color, float g, float b) => new(color.r, g, b, color.a);
  }
}
