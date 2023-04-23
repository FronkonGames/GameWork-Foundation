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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Quaternion extensions. </summary>
  public static class QuaternionExtensions
  {
    /// <summary> Quaternion magnitude. </summary>
    /// <param name="self">Value</param>
    /// <returns>Magnitude</returns>
    public static float Magnitude(this Quaternion self) => Mathf.Sqrt(self.x * self.x + self.y * self.y + self.z * self.z + self.w * self.w);

    /// <summary> Checks for equality with another quaternion given a margin of error specified by an epsilon. </summary>
    /// <param name="a">The left-hand side of the equality check.</param>
    /// <param name="b">The right-hand side of the equality check.</param>
    /// <returns>True if the values are equal.</returns>
    public static bool NearlyEquals(this Quaternion a, Quaternion b, float epsilon = MathConstants.Epsilon) =>
      1.0f - Mathf.Abs(Quaternion.Dot(a, b)) < epsilon;

    /// <summary> Quaternion to string. </summary>
    /// <param name="self">Value</param>
    /// <returns>string</returns>
    public static string ToString(this Quaternion self) => $"{self.x},{self.y},{self.z},{self.w}";
  }
}
