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
  /// Float extensions.
  /// </summary>
  public static class FloatExtensions
  {
    /// <summary>
    /// Returns the maximum value.
    /// </summary>
    /// <param name="a">Value</param>
    /// <param name="b">Value</param>
    /// <returns>Float</returns>
    public static float Max(this float a, float b) => Mathf.Max(a, b);

    /// <summary>
    /// Returns the minimum value.
    /// </summary>
    /// <param name="a">Value</param>
    /// <param name="b">Value</param>
    /// <returns>Float</returns>
    public static float Min(this float a, float b) => Mathf.Min(a, b);
    
    /// <summary>
    /// Returns the absolute value.
    /// </summary>
    /// <param name="self">Value</param>
    /// <returns>Float</returns>
    public static float Abs(this float self) => Mathf.Abs(self);
    
    /// <summary>
    /// Constrain the value to a range.
    /// </summary>
    /// <param name="self">Value</param>
    /// <param name="min">Lower range</param>
    /// <param name="max">Upper range</param>
    /// <returns>Float</returns>
    public static float Clamp(this float value, float min, float max) => Mathf.Clamp(value, min, max);

    /// <summary>
    /// Constrain the angle to a range.
    /// </summary>
    /// <param name="angle">Value</param>
    /// <param name="min">Lower angle</param>
    /// <param name="max">Upper angle</param>
    /// <returns>Float</returns>
    public static float ClampAngle(this float angle, float min, float max)
    {
      if (angle < -360.0f)
        angle += 360.0f;
      else if (angle > 360.0f)
        angle -= 360.0f;

      return Mathf.Clamp(angle, min, max);
    }

    /// <summary>
    /// Approximately equal values.
    /// </summary>
    /// <param name="a">Value</param>
    /// <param name="b">Value</param>
    /// <param name="epsilon">Difference range</param>
    /// <returns>True/false</returns>
    public static bool NearlyEquals(this float a, float b, float epsilon = float.Epsilon) =>
      (a == b) ? true : Mathf.Abs(a - b) < epsilon;

    /// <summary>
    /// The value is within a range.
    /// </summary>
    /// <param name="self">Value</param>
    /// <param name="min">Minimum range (included)</param>
    /// <param name="max">Maximum range (included)</param>
    /// <returns>True/false</returns>
    public static bool IsWithin(this float self, float min, float max) => self >= min && self <= max;

    /// <summary>
    /// The value is between a range.
    /// </summary>
    /// <param name="self">Value</param>
    /// <param name="min">Minimum range (not included)</param>
    /// <param name="max">Maximum range (not included)</param>
    /// <returns>True/false</returns>
    public static bool IsBetween(this float self, float min, float max) => self > min && self < max;
  }
}
