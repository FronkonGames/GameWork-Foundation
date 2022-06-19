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
/// Extensions test.
/// </summary>
public partial class ExtensionsTests
{
  /// <summary>
  /// Array extensions test.
  /// </summary>
  [UnityTest]
  public IEnumerator Array()
  {
    int[] arrayA = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    int[] arrayB = arrayA.Copy();
    Assert.AreEqual(arrayA, arrayB);

    int[] subArrayA = { 5, 6, 7, 8, 9 };
    Assert.AreEqual(arrayA.Sub(5, 5), subArrayA);

    arrayA = new []{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    arrayB = new []{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    for (int i = 0; i < arrayA.Length; ++i)
      Assert.IsTrue(System.Array.IndexOf(arrayA, arrayA.Random()) != -1);

    arrayA.Shuffle();
    Assert.AreNotEqual(arrayA, arrayB);

    arrayA = new []{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    arrayA.Swap(0, 9);
    Assert.AreEqual(arrayA[0], 9);
    Assert.AreEqual(arrayA[9], 0);

    arrayA = new []{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    arrayB = new []{ 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
    arrayA.Reverse();
    Assert.AreEqual(arrayA, arrayB);

    arrayA.Shuffle();
    Assert.AreNotEqual(arrayA, arrayB);

    Assert.AreEqual(arrayA.Sum(), 45);

    float[] arrayC = { 0.0f, 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f };
    Assert.AreEqual(arrayC.Sum(), 45.0f);

    Assert.AreEqual(arrayA.Max(), 9);
    Assert.AreEqual(arrayA.Min(), 0);

    yield return null;
  }
}