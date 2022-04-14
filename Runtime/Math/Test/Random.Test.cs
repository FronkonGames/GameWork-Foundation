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
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;
using UnityEngine;

/// <summary>
/// Math tests.
/// </summary>
public partial class ExtensionsTests
{
  private const int Tries = 10000;

  /// <summary>
  /// Random extensions test.
  /// </summary>
  [UnityTest]
  public IEnumerator Random()
  {
    // 1D

    float mean = 0.0f;
    for (int i = 0; i < Tries; ++i)
    {
      float value = Rand.Value;
      mean += value;
      
      Assert.IsTrue(value >= 0.0f && value <= 1.0f);
    }

    mean /= Tries;
    Assert.IsTrue(mean >= 0.49f && mean <= 0.51f);

    mean = 0.0f;
    for (int i = 0; i < Tries; ++i)
    {
      float sign = Rand.Sign;
      mean += sign == 1.0f ? 1.0f : 0.0f;
      
      Assert.IsTrue(sign.NearlyEquals(1.0f) || sign.NearlyEquals(-1.0f));
    }
    
    Assert.IsTrue(mean >= (Tries / 2) - (Tries / 100) && mean <= (Tries / 2) + (Tries / 100));

    mean = 0.0f;
    const float min = 0.0f;
    const float max = 10.0f;
    for (int i = 0; i < Tries; ++i)
    {
      float value = Rand.Range(min, max);
      mean += value;

      Assert.IsTrue(value >= min && value <= max);
    }

    mean /= Tries;
    const float middle = (max - min) * 0.5f;
    Assert.IsTrue(mean >= middle - (middle / 100) && mean <= middle + (middle / 100));

    yield return null;
  }
}
