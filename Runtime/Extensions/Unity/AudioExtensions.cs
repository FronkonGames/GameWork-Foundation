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
  /// <summary> Audio conversion extensions. </summary>
  public static class AudioExtensions
  {
    private const float MinLinear = 0.00001f;
    private const float MinDecibel = -80.0f;

    /// <summary> Converts a normalized linear amplitude [0, 1] to decibels. </summary>
    /// <param name="linear"> Linear amplitude. </param>
    /// <returns> Decibel value. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ToDecibel(this float linear)
    {
      linear = Mathf.Clamp01(linear);

      if (linear < MinLinear)
        return MinDecibel;

      return Mathf.Log10(linear) * 20.0f;
    }

    /// <summary> Converts decibels to a normalized linear amplitude [0, 1]. </summary>
    /// <param name="decibel"> Decibel value. </param>
    /// <returns> Linear amplitude. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ToLinear(this float decibel) => Mathf.Pow(10.0f, decibel * 0.05f);
  }
}
