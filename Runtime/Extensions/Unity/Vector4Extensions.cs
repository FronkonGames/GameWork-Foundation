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
  /// <summary> Vector4 extensions. </summary>
  public static class Vector4Extensions
  {
    /// <summary> Returns the absolute value of the vector. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new vector of the absolute value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Abs(this Vector4 self) => new(Mathf.Abs(self.x), Mathf.Abs(self.y), Mathf.Abs(self.z), Mathf.Abs(self.w));

    /// <summary> Rounds the vector up to the nearest whole number. </summary>
    /// <param name="value">The vector to Value.</param>
    /// <returns>A new rounded vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Ceil(this Vector4 value) => new(Mathf.Ceil(value.x), Mathf.Ceil(value.y), Mathf.Ceil(value.z), Mathf.Ceil(value.w));

    /// <summary> Clamps the vector to the range [min..max]. </summary>
    /// <param name="self">Value.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>A new clamped vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Clamp(this Vector4 self, Vector4 min, Vector4 max) => new(Mathf.Clamp(self.x, min.x, max.x),
                                                                                    Mathf.Clamp(self.y, min.y, max.y),
                                                                                    Mathf.Clamp(self.z, min.z, max.z),
                                                                                    Mathf.Clamp(self.w, min.w, max.w));

    /// <summary> Clamps the vector to the range [0..1]. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new clamped vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Clamp01(this Vector4 self) => new (Mathf.Clamp01(self.x),
                                                             Mathf.Clamp01(self.y),
                                                             Mathf.Clamp01(self.z),
                                                             Mathf.Clamp01(self.w));

    /// <summary> Rounds the vector down to the nearest whole number. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new rounded vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Floor(this Vector4 self) => new(Mathf.Floor(self.x),
                                                          Mathf.Floor(self.y),
                                                          Mathf.Floor(self.z),
                                                          Mathf.Floor(self.w));

    /// <summary> Checks for equality with another vector given a margin of error specified by an epsilon. </summary>
    /// <param name="a">The left-hand side of the equality check.</param>
    /// <param name="b">The right-hand side of the equality check.</param>
    /// <returns>True if the values are equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearlyEquals(this Vector4 a, Vector4 b, float epsilon = MathConstants.Epsilon) => a.x.NearlyEquals(b.x, epsilon) &&
                                                                                                         a.y.NearlyEquals(b.y, epsilon) &&
                                                                                                         a.z.NearlyEquals(b.z, epsilon) &&
                                                                                                         a.w.NearlyEquals(b.w, epsilon);

    /// <summary> Rounds the vector to the nearest whole number. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new rounded vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Rounded(this Vector4 self) => new(Mathf.Round(self.x),
                                                            Mathf.Round(self.y),
                                                            Mathf.Round(self.z),
                                                            Mathf.Round(self.w));

    /// <summary> Apply the modulo operator. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 Remainder(this Vector4 self, Vector4 modulus) => new(self.x % modulus.x,
                                                                               self.y % modulus.y,
                                                                               self.z % modulus.z,
                                                                               self.w % modulus.w);

    /// <summary> Vector4 to string. </summary>
    /// <param name="self">Value</param>
    /// <returns>string</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString(this Vector4 self) => $"{self.x},{self.y},{self.z},{self.w}";

    /// <summary> Returns a new Vector4 with the X component replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithX(this Vector4 value, float x) => new(x, value.y, value.z, value.w);

    /// <summary> Returns a new Vector4 with the Y component replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithY(this Vector4 value, float y) => new(value.x, y, value.z, value.w);

    /// <summary> Returns a new Vector4 with the Z component replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithZ(this Vector4 value, float z) => new(value.x, value.y, z, value.w);

    /// <summary> Returns a new Vector4 with the W component replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithW(this Vector4 value, float w) => new(value.x, value.y, value.z, w);

    /// <summary> Returns a new Vector4 with X and Y components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithXY(this Vector4 value, float x, float y) => new(x, y, value.z, value.w);

    /// <summary> Returns a new Vector4 with X and Z components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithXZ(this Vector4 value, float x, float z) => new(x, value.y, z, value.w);

    /// <summary> Returns a new Vector4 with X and W components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithXW(this Vector4 value, float x, float w) => new(x, value.y, value.z, w);

    /// <summary> Returns a new Vector4 with Y and Z components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithYZ(this Vector4 value, float y, float z) => new(value.x, y, z, value.w);

    /// <summary> Returns a new Vector4 with Y and W components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithYW(this Vector4 value, float y, float w) => new(value.x, y, value.z, w);

    /// <summary> Returns a new Vector4 with Z and W components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithZW(this Vector4 value, float z, float w) => new(value.x, value.y, z, w);

    /// <summary> Returns a new Vector4 with X, Y and Z components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithXYZ(this Vector4 value, float x, float y, float z) => new(x, y, z, value.w);

    /// <summary> Returns a new Vector4 with X, Y and W components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithXYW(this Vector4 value, float x, float y, float w) => new(x, y, value.z, w);

    /// <summary> Returns a new Vector4 with X, Z and W components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithXZW(this Vector4 value, float x, float z, float w) => new(x, value.y, z, w);

    /// <summary> Returns a new Vector4 with Y, Z and W components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 WithYZW(this Vector4 value, float y, float z, float w) => new(value.x, y, z, w);

    /// <summary> Adjusts X component by adding value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjX(this Vector4 value, float x) => new(value.x + x, value.y, value.z, value.w);

    /// <summary> Adjusts Y component by adding value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjY(this Vector4 value, float y) => new(value.x, value.y + y, value.z, value.w);

    /// <summary> Adjusts Z component by adding value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjZ(this Vector4 value, float z) => new(value.x, value.y, value.z + z, value.w);

    /// <summary> Adjusts W component by adding value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjW(this Vector4 value, float w) => new(value.x, value.y, value.z, value.w + w);

    /// <summary> Adjusts X and Y components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjXY(this Vector4 value, float x, float y) => new(value.x + x, value.y + y, value.z, value.w);

    /// <summary> Adjusts X and Z components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjXZ(this Vector4 value, float x, float z) => new(value.x + x, value.y, value.z + z, value.w);

    /// <summary> Adjusts X and W components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjXW(this Vector4 value, float x, float w) => new(value.x + x, value.y, value.z, value.w + w);

    /// <summary> Adjusts Y and Z components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjYZ(this Vector4 value, float y, float z) => new(value.x, value.y + y, value.z + z, value.w);

    /// <summary> Adjusts Y and W components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjYW(this Vector4 value, float y, float w) => new(value.x, value.y + y, value.z, value.w + w);

    /// <summary> Adjusts Z and W components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjZW(this Vector4 value, float z, float w) => new(value.x, value.y, value.z + z, value.w + w);

    /// <summary> Adjusts X, Y and Z components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjXYZ(this Vector4 value, float x, float y, float z) => new(value.x + x, value.y + y, value.z + z, value.w);

    /// <summary> Adjusts X, Y and W components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjXYW(this Vector4 value, float x, float y, float w) => new(value.x + x, value.y + y, value.z, value.w + w);

    /// <summary> Adjusts X, Z and W components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjXZW(this Vector4 value, float x, float z, float w) => new(value.x + x, value.y, value.z + z, value.w + w);

    /// <summary> Adjusts Y, Z and W components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjYZW(this Vector4 value, float y, float z, float w) => new(value.x, value.y + y, value.z + z, value.w + w);

    /// <summary> Adjusts all components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjXYZW(this Vector4 value, float x, float y, float z, float w) => new(value.x + x, value.y + y, value.z + z, value.w + w);

    /// <summary> Adjusts all components by the same value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 AdjAll(this Vector4 value, float all) => new(value.x + all, value.y + all, value.z + all, value.w + all);
  }
}
