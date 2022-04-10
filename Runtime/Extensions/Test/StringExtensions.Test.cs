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
using UnityEngine;

/// <summary>
/// Extensions test.
/// </summary>
public partial class ExtensionsTests
{
  /// <summary>
  /// StringExtensions test.
  /// </summary>
  [UnityTest]
  public IEnumerator String()
  {
    Assert.IsTrue("true".ToBoolean());
    Assert.IsTrue("True".ToBoolean());
    Assert.IsTrue("ok".ToBoolean());
    Assert.IsTrue("Yes".ToBoolean());
    Assert.IsTrue("1".ToBoolean());
    Assert.IsFalse("false".ToBoolean());
    Assert.IsFalse("no".ToBoolean());

    Assert.AreEqual("1".ToInt(), 1);
    Assert.AreEqual("1.0".ToFloat(), 1.0f);

    Assert.AreEqual("Hello world!".ToMD5(), "86fb269d190d2c85f6e0468ceca42a20");

    Assert.AreEqual("Camel case string".ToCamelCase(), "CamelCaseString");
    Assert.AreEqual("CamelCaseString".FromCamelCase(), "Camel case string");
    Assert.AreEqual("0,0,0".ToVector3(), Vector3.zero);
    Assert.AreEqual("1.0f,1.0,1".ToVector3(), Vector3.one);
    Assert.AreEqual("All your base are belong to us".ToBase64(), "QWxsIHlvdXIgYmFzZSBhcmUgYmVsb25nIHRvIHVz");
    Assert.AreEqual("QWxsIHlvdXIgYmFzZSBhcmUgYmVsb25nIHRvIHVz".FromBase64(), "All your base are belong to us");
    Assert.AreEqual("hello World!".Capitalized(), "Hello World!");
    Assert.AreEqual("Hello World!".Reverse(), "!dlroW olleH");
    Assert.AreEqual("!dlroW olleH".Reverse(), "Hello World!");

    Assert.IsTrue("All your base are belong to us".IsIndexValid(0));
    Assert.IsTrue("All your base are belong to us".IsIndexValid(10));
    Assert.IsTrue("All your base are belong to us".IsIndexValid(29));
    Assert.IsFalse("All your base are belong to us".IsIndexValid(-1));
    Assert.IsFalse("All your base are belong to us".IsIndexValid(30));

    Assert.IsTrue("FronkonGames".IsValidLatinUsername());
    Assert.IsFalse("-=>]|Bad::Name|[<=-".IsValidLatinUsername());
    Assert.IsFalse("ABC".IsValidLatinUsername());
    Assert.IsFalse("007Cereal".IsValidLatinUsername());
    Assert.IsTrue("fronkongames@gmail.com".IsValidEmail());
    Assert.IsFalse("@gmail.com".IsValidEmail());
    Assert.IsFalse("email.gmail.com".IsValidEmail());
    Assert.IsFalse("email@".IsValidEmail());

    Assert.AreEqual("Hello word!".CalculateHash(), 10912864628638640976ul);

    Assert.AreEqual("Hello word!".First(), 'H');
    Assert.AreEqual("Hello word!".Last(), '!');

    Assert.AreEqual("hello word!".ReplaceChars("hw!", "HW?"), "Hello Word?");

    string text1 = "All your base are belong to us";
    string compress = text1.Compress();
    string text2 = compress.Decompress();
    Assert.AreEqual(text1, text2);

    string secret = "118.3 ml fluid extract of coca leaves, 9.5 l water, caramel";
    string password = "1234";

    string encrypted = secret.Encrypt(password);
    Assert.AreNotEqual(secret, encrypted);

    string decrypted = encrypted.Decrypt(password);
    Assert.AreEqual(decrypted, secret);

    Assert.AreEqual("Hello word!".Similarity("Hello word!"), 0);
    Assert.AreNotEqual("Hello word!".Similarity("Hell to word!"), 0);

    yield return null;
  }
}
