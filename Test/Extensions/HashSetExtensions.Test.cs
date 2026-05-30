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

/// <summary>
/// HashSet extensions test.
/// </summary>
public class HashSetExtensionsTests
{
  [Test]
  public void Random_ReturnsElementFromSet()
  {
    HashSet<int> set = new HashSet<int> { 1, 2, 3, 4, 5 };
    Assert.IsTrue(set.Contains(set.Random()));
  }

  [Test]
  public void Random_EmptySet_ReturnsDefault()
  {
    HashSet<int> set = new HashSet<int>();
    Assert.AreEqual(default(int), set.Random());
  }

  [Test]
  public void Random_NullSet_ReturnsDefault()
  {
    HashSet<int> set = null;
    Assert.AreEqual(default(int), set.Random());
  }

  [Test]
  public void IsEmpty_NullSet_ReturnsTrue()
  {
    HashSet<int> set = null;
    Assert.IsTrue(set.IsEmpty());
  }

  [Test]
  public void IsEmpty_EmptySet_ReturnsTrue()
  {
    HashSet<int> set = new HashSet<int>();
    Assert.IsTrue(set.IsEmpty());
  }

  [Test]
  public void IsEmpty_NonEmptySet_ReturnsFalse()
  {
    HashSet<int> set = new HashSet<int> { 1, 2, 3 };
    Assert.IsFalse(set.IsEmpty());
  }

  [Test]
  public void Clone_ReturnsNewSetWithSameElements()
  {
    HashSet<int> original = new HashSet<int> { 1, 2, 3, 4, 5 };
    HashSet<int> clone = original.Clone();

    Assert.AreNotSame(original, clone);
    Assert.AreEqual(original.Count, clone.Count);
    foreach (int item in original)
      Assert.IsTrue(clone.Contains(item));
  }

  [Test]
  public void Clone_NullSet_ReturnsEmptySet()
  {
    HashSet<int> set = null;
    HashSet<int> clone = set.Clone();
    Assert.IsNotNull(clone);
    Assert.AreEqual(0, clone.Count);
  }

  [Test]
  public void TryGetRandom_NonEmptySet_ReturnsTrueAndValue()
  {
    HashSet<int> set = new HashSet<int> { 10, 20, 30 };
    Assert.IsTrue(set.TryGetRandom(out int value));
    Assert.IsTrue(set.Contains(value));
  }

  [Test]
  public void TryGetRandom_EmptySet_ReturnsFalse()
  {
    HashSet<int> set = new HashSet<int>();
    Assert.IsFalse(set.TryGetRandom(out int value));
    Assert.AreEqual(default(int), value);
  }

  [Test]
  public void TryGetRandom_NullSet_ReturnsFalse()
  {
    HashSet<int> set = null;
    Assert.IsFalse(set.TryGetRandom(out int value));
    Assert.AreEqual(default(int), value);
  }

  [Test]
  public void Contains_Predicate_TrueWhenMatchFound()
  {
    HashSet<int> set = new HashSet<int> { 1, 2, 3, 4, 5 };
    Assert.IsTrue(set.Contains(x => x > 3));
  }

  [Test]
  public void Contains_Predicate_FalseWhenNoMatch()
  {
    HashSet<int> set = new HashSet<int> { 1, 2, 3, 4, 5 };
    Assert.IsFalse(set.Contains(x => x > 10));
  }

  [Test]
  public void Contains_Predicate_EmptySet_ReturnsFalse()
  {
    HashSet<int> set = new HashSet<int>();
    Assert.IsFalse(set.Contains(x => x == 1));
  }

  [Test]
  public void Shuffle_ReturnsSameSetWithSameElements()
  {
    HashSet<int> set = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    HashSet<int> result = set.Shuffle();

    Assert.AreSame(set, result);
    Assert.AreEqual(10, result.Count);
    for (int i = 1; i <= 10; i++)
      Assert.IsTrue(result.Contains(i));
  }

  [Test]
  public void Shuffle_SingleElementSet_ReturnsSameSet()
  {
    HashSet<int> set = new HashSet<int> { 42 };
    HashSet<int> result = set.Shuffle();

    Assert.AreSame(set, result);
    Assert.AreEqual(1, result.Count);
    Assert.IsTrue(result.Contains(42));
  }

  [Test]
  public void AddRange_AddsMultipleElements()
  {
    HashSet<int> set = new HashSet<int> { 1, 2 };
    set.AddRange(new[] { 3, 4, 5 });

    Assert.AreEqual(5, set.Count);
    for (int i = 1; i <= 5; i++)
      Assert.IsTrue(set.Contains(i));
  }

  [Test]
  public void AddRange_NullItems_DoesNotThrow()
  {
    HashSet<int> set = new HashSet<int> { 1 };
    Assert.DoesNotThrow(() => set.AddRange(null));
    Assert.AreEqual(1, set.Count);
  }

  [Test]
  public void FindRandom_Predicate_ReturnsMatchingElement()
  {
    HashSet<int> set = new HashSet<int> { 1, 2, 3, 4, 5 };
    int result = set.FindRandom(x => x > 3);
    Assert.IsTrue(result == 4 || result == 5);
  }

  [Test]
  public void FindRandom_Predicate_NoMatch_ReturnsDefault()
  {
    HashSet<int> set = new HashSet<int> { 1, 2, 3 };
    int result = set.FindRandom(x => x > 10);
    Assert.AreEqual(default(int), result);
  }

  [Test]
  public void FindRandom_NullSet_ReturnsDefault()
  {
    HashSet<int> set = null;
    int result = set.FindRandom(x => x > 0);
    Assert.AreEqual(default(int), result);
  }
}
