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
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> Patterns tests. </summary>
public partial class PatternsTests
{
  private enum ProductType { A, B }

  private interface ITestProduct { string Name { get; } }

  private class ProductA : ITestProduct
  {
    public string Name => "A";
  }

  private class ProductB : ITestProduct
  {
    public string Name => "B";
  }

  private class TestFactory : Factory<ProductType, ITestProduct>
  {
    public TestFactory()
    {
      Register(ProductType.A, () => new ProductA());
      Register(ProductType.B, () => new ProductB());
    }
  }

  private class TestParamFactory : Factory<ProductType, ITestProduct, string>
  {
    public TestParamFactory()
    {
      Register(ProductType.A, name => new ProductA());
      Register(ProductType.B, name => new ProductB());
    }
  }

  /// <summary> Factory test. </summary>
  [UnityTest]
  public IEnumerator Factory()
  {
    TestFactory factory = new();

    ITestProduct productA = factory.Create(ProductType.A);
    Assert.AreEqual("A", productA.Name);

    ITestProduct productB = factory.Create(ProductType.B);
    Assert.AreEqual("B", productB.Name);

    Assert.Throws<KeyNotFoundException>(() => factory.Create((ProductType)99));

    TestParamFactory paramFactory = new();
    ITestProduct paramProduct = paramFactory.Create(ProductType.A, "test");
    Assert.AreEqual("A", paramProduct.Name);

    yield return null;
  }
}
