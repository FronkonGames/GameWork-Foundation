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
using System.Reflection;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using FronkonGames.GameWork.Foundation;

/// <summary> Extensions test. </summary>
public partial class ExtensionsTests
{
  private class TestClass
  {
    [Label("Test Label")]
    public int publicField = 42;
    public int noAttributeField = 0;
#pragma warning disable CS0414
    private string privateField = "hidden";
#pragma warning restore CS0414
  }

  /// <summary> Reflection extensions test. </summary>
  [UnityTest]
  public IEnumerator Reflection()
  {
    TestClass test = new();
    FieldInfo fieldWithLabel = typeof(TestClass).GetField("publicField");
    FieldInfo fieldWithoutLabel = typeof(TestClass).GetField("noAttributeField");

    Assert.IsNotNull(fieldWithLabel.GetAttribute<LabelAttribute>());
    Assert.AreEqual("Test Label", fieldWithLabel.GetAttribute<LabelAttribute>().label);

    Assert.IsTrue(fieldWithLabel.HasAttribute<LabelAttribute>());
    Assert.IsFalse(fieldWithoutLabel.HasAttribute<LabelAttribute>());

    Assert.IsNotNull(test.GetField("privateField"));
    Assert.IsNull(test.GetField("nonExistentField"));

    Assert.IsNull(test.GetProperty("nonExistentProperty"));

    yield return null;
  }
}
