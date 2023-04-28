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
  /// <summary> Math utilities. </summary>
  public static partial class MathUtils
  {
    /// <summary> Sine of angle. </summary>
    /// <param name="rad">Angle in radians. </param>
    /// <returns>Value</returns>
    public static float Sin(float rad) => Mathf.Sin(rad);

    /// <summary> Cosine of angle. </summary>
    /// <param name="rad">Angle in radians. </param>
    /// <returns>Value</returns>
    public static float Cos(float rad) => Mathf.Cos(rad);

    /// <summary> Tangent of angle. </summary>
    /// <param name="rad">Angle in radians. </param>
    /// <returns>Value</returns>
    public static float Tan(float rad) => Mathf.Tan(rad);

    /// <summary> Arc-sine of angle. </summary>
    /// <param name="rad">Angle in radians. </param>
    /// <returns>Value</returns>
    public static float Asin(float rad) => Mathf.Asin(rad);

    /// <summary> Arc-cosine of angle. </summary>
    /// <param name="rad">Angle in radians. </param>
    /// <returns>Value</returns>
    public static float Acos(float rad) => Mathf.Acos(rad);

    /// <summary> Arc-tangent of angle. </summary>
    /// <param name="rad">Angle in radians. </param>
    /// <returns>Value</returns>
    public static float Atan(float rad) => Mathf.Atan(rad);

    /// <summary> Angle in radians whose Tan is y/x </summary>
    /// <param name="y">Value</param>
    /// <param name="x">Value</param>
    /// <returns>Value</returns>
    public static float Atan2(float y, float x) => Mathf.Atan2(y, x);

    /// <summary> Cosecant of angle. </summary>
    /// <param name="x">Value</param>
    /// <returns>Value</returns>
    public static float Csc(float x) => 1.0f / Mathf.Sin(x);

    /// <summary> Secant of angle. </summary>
    /// <param name="x">Value</param>
    /// <returns>Value</returns>
    public static float Sec(float x) => 1.0f / Mathf.Cos(x);

    /// <summary> Cotangent of angle. </summary>
    /// <param name="x">Value</param>
    /// <returns>Value</returns>
    public static float Cot(float x) => 1.0f / Mathf.Tan(x);

    /// <summary> Versine of angle. </summary>
    /// <param name="x">Value</param>
    /// <returns>Value</returns>
    public static float Ver(float x) => 1.0f - Mathf.Cos(x);

    /// <summary> Coversine of angle. </summary>
    /// <param name="x">Value</param>
    /// <returns>Value</returns>
    public static float Cvs(float x) => 1.0f - Mathf.Sin(x);

    /// <summary> Chord of angle. </summary>
    /// <param name="x">Value</param>
    /// <returns>Value</returns>
    public static float Crd(float x) => 2.0f * Mathf.Sin(x * 0.5f);
  }
}
