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
using System.Collections.Generic;
using NUnit.Framework;
using FronkonGames.GameWork.Foundation;

/// <summary> Collection extensions test. </summary>
[TestFixture]
public class CollectionExtensionsTests
{
  [Test]
  public void IndexOf_FindsElementInReadOnlyCollection()
  {
    IReadOnlyCollection<string> collection = new[] { "a", "b", "c" };

    Assert.AreEqual(1, collection.IndexOf("b"));
    Assert.AreEqual(-1, collection.IndexOf("missing"));
  }

  [Test]
  public void AddIfMissing_AddsOnlyWhenMissing()
  {
    List<int> list = new() { 1, 2 };

    Assert.IsFalse(list.AddIfMissing(2));
    Assert.AreEqual(2, list.Count);

    Assert.IsTrue(list.AddIfMissing(3));
    CollectionAssert.AreEqual(new[] { 1, 2, 3 }, list);
  }
}
