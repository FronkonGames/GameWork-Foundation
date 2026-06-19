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
using NUnit.Framework;
using UnityEngine;
using FronkonGames.GameWork.Foundation;

namespace FronkonGames.GameWork.Foundation.Tests
{
  /// <summary> Color extensions new methods tests. </summary>
  [TestFixture]
  public class ColorExtensionsNewTests
  {
    private const float Tolerance = 0.01f;

    /// <summary> ToHexRaw returns hex string without # prefix. </summary>
    [Test]
    public void ToHexRaw_ReturnsHexWithoutHash()
    {
      string hex = Color.red.ToHexRaw();

      Assert.AreEqual("FF0000", hex);
    }

    /// <summary> ToHexRaw with alpha includes alpha component. </summary>
    [Test]
    public void ToHexRaw_WithAlpha_IncludesAlpha()
    {
      string hex = Color.red.ToHexRaw(includeAlpha: true);

      Assert.AreEqual("FF0000FF", hex);
    }

    /// <summary> ToRGB255 returns correct RGB values in 0-255 range. </summary>
    [Test]
    public void ToRGB255_ReturnsCorrectValues()
    {
      int[] rgb = Color.red.ToRGB255();

      Assert.AreEqual(3, rgb.Length);
      Assert.AreEqual(255, rgb[0]);
      Assert.AreEqual(0, rgb[1]);
      Assert.AreEqual(0, rgb[2]);
    }

    /// <summary> ToRGBString returns correct "rgb(r, g, b)" format. </summary>
    [Test]
    public void ToRGBString_ReturnsCorrectFormat()
    {
      string result = Color.white.ToRGBString();

      Assert.AreEqual("rgb(255, 255, 255)", result);
    }

    /// <summary> ToRGBAString returns correct "rgba(r, g, b, a)" format. </summary>
    [Test]
    public void ToRGBAString_ReturnsCorrectFormat()
    {
      Color color = new Color(1f, 0f, 0f, 0.5f);
      string result = color.ToRGBAString();

      Assert.AreEqual("rgba(255, 0, 0, 127)", result);
    }

    /// <summary> ToHSV returns correct HSV values for red. </summary>
    [Test]
    public void ToHSV_ReturnsCorrectValues()
    {
      Vector3 hsv = Color.red.ToHSV();

      Assert.AreEqual(0f, hsv.x, Tolerance);
      Assert.AreEqual(1f, hsv.y, Tolerance);
      Assert.AreEqual(1f, hsv.z, Tolerance);
    }

    /// <summary> FromHSV creates correct Color from HSV values. </summary>
    [Test]
    public void FromHSV_CreatesCorrectColor()
    {
      Color result = ColorExtensions.FromHSV(0f, 1f, 1f);

      Assert.AreEqual(1f, result.r, Tolerance);
      Assert.AreEqual(0f, result.g, Tolerance);
      Assert.AreEqual(0f, result.b, Tolerance);
    }

    /// <summary> FromHSV round-trips with ToHSV. </summary>
    [Test]
    public void FromHSV_RoundTrips()
    {
      Color original = Color.cyan;
      Vector3 hsv = original.ToHSV();
      Color result = ColorExtensions.FromHSV(hsv.x, hsv.y, hsv.z);

      Assert.AreEqual(original.r, result.r, Tolerance);
      Assert.AreEqual(original.g, result.g, Tolerance);
      Assert.AreEqual(original.b, result.b, Tolerance);
    }

    /// <summary> GetBrightness returns correct luminance value. </summary>
    [Test]
    public void GetBrightness_ReturnsCorrectValue()
    {
      float brightness = Color.white.GetBrightness();

      Assert.AreEqual(1f, brightness, Tolerance);
    }

    /// <summary> GetBrightness for black returns zero. </summary>
    [Test]
    public void GetBrightness_Black_ReturnsZero()
    {
      float brightness = Color.black.GetBrightness();

      Assert.AreEqual(0f, brightness, Tolerance);
    }

    /// <summary> IsDark on dark color returns true. </summary>
    [Test]
    public void IsDark_DarkColor_ReturnsTrue()
    {
      Assert.IsTrue(Color.black.IsDark());
      Assert.IsTrue(new Color(0.1f, 0.1f, 0.1f).IsDark());
    }

    /// <summary> IsDark on light color returns false. </summary>
    [Test]
    public void IsDark_LightColor_ReturnsFalse()
    {
      Assert.IsFalse(Color.white.IsDark());
    }

    /// <summary> IsLight on light color returns true. </summary>
    [Test]
    public void IsLight_LightColor_ReturnsTrue()
    {
      Assert.IsTrue(Color.white.IsLight());
      Assert.IsTrue(Color.yellow.IsLight());
    }

    /// <summary> IsLight on dark color returns false. </summary>
    [Test]
    public void IsLight_DarkColor_ReturnsFalse()
    {
      Assert.IsFalse(Color.black.IsLight());
    }

    /// <summary> GetComplement returns color with inverted hue. </summary>
    [Test]
    public void GetComplement_ReturnsInvertedHue()
    {
      Color complement = Color.red.GetComplement();

      Assert.AreEqual(0f, complement.r, Tolerance);
      Assert.AreEqual(1f, complement.g, Tolerance);
      Assert.AreEqual(1f, complement.b, Tolerance);
    }

    /// <summary> GetContrastingColor on dark color returns white. </summary>
    [Test]
    public void GetContrastingColor_Dark_ReturnsWhite()
    {
      Color result = Color.black.GetContrastingColor();

      Assert.AreEqual(Color.white, result);
    }

    /// <summary> GetContrastingColor on light color returns black. </summary>
    [Test]
    public void GetContrastingColor_Light_ReturnsBlack()
    {
      Color result = Color.white.GetContrastingColor();

      Assert.AreEqual(Color.black, result);
    }

    /// <summary> Tint mixes color toward white. </summary>
    [Test]
    public void Tint_MixesWithWhite()
    {
      Color darkRed = new Color(0.5f, 0.0f, 0.0f);
      Color tinted = darkRed.Tint(0.5f);

      Assert.Greater(tinted.r, darkRed.r);
      Assert.Greater(tinted.g, darkRed.g);
      Assert.Greater(tinted.b, darkRed.b);
    }

    /// <summary> Tint with amount 0 returns original color. </summary>
    [Test]
    public void Tint_ZeroAmount_ReturnsOriginal()
    {
      Color tinted = Color.red.Tint(0f);

      Assert.AreEqual(Color.red, tinted);
    }

    /// <summary> Tint with amount 1 returns white. </summary>
    [Test]
    public void Tint_FullAmount_ReturnsWhite()
    {
      Color tinted = Color.red.Tint(1f);

      Assert.AreEqual(Color.white, tinted);
    }

    /// <summary> Shade mixes color toward black. </summary>
    [Test]
    public void Shade_MixesWithBlack()
    {
      Color shaded = Color.white.Shade(0.5f);

      Assert.Less(shaded.r, Color.white.r);
      Assert.Less(shaded.g, Color.white.g);
      Assert.Less(shaded.b, Color.white.b);
    }

    /// <summary> Shade with amount 0 returns original color. </summary>
    [Test]
    public void Shade_ZeroAmount_ReturnsOriginal()
    {
      Color shaded = Color.red.Shade(0f);

      Assert.AreEqual(Color.red, shaded);
    }

    /// <summary> Shade with amount 1 returns black. </summary>
    [Test]
    public void Shade_FullAmount_ReturnsBlack()
    {
      Color shaded = Color.red.Shade(1f);

      Assert.AreEqual(Color.black, shaded);
    }

    /// <summary> Tone mixes color toward gray. </summary>
    [Test]
    public void Tone_MixesWithGray()
    {
      Color toned = Color.red.Tone(0.5f);

      Assert.Less(toned.r, Color.red.r);
      Assert.Greater(toned.g, Color.red.g);
      Assert.Greater(toned.b, Color.red.b);
    }

    /// <summary> Tone with amount 0 returns original color. </summary>
    [Test]
    public void Tone_ZeroAmount_ReturnsOriginal()
    {
      Color toned = Color.red.Tone(0f);

      Assert.AreEqual(Color.red, toned);
    }

    /// <summary> Tone with amount 1 returns gray. </summary>
    [Test]
    public void Tone_FullAmount_ReturnsGray()
    {
      Color toned = Color.red.Tone(1f);

      Assert.AreEqual(0.5f, toned.r, Tolerance);
      Assert.AreEqual(0.5f, toned.g, Tolerance);
      Assert.AreEqual(0.5f, toned.b, Tolerance);
    }

    /// <summary> ToGrayscale returns grayscale version. </summary>
    [Test]
    public void ToGrayscale_ReturnsGrayscale()
    {
      Color grayscale = Color.red.ToGrayscale();

      Assert.AreEqual(grayscale.r, grayscale.g, Tolerance);
      Assert.AreEqual(grayscale.g, grayscale.b, Tolerance);
    }

    /// <summary> ToGrayscale preserves alpha. </summary>
    [Test]
    public void ToGrayscale_PreservesAlpha()
    {
      Color color = new Color(1f, 0f, 0f, 0.5f);
      Color grayscale = color.ToGrayscale();

      Assert.AreEqual(0.5f, grayscale.a, Tolerance);
    }

    /// <summary> ToGrayscale white stays white. </summary>
    [Test]
    public void ToGrayscale_White_StaysWhite()
    {
      Color grayscale = Color.white.ToGrayscale();

      Assert.AreEqual(1f, grayscale.r, Tolerance);
      Assert.AreEqual(1f, grayscale.g, Tolerance);
      Assert.AreEqual(1f, grayscale.b, Tolerance);
    }

    /// <summary> ShiftHue shifts the hue by specified degrees. </summary>
    [Test]
    public void ShiftHue_ShiftsHue()
    {
      Color shifted = Color.red.ShiftHue(120f);

      Assert.AreEqual(0f, shifted.r, 0.05f);
      Assert.Greater(shifted.g, 0.5f);
      Assert.Less(shifted.b, 0.5f);
    }

    /// <summary> ShiftHue by zero returns original color. </summary>
    [Test]
    public void ShiftHue_ZeroDegrees_ReturnsOriginal()
    {
      Color shifted = Color.red.ShiftHue(0f);

      Assert.AreEqual(Color.red.r, shifted.r, Tolerance);
      Assert.AreEqual(Color.red.g, shifted.g, Tolerance);
      Assert.AreEqual(Color.red.b, shifted.b, Tolerance);
    }

    /// <summary> ShiftHue by 360 returns original color. </summary>
    [Test]
    public void ShiftHue_360Degrees_ReturnsOriginal()
    {
      Color shifted = Color.red.ShiftHue(360f);

      Assert.AreEqual(Color.red.r, shifted.r, Tolerance);
      Assert.AreEqual(Color.red.g, shifted.g, Tolerance);
      Assert.AreEqual(Color.red.b, shifted.b, Tolerance);
    }

    /// <summary> ShiftHue wraps negative degrees. </summary>
    [Test]
    public void ShiftHue_NegativeDegrees_Wraps()
    {
      Color shifted = Color.red.ShiftHue(-360f);

      Assert.AreEqual(Color.red.r, shifted.r, Tolerance);
      Assert.AreEqual(Color.red.g, shifted.g, Tolerance);
      Assert.AreEqual(Color.red.b, shifted.b, Tolerance);
    }

    [Test]
    public void Brightness_White_ReturnsOne()
    {
      Assert.AreEqual(1.0f, Color.white.Brightness(), Tolerance);
    }

    [Test]
    public void Brightness_Black_ReturnsZero()
    {
      Assert.AreEqual(0.0f, Color.black.Brightness(), Tolerance);
    }

    [Test]
    public void Brightness_Red_ReturnsLuminance()
    {
      Assert.AreEqual(0.299f, Color.red.Brightness(), Tolerance);
    }

    [Test]
    public void Mono_White_ReturnsWhite()
    {
      Color result = Color.white.Mono();
      Assert.AreEqual(1.0f, result.r, Tolerance);
      Assert.AreEqual(1.0f, result.g, Tolerance);
      Assert.AreEqual(1.0f, result.b, Tolerance);
    }

    [Test]
    public void Mono_PreservesAlpha()
    {
      Color color = new Color(1.0f, 0.5f, 0.25f, 0.75f);
      Assert.AreEqual(color.a, color.Mono().a, Tolerance);
    }

    [Test]
    public void WithR_ReplacesRedChannel()
    {
      Color result = Color.white.WithR(0.0f);
      Assert.AreEqual(0.0f, result.r, Tolerance);
      Assert.AreEqual(1.0f, result.g, Tolerance);
    }

    [Test]
    public void WithRG_ReplacesRedAndGreenChannels()
    {
      Color result = Color.white.WithRG(0.0f, 0.5f);
      Assert.AreEqual(0.0f, result.r, Tolerance);
      Assert.AreEqual(0.5f, result.g, Tolerance);
      Assert.AreEqual(1.0f, result.b, Tolerance);
    }

    [Test]
    public void ResizePixels_ScaleUp_PreservesCorners()
    {
      Color[] pixels = { Color.red, Color.blue, Color.green, Color.yellow };
      Color[] result = pixels.ResizePixels(2, 2, 2, 2);

      Assert.AreEqual(4, result.Length);
      Assert.AreEqual(Color.red, result[0]);
      Assert.AreEqual(Color.blue, result[1]);
    }

    [Test]
    public void ResizePixels_SingleColor_UniformResult()
    {
      Color[] result = new[] { Color.red }.ResizePixels(1, 1, 4, 4);

      Assert.AreEqual(16, result.Length);
      for (int i = 0; i < result.Length; i++)
        Assert.AreEqual(Color.red, result[i]);
    }

    /// <summary> GetHdrIntensity returns log2 of max RGB component. </summary>
    [Test]
    public void GetHdrIntensity_ReturnsLog2OfMaxComponent()
    {
      Color color = new Color(2.0f, 1.0f, 0.5f, 1.0f);

      Assert.AreEqual(1.0f, color.GetHdrIntensity(), Tolerance);
    }

    /// <summary> AdjustHdrIntensity scales RGB by exposure stops. </summary>
    [Test]
    public void AdjustHdrIntensity_ScalesByExposureStops()
    {
      Color color = new Color(1.0f, 0.5f, 0.25f, 1.0f);
      Color brighter = color.AdjustHdrIntensity(1.0f);

      Assert.AreEqual(2.0f, brighter.r, Tolerance);
      Assert.AreEqual(1.0f, brighter.g, Tolerance);
      Assert.AreEqual(0.5f, brighter.b, Tolerance);
    }

    /// <summary> AtHdrIntensity sets target exposure. </summary>
    [Test]
    public void AtHdrIntensity_SetsTargetExposure()
    {
      Color color = new Color(4.0f, 2.0f, 1.0f, 1.0f);
      Color result = color.AtHdrIntensity(1.0f);

      Assert.AreEqual(1.0f, result.GetHdrIntensity(), Tolerance);
    }
  }
}
