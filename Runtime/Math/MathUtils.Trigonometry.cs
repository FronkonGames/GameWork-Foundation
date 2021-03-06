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
  /// <summary>
  /// Math utilities.
  /// </summary>
  public static partial class MathUtils
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="angRad"></param>
    /// <returns></returns>
    public static float Sin(float angRad) => Mathf.Sin(angRad);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="angRad"></param>
    /// <returns></returns>
    public static float Cos(float angRad) => Mathf.Cos(angRad);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="angRad"></param>
    /// <returns></returns>
    public static float Tan(float angRad) => Mathf.Tan(angRad);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Asin(float value) => Mathf.Asin(value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Acos(float value) => Mathf.Acos(value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float Atan(float value) => Mathf.Atan(value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float Atan2(float y, float x) => Mathf.Atan2(y, x);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float Csc(float x) => 1.0f / Mathf.Sin(x);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float Sec(float x) => 1.0f / Mathf.Cos(x);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float Cot(float x) => 1.0f / Mathf.Tan(x);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float Ver(float x) => 1.0f - Mathf.Cos(x);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float Cvs(float x) => 1.0f - Mathf.Sin(x);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static float Crd(float x) => 2.0f * Mathf.Sin(x * 0.5f);
  }
}
