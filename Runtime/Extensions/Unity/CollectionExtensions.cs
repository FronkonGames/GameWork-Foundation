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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Collection extensions. </summary>
  public static class CollectionExtensions
  {
    /// <summary> Retrieves an element from a list assuming it's a 2D map of given width. </summary>
    /// <param name="self">List representing the map.</param>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    /// <param name="width">Width of the map.</param>
    /// <returns>Element at the given 2D position.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetXY<T>(this IReadOnlyList<T> self, int x, int y, int width)
      => self[y * width + x];

    /// <summary> Sets an element in a list assuming it's a 2D map of given width. </summary>
    /// <param name="self">List representing the map.</param>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    /// <param name="width">Width of the map.</param>
    /// <param name="value">Value to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetXY<T>(this IList<T> self, int x, int y, int width, T value)
      => self[y * width + x] = value;
  }
}
