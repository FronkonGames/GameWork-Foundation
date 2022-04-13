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
using UnityRandom = UnityEngine.Random;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Random functions.
  /// </summary>
  public static class Rand
  {
    // 1D
    
    /// <summary>Random float within [0 .. 1]</summary>
    public static float Value => UnityRandom.value;
    
    /// <summary></summary>
    public static float Sign => Value > 0.5f ? 1.0f : -1.0f;

    /// <summary></summary>
    public static float Direction1D => Sign;
    
    /// <summary></summary>
    public static float Range(float min, float max) => UnityRandom.Range(min, max);

    // 2D
    
    /// <summary></summary>
    public static Vector2 OnUnitCircle => MathUtils.AngToDir(Value * MathConstants.Tau);

    /// <summary></summary>
    public static Vector2 Direction2D => OnUnitCircle;

    /// <summary></summary>
    public static Vector2 InUnitCircle => UnityRandom.insideUnitCircle;

    /// <summary></summary>
    public static Vector2 InUnitSquare => new Vector2(Value, Value);

    // 3D

    /// <summary></summary>
    public static Vector3 OnUnitSphere => UnityRandom.onUnitSphere;

    /// <summary></summary>
    public static Vector3 Direction3D => OnUnitSphere;
    
    /// <summary></summary>
    public static Vector3 InUnitSphere => UnityRandom.insideUnitSphere;

    /// <summary></summary>
    public static Vector3 InUnitCube => new Vector3(Value, Value, Value);

    // 2D orientation
    
    /// <summary>Returns a random angle in radians from 0 to TAU</summary>
    public static float Angle => Value * MathConstants.Tau;

    // 3D Orientation

    /// <summary>Returns a random uniformly distributed rotation</summary>
    public static Quaternion Rotation => UnityRandom.rotationUniform;   
  }
}
