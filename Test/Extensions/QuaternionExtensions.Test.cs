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
  /// Quaternion extensions test.
  /// </summary>
  [UnityTest]
  public IEnumerator Quaternion()
  {
    Assert.AreEqual(1.0f, UnityEngine.Quaternion.identity.Magnitude());
    
    UnityEngine.Quaternion quaternionA = UnityEngine.Quaternion.identity;
    UnityEngine.Quaternion quaternionB = UnityEngine.Quaternion.identity;
    UnityEngine.Quaternion quaternionC = UnityEngine.Quaternion.Euler(90.0f, 0.0f, 0.0f);
    UnityEngine.Quaternion quaternionD = UnityEngine.Quaternion.Euler(90.01f, 0.0f, 0.0f);
    UnityEngine.Quaternion quaternionE = UnityEngine.Quaternion.Euler(90.1f, 0.0f, 0.0f);

    Assert.IsTrue(quaternionA.NearlyEquals(quaternionB));
    Assert.IsFalse(quaternionA.NearlyEquals(quaternionC));
    Assert.IsTrue(quaternionC.NearlyEquals(quaternionD));
    Assert.IsFalse(quaternionC.NearlyEquals(quaternionE));
    
    yield return null;
  }
}
