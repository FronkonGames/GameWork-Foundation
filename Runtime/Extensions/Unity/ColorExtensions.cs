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
  /// <summary>
  /// Color extensions.
  /// </summary>
  public static class ColorExtensions
  {
    public static Color SetR(this Color self, float r)
    {
      self.r = r;
      
      return self;
    }

    public static Color SetG(this Color self, float g)
    {
      self.g = g;

      return self;
    }

    public static Color SetB(this Color self, float b)
    {
      self.b = b;

      return self;
    }

    public static Color SetA(this Color self, float a)
    {
      self.a = a;

      return self;
    }

    /// <summary>
    /// To hex string.
    /// </summary>
    /// <param name="self">Value</param>
    /// <returns>String</returns>
    public static string ToHex(this Color self)
    {
      Color32 color32 = self;

      return $"{color32.r:X2}{color32.g:X2}{color32.b:X2}";    
    }

    /// <summary>
    /// Brightness correction.
    /// </summary>
    /// <param name="self">Value</param>
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

    /// <summary>
    /// Is it almost black?
    /// </summary>
    /// <param name="self">Value</param>
    /// <returns>True if it is almost almost black.</returns>
    public static bool IsApproximatelyBlack(this Color self) => self.r <= Mathf.Epsilon &&
                                                                self.g <= Mathf.Epsilon &&
                                                                self.b <= Mathf.Epsilon;

    /// <summary>
    /// Is it almost white?
    /// </summary>
    /// <param name="self">Value</param>
    /// <returns>True if it is almost almost white.</returns>
    public static bool IsApproximatelyWhite(this Color self) => self.r >= 1.0f - Mathf.Epsilon &&
                                                                self.g >= 1.0f - Mathf.Epsilon &&
                                                                self.b >= 1.0f - Mathf.Epsilon;

    /// <summary>
    /// Opaque version of the color.
    /// </summary>
    /// <param name="self">Value</param>
    /// <returns>New opaque color.</returns>
    public static Color Opaque(this Color self) => new Color(self.r, self.g, self.b);

    /// <summary>
    /// Inverted color.
    /// </summary>
    /// <param name="self">Value</param>
    /// <returns>New inverted color.</returns>
    public static Color Invert(this Color self) => new Color(1.0f - self.r, 1.0f - self.g, 1.0f - self.b, self.a);

    /// <summary>
    /// Same color, different alpha.
    /// </summary>
    /// <param name="self">Value</param>
    /// <param name="alpha">Alpha</param>
    /// <returns>New color.</returns>
    public static Color WithAlpha(this Color self, float alpha) => new Color(self.r, self.g, self.b, alpha);

    /// <summary>
    /// Random color.
    /// </summary>
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

    /// <summary>
    /// Random color.
    /// </summary>
    /// <param name="self">Value</param>
    /// <returns>New random color.</returns>
    public static Color Random(this Color self)
    {
      self.r = UnityEngine.Random.Range(0.0f, 1.0f);
      self.g = UnityEngine.Random.Range(0.0f, 1.0f);
      self.b = UnityEngine.Random.Range(0.0f, 1.0f);
      self.a = 1.0f;

      return self;
    }
  }
}
