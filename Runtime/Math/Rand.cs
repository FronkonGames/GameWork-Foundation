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
using UnityRandom = UnityEngine.Random;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Random functions. </summary>
  public static class Rand
  {
    // Int

    /// <summary> A random int within [min..max] (range is inclusive). </summary>
    /// <param name="min">Minimum</param>
    /// <param name="max">Maximum</param>
    /// <returns>Number</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Range(int min, int max) => UnityRandom.Range(min, max);

    /// <summary> Four-sided die. </summary>
    /// <returns> Between 1 and 4. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int D4() => Range(1, 4);

    /// <summary> Six-sided dice. </summary>
    /// <returns> Between 1 and 6. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int D6() => Range(1, 6);

    /// <summary> Ten-sided die. </summary>
    /// <returns> Between 1 and 10. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int D10() => Range(1, 10);

    /// <summary> Twenty-sided die. </summary>
    /// <returns> Between 1 and 20. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int D20() => Range(1, 20);

    /// <summary> Hundred-sided die. </summary>
    /// <returns> Between 1 and 100. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int D100() => Range(1, 100);

    // 1D

    /// <summary>Random float within [0 .. 1]</summary>
    public static float Value => UnityRandom.value;

    /// <summary> Positive or negative at 50% </summary>
    public static float Sign => Value > 0.5f ? 1.0f : -1.0f;

    /// <summary> One dimension direction at 50% </summary>
    public static float Direction1D => Sign;

    /// <summary> A random float within [min..max] (range is inclusive). </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Range(float min, float max) => UnityRandom.Range(min, max);

    // 2D

    /// <summary> Random unit Vector2 </summary>
    public static Vector2 OnUnitCircle => MathUtils.AngToDir(Value * MathConstants.Tau);

    /// <summary> Random unit 2D direction </summary>
    public static Vector2 Direction2D => OnUnitCircle;

    /// <summary> A random point inside or on a circle with radius 1 </summary>
    public static Vector2 InUnitCircle => UnityRandom.insideUnitCircle;

    /// <summary> A random point inside or on a square with sides 1 </summary>
    public static Vector2 InUnitSquare => new(Value, Value);

    // 3D

    /// <summary> A random point on the surface of a sphere with radius 1 </summary>
    public static Vector3 OnUnitSphere => UnityRandom.onUnitSphere;

    /// <summary> Random unit 3D direction </summary>
    public static Vector3 Direction3D => OnUnitSphere;

    /// <summary> A random point inside or on a sphere with radius 1 </summary>
    public static Vector3 InUnitSphere => UnityRandom.insideUnitSphere;

    /// <summary> A random point inside or on a cube with sides 1 </summary>
    public static Vector3 InUnitCube => new(Value, Value, Value);

    // 2D orientation

    /// <summary>Returns a random angle in radians from 0 to TAU</summary>
    public static float Angle => Value * MathConstants.Tau;

    // 3D Orientation

    /// <summary>Returns a random uniformly distributed rotation</summary>
    public static Quaternion Rotation => UnityRandom.rotationUniform;

    /// <summary> Picks a random index using the given weights. </summary>
    /// <param name="weights"> Non-negative weights, one per index. </param>
    /// <returns> Selected index. </returns>
    public static int PickWeighted(float[] weights)
    {
      if (weights == null || weights.Length == 0)
        throw new ArgumentException("Weights must not be null or empty.");

      float totalWeight = 0.0f;

      for (int i = 0; i < weights.Length; i++)
        totalWeight += weights[i];

      if (totalWeight <= 0.0f)
        throw new ArgumentException("Total weight must be greater than zero.");

      float randomPoint = Value * totalWeight;

      for (int i = 0; i < weights.Length; i++)
      {
        if (randomPoint < weights[i])
          return i;

        randomPoint -= weights[i];
      }

      return weights.Length - 1;
    }

    /// <summary> Picks a random index using the given weights. </summary>
    /// <param name="weights"> Non-negative weights, one per index. </param>
    /// <returns> Selected index. </returns>
    public static int PickWeighted(IList<float> weights)
    {
      if (weights == null || weights.Count == 0)
        throw new ArgumentException("Weights must not be null or empty.");

      float totalWeight = 0.0f;

      for (int i = 0; i < weights.Count; i++)
        totalWeight += weights[i];

      if (totalWeight <= 0.0f)
        throw new ArgumentException("Total weight must be greater than zero.");

      float randomPoint = Value * totalWeight;

      for (int i = 0; i < weights.Count; i++)
      {
        if (randomPoint < weights[i])
          return i;

        randomPoint -= weights[i];
      }

      return weights.Count - 1;
    }

    /// <summary> Returns a normally distributed random value (Box-Muller). </summary>
    /// <param name="mean"> Mean of the distribution. </param>
    /// <param name="stdDev"> Standard deviation. </param>
    /// <returns> Random sample. </returns>
    public static float NextNormal(float mean, float stdDev)
    {
      double u1 = 1.0 - UnityRandom.value;
      double u2 = 1.0 - UnityRandom.value;
      double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

      return (float)(mean + stdDev * randStdNormal);
    }

    /// <summary> Returns true when a random value in [0, denominator] is less than or equal to numerator. </summary>
    /// <param name="numerator"> Success threshold. </param>
    /// <param name="denominator"> Range maximum. </param>
    /// <returns> True on success. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Chance(float numerator, float denominator) => Range(0.0f, denominator) <= numerator;

    /// <summary> Returns true approximately percentage percent of the time. </summary>
    /// <param name="percentage"> Success rate from 0 to 100. </param>
    /// <returns> True on success. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ChancePercent(float percentage) => Chance(percentage, 100.0f);

    /// <summary> Returns a random value between a and b. </summary>
    /// <param name="a"> Start value. </param>
    /// <param name="b"> End value. </param>
    /// <returns> Interpolated random value. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Lerp(float a, float b) => Mathf.Lerp(a, b, Value);

    /// <summary> Returns a random value between a and b. </summary>
    /// <param name="range"> Range as (a, b). </param>
    /// <returns> Interpolated random value. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Lerp(Vector2 range) => Lerp(range.x, range.y);

    /// <summary> Returns a random vector between a and b. </summary>
    /// <param name="a"> Start value. </param>
    /// <param name="b"> End value. </param>
    /// <returns> Interpolated random vector. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Lerp(Vector2 a, Vector2 b) => Vector2.Lerp(a, b, Value);

    /// <summary> Returns a random vector between a and b. </summary>
    /// <param name="a"> Start value. </param>
    /// <param name="b"> End value. </param>
    /// <returns> Interpolated random vector. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Lerp(Vector3 a, Vector3 b) => Vector3.Lerp(a, b, Value);

    /// <summary> Returns a random color between a and b. </summary>
    /// <param name="a"> Start color. </param>
    /// <param name="b"> End color. </param>
    /// <returns> Interpolated random color. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color Lerp(Color a, Color b) => Color.Lerp(a, b, Value);
  }
}
