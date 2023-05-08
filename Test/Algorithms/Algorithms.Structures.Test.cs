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

/// <summary> Algorithms tests. </summary>
public partial class AlgorithmsTests
{
  /// <summary> Random extensions test. </summary>
  [UnityTest]
  public IEnumerator ArrayList()
  {
    ArrayList<int> array0 = new();

    int count = 1000000;
    for (int i = 0; i < count; ++i)
      array0.Add(i);

    Assert.AreEqual(count, array0.Count);
    Assert.True(array0.Capacity > count);
    Assert.True(array0.Contains(count / 2));

    Assert.AreEqual(count / 2, array0.Find(item => item == count / 2));
    Assert.AreEqual(count / 2, array0.FindIndex(item => item == count / 2));
    Assert.AreEqual(2, array0.FindAll(item => item == 0 || item == count - 1).Count);

    Assert.AreEqual(0, array0[0]);
    Assert.AreEqual(count - 1, array0[count - 1]);
    Assert.AreEqual(0, array0.First);
    Assert.AreEqual(count - 1, array0.Last);

    Assert.True(array0.Contains(0));
    Assert.False(array0.Contains(count));
    Assert.True(array0.Exists(item => item == 0));
    Assert.False(array0.Exists(item => item == count));
    Assert.AreEqual(0, array0.IndexOf(0));
    Assert.AreEqual(count - 1, array0.IndexOf(count - 1));

    Assert.True(array0.Remove(0));
    Assert.False(array0.Remove(count + 1));
    Assert.AreEqual(-1, array0.IndexOf(0));

    array0.Insert(0, 0);
    Assert.AreEqual(count, array0.Count);
    Assert.AreEqual(0, array0[0]);

    array0.Remove(count - 1);
    Assert.AreEqual(count - 1, array0.Count);
    Assert.AreEqual(count - 2, array0.Last);

    array0.Insert(array0.Count, count - 1);
    Assert.AreEqual(count, array0.Count);
    Assert.AreEqual(count - 1, array0.Last);

    ArrayList<int> array1 = new();
    Assert.AreEqual(0, array1.Count);

    array1.AddRange(array0);
    Assert.AreEqual(count, array1.Count);
    Assert.True(array1.Contains(count / 2));

    array1.Reverse();
    Assert.AreEqual(count - 1, array1.First);
    Assert.AreEqual(0, array1.Last);

    int sum = 0;
    int result = 0;
    for (int i = 0; i < count; ++i)
      result += i;

    array1.ForEach(item => sum += item);
    Assert.AreEqual(result, sum);

#if false
    int ops = 1000000;
    int tmp = 0;
    ArrayList<int> array = new();
    System.Collections.Generic.List<int> list = new();

    TimeBlock timer = new();
    for (int i = 0; i < ops; ++i)
      array.Add(i);
    float timeAddArray = timer.Duration;

    timer = new();
    for (int i = 0; i < ops; ++i)
      list.Add(i);
    float timeAddList = timer.Duration;

    timer = new();
    for (int i = 0; i < ops; ++i)
      tmp += array[Rand.Range(0, ops)];
    float timeRandAccessArray = timer.Duration;

    tmp = 0;
    timer = new();
    for (int i = 0; i < ops; ++i)
      tmp += list[Rand.Range(0, ops)];
    float timeRandAccessList = timer.Duration;

    ops = 10000;

    timer = new();
    for (int i = 0; i < ops; ++i)
      array.Insert(Rand.Range(0, 10), Rand.Range(0, array.Count));
    float timeRandInsertArray = timer.Duration;

    timer = new();
    for (int i = 0; i < ops; ++i)
      list.Insert(Rand.Range(0, list.Count), Rand.Range(0, 10));
    float timeRandInsertList = timer.Duration;

    timer = new();
    for (int i = 0; i < ops; ++i)
      array.RemoveAt(Rand.Range(0, array.Count));
    float timeRandRemoveArray = timer.Duration;

    timer = new();
    for (int i = 0; i < ops; ++i)
      list.Remove(Rand.Range(0, list.Count));
    float timeRandRemoveList = timer.Duration;

    Log.Info($"Add({timeAddList / timeAddArray * 100.0f:.02}%): {timeAddArray} ArrayList, {timeAddList}ms List.");
    Log.Info($"RandAccess({timeRandAccessList / timeRandAccessArray * 100.0f:.02}%): {timeRandAccessArray} ArrayList, {timeRandAccessList}ms List.");
    Log.Info($"RandInsert({timeRandInsertList / timeRandInsertArray * 100.0f:.02}%): {timeRandInsertArray} ArrayList, {timeRandInsertList}ms List.");
    Log.Info($"RandRemove({timeRandRemoveList / timeRandRemoveArray * 100.0f:.02}%): {timeRandRemoveArray} ArrayList, {timeRandRemoveList}ms List.");
#endif

    yield return null;
  }
}
