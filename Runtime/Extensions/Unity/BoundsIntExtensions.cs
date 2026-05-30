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
  /// <summary> BoundsInt extensions. </summary>
  public static class BoundsIntExtensions
  {
    /// <summary> Returns true if the position is within the bounds (x and y only). </summary>
    /// <param name="self">Bounds.</param>
    /// <param name="position">Position to check.</param>
    /// <returns>True if the position is inside the bounds on x and y axes.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains2D(this BoundsInt self, Vector2Int position)
      => position.x >= self.xMin && position.x < self.xMax &&
         position.y >= self.yMin && position.y < self.yMax;

    /// <summary> Returns true if the position is within the bounds (x and y only). </summary>
    /// <param name="self">Bounds.</param>
    /// <param name="position">Position to check.</param>
    /// <returns>True if the position is inside the bounds on x and y axes.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains2D(this BoundsInt self, Vector3Int position)
      => position.x >= self.xMin && position.x < self.xMax &&
         position.y >= self.yMin && position.y < self.yMax;
  }
}
