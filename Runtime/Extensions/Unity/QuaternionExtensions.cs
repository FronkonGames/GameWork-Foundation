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
  /// <summary> Quaternion extensions. </summary>
  public static class QuaternionExtensions
  {
    /// <summary> Quaternion magnitude. </summary>
    /// <param name="self">Value</param>
    /// <returns>Magnitude</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Magnitude(this Quaternion self) => Mathf.Sqrt(self.x * self.x + self.y * self.y + self.z * self.z + self.w * self.w);

    /// <summary> Checks for equality with another quaternion given a margin of error specified by an epsilon. </summary>
    /// <param name="a">The left-hand side of the equality check.</param>
    /// <param name="b">The right-hand side of the equality check.</param>
    /// <returns>True if the values are equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NearlyEquals(this Quaternion a, Quaternion b, float epsilon = MathConstants.Epsilon) =>
      1.0f - Mathf.Abs(Quaternion.Dot(a, b)) < epsilon;

    /// <summary> Quaternion to string. </summary>
    /// <param name="self">Value</param>
    /// <returns>string</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToString(this Quaternion self) => $"{self.x},{self.y},{self.z},{self.w}";

    /// <summary> Returns the negated quaternion (inverted rotation). </summary>
    /// <param name="quaternion">Value</param>
    /// <returns>Negated quaternion</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Negative(this Quaternion quaternion) => new Quaternion(-quaternion.x, -quaternion.y, -quaternion.z, -quaternion.w);

    /// <summary> Returns the quaternion with only the rotation part (no scale). </summary>
    /// <param name="quaternion">Value</param>
    /// <returns>Normalized quaternion</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Normalized(this Quaternion quaternion) => Quaternion.Normalize(quaternion);

    /// <summary> Returns the angle in degrees to another quaternion. </summary>
    /// <param name="from">Source quaternion</param>
    /// <param name="to">Target quaternion</param>
    /// <returns>Angle in degrees</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float AngleTo(this Quaternion from, Quaternion to) => Quaternion.Angle(from, to);

    /// <summary> Returns true if the quaternion is approximately identity. </summary>
    /// <param name="quaternion">Value</param>
    /// <param name="tolerance">Maximum allowed deviation</param>
    /// <returns>True if approximately identity</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIdentity(this Quaternion quaternion, float tolerance = 0.001f) =>
      Mathf.Abs(quaternion.x) < tolerance && Mathf.Abs(quaternion.y) < tolerance &&
      Mathf.Abs(quaternion.z) < tolerance && Mathf.Abs(quaternion.w - 1.0f) < tolerance;

    /// <summary> Returns the euler angles as a Vector3. </summary>
    /// <param name="quaternion">Value</param>
    /// <returns>Euler angles in degrees</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 ToEulerVector(this Quaternion quaternion) => quaternion.eulerAngles;
  }
}
