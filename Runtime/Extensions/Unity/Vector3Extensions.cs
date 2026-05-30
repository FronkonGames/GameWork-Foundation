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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Vector3 extensions. </summary>
  public static class Vector3Extensions
  {
    /// <summary> Add value to X axis. </summary>
    /// <param name="self">Value.</param>
    /// <param name="value">X</param>
    /// <returns>A new vector with X added.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 AddX(this Vector3 self, float value) => new(self.x + value, self.y, self.z);

    /// <summary> Add value to Y axis. </summary>
    /// <param name="self">Value.</param>
    /// <param name="value">Y</param>
    /// <returns>A new vector with Y added.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 AddY(this Vector3 self, float value) => new(self.x, self.y + value, self.z);

    /// <summary> Add value to Z axis. </summary>
    /// <param name="self">Value.</param>
    /// <param name="value">Z</param>
    /// <returns>A new vector with Z added.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 AddZ(this Vector3 self, float value) => new(self.x, self.y, self.z + value);

    /// <summary> Returns the absolute value of the vector. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new vector of the absolute value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Abs(this Vector3 self) => new(Mathf.Abs(self.x), Mathf.Abs(self.y), Mathf.Abs(self.z));

    /// <summary> Rounds the vector up to the nearest whole number. </summary>
    /// <param name="value">The vector to Value.</param>
    /// <returns>A new rounded vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Ceil(this Vector3 value) => new(Mathf.Ceil(value.x), Mathf.Ceil(value.y), Mathf.Ceil(value.z));

    /// <summary> Clamps the vector to the range [min..max]. </summary>
    /// <param name="self">Value.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>A new clamped vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Clamp(this Vector3 self, Vector3 min, Vector3 max) => new(Mathf.Clamp(self.x, min.x, max.x),
                                                                                    Mathf.Clamp(self.y, min.y, max.y),
                                                                                    Mathf.Clamp(self.z, min.z, max.z));

    /// <summary> Clamps the vector to the range [0..1]. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new clamped vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Clamp01(this Vector3 self) => new(Mathf.Clamp01(self.x), Mathf.Clamp01(self.y), Mathf.Clamp01(self.z));

    /// <summary> Rounds the vector down to the nearest whole number. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new rounded vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Floor(this Vector3 self) => new(Mathf.Floor(self.x), Mathf.Floor(self.y), Mathf.Floor(self.z));

    /// <summary> Checks for equality with another vector given a margin of error specified by an epsilon. </summary>
    /// <param name="a">The left-hand side of the equality check.</param>
    /// <param name="b">The right-hand side of the equality check.</param>
    /// <returns>True if the values are equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearlyEquals(this Vector3 a, Vector3 b, float epsilon = MathConstants.Epsilon) => a.x.NearlyEquals(b.x, epsilon) &&
                                                                                                         a.y.NearlyEquals(b.y, epsilon) &&
                                                                                                         a.z.NearlyEquals(b.z, epsilon);

    /// <summary>Rounds the vector to the nearest whole number. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new rounded vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Rounded(this Vector3 self) => new(Mathf.Round(self.x), Mathf.Round(self.y), Mathf.Round(self.z));

    /// <summary> Apply the modulo operator. </summary>
    /// <param name="self">Value.</param>
    /// <returns>A new vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Remainder(this Vector3 self, Vector3 modulus) => new(self.x % modulus.x, self.y % modulus.y, self.z % modulus.z);

    /// <summary> Vector3 to string. </summary>
    /// <param name="self">Value</param>
    /// <returns>string</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString(this Vector3 self) => $"{self.x},{self.y},{self.z}";

    /// <summary> Returns a new vector with the X component replaced. </summary>
    /// <param name="v">Value.</param>
    /// <param name="x">New X value.</param>
    /// <returns>A new vector with the X component replaced.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 WithX(this Vector3 v, float x) => new(x, v.y, v.z);

    /// <summary> Returns a new vector with the Y component replaced. </summary>
    /// <param name="v">Value.</param>
    /// <param name="y">New Y value.</param>
    /// <returns>A new vector with the Y component replaced.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 WithY(this Vector3 v, float y) => new(v.x, y, v.z);

    /// <summary> Returns a new vector with the Z component replaced. </summary>
    /// <param name="v">Value.</param>
    /// <param name="z">New Z value.</param>
    /// <returns>A new vector with the Z component replaced.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 WithZ(this Vector3 v, float z) => new(v.x, v.y, z);

    /// <summary> Returns a new Vector3 with X and Y components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 WithXY(this Vector3 v, float x, float y) => new(x, y, v.z);

    /// <summary> Returns a new Vector3 with X and Z components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 WithXZ(this Vector3 v, float x, float z) => new(x, v.y, z);

    /// <summary> Returns a new Vector3 with Y and Z components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 WithYZ(this Vector3 v, float y, float z) => new(v.x, y, z);

    /// <summary> Returns true if the point is inside a cube. </summary>
    /// <param name="point">The point to check.</param>
    /// <param name="cubePos">Center of the cube.</param>
    /// <param name="cubeSize">Half-size of the cube along each axis.</param>
    /// <returns>True if the point is inside the cube.</returns>
    public static bool IsInsideCube(this Vector3 point, Vector3 cubePos, Vector3 cubeSize)
    {
      float dx = Mathf.Abs(point.x - cubePos.x);
      float dy = Mathf.Abs(point.y - cubePos.y);
      float dz = Mathf.Abs(point.z - cubePos.z);
      return dx <= cubeSize.x && dy <= cubeSize.y && dz <= cubeSize.z;
    }

    /// <summary> Returns true if the Transform position is inside a cube. </summary>
    /// <param name="tr">The transform to check.</param>
    /// <param name="cubePos">Center of the cube.</param>
    /// <param name="cubeSize">Half-size of the cube along each axis.</param>
    /// <returns>True if the transform position is inside the cube.</returns>
    public static bool IsInsideCube(this Transform tr, Vector3 cubePos, Vector3 cubeSize)
      => IsInsideCube(tr.position, cubePos, cubeSize);

    /// <summary> Returns true if the point is inside a sphere (using squared distance). </summary>
    /// <param name="point">The point to check.</param>
    /// <param name="spherePos">Center of the sphere.</param>
    /// <param name="sqrSphereRadius">Squared radius of the sphere.</param>
    /// <returns>True if the point is inside the sphere.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInsideSphereSqr(this Vector3 point, Vector3 spherePos, float sqrSphereRadius)
      => (point - spherePos).sqrMagnitude <= sqrSphereRadius;

    /// <summary> Returns true if the point is inside a sphere. </summary>
    /// <param name="point">The point to check.</param>
    /// <param name="spherePos">Center of the sphere.</param>
    /// <param name="sphereRadius">Radius of the sphere.</param>
    /// <returns>True if the point is inside the sphere.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInsideSphere(this Vector3 point, Vector3 spherePos, float sphereRadius)
      => IsInsideSphereSqr(point, spherePos, sphereRadius * sphereRadius);

    /// <summary> Returns true if the Transform position is inside a sphere. </summary>
    /// <param name="tr">The transform to check.</param>
    /// <param name="spherePos">Center of the sphere.</param>
    /// <param name="sphereRadius">Radius of the sphere.</param>
    /// <returns>True if the transform position is inside the sphere.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInsideSphere(this Transform tr, Vector3 spherePos, float sphereRadius)
      => IsInsideSphere(tr.position, spherePos, sphereRadius);

    /// <summary> Returns the maximum component of the vector. </summary>
    /// <param name="v">Value.</param>
    /// <returns>The maximum component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float MaxComponent(this Vector3 v) => Mathf.Max(v.x, Mathf.Max(v.y, v.z));

    /// <summary> Returns the minimum component of the vector. </summary>
    /// <param name="v">Value.</param>
    /// <returns>The minimum component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float MinComponent(this Vector3 v) => Mathf.Min(v.x, Mathf.Min(v.y, v.z));

    /// <summary> Component-wise multiplication. </summary>
    /// <param name="a">Left-hand side.</param>
    /// <param name="b">Right-hand side.</param>
    /// <returns>The component-wise product.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Multiply(this Vector3 a, Vector3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);

    /// <summary> Component-wise division. </summary>
    /// <param name="a">Left-hand side.</param>
    /// <param name="b">Right-hand side.</param>
    /// <returns>The component-wise quotient.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Divide(this Vector3 a, Vector3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);

    /// <summary> Rotates the point around a pivot. </summary>
    /// <param name="point">The point to rotate.</param>
    /// <param name="pivot">The pivot point.</param>
    /// <param name="eulerAngles">Rotation in Euler angles (degrees).</param>
    /// <returns>The rotated point.</returns>
    public static Vector3 RotateAround(this Vector3 point, Vector3 pivot, Vector3 eulerAngles)
      => Quaternion.Euler(eulerAngles) * (point - pivot) + pivot;

    /// <summary> Sets the magnitude of the vector. </summary>
    /// <param name="v">Value.</param>
    /// <param name="magnitude">The desired magnitude.</param>
    /// <returns>A vector with the same direction but the specified magnitude.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 SetMagnitude(this Vector3 v, float magnitude)
      => v.sqrMagnitude > 0f ? v.normalized * magnitude : Vector3.zero;

    /// <summary> Converts to Vector2 using X and Y components. </summary>
    /// <param name="v">Value.</param>
    /// <returns>A Vector2 with the X and Y components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ToVector2XY(this Vector3 v) => new(v.x, v.y);

    /// <summary> Converts to Vector2 using X and Z components. </summary>
    /// <param name="v">Value.</param>
    /// <returns>A Vector2 with the X and Z components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ToVector2XZ(this Vector3 v) => new(v.x, v.z);

    /// <summary> Returns a random point inside a sphere around the center. </summary>
    /// <param name="center">The center of the sphere.</param>
    /// <param name="radius">The radius of the sphere.</param>
    /// <returns>A random point inside the sphere.</returns>
    public static Vector3 RandomInSphere(this Vector3 center, float radius)
    {
      Vector2 circle = UnityEngine.Random.insideUnitCircle * radius;
      float z = UnityEngine.Random.Range(-radius, radius);

      return center + new Vector3(circle.x, circle.y, z);
    }

    /// <summary> Linear interpolation unclamped. </summary>
    /// <param name="a">Start value.</param>
    /// <param name="b">End value.</param>
    /// <param name="t">Interpolation parameter.</param>
    /// <returns>The interpolated vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 LerpUnclamped(this Vector3 a, Vector3 b, float t) => Vector3.LerpUnclamped(a, b, t);

    /// <summary> Returns true if the vectors are approximately equal. </summary>
    /// <param name="a">The left-hand side of the equality check.</param>
    /// <param name="b">The right-hand side of the equality check.</param>
    /// <param name="tolerance">The maximum allowed distance.</param>
    /// <returns>True if the vectors are approximately equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ApproximatelyEqual(this Vector3 a, Vector3 b, float tolerance = 0.01f)
      => (a - b).sqrMagnitude <= tolerance * tolerance;

    /// <summary> Remaps the vector from one range to another. </summary>
    /// <param name="value">The value to remap.</param>
    /// <param name="from1">Start of source range.</param>
    /// <param name="to1">End of source range.</param>
    /// <param name="from2">Start of destination range.</param>
    /// <param name="to2">End of destination range.</param>
    /// <returns>The remapped vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Remap(this Vector3 value, Vector3 from1, Vector3 to1, Vector3 from2, Vector3 to2)
    {
      Vector3 t = new(
        (to1.x - from1.x) != 0f ? (value.x - from1.x) / (to1.x - from1.x) : 0f,
        (to1.y - from1.y) != 0f ? (value.y - from1.y) / (to1.y - from1.y) : 0f,
        (to1.z - from1.z) != 0f ? (value.z - from1.z) / (to1.z - from1.z) : 0f);
      t = t.Clamp01();
      return new Vector3(
        from2.x + (to2.x - from2.x) * t.x,
        from2.y + (to2.y - from2.y) * t.y,
        from2.z + (to2.z - from2.z) * t.z);
    }

    /// <summary> Remaps the vector from one range to 0-1. </summary>
    /// <param name="value">The value to remap.</param>
    /// <param name="from">Start of source range.</param>
    /// <param name="to">End of source range.</param>
    /// <returns>The remapped vector in 0-1 range.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Remap01(this Vector3 value, Vector3 from, Vector3 to)
    {
      Vector3 t = new(
        (to.x - from.x) != 0f ? (value.x - from.x) / (to.x - from.x) : 0f,
        (to.y - from.y) != 0f ? (value.y - from.y) / (to.y - from.y) : 0f,
        (to.z - from.z) != 0f ? (value.z - from.z) / (to.z - from.z) : 0f);
      return t.Clamp01();
    }

    /// <summary> Remaps the vector unclamped from one range to another. </summary>
    /// <param name="value">The value to remap.</param>
    /// <param name="from1">Start of source range.</param>
    /// <param name="to1">End of source range.</param>
    /// <param name="from2">Start of destination range.</param>
    /// <param name="to2">End of destination range.</param>
    /// <returns>The remapped vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 RemapUnclamped(this Vector3 value, Vector3 from1, Vector3 to1, Vector3 from2, Vector3 to2)
    {
      Vector3 t = new(
        (to1.x - from1.x) != 0f ? (value.x - from1.x) / (to1.x - from1.x) : 0f,
        (to1.y - from1.y) != 0f ? (value.y - from1.y) / (to1.y - from1.y) : 0f,
        (to1.z - from1.z) != 0f ? (value.z - from1.z) / (to1.z - from1.z) : 0f);
      return new Vector3(
        from2.x + (to2.x - from2.x) * t.x,
        from2.y + (to2.y - from2.y) * t.y,
        from2.z + (to2.z - from2.z) * t.z);
    }

    /// <summary> Multiplies the vector by a double scalar. </summary>
    /// <param name="value">The vector to multiply.</param>
    /// <param name="scalar">The scalar multiplier.</param>
    /// <returns>The scaled vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Multiply(this Vector3 value, double scalar)
      => new(value.x * (float)scalar, value.y * (float)scalar, value.z * (float)scalar);

    /// <summary> Returns the average of an array of vectors. </summary>
    /// <param name="vectors">The array of vectors.</param>
    /// <returns>The average vector.</returns>
    public static Vector3 Average(this Vector3[] vectors)
    {
      if (vectors == null || vectors.Length == 0)
        return Vector3.zero;

      Vector3 sum = Vector3.zero;
      for (int i = 0; i < vectors.Length; i++)
        sum += vectors[i];

      return sum / vectors.Length;
    }

    /// <summary> Returns XY components as Vector2. </summary>
    /// <param name="value">The vector.</param>
    /// <returns>A Vector2 with X and Y components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 XY(this Vector3 value) => new(value.x, value.y);

    /// <summary> Returns XZ components as Vector2. </summary>
    /// <param name="value">The vector.</param>
    /// <returns>A Vector2 with X and Z components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 XZ(this Vector3 value) => new(value.x, value.z);

    /// <summary> Returns YX components as Vector2. </summary>
    /// <param name="value">The vector.</param>
    /// <returns>A Vector2 with Y and X components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 YX(this Vector3 value) => new(value.y, value.x);

    /// <summary> Returns ZX components as Vector2. </summary>
    /// <param name="value">The vector.</param>
    /// <returns>A Vector2 with Z and X components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ZX(this Vector3 value) => new(value.z, value.x);

    /// <summary> Returns YXZ swizzle. </summary>
    /// <param name="value">The vector.</param>
    /// <returns>A Vector3 with Y, X, Z component order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 YXZ(this Vector3 value) => new(value.y, value.x, value.z);

    /// <summary> Adjusts X component by adding value. </summary>
    /// <param name="value">The vector.</param>
    /// <param name="x">Value to add to X.</param>
    /// <returns>A new vector with adjusted X.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 AdjX(this Vector3 value, float x) => new(value.x + x, value.y, value.z);

    /// <summary> Adjusts Y component by adding value. </summary>
    /// <param name="value">The vector.</param>
    /// <param name="y">Value to add to Y.</param>
    /// <returns>A new vector with adjusted Y.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 AdjY(this Vector3 value, float y) => new(value.x, value.y + y, value.z);

    /// <summary> Adjusts Z component by adding value. </summary>
    /// <param name="value">The vector.</param>
    /// <param name="z">Value to add to Z.</param>
    /// <returns>A new vector with adjusted Z.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 AdjZ(this Vector3 value, float z) => new(value.x, value.y, value.z + z);

    /// <summary> Adjusts X and Y components by adding values. </summary>
    /// <param name="value">The vector.</param>
    /// <param name="x">Value to add to X.</param>
    /// <param name="y">Value to add to Y.</param>
    /// <returns>A new vector with adjusted X and Y.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 AdjXY(this Vector3 value, float x, float y) => new(value.x + x, value.y + y, value.z);

    /// <summary> Adjusts X and Z components by adding values. </summary>
    /// <param name="value">The vector.</param>
    /// <param name="x">Value to add to X.</param>
    /// <param name="z">Value to add to Z.</param>
    /// <returns>A new vector with adjusted X and Z.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 AdjXZ(this Vector3 value, float x, float z) => new(value.x + x, value.y, value.z + z);

    /// <summary> Adjusts Y and Z components by adding values. </summary>
    /// <param name="value">The vector.</param>
    /// <param name="y">Value to add to Y.</param>
    /// <param name="z">Value to add to Z.</param>
    /// <returns>A new vector with adjusted Y and Z.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 AdjYZ(this Vector3 value, float y, float z) => new(value.x, value.y + y, value.z + z);

    /// <summary> Adjusts all components by adding values. </summary>
    /// <param name="value">The vector.</param>
    /// <param name="x">Value to add to X.</param>
    /// <param name="y">Value to add to Y.</param>
    /// <param name="z">Value to add to Z.</param>
    /// <returns>A new vector with all components adjusted.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 AdjXYZ(this Vector3 value, float x, float y, float z) => new(value.x + x, value.y + y, value.z + z);

    /// <summary> Adjusts all components by the same value. </summary>
    /// <param name="value">The vector.</param>
    /// <param name="all">Value to add to all components.</param>
    /// <returns>A new vector with all components adjusted.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 AdjAll(this Vector3 value, float all) => new(value.x + all, value.y + all, value.z + all);

    /// <summary> Rotates the vector around the Y axis by the given angle in degrees. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 RotateY(this Vector3 vector, float angle) => Quaternion.Euler(0.0f, angle, 0.0f) * vector;

    /// <summary> Returns true if the point is inside the XZ polygon defined by the points. </summary>
    public static bool IsInsideXZ(this Vector3 position, Vector3[] points)
    {
      bool isInside = false;

      for (int pointId = 0, endPointId = points.Length - 1; pointId < points.Length; endPointId = pointId++)
      {
        if (points[pointId].z > position.z != points[endPointId].z > position.z
            && position.x < (points[endPointId].x - points[pointId].x) * (position.z - points[pointId].z) / (points[endPointId].z - points[pointId].z) + points[pointId].x)
          isInside = !isInside;
      }

      return isInside;
    }

    /// <summary> Returns the average of the vector components. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Median(this Vector3 vector) => (vector.x + vector.y + vector.z) / 3.0f;

    /// <summary> Returns the average vector of an array. </summary>
    public static Vector3 Median(this Vector3[] vectors)
    {
      Vector3 result = Vector3.zero;

      for (int i = 0; i < vectors.Length; i++)
        result += vectors[i];

      return vectors.Length > 0 ? result / vectors.Length : Vector3.zero;
    }

    /// <summary> Returns the average X component of an array. </summary>
    public static float MedianX(this Vector3[] vectors)
    {
      float result = 0.0f;

      for (int i = 0; i < vectors.Length; i++)
        result += vectors[i].x;

      return vectors.Length > 0 ? result / vectors.Length : 0.0f;
    }

    /// <summary> Returns the average Y component of an array. </summary>
    public static float MedianY(this Vector3[] vectors)
    {
      float result = 0.0f;

      for (int i = 0; i < vectors.Length; i++)
        result += vectors[i].y;

      return vectors.Length > 0 ? result / vectors.Length : 0.0f;
    }

    /// <summary> Returns the average Z component of an array. </summary>
    public static float MedianZ(this Vector3[] vectors)
    {
      float result = 0.0f;

      for (int i = 0; i < vectors.Length; i++)
        result += vectors[i].z;

      return vectors.Length > 0 ? result / vectors.Length : 0.0f;
    }

    /// <summary> Rotates a point around a center using euler angles in degrees. </summary>
    public static Vector3 Rotate(this Vector3 point, Vector3 center, Vector3 eulerAngles)
    {
      point = RotateAroundX(point, center, eulerAngles.x * Mathf.Deg2Rad);
      point = RotateAroundY(point, center, eulerAngles.y * Mathf.Deg2Rad);
      point = RotateAroundZ(point, center, eulerAngles.z * Mathf.Deg2Rad);
      return point;
    }

    /// <summary> Tries to calculate the average of a list of vectors. </summary>
    public static bool TryCalculateAverage(this List<Vector3> positions, out Vector3 result)
    {
      result = Vector3.zero;

      if (positions.Count == 0)
        return false;

      for (int i = 0; i < positions.Count; i++)
        result += positions[i];

      result /= positions.Count;
      return true;
    }

    /// <summary> Removes positions blocked by a linecast from the origin. </summary>
    public static List<Vector3> FilterLineCast(this List<Vector3> positions, Vector3 from, int layerMask)
    {
      for (int i = positions.Count - 1; i >= 0; i--)
      {
        if (Physics.Linecast(from, positions[i], layerMask))
          positions.RemoveAt(i);
      }

      return positions;
    }

    /// <summary> Removes positions that are near any position in the filter collection. </summary>
    public static List<Vector3> FilterNearest<T>(this List<Vector3> positions, T filter, float distance = 0.1f) where T : IEnumerable<Vector3>
    {
      for (int i = positions.Count - 1; i >= 0; i--)
      {
        if (filter.IsContainNearest(positions[i], distance))
          positions.RemoveAt(i);
      }

      return positions;
    }

    /// <summary> Returns true if any position in the collection is within distance of the target. </summary>
    public static bool IsContainNearest<T>(this T positions, Vector3 position, float distance = 0.1f) where T : IEnumerable<Vector3>
    {
      foreach (Vector3 target in positions)
      {
        if (Vector3.Distance(target, position) < distance)
          return true;
      }

      return false;
    }

    /// <summary> Returns true if any position in the collection is within distance of the target. </summary>
    public static bool TryFindNearest<T>(this T positions, Vector3 position, float distance) where T : IEnumerable<Vector3>
    {
      foreach (Vector3 target in positions)
      {
        if (Vector3.Distance(position, target) < distance)
          return true;
      }

      return false;
    }

    /// <summary> Returns a new array with value added to each element. </summary>
    public static Vector3[] Add(this Vector3[] array, Vector3 value)
    {
      Vector3[] result = new Vector3[array.Length];

      for (int i = 0; i < array.Length; i++)
        result[i] = array[i] + value;

      return result;
    }

    private static Vector3 RotateAroundX(Vector3 point, Vector3 center, float angle)
    {
      float sin = Mathf.Sin(angle);
      float cos = Mathf.Cos(angle);
      float xx = point.x - center.x;
      float yy = point.y - center.y;
      float zz = point.z - center.z;

      point.x = center.x + xx;
      point.y = center.y + yy * cos - zz * sin;
      point.z = center.z + yy * sin + zz * cos;
      return point;
    }

    private static Vector3 RotateAroundY(Vector3 point, Vector3 center, float angle)
    {
      float sin = Mathf.Sin(angle);
      float cos = Mathf.Cos(angle);
      float xx = point.x - center.x;
      float yy = point.y - center.y;
      float zz = point.z - center.z;

      point.x = center.x + xx * cos + zz * sin;
      point.y = center.y + yy;
      point.z = center.z - xx * sin + zz * cos;
      return point;
    }

    private static Vector3 RotateAroundZ(Vector3 point, Vector3 center, float angle)
    {
      float sin = Mathf.Sin(angle);
      float cos = Mathf.Cos(angle);
      float xx = point.x - center.x;
      float yy = point.y - center.y;
      float zz = point.z - center.z;

      point.x = center.x + xx * cos - yy * sin;
      point.y = center.y + xx * sin + yy * cos;
      point.z = center.z + zz;
      return point;
    }
  }
}
