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
using System.Globalization;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Float extensions. </summary>
  public static class FloatExtensions
  {
    /// <summary> Value sign. </summary>
    /// <returns>1.0 if greater than or equal to 0, -1.0 if less than 0.</returns>
    public static float Sign(this float self) => self >= 0.0f ? 1.0f : -1.0f;

    /// <summary> The smallest integer greater to or equal to value. </summary>
    /// <returns>Float</returns>
    public static float Ceil(this float self) => Mathf.Ceil(self);

    /// <summary> Value rounded to the nearest integer. </summary>
    /// <returns>Float</returns>
    public static float Round(this float self) => Mathf.Round(self);

    /// <summary> Largest integer smaller than or equal to value. </summary>
    /// <returns>Int</returns>
    public static int ToIntFloor(this float self) => (int)Mathf.Floor(self);

    /// <summary> Smallest integer greater to or equal to value. </summary>
    /// <returns>Int</returns>
    public static int ToIntCeil(this float value) => Mathf.CeilToInt(value);

    /// <summary> Rounded to the nearest integer. </summary>
    /// <returns>Float</returns>
    public static int ToIntRound(this float self) => Mathf.RoundToInt(self);

    /// <summary> Decimal part. </summary>
    /// <returns>Float</returns>
    public static float Frac(this float self) => self - Mathf.Floor(self);

    /// <summary> Returns the maximum value. </summary>
    /// <param name="a">Value</param>
    /// <param name="b">Value</param>
    /// <returns>Float</returns>
    public static float Max(this float a, float b) => Mathf.Max(a, b);

    /// <summary> Returns the minimum value. </summary>
    /// <param name="a">Value</param>
    /// <param name="b">Value</param>
    /// <returns>Float</returns>
    public static float Min(this float a, float b) => Mathf.Min(a, b);

    /// <summary> Returns the absolute value. </summary>
    /// <returns>Float</returns>
    public static float Abs(this float self) => Mathf.Abs(self);

    /// <summary> Returns the rounded value. </summary>
    /// <param name="snap"> Rounding distance </param>
    /// <returns>Float</returns>
    public static float Snap(this float self, float snap) => snap > 0.0f ? Mathf.Round(self / snap) * snap : self;

    /// <summary> Constrain the value to a range. </summary>
    /// <param name="min">Lower range</param>
    /// <param name="max">Upper range</param>
    /// <returns>Float</returns>
    public static float Clamp(this float value, float min, float max) => Mathf.Clamp(value, min, max);

    /// <summary> Constrain the value to a [0 .. 1]. </summary>
    /// <returns>Float</returns>
    public static float Clamp01(this float value) => Mathf.Clamp01(value);

    /// <summary> Approximately equal values. </summary>
    /// <param name="a">Value</param>
    /// <param name="b">Value</param>
    /// <param name="epsilon">Difference range</param>
    /// <returns>True/false</returns>
    public static bool NearlyEquals(this float a, float b, float epsilon = MathConstants.Epsilon) => Mathf.Abs(a - b) < epsilon;

    /// <summary> Convert to string using CultureInfo.InvariantCulture. </summary>
    /// <param name="value">Value</param>
    /// <returns>String</returns>
    public static string ToInvariantCulture(this float value) => value.ToString(CultureInfo.InvariantCulture);
  }
}
