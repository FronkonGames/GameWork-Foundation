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
using System.Diagnostics;
using System.Collections.Generic;
using NUnit.Framework;
using FronkonGames.GameWork.Foundation;

/// <summary> FastList performance comparison tests. </summary>
[TestFixture]
public class FastListPerformanceTests
{
  private const int Iterations = 100000;

  /// <summary> RemoveAt from middle is faster than List (swap vs shift). </summary>
  [Test]
  public void RemoveAt_Middle_FastList_IsFasterThan_List()
  {
    var fastList = new FastList<int>();
    var list = new List<int>();
    for (int i = 0; i < Iterations; i++)
    {
      fastList.Add(i);
      list.Add(i);
    }

    var sw = Stopwatch.StartNew();
    for (int i = Iterations / 2; i >= 0; i--)
      fastList.RemoveAt(i);
    sw.Stop();
    long fastListTime = sw.ElapsedMilliseconds;
    fastList.Dispose();

    sw.Restart();
    for (int i = Iterations / 2; i >= 0; i--)
      list.RemoveAt(i);
    sw.Stop();
    long listTime = sw.ElapsedMilliseconds;

    Assert.Less(fastListTime, listTime, $"FastList ({fastListTime}ms) should be faster than List ({listTime}ms) for RemoveAt from middle");
  }

  /// <summary> RemoveAt from beginning is faster than List (swap vs shift). </summary>
  [Test]
  public void RemoveAt_Beginning_FastList_IsFasterThan_List()
  {
    var fastList = new FastList<int>();
    var list = new List<int>();
    for (int i = 0; i < Iterations; i++)
    {
      fastList.Add(i);
      list.Add(i);
    }

    var sw = Stopwatch.StartNew();
    for (int i = 0; i < Iterations; i++)
      fastList.RemoveAt(0);
    sw.Stop();
    long fastListTime = sw.ElapsedMilliseconds;
    fastList.Dispose();

    sw.Restart();
    for (int i = 0; i < Iterations; i++)
      list.RemoveAt(0);
    sw.Stop();
    long listTime = sw.ElapsedMilliseconds;

    Assert.Less(fastListTime, listTime, $"FastList ({fastListTime}ms) should be faster than List ({listTime}ms) for RemoveAt from beginning");
  }

  /// <summary> Iteration is comparable or faster than List. </summary>
  [Test]
  public void Iteration_FastList_IsComparableToList()
  {
    var fastList = new FastList<int>();
    var list = new List<int>();
    for (int i = 0; i < Iterations; i++)
    {
      fastList.Add(i);
      list.Add(i);
    }

    long fastListTime = 0;
    var sw = Stopwatch.StartNew();
    for (int iter = 0; iter < 100; iter++)
    {
      long sum = 0;
      foreach (var item in fastList)
        sum += item;
    }
    sw.Stop();
    fastListTime = sw.ElapsedMilliseconds;

    sw.Restart();
    for (int iter = 0; iter < 100; iter++)
    {
      long sum = 0;
      foreach (var item in list)
        sum += item;
    }
    sw.Stop();
    long listTime = sw.ElapsedMilliseconds;

    fastList.Dispose();

    // FastList should be within 50% of List performance (struct enumerator avoids allocations)
    Assert.Less(fastListTime, listTime * 1.5f, $"FastList ({fastListTime}ms) should be comparable to List ({listTime}ms) for iteration");
  }
}
