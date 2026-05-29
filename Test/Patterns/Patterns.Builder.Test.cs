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

/// <summary> Patterns tests. </summary>
public partial class PatternsTests
{
  private class TestProduct
  {
    public string Name;
    public int Value;
  }

  private class TestProductBuilder : Builder<TestProductBuilder, TestProduct>
  {
    private readonly TestProduct product = new();

    public TestProductBuilder SetName(string name) { product.Name = name; return this; }
    public TestProductBuilder SetValue(int value) { product.Value = value; return this; }

    public override TestProduct Build() => product;
  }

  /// <summary> Builder test. </summary>
  [UnityTest]
  public IEnumerator Builder()
  {
    TestProduct product = TestProductBuilder.Create()
      .SetName("Test")
      .SetValue(42)
      .Build();

    Assert.AreEqual("Test", product.Name);
    Assert.AreEqual(42, product.Value);

    TestProduct defaultProduct = TestProductBuilder.Create().Build();
    Assert.IsNull(defaultProduct.Name);
    Assert.AreEqual(0, defaultProduct.Value);

    yield return null;
  }
}
