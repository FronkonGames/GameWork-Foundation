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
  /// <summary> Vector3Int extensions. </summary>
  public static class Vector3IntExtensions
  {
    /// <summary> Deconstructs a Vector3Int into x, y, and z. </summary>
    /// <param name="vector">Value.</param>
    /// <param name="x">X component.</param>
    /// <param name="y">Y component.</param>
    /// <param name="z">Z component.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deconstruct(this Vector3Int vector, out int x, out int y, out int z)
    {
      x = vector.x;
      y = vector.y;
      z = vector.z;
    }

    /// <summary> Returns a new Vector3Int with the X component replaced. </summary>
    /// <param name="vector">Value.</param>
    /// <param name="x">New X value.</param>
    /// <returns>A new Vector3Int with the X component replaced.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int WithX(this Vector3Int vector, int x) => new(x, vector.y, vector.z);

    /// <summary> Returns a new Vector3Int with the Y component replaced. </summary>
    /// <param name="vector">Value.</param>
    /// <param name="y">New Y value.</param>
    /// <returns>A new Vector3Int with the Y component replaced.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int WithY(this Vector3Int vector, int y) => new(vector.x, y, vector.z);

    /// <summary> Returns a new Vector3Int with the Z component replaced. </summary>
    /// <param name="vector">Value.</param>
    /// <param name="z">New Z value.</param>
    /// <returns>A new Vector3Int with the Z component replaced.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int WithZ(this Vector3Int vector, int z) => new(vector.x, vector.y, z);

    /// <summary> Swizzles to Vector2Int as (x, y). </summary>
    /// <param name="vector">Value.</param>
    /// <returns>A Vector2Int with (x, y).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int SwizzleXY(this Vector3Int vector) => new(vector.x, vector.y);

    /// <summary> Swizzles to Vector2Int as (x, z). </summary>
    /// <param name="vector">Value.</param>
    /// <returns>A Vector2Int with (x, z).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int SwizzleXZ(this Vector3Int vector) => new(vector.x, vector.z);

    /// <summary> Swizzles to Vector2Int as (y, z). </summary>
    /// <param name="vector">Value.</param>
    /// <returns>A Vector2Int with (y, z).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int SwizzleYZ(this Vector3Int vector) => new(vector.y, vector.z);

    /// <summary> Converts Vector3Int to Vector3 (float). </summary>
    /// <param name="vector">Value.</param>
    /// <returns>A Vector3 with the same components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ToVector3(this Vector3Int vector) => new(vector.x, vector.y, vector.z);

    /// <summary> Returns the manhattan distance to another point. </summary>
    /// <param name="a">The first point.</param>
    /// <param name="b">The second point.</param>
    /// <returns>The manhattan distance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ManhattanDistance(this Vector3Int a, Vector3Int b)
      => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);

    /// <summary> Lexicographic comparison (x, then y, then z). </summary>
    public static int CompareTo(this Vector3Int value, Vector3Int other)
    {
      int result = value.x.CompareTo(other.x);
      if (result != 0)
        return result;

      result = value.y.CompareTo(other.y);
      return result != 0 ? result : value.z.CompareTo(other.z);
    }

    /// <summary> Returns YXZ swizzle. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int SwizzleYXZ(this Vector3Int value) => new(value.y, value.x, value.z);

    /// <summary> Returns a new Vector3Int with X and Y components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int WithXY(this Vector3Int vector, int x, int y) => new(x, y, vector.z);

    /// <summary> Returns a new Vector3Int with X and Z components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int WithXZ(this Vector3Int vector, int x, int z) => new(x, vector.y, z);

    /// <summary> Returns a new Vector3Int with Y and Z components replaced. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int WithYZ(this Vector3Int vector, int y, int z) => new(vector.x, y, z);

    /// <summary> Adjusts X component by adding value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int AdjX(this Vector3Int value, int x) => new(value.x + x, value.y, value.z);

    /// <summary> Adjusts Y component by adding value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int AdjY(this Vector3Int value, int y) => new(value.x, value.y + y, value.z);

    /// <summary> Adjusts Z component by adding value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int AdjZ(this Vector3Int value, int z) => new(value.x, value.y, value.z + z);

    /// <summary> Adjusts X and Y components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int AdjXY(this Vector3Int value, int x, int y) => new(value.x + x, value.y + y, value.z);

    /// <summary> Adjusts X and Z components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int AdjXZ(this Vector3Int value, int x, int z) => new(value.x + x, value.y, value.z + z);

    /// <summary> Adjusts Y and Z components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int AdjYZ(this Vector3Int value, int y, int z) => new(value.x, value.y + y, value.z + z);

    /// <summary> Adjusts all components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int AdjXYZ(this Vector3Int value, int x, int y, int z) => new(value.x + x, value.y + y, value.z + z);

    /// <summary> Adjusts all components by the same value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int AdjAll(this Vector3Int value, int all) => new(value.x + all, value.y + all, value.z + all);
  }
}
