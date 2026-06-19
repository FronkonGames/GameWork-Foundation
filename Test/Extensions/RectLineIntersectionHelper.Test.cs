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
using NUnit.Framework;
using UnityEngine;
using FronkonGames.GameWork.Foundation;

/// <summary> Rect line intersection helper test. </summary>
[TestFixture]
public class RectLineIntersectionHelperTests
{
  private readonly Rect rect = new(0.0f, 0.0f, 10.0f, 10.0f);

  [Test]
  public void GetLineIntersections_CrossingHorizontalLine_ReturnsEntryAndExit()
  {
    (float? entry, float? exit) = rect.GetLineIntersections(new Vector2(-5.0f, 5.0f), new Vector2(15.0f, 5.0f));

    Assert.IsNotNull(entry);
    Assert.IsNotNull(exit);
    Assert.AreEqual(0.25f, entry.Value, 0.001f);
    Assert.AreEqual(0.75f, exit.Value, 0.001f);
  }

  [Test]
  public void GetLineIntersections_LineInsideRect_ReturnsZeroAndOne()
  {
    (float? entry, float? exit) = rect.GetLineIntersections(new Vector2(2.0f, 2.0f), new Vector2(8.0f, 8.0f));

    Assert.AreEqual(0.0f, entry.Value, 0.001f);
    Assert.AreEqual(1.0f, exit.Value, 0.001f);
  }

  [Test]
  public void GetLineIntersections_MissingRect_ReturnsNull()
  {
    (float? entry, float? exit) = rect.GetLineIntersections(new Vector2(-10.0f, -10.0f), new Vector2(-1.0f, -1.0f));

    Assert.IsNull(entry);
    Assert.IsNull(exit);
  }
}
