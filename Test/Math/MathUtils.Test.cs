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
using UnityEngine;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> Math tests. </summary>
public partial class MathTests
{
  /// <summary> MathUtils trigonometry test. </summary>
  [UnityTest]
  public IEnumerator MathUtilsTrigonometry()
  {
    float piHalf = MathConstants.PiHalf;
    float pi = MathConstants.Pi;

    Assert.IsTrue(MathUtils.Sin(0.0f).NearlyEquals(0.0f));
    Assert.IsTrue(MathUtils.Sin(piHalf).NearlyEquals(1.0f));
    Assert.IsTrue(MathUtils.Cos(0.0f).NearlyEquals(1.0f));
    Assert.IsTrue(MathUtils.Cos(pi).NearlyEquals(-1.0f));
    Assert.IsTrue(MathUtils.Tan(0.0f).NearlyEquals(0.0f));

    Assert.IsTrue(MathUtils.Asin(0.0f).NearlyEquals(0.0f));
    Assert.IsTrue(MathUtils.Acos(1.0f).NearlyEquals(0.0f));
    Assert.IsTrue(MathUtils.Atan(0.0f).NearlyEquals(0.0f));
    Assert.IsTrue(MathUtils.Atan2(0.0f, 1.0f).NearlyEquals(0.0f));

    Assert.IsTrue(MathUtils.Csc(piHalf).NearlyEquals(1.0f));
    Assert.IsTrue(MathUtils.Sec(0.0f).NearlyEquals(1.0f));
    Assert.IsTrue(MathUtils.Cot(pi / 4.0f).NearlyEquals(1.0f));

    Assert.IsTrue(MathUtils.Ver(0.0f).NearlyEquals(0.0f));
    Assert.IsTrue(MathUtils.Cvs(0.0f).NearlyEquals(1.0f));
    Assert.IsTrue(MathUtils.Crd(0.0f).NearlyEquals(0.0f));

    Vector2 dir = MathUtils.AngToDir(0.0f);
    Assert.IsTrue(dir.x.NearlyEquals(1.0f));
    Assert.IsTrue(dir.y.NearlyEquals(0.0f));

    dir = MathUtils.AngToDir(piHalf);
    Assert.IsTrue(dir.x.NearlyEquals(0.0f, 0.001f));
    Assert.IsTrue(dir.y.NearlyEquals(1.0f));

    yield return null;
  }
}
