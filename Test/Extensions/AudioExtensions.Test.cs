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
using NUnit.Framework;
using FronkonGames.GameWork.Foundation;

namespace FronkonGames.GameWork.Foundation.Tests
{
  /// <summary> Audio extensions tests. </summary>
  [TestFixture]
  public class AudioExtensionsTests
  {
    private const float Tolerance = 0.01f;

    /// <summary> ToDecibel converts unity gain to 0 dB. </summary>
    [Test]
    public void ToDecibel_UnityGain_ReturnsZero()
    {
      Assert.AreEqual(0.0f, 1.0f.ToDecibel(), Tolerance);
    }

    /// <summary> ToDecibel clamps near-silent values. </summary>
    [Test]
    public void ToDecibel_Silence_ReturnsFloor()
    {
      Assert.AreEqual(-80.0f, 0.0f.ToDecibel(), Tolerance);
    }

    /// <summary> ToLinear and ToDecibel are inverse operations. </summary>
    [Test]
    public void ToLinear_InvertsToDecibel()
    {
      float linear = 0.5f;
      float roundTrip = linear.ToDecibel().ToLinear();

      Assert.AreEqual(linear, roundTrip, Tolerance);
    }
  }
}
