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

/// <summary>
/// Math tests.
/// </summary>
public partial class ExtensionsTests
{
  private int Tries = 1000;

  /// <summary>
  /// Random extensions test.
  /// </summary>
  [UnityTest]
  public IEnumerator Random()
  {
    // 1D

    for (int i = 0; i < Tries; ++i)
    {
      float value = Rand.Value;
      Assert.IsTrue(value >= 0.0f && value <= 1.0f);
    }

    for (int i = 0; i < Tries; ++i)
    {
      float sign = Rand.Sign;
      Assert.IsTrue(sign == 1.0f || sign == -1.0f);
    }

    for (int i = 0; i < Tries; ++i)
    {
      const float min = 0.0f;
      const float max = 10.0f;

      float value = Rand.Range(min, max);
      Assert.IsTrue(value >= min && value <= max);
    }

    yield return null;
  }
}
