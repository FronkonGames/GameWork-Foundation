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
  /// <summary> Vector2Int extensions. </summary>
  public static class Vector2IntExtensions
  {
    /// <summary> Deconstructs a Vector2Int into x and y. </summary>
    /// <param name="vector">Value.</param>
    /// <param name="x">X component.</param>
    /// <param name="y">Y component.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deconstruct(this Vector2Int vector, out int x, out int y)
    {
      x = vector.x;
      y = vector.y;
    }

    /// <summary> Returns a new Vector2Int with the X component replaced. </summary>
    /// <param name="vector">Value.</param>
    /// <param name="x">New X value.</param>
    /// <returns>A new Vector2Int with the X component replaced.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int WithX(this Vector2Int vector, int x) => new(x, vector.y);

    /// <summary> Returns a new Vector2Int with the Y component replaced. </summary>
    /// <param name="vector">Value.</param>
    /// <param name="y">New Y value.</param>
    /// <returns>A new Vector2Int with the Y component replaced.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int WithY(this Vector2Int vector, int y) => new(vector.x, y);

    /// <summary> Swizzles to Vector3Int as (x, 0, y). </summary>
    /// <param name="vector">Value.</param>
    /// <returns>A Vector3Int with (x, 0, y).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int SwizzleXZ(this Vector2Int vector) => new(vector.x, 0, vector.y);

    /// <summary> Swizzles to Vector3Int as (x, y, 0). </summary>
    /// <param name="vector">Value.</param>
    /// <returns>A Vector3Int with (x, y, 0).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int SwizzleXY(this Vector2Int vector) => new(vector.x, vector.y, 0);

    /// <summary> Swizzles to Vector3Int as (0, x, y). </summary>
    /// <param name="vector">Value.</param>
    /// <returns>A Vector3Int with (0, x, y).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int SwizzleYZ(this Vector2Int vector) => new(0, vector.x, vector.y);

    /// <summary> Converts Vector2Int to Vector2 (float). </summary>
    /// <param name="vector">Value.</param>
    /// <returns>A Vector2 with the same components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ToVector2(this Vector2Int vector) => new(vector.x, vector.y);

    /// <summary> Returns the manhattan distance to another point. </summary>
    /// <param name="a">The first point.</param>
    /// <param name="b">The second point.</param>
    /// <returns>The manhattan distance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ManhattanDistance(this Vector2Int a, Vector2Int b)
      => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);

    /// <summary> Lexicographic comparison (x, then y). </summary>
    public static int CompareTo(this Vector2Int value, Vector2Int other)
    {
      int result = value.x.CompareTo(other.x);
      return result != 0 ? result : value.y.CompareTo(other.y);
    }

    /// <summary> Swizzles to Vector3Int as (x, y, z). </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int ToVector3Int(this Vector2Int value, int z = 0) => new(value.x, value.y, z);

    /// <summary> Returns YX swizzle. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int SwizzleYX(this Vector2Int value) => new(value.y, value.x);

    /// <summary> Swizzles to Vector3Int as (y, x, z). </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3Int SwizzleYXZ(this Vector2Int value, int z = 0) => new(value.y, value.x, z);

    /// <summary> Adjusts X component by adding value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int AdjX(this Vector2Int value, int x) => new(value.x + x, value.y);

    /// <summary> Adjusts Y component by adding value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int AdjY(this Vector2Int value, int y) => new(value.x, value.y + y);

    /// <summary> Adjusts X and Y components by adding values. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int AdjXY(this Vector2Int value, int x, int y) => new(value.x + x, value.y + y);

    /// <summary> Adjusts all components by the same value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2Int AdjAll(this Vector2Int value, int all) => new(value.x + all, value.y + all);
  }
}
