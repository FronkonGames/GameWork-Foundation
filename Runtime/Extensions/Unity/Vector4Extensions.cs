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
  /// Vector4 extensions.
  /// </summary>
  public static class Vector4Extensions
  {
    /// <summary>
    /// Returns the absolute value of the vector.
    /// </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new vector of the absolute value.</returns>
    public static Vector4 Abs(this Vector4 self) => new Vector4(Mathf.Abs(self.x), Mathf.Abs(self.y), Mathf.Abs(self.z), Mathf.Abs(self.w));

    /// <summary>
    /// Rounds the vector up to the nearest whole number.
    /// </summary>
    /// <param name="value">The vector to Value.</param>
    /// <returns>A new rounded vector.</returns>
    public static Vector4 Ceil(this Vector4 value) => new Vector4(Mathf.Ceil(value.x), Mathf.Ceil(value.y), Mathf.Ceil(value.z), Mathf.Ceil(value.w));

    /// <summary>
    /// Clamps the vector to the range [min..max].
    /// </summary>
    /// <param name="self">Value.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>A new clamped vector.</returns>
    public static Vector4 Clamp(this Vector4 self, Vector4 min, Vector4 max) => new Vector4(Mathf.Clamp(self.x, min.x, max.x),
                                                                                            Mathf.Clamp(self.y, min.y, max.y),
                                                                                            Mathf.Clamp(self.z, min.z, max.z),
                                                                                            Mathf.Clamp(self.w, min.w, max.w));

    /// <summary>
    /// Clamps the vector to the range [0..1].
    /// </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new clamped vector.</returns>
    public static Vector4 Clamp01(this Vector4 self) => new Vector4(Mathf.Clamp01(self.x), Mathf.Clamp01(self.y), Mathf.Clamp01(self.z), Mathf.Clamp01(self.w));

    /// <summary>
    /// Rounds the vector down to the nearest whole number.
    /// </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new rounded vector.</returns>
    public static Vector4 Floor(this Vector4 self) => new Vector4(Mathf.Floor(self.x), Mathf.Floor(self.y), Mathf.Floor(self.z), Mathf.Floor(self.w));

    /// <summary>
    /// Checks for equality with another vector given a margin of error specified by an epsilon.
    /// </summary>
    /// <param name="a">The left-hand side of the equality check.</param>
    /// <param name="b">The right-hand side of the equality check.</param>
    /// <returns>True if the values are equal.</returns>
    public static bool NearlyEquals(this Vector4 a, Vector4 b, float epsilon = MathConstants.Epsilon) => a.x.NearlyEquals(b.x, epsilon) &&
                                                                                                         a.y.NearlyEquals(b.y, epsilon) &&
                                                                                                         a.z.NearlyEquals(b.z, epsilon) &&
                                                                                                         a.w.NearlyEquals(b.w, epsilon);

    /// <summary>
    /// Rounds the vector to the nearest whole number.
    /// </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new rounded vector.</returns>
    public static Vector4 Rounded(this Vector4 self) => new Vector4(Mathf.Round(self.x), Mathf.Round(self.y), Mathf.Round(self.z), Mathf.Round(self.w));
  }
}
