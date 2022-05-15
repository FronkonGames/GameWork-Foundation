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
  /// Math constants.
  /// </summary>
  public static class MathConstants
  {
    /// <summary></summary>
    public const float Pi                 = 3.14159265358979f;

    /// <summary></summary>     
    public const float PiHalf             = Pi * 0.5f;

    /// <summary></summary>     
    public const float Pi2                = Pi * 2.0f;

    /// <summary></summary>     
    public const float E                  = 2.71828182846f;

    /// <summary></summary>     
    public const float Tau                = 6.28318530717959f;

    /// <summary></summary>     
    public const float GoldenRation       = 1.61803398875f;
    
    /// <summary></summary>
    public const float Deg2Rad            = Tau / 360.0f;

    /// <summary></summary>
    public const float Rad2Deg            = 360.0f / Tau;

    /// <summary></summary>
    public const float Epsilon            = float.Epsilon;
    
    /// <summary></summary>
    public const float Infinity           = Mathf.Infinity;
    
    /// <summary></summary>
    public const float NegativeInfinity   = Mathf.NegativeInfinity;

    /// <summary></summary>
    public static readonly Vector2 NaNVector2      = new Vector2(float.NaN, float.NaN);
    
    /// <summary></summary>
    public static readonly Vector2 InfinityVector2 = new Vector2(Infinity, Infinity);
    
    /// <summary></summary>
    public static readonly Vector3 NaNVector3      = new Vector3(float.NaN, float.NaN, float.NaN);
    
    /// <summary></summary>
    public static readonly Vector3 InfinityVector3 = new Vector3(Infinity, Infinity, Infinity);

    /// <summary></summary>
    public static readonly Vector4 NaNVector4      = new Vector4(float.NaN, float.NaN, float.NaN, float.NaN);
    
    /// <summary></summary>
    public static readonly Vector3 InfinityVector4 = new Vector4(Infinity, Infinity, Infinity, Infinity);
    
    /// <summary></summary>
    public static readonly Ray EmptyRay = new Ray();
  }
}
