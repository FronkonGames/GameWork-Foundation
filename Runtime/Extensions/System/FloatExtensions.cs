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
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Float extensions. </summary>
  public static class FloatExtensions
  {
    /// <summary> Value sign. </summary>
    /// <returns>1.0 if greater than or equal to 0, -1.0 if less than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sign(this float self) => self >= 0.0f ? 1.0f : -1.0f;

    /// <summary> The smallest integer greater to or equal to value. </summary>
    /// <returns>Float</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Ceil(this float self) => Mathf.Ceil(self);

    /// <summary> Value rounded to the nearest integer. </summary>
    /// <returns>Float</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Round(this float self) => Mathf.Round(self);

    /// <summary> Largest integer smaller than or equal to value. </summary>
    /// <returns>Int</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToIntFloor(this float self) => (int)Mathf.Floor(self);

    /// <summary> Smallest integer greater to or equal to value. </summary>
    /// <returns>Int</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToIntCeil(this float value) => Mathf.CeilToInt(value);

    /// <summary> Rounded to the nearest integer. </summary>
    /// <returns>Float</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToIntRound(this float self) => Mathf.RoundToInt(self);

    /// <summary> Decimal part. </summary>
    /// <returns>Float</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Frac(this float self) => self - Mathf.Floor(self);

    /// <summary> Returns the maximum value. </summary>
    /// <param name="a">Value</param>
    /// <param name="b">Value</param>
    /// <returns>Float</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Max(this float a, float b) => Mathf.Max(a, b);

    /// <summary> Returns the minimum value. </summary>
    /// <param name="a">Value</param>
    /// <param name="b">Value</param>
    /// <returns>Float</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Min(this float a, float b) => Mathf.Min(a, b);

    /// <summary> Returns the absolute value. </summary>
    /// <returns>Float</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Abs(this float self) => Mathf.Abs(self);

    /// <summary> Returns the rounded value. </summary>
    /// <param name="snap"> Rounding distance </param>
    /// <returns>Float</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Snap(this float self, float snap) => snap > 0.0f ? Mathf.Round(self / snap) * snap : self;

    /// <summary> Constrain the value to a range. </summary>
    /// <param name="min">Lower range</param>
    /// <param name="max">Upper range</param>
    /// <returns>Float</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp(this float value, float min, float max) => Mathf.Clamp(value, min, max);

    /// <summary> Constrain the value to a [0 .. 1]. </summary>
    /// <returns>Float</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp01(this float value) => Mathf.Clamp01(value);

    /// <summary> Approximately equal values. </summary>
    /// <param name="a">Value</param>
    /// <param name="b">Value</param>
    /// <param name="epsilon">Difference range</param>
    /// <returns>True/false</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearlyEquals(this float a, float b, float epsilon = MathConstants.Epsilon) => Mathf.Abs(a - b) < epsilon;

    /// <summary> Convert to string using CultureInfo.InvariantCulture. </summary>
    /// <param name="value">Value</param>
    /// <returns>String</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToInvariantCulture(this float value) => value.ToString(CultureInfo.InvariantCulture);

    /// <summary> Returns true if the value is between min and max (configurable inclusivity). </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBetween(this float value, float min, float max, bool inclusiveMin = true, bool inclusiveMax = true)
    {
      if (inclusiveMin && inclusiveMax)
        return value >= min && value <= max;
      if (inclusiveMin)
        return value >= min && value < max;
      if (inclusiveMax)
        return value > min && value <= max;
      return value > min && value < max;
    }

    /// <summary> Remaps a value from one range to another. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
      => from2 + (value - from1) * (to2 - from2) / (to1 - from1);

    /// <summary> Normalizes to 0-1 range. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Remap01(this float value, float from1, float to1)
      => Mathf.Approximately(to1, from1) ? value : Mathf.Clamp((value - from1) / (to1 - from1), 0.0f, 1.0f);

    /// <summary> Remaps unclamped from one range to another. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float RemapUnclamped(this float value, float from1, float to1, float from2, float to2)
    {
      if (Mathf.Approximately(to1, from1))
        return from2;

      float scale = (to2 - from2) / (to1 - from1);
      return from2 + (value - from1) * scale;
    }

    /// <summary> Snaps up to the nearest multiple of snapValue. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SnapToCeil(this float value, int snapValue)
      => Mathf.CeilToInt(value / snapValue) * snapValue;

    /// <summary> Snaps down to the nearest multiple of snapValue. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SnapToFloor(this float value, int snapValue)
      => Mathf.FloorToInt(value / snapValue) * snapValue;

    /// <summary> Normalizes to 0-1 range. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Normalize(this float value, float min, float max)
      => max - min > 0.0f ? (value - min) / (max - min) : 0.0f;

    /// <summary> Converts to rounded int. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToRoundedInt(this float value) => Mathf.RoundToInt(value);

    /// <summary> Converts to fixed-point string. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToStringFixed(this float value, int decimals = 2) => value.ToString($"F{decimals}", CultureInfo.InvariantCulture);

    /// <summary> Returns true if the value is very close to zero. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this float value, float epsilon = 1e-6f) => Mathf.Abs(value) < epsilon;

    /// <summary> Snaps to nearest grid size. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SnapToGrid(this float value, float gridSize)
      => gridSize > 0.0f ? Mathf.Round(value / gridSize) * gridSize : value;

    /// <summary> Converts degrees to radians. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ToRadians(this float degrees) => degrees * Mathf.Deg2Rad;

    /// <summary> Converts radians to degrees. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ToDegrees(this float radians) => radians * Mathf.Rad2Deg;

    /// <summary> Converts to percentage string (e.g., 0.75 -> "75%"). </summary>
    public static string ToPercentageString(this float value, int decimals = 0)
      => $"{(value * 100.0f).ToString($"F{decimals}", CultureInfo.InvariantCulture)}%";

    /// <summary> Rounds to the nearest multiple of step. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float RoundToNearest(this float value, float step)
      => step > 0.0f ? Mathf.Round(value / step) * step : value;

    /// <summary> Returns the clamped value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamped(this float value, float min, float max) => Mathf.Clamp(value, min, max);

    /// <summary> Returns the wrapped value in range [min, max). </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Wrapped(this float value, float min, float max)
    {
      float range = max - min;
      if (range <= 0.0f)
        return min;
      return min + ((value - min) % range + range) % range;
    }

    /// <summary> Converts seconds to milliseconds. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToMilliseconds(this float seconds) => (int)(seconds * 1000.0f);

#if UNITY_EDITOR
    /// <summary> Save the float to editor prefs. </summary>
    /// <param name="key">Key</param>
    public static void ToEditorPrefs(this float self, string key)
    {
      if (string.IsNullOrEmpty(key) == false)
        UnityEditor.EditorPrefs.SetFloat(key, self);
    }

    /// <summary> Get the float from editor prefs. </summary>
    /// <param name="key">Key</param>
    /// <param name="defaultValue">Default value</param>
    /// <returns>Editor prefs value</returns>
    public static float FromEditorPrefs(this string key, float defaultValue = default)
    {
      if (string.IsNullOrEmpty(key) == false)
        return UnityEditor.EditorPrefs.GetFloat(key, defaultValue);

      return defaultValue;
    }
#endif

    /// <summary> Save the float to player prefs. </summary>
    /// <param name="key">Key</param>
    public static void ToPlayerPrefs(this float self, string key)
    {
      if (string.IsNullOrEmpty(key) == false)
        PlayerPrefs.SetFloat(key, self);
    }

    /// <summary> Get the float from player prefs. </summary>
    /// <param name="key">Key</param>
    /// <param name="defaultValue">Default value</param>
    /// <returns>Player prefs value</returns>
    public static float FromPlayerPrefs(this string key, float defaultValue = default)
    {
      if (string.IsNullOrEmpty(key) == false)
        return PlayerPrefs.GetFloat(key, defaultValue);

      return defaultValue;
    }
  }
}
