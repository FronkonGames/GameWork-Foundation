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
  public class Foo
  {
    public int Value { get; private set; }

    public Foo(int value) => Value = value;
  };
  
  /// <summary>
  /// Array extensions test.
  /// </summary>
  [UnityTest]
  public IEnumerator Array()
  {
    Foo[] fooArray = {};
    
    fooArray = fooArray.Append(new Foo(0));
    Assert.AreEqual(1, fooArray.Length);
    Assert.AreEqual(0, fooArray[0].Value);
    
    fooArray = fooArray.Append(new []{ new Foo(1), new Foo(2) });
    Assert.AreEqual(3, fooArray.Length);
    Assert.AreEqual(1, fooArray[1].Value);
    Assert.AreEqual(2, fooArray[2].Value);
    
    fooArray = fooArray.Append(new []{ new Foo(3), new Foo(4) });
    Assert.AreEqual(5, fooArray.Length);
    Assert.AreEqual(3, fooArray[3].Value);
    Assert.AreEqual(4, fooArray[4].Value);

    int[] arrayA = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    int[] arrayB = arrayA.Copy();
    Assert.AreEqual(arrayA, arrayB);

    Assert.IsTrue(arrayA.Contains(5));
    Assert.IsFalse(arrayA.Contains(10));

    Assert.AreEqual(5, arrayA.IndexOf(5));
    Assert.AreEqual(-1, arrayA.IndexOf(10));

    Assert.AreEqual(5, arrayA.IndexOf(value => value == 5));
    Assert.AreEqual(-1, arrayA.IndexOf(value => value == 10));
    
    int[] subArrayA = { 5, 6, 7, 8, 9 };
    Assert.AreEqual(arrayA.Sub(5, 5), subArrayA);

    Assert.IsTrue(arrayA.Contains(5));
    arrayA = arrayA.Remove(5);
    Assert.AreEqual(9, arrayA.Length);
    Assert.IsFalse(arrayA.Contains(5));

    Assert.IsTrue(arrayA.Contains(1));
    arrayA = arrayA.RemoveAt(1);
    Assert.AreEqual(8, arrayA.Length);
    Assert.IsFalse(arrayA.Contains(1));

    Assert.AreEqual(10, arrayB.Length);
    arrayB = arrayB.Fill(5);
    for (int i = 0; i < arrayB.Length; ++i)
      Assert.AreEqual(5, arrayB[i]);

    arrayB = arrayB.Fill(index => index);
    for (int i = 0; i < arrayB.Length; ++i)
      Assert.AreEqual(i, arrayB[i]);
    
    arrayA = new []{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    arrayB = new []{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    for (int i = 0; i < arrayA.Length; ++i)
      Assert.IsTrue(System.Array.IndexOf(arrayA, arrayA.Random()) != -1);

    arrayA.Shuffle();
    Assert.AreNotEqual(arrayA, arrayB);

    arrayA = new []{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    arrayA.Swap(0, 9);
    Assert.AreEqual(9, arrayA[0]);
    Assert.AreEqual(0, arrayA[9]);

    arrayA = new []{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    arrayB = new []{ 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
    arrayA.Reverse();
    Assert.AreEqual(arrayA, arrayB);

    arrayA.Shuffle();
    Assert.AreNotEqual(arrayA, arrayB);

    Assert.AreEqual(45, arrayA.Sum());

    float[] arrayC = { 0.0f, 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f };
    Assert.AreEqual(45.0f, arrayC.Sum());

    Assert.AreEqual(9, arrayA.Max());
    Assert.AreEqual(0, arrayA.Min());

    arrayC.Clear();
    Assert.AreEqual(0.0f, arrayC.Sum());
    
    yield return null;
  }
}