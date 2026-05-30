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
using System.Globalization;
using System.Runtime.CompilerServices;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Double extensions. </summary>
  public static class DoubleExtensions
  {
    /// <summary> Returns true if the value is between min and max (configurable inclusivity). </summary>
    /// <param name="value">Value</param>
    /// <param name="min">Lower bound</param>
    /// <param name="max">Upper bound</param>
    /// <param name="inclusiveMin">Include min in check</param>
    /// <param name="inclusiveMax">Include max in check</param>
    /// <returns>True/false</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBetween(this double value, double min, double max, bool inclusiveMin = true, bool inclusiveMax = true)
    {
      if (max < min)
        throw new ArgumentException($"max ({max}) must be greater than or equal to min ({min}).");

      if (inclusiveMin && inclusiveMax)
        return value >= min && value <= max;

      if (inclusiveMin)
        return value >= min && value < max;

      if (inclusiveMax)
        return value > min && value <= max;

      return value > min && value < max;
    }

    /// <summary> Returns true if the value is very close to zero. </summary>
    /// <param name="value">Value</param>
    /// <param name="epsilon">Difference range</param>
    /// <returns>True/false</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this double value, double epsilon = 1e-6) => Math.Abs(value) < epsilon;

    /// <summary> Returns true if approximately equal to another value. </summary>
    /// <param name="a">Value</param>
    /// <param name="b">Value</param>
    /// <param name="epsilon">Difference range</param>
    /// <returns>True/false</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ApproximatelyEquals(this double a, double b, double epsilon = 1e-10) => Math.Abs(a - b) < epsilon;

    /// <summary> Rounds to the nearest multiple of step. </summary>
    /// <param name="value">Value</param>
    /// <param name="step">Step size</param>
    /// <returns>Double</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double RoundToNearest(this double value, double step) => step > 0.0 ? Math.Round(value / step) * step : value;

    /// <summary> Converts to percentage string (e.g., 0.75 -> "75%"). </summary>
    /// <param name="value">Value</param>
    /// <param name="decimals">Number of decimal places</param>
    /// <returns>String</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToPercentageString(this double value, int decimals = 0) => $"{(value * 100.0).ToString($"F{decimals}", CultureInfo.InvariantCulture)}%";

    /// <summary> Linearly interpolates between from and to. </summary>
    /// <param name="t">Interpolation factor [0..1]</param>
    /// <param name="from">Start value</param>
    /// <param name="to">End value</param>
    /// <returns>Double</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Lerp(this double t, double from, double to) => from + (to - from) * t;

    /// <summary> Normalizes to 0-1 range. </summary>
    /// <param name="value">Value</param>
    /// <param name="min">Minimum value</param>
    /// <param name="max">Maximum value</param>
    /// <returns>Double</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Normalize(this double value, double min, double max) => max != min ? (value - min) / (max - min) : 0.0;

    /// <summary> Converts degrees to radians. </summary>
    /// <param name="degrees">Degrees</param>
    /// <returns>Double</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToRadians(this double degrees) => degrees * Math.PI / 180.0;

    /// <summary> Converts radians to degrees. </summary>
    /// <param name="radians">Radians</param>
    /// <returns>Double</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToDegrees(this double radians) => radians * 180.0 / Math.PI;

    /// <summary> Converts to rounded int. </summary>
    /// <param name="value">Value</param>
    /// <returns>Int</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToRoundedInt(this double value) => (int)Math.Round(value);

    /// <summary> Converts to fixed-point string. </summary>
    /// <param name="value">Value</param>
    /// <param name="decimals">Number of decimal places</param>
    /// <returns>String</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToStringFixed(this double value, int decimals = 2) => value.ToString($"F{decimals}", CultureInfo.InvariantCulture);

    /// <summary> Snaps to nearest grid size. </summary>
    /// <param name="value">Value</param>
    /// <param name="gridSize">Grid size</param>
    /// <returns>Double</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SnapToGrid(this double value, double gridSize) => gridSize > 0.0 ? Math.Round(value / gridSize) * gridSize : value;

    /// <summary> Returns the clamped value. </summary>
    /// <param name="value">Value</param>
    /// <param name="min">Lower bound</param>
    /// <param name="max">Upper bound</param>
    /// <returns>Double</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Clamped(this double value, double min, double max)
    {
      if (value < min)
        return min;

      return value > max ? max : value;
    }

    /// <summary> Returns the wrapped value in range [min, max). </summary>
    /// <param name="value">Value</param>
    /// <param name="min">Lower bound</param>
    /// <param name="max">Upper bound</param>
    /// <returns>Double</returns>
    public static double Wrapped(this double value, double min, double max)
    {
      double range = max - min;

      if (range <= 0.0)
        return min;

      return min + ((value - min) % range + range) % range;
    }

    /// <summary> Converts milliseconds to seconds. </summary>
    public static float ToSeconds(this double milliseconds) => (float)(milliseconds * 0.001);
  }
}
