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
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> Extensions test. </summary>
public partial class ExtensionsTests
{
  /// <summary> Dictionary extensions test. </summary>
  [UnityTest]
  public IEnumerator Dictionary()
  {
    Dictionary<string, int> dict = new() { { "a", 1 }, { "b", 2 }, { "c", 3 } };

    Assert.IsFalse(dict.IsEmptyOrNull());
    Assert.IsFalse(dict.IsEmpty());

    Dictionary<string, int> empty = new();
    Assert.IsTrue(empty.IsEmptyOrNull());
    Assert.IsTrue(empty.IsEmpty());

    Dictionary<string, int> nullDict = null;
    Assert.IsTrue(nullDict.IsEmptyOrNull());

    string randomKey = dict.RandomKey();
    Assert.IsTrue(dict.ContainsKey(randomKey));

    Assert.IsTrue(dict.TryGetFirstKeyByValue(2, out string foundKey));
    Assert.AreEqual("b", foundKey);

    Assert.IsFalse(dict.TryGetFirstKeyByValue(99, out _));

    string keyByValue = dict.GetKeyByValue(3);
    Assert.AreEqual("c", keyByValue);

    string missingKey = dict.GetKeyByValue(99, "default");
    Assert.AreEqual("default", missingKey);

    List<string> keysForValue = new(dict.GetKeysByValue(1));
    Assert.AreEqual(1, keysForValue.Count);
    Assert.IsTrue(keysForValue.Contains("a"));

    Dictionary<string, int> dupes = new() { { "x", 1 }, { "y", 1 }, { "z", 3 } };
    List<string> keysForDupe = new(dupes.GetKeysByValue(1));
    Assert.AreEqual(2, keysForDupe.Count);
    Assert.IsTrue(keysForDupe.Contains("x"));
    Assert.IsTrue(keysForDupe.Contains("y"));

    dict.SwapValues("a", "c");
    Assert.AreEqual(3, dict["a"]);
    Assert.AreEqual(1, dict["c"]);

    yield return null;
  }
}
