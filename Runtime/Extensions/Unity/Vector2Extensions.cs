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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Vector2 extensions. </summary>
  public static class Vector2Extensions
  {
    /// <summary> Returns the absolute value of the vector. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new vector of the absolute value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Abs(this Vector2 self) => new(Mathf.Abs(self.x), Mathf.Abs(self.y));

    /// <summary> Rounds the vector up to the nearest whole number. </summary>
    /// <param name="value">The vector to Value.</param>
    /// <returns>A new rounded vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Ceil(this Vector2 value) => new(Mathf.Ceil(value.x), Mathf.Ceil(value.y));

    /// <summary> Clamps the vector to the range [min..max]. </summary>
    /// <param name="self">Value.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>A new clamped vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Clamp(this Vector2 self, Vector2 min, Vector2 max) => new(Mathf.Clamp(self.x, min.x, max.x), Mathf.Clamp(self.y, min.y, max.y));

    /// <summary> Clamps the vector to the range [0..1]. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new clamped vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Clamp01(this Vector2 self) => new(Mathf.Clamp01(self.x), Mathf.Clamp01(self.y));

    /// <summary> Rounds the vector down to the nearest whole number. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new rounded vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Floor(this Vector2 self) => new(Mathf.Floor(self.x), Mathf.Floor(self.y));

    /// <summary> Checks for equality with another vector given a margin of error specified by an epsilon. </summary>
    /// <param name="a">The left-hand side of the equality check.</param>
    /// <param name="b">The right-hand side of the equality check.</param>
    /// <returns>True if the values are equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearlyEquals(this Vector2 a, Vector2 b, float epsilon = MathConstants.Epsilon) => a.x.NearlyEquals(b.x, epsilon) && a.y.NearlyEquals(b.y, epsilon);

    /// <summary> Rounds the vector to the nearest whole number. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new rounded vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Rounded(this Vector2 self) => new(Mathf.Round(self.x), Mathf.Round(self.y));

    /// <summary> Apply the modulo operator. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Remainder(this Vector2 self, Vector2 modulus) => new(self.x % modulus.x, self.y % modulus.y);

    /// <summary> Vector2 to string. </summary>
    /// <param name="self">Value</param>
    /// <returns>string</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString(this Vector2 self) => $"{self.x},{self.y}";

    /// <summary> Remaps the vector from one range to another. </summary>
    public static Vector2 Remap(this Vector2 value, Vector2 from1, Vector2 to1, Vector2 from2, Vector2 to2)
      => new(
        value.x.Remap(from1.x, to1.x, from2.x, to2.x),
        value.y.Remap(from1.y, to1.y, from2.y, to2.y));

    /// <summary> Remaps the vector from one range to 0-1. </summary>
    public static Vector2 Remap01(this Vector2 value, Vector2 from, Vector2 to)
      => new(
        value.x.Remap(from.x, to.x, 0.0f, 1.0f),
        value.y.Remap(from.y, to.y, 0.0f, 1.0f));

    /// <summary> Remaps the vector unclamped from one range to another. </summary>
    public static Vector2 RemapUnclamped(this Vector2 value, Vector2 from1, Vector2 to1, Vector2 from2, Vector2 to2)
      => new(
        from2.x + (value.x - from1.x) * (to2.x - from2.x) / (to1.x - from1.x),
        from2.y + (value.y - from1.y) * (to2.y - from2.y) / (to1.y - from1.y));

    /// <summary> Multiplies the vector by a scalar. </summary>
    public static Vector2 Multiply(this Vector2 value, double scalar)
      => new((float)(value.x * scalar), (float)(value.y * scalar));

    /// <summary> Returns the average of an array of vectors. </summary>
    public static Vector2 Average(this Vector2[] vectors)
    {
      if (vectors == null || vectors.Length == 0)
        return Vector2.zero;

      Vector2 sum = Vector2.zero;
      for (int i = 0; i < vectors.Length; i++)
        sum += vectors[i];

      return sum / vectors.Length;
    }

    /// <summary> Returns a new Vector2Int with the X component replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 WithX(this Vector2 value, float x) => new(x, value.y);

    /// <summary> Returns a new Vector2 with the Y component replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 WithY(this Vector2 value, float y) => new(value.x, y);

    /// <summary> Swizzles to Vector3 as (x, y, z). </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ToVector3(this Vector2 value, float z = 0.0f) => new(value.x, value.y, z);

    /// <summary> Swizzles to Vector4 as (x, y, z, w). </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 ToVector4(this Vector2 value, float z = 0.0f, float w = 0.0f) => new(value.x, value.y, z, w);

    /// <summary> Swizzles to Vector3 as (y, x, z). </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 SwizzleYXZ(this Vector2 value, float z = 0.0f) => new(value.y, value.x, z);

    /// <summary> Swizzles to Vector4 as (y, x, z, w). </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 SwizzleYXZW(this Vector2 value, float z = 0.0f, float w = 0.0f) => new(value.y, value.x, z, w);

    /// <summary> Returns YX swizzle. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 SwizzleYX(this Vector2 value) => new(value.y, value.x);

    /// <summary> Adjusts X component by adding value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 AdjX(this Vector2 value, float x) => new(value.x + x, value.y);

    /// <summary> Adjusts Y component by adding value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 AdjY(this Vector2 value, float y) => new(value.x, value.y + y);

    /// <summary> Adjusts X and Y components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 AdjXY(this Vector2 value, float x, float y) => new(value.x + x, value.y + y);

    /// <summary> Adjusts all components by the same value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 AdjAll(this Vector2 value, float all) => new(value.x + all, value.y + all);

    /// <summary> Tries to calculate the average of a list of vectors. </summary>
    public static bool TryCalculateAverage(this List<Vector2> positions, out Vector2 result)
    {
      result = Vector2.zero;

      if (positions.Count == 0)
        return false;

      for (int i = 0; i < positions.Count; i++)
        result += positions[i];

      result /= positions.Count;
      return true;
    }

    /// <summary> Tries to calculate the average of an array of vectors. </summary>
    public static bool TryCalculateAverage(this Vector2[] positions, out Vector2 result)
    {
      result = Vector2.zero;

      if (positions.Length == 0)
        return false;

      for (int i = 0; i < positions.Length; i++)
        result += positions[i];

      result /= positions.Length;
      return true;
    }

    /// <summary> Rotates the vector by the given angle in radians. </summary>
    /// <param name="vector"> Source vector. </param>
    /// <param name="angleRadians"> Rotation angle in radians. </param>
    /// <returns> Rotated vector. </returns>
    public static Vector2 Rotate(this Vector2 vector, float angleRadians)
    {
      float cos = Mathf.Cos(angleRadians);
      float sin = Mathf.Sin(angleRadians);

      return new Vector2(
        cos * vector.x - sin * vector.y,
        sin * vector.x + cos * vector.y);
    }
  }
}
