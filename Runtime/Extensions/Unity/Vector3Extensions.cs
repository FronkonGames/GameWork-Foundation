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

using System.Runtime.CompilerServices;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Vector3 extensions.
  /// </summary>
  public static class Vector3Extensions
  {
    /// <summary>
    /// Add value to X axis.
    /// </summary>
    /// <param name="self">Value.</param>
    /// <param name="value">X</param>
    /// <returns>A new vector with X added.</returns>
    public static Vector3 AddX(this Vector3 self, float value) => new(self.x + value, self.y, self.z);

    /// <summary>
    /// Add value to Y axis.
    /// </summary>
    /// <param name="self">Value.</param>
    /// <param name="value">Y</param>
    /// <returns>A new vector with Y added.</returns>
    public static Vector3 AddY(this Vector3 self, float value) => new(self.x, self.y + value, self.z);

    /// <summary>
    /// Add value to Z axis.
    /// </summary>
    /// <param name="self">Value.</param>
    /// <param name="value">Z</param>
    /// <returns>A new vector with Z added.</returns>
    public static Vector3 AddZ(this Vector3 self, float value) => new(self.x, self.y, self.z + value);

    /// <summary>
    /// Returns the absolute value of the vector.
    /// </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new vector of the absolute value.</returns>
    public static Vector3 Abs(this Vector3 self) => new(Mathf.Abs(self.x), Mathf.Abs(self.y), Mathf.Abs(self.z));

    /// <summary>
    /// Rounds the vector up to the nearest whole number.
    /// </summary>
    /// <param name="value">The vector to Value.</param>
    /// <returns>A new rounded vector.</returns>
    public static Vector3 Ceil(this Vector3 value) => new(Mathf.Ceil(value.x), Mathf.Ceil(value.y), Mathf.Ceil(value.z));

    /// <summary>
    /// Clamps the vector to the range [min..max].
    /// </summary>
    /// <param name="self">Value.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>A new clamped vector.</returns>
    public static Vector3 Clamp(this Vector3 self, Vector3 min, Vector3 max) => new(Mathf.Clamp(self.x, min.x, max.x),
                                                                                    Mathf.Clamp(self.y, min.y, max.y),
                                                                                    Mathf.Clamp(self.z, min.z, max.z));

    /// <summary>
    /// Clamps the vector to the range [0..1].
    /// </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new clamped vector.</returns>
    public static Vector3 Clamp01(this Vector3 self) => new(Mathf.Clamp01(self.x), Mathf.Clamp01(self.y), Mathf.Clamp01(self.z));

    /// <summary>
    /// Rounds the vector down to the nearest whole number.
    /// </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new rounded vector.</returns>
    public static Vector3 Floor(this Vector3 self) => new(Mathf.Floor(self.x), Mathf.Floor(self.y), Mathf.Floor(self.z));

    /// <summary>
    /// Checks for equality with another vector given a margin of error specified by an epsilon.
    /// </summary>
    /// <param name="a">The left-hand side of the equality check.</param>
    /// <param name="b">The right-hand side of the equality check.</param>
    /// <returns>True if the values are equal.</returns>
    public static bool NearlyEquals(this Vector3 a, Vector3 b, float epsilon = MathConstants.Epsilon) => a.x.NearlyEquals(b.x, epsilon) &&
                                                                                                         a.y.NearlyEquals(b.y, epsilon) &&
                                                                                                         a.z.NearlyEquals(b.z, epsilon);

    /// <summary>
    /// Rounds the vector to the nearest whole number.
    /// </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new rounded vector.</returns>
    public static Vector3 Rounded(this Vector3 self) => new(Mathf.Round(self.x), Mathf.Round(self.y), Mathf.Round(self.z));
  }
}
