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
  /// <summary> Math utilities. </summary>
  public static partial class MathUtils
  {
    /// <summary> Angle to direction in the XY plane. </summary>
    /// <param name="radian">Radian angle</param>
    /// <returns>Direction</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 AngToDir(float radian) => new(Cos(radian), Sin(radian));

    /// <summary> Clamps an angle in degrees, accounting for 360° wrap-around. </summary>
    /// <param name="angle"> Angle in degrees. </param>
    /// <param name="min"> Minimum angle in degrees. </param>
    /// <param name="max"> Maximum angle in degrees. </param>
    /// <returns> Clamped angle in degrees. </returns>
    public static float ClampAngle(float angle, float min, float max)
    {
      float start = (min + max) * 0.5f - 180.0f;
      float floor = Mathf.FloorToInt((angle - start) / 360.0f) * 360.0f;

      return Mathf.Clamp(angle, min + floor, max + floor);
    }

    /// <summary> Normalizes then clamps an angle in degrees within the given range. </summary>
    /// <param name="angle"> Angle in degrees. </param>
    /// <param name="min"> Minimum angle in degrees. </param>
    /// <param name="max"> Maximum angle in degrees. </param>
    /// <param name="normalizeMin"> Normalization lower bound in degrees. </param>
    /// <param name="normalizeMax"> Normalization upper bound in degrees. </param>
    /// <returns> Clamped angle in degrees. </returns>
    public static float ClampAngleNormalized(float angle, float min, float max, float normalizeMin = -180.0f, float normalizeMax = 180.0f)
    {
      float range = normalizeMax - normalizeMin;
      angle = ((angle - normalizeMin) % range + range) % range + normalizeMin;

      return Mathf.Clamp(angle, min, max);
    }
  }
}
