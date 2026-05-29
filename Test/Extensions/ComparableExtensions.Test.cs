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

/// <summary> Extensions test. </summary>
public partial class ExtensionsTests
{
  /// <summary> Comparable extensions test. </summary>
  [UnityTest]
  public IEnumerator Comparable()
  {
    Assert.IsTrue(5.IsBetween(1, 10));
    Assert.IsTrue(1.IsBetween(1, 10));
    Assert.IsTrue(10.IsBetween(1, 10));
    Assert.IsFalse(0.IsBetween(1, 10));
    Assert.IsFalse(11.IsBetween(1, 10));

    Assert.IsFalse(1.IsBetween(1, 10, false));
    Assert.IsFalse(10.IsBetween(1, 10, true, false));
    Assert.IsTrue(5.IsBetween(1, 10, false, false));

    Assert.IsTrue(5.0f.IsBetween(1.0f, 10.0f));
    Assert.IsFalse(0.5f.IsBetween(1.0f, 10.0f));

    Assert.IsTrue(5.IsBetweenExclusive(1, 10));
    Assert.IsFalse(1.IsBetweenExclusive(1, 10));
    Assert.IsFalse(10.IsBetweenExclusive(1, 10));

    Assert.IsTrue("b".IsBetween("a", "c"));
    Assert.IsFalse("a".IsBetween("b", "c"));

    yield return null;
  }
}
