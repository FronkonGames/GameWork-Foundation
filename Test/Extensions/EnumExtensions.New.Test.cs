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
using NUnit.Framework;
using FronkonGames.GameWork.Foundation;

namespace FronkonGames.GameWork.Foundation.Tests
{
  /// <summary> Enum extensions new methods tests. </summary>
  [TestFixture]
  public class EnumExtensionsNewTests
  {
    private enum SampleEnum
    {
      Zero = 0,
      One = 1,
      Two = 2,
      Three = 3,
    }

    /// <summary> PickRandom returns a defined enum value. </summary>
    [Test]
    public void PickRandom_ReturnsDefinedValue()
    {
      SampleEnum value = EnumExtensions.PickRandom<SampleEnum>();

      Assert.IsTrue(Enum.IsDefined(typeof(SampleEnum), value));
    }

    /// <summary> PickWeighted respects weight array length. </summary>
    [Test]
    public void PickWeighted_UsesMatchingWeightCount()
    {
      SampleEnum value = EnumExtensions.PickWeighted<SampleEnum>(new[] { 10.0f, 0.0f, 0.0f, 0.0f });

      Assert.AreEqual(SampleEnum.Zero, value);
    }

    /// <summary> PickBetween excludes both bounds. </summary>
    [Test]
    public void PickBetween_ExcludesBounds()
    {
      for (int i = 0; i < 100; i++)
      {
        SampleEnum value = EnumExtensions.PickBetween(SampleEnum.Zero, SampleEnum.Three);

        Assert.AreNotEqual(SampleEnum.Zero, value);
        Assert.AreNotEqual(SampleEnum.Three, value);
      }
    }

    /// <summary> PickUpTo excludes the upper bound. </summary>
    [Test]
    public void PickUpTo_ExcludesUpperBound()
    {
      for (int i = 0; i < 100; i++)
      {
        SampleEnum value = EnumExtensions.PickUpTo(SampleEnum.Three);

        Assert.AreNotEqual(SampleEnum.Three, value);
      }
    }
  }
}
