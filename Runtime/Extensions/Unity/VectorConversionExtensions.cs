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
  /// <summary> Vector conversion extensions between System.Numerics and UnityEngine. </summary>
  public static class VectorConversionExtensions
  {
    /// <summary> Converts System.Numerics.Vector2 to Unity Vector2. </summary>
    /// <param name="vector">The System.Numerics vector to convert.</param>
    /// <returns>The equivalent Unity Vector2.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ToUnityVector(this System.Numerics.Vector2 vector) => new(vector.X, vector.Y);

    /// <summary> Converts Unity Vector2 to System.Numerics.Vector2. </summary>
    /// <param name="vector">The Unity vector to convert.</param>
    /// <returns>The equivalent System.Numerics.Vector2.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.Vector2 ToSystemVector(this Vector2 vector) => new(vector.x, vector.y);

    /// <summary> Converts System.Numerics.Vector3 to Unity Vector3. </summary>
    /// <param name="vector">The System.Numerics vector to convert.</param>
    /// <returns>The equivalent Unity Vector3.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ToUnityVector(this System.Numerics.Vector3 vector) => new(vector.X, vector.Y, vector.Z);

    /// <summary> Converts Unity Vector3 to System.Numerics.Vector3. </summary>
    /// <param name="vector">The Unity vector to convert.</param>
    /// <returns>The equivalent System.Numerics.Vector3.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static System.Numerics.Vector3 ToSystemVector(this Vector3 vector) => new(vector.x, vector.y, vector.z);
  }
}
