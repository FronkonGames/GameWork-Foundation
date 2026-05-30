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
  /// <summary> Object extensions test. </summary>
  [UnityTest]
  public IEnumerator Object()
  {
    // IsNull on null object.
    object nullObj = null;
    Assert.IsTrue(nullObj.IsNull());

    // IsNull on non-null object.
    object nonNullObj = "hello";
    Assert.IsFalse(nonNullObj.IsNull());

    // Cast on compatible type.
    string str = "test";
    object objStr = str;
    Assert.AreEqual(objStr.Cast<string>(), "test");

    // Cast on incompatible type.
    object objInt = 42;
    Assert.AreEqual(objInt.Cast<string>(), default(string));

    // Cast with custom default.
    Assert.AreEqual(objInt.Cast<string>("fallback"), "fallback");

    // TryCast on compatible type.
    Assert.IsTrue(objStr.TryCast<string>(out string tryResult));
    Assert.AreEqual(tryResult, "test");

    // TryCast on incompatible type.
    Assert.IsFalse(objInt.TryCast<string>(out string tryFail));
    Assert.IsNull(tryFail);

    // ConvertTo on compatible type.
    object boxedInt = 123;
    Assert.AreEqual(boxedInt.ConvertTo<int>(), 123);

    // SafeConvertTo on compatible type.
    Assert.AreEqual(boxedInt.SafeConvertTo<int>(), 123);

    // SafeConvertTo on incompatible type.
    object objString = "not a number";
    Assert.AreEqual(objString.SafeConvertTo<int>(), default(int));

    yield return null;
  }
}
