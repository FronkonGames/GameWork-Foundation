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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> IList extensions for 2D/3D grid access. </summary>
  public static class IListExtensions
  {
    /// <summary> Returns an element from a 1D list as if it were a 2D grid. </summary>
    /// <param name="source"> The flat list. </param>
    /// <param name="x"> Column index. </param>
    /// <param name="y"> Row index. </param>
    /// <param name="width"> Grid width. </param>
    /// <param name="height"> Grid height. </param>
    /// <returns> The element at (x, y). </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T At2D<T>(this IList<T> source, int x, int y, int width, int height)
    {
      if (x < 0 || x >= width || y < 0 || y >= height)
        throw new IndexOutOfRangeException();

      return source[y * width + x];
    }

    /// <summary> Returns an element from a 1D list as if it were a 3D grid. </summary>
    /// <param name="source"> The flat list. </param>
    /// <param name="x"> Column index. </param>
    /// <param name="y"> Row index. </param>
    /// <param name="z"> Depth index. </param>
    /// <param name="width"> Grid width. </param>
    /// <param name="height"> Grid height. </param>
    /// <param name="depth"> Grid depth. </param>
    /// <returns> The element at (x, y, z). </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T At3D<T>(this IList<T> source, int x, int y, int z, int width, int height, int depth)
    {
      if (x < 0 || x >= width || y < 0 || y >= height || z < 0 || z >= depth)
        throw new IndexOutOfRangeException();

      return source[(z * height + y) * width + x];
    }

    /// <summary> Tries to get an element from a 2D grid. Returns false if out of bounds. </summary>
    /// <param name="source"> The flat list. </param>
    /// <param name="x"> Column index. </param>
    /// <param name="y"> Row index. </param>
    /// <param name="width"> Grid width. </param>
    /// <param name="height"> Grid height. </param>
    /// <param name="value"> The element at (x, y) if in bounds; default otherwise. </param>
    /// <returns> True if the element was retrieved; false if out of bounds. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryAt2D<T>(this IList<T> source, int x, int y, int width, int height, out T value)
    {
      if (x < 0 || x >= width || y < 0 || y >= height)
      {
        value = default;
        return false;
      }

      value = source[y * width + x];
      return true;
    }

    /// <summary> Tries to get an element from a 3D grid. Returns false if out of bounds. </summary>
    /// <param name="source"> The flat list. </param>
    /// <param name="x"> Column index. </param>
    /// <param name="y"> Row index. </param>
    /// <param name="z"> Depth index. </param>
    /// <param name="width"> Grid width. </param>
    /// <param name="height"> Grid height. </param>
    /// <param name="depth"> Grid depth. </param>
    /// <param name="value"> The element at (x, y, z) if in bounds; default otherwise. </param>
    /// <returns> True if the element was retrieved; false if out of bounds. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryAt3D<T>(this IList<T> source, int x, int y, int z, int width, int height, int depth, out T value)
    {
      if (x < 0 || x >= width || y < 0 || y >= height || z < 0 || z >= depth)
      {
        value = default;
        return false;
      }

      value = source[(z * height + y) * width + x];
      return true;
    }

    /// <summary> Sets an element in a 1D list as if it were a 2D grid. </summary>
    /// <param name="source"> The flat list. </param>
    /// <param name="x"> Column index. </param>
    /// <param name="y"> Row index. </param>
    /// <param name="width"> Grid width. </param>
    /// <param name="height"> Grid height. </param>
    /// <param name="value"> Value to set. </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Set2D<T>(this IList<T> source, int x, int y, int width, int height, T value)
    {
      if (x < 0 || x >= width || y < 0 || y >= height)
        throw new IndexOutOfRangeException();

      source[y * width + x] = value;
    }

    /// <summary> Sets an element in a 1D list as if it were a 3D grid. </summary>
    /// <param name="source"> The flat list. </param>
    /// <param name="x"> Column index. </param>
    /// <param name="y"> Row index. </param>
    /// <param name="z"> Depth index. </param>
    /// <param name="width"> Grid width. </param>
    /// <param name="height"> Grid height. </param>
    /// <param name="depth"> Grid depth. </param>
    /// <param name="value"> Value to set. </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Set3D<T>(this IList<T> source, int x, int y, int z, int width, int height, int depth, T value)
    {
      if (x < 0 || x >= width || y < 0 || y >= height || z < 0 || z >= depth)
        throw new IndexOutOfRangeException();

      source[(z * height + y) * width + x] = value;
    }
  }
}
