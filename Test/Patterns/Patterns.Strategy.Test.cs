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
using NUnit.Framework;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> Patterns tests. </summary>
public partial class PatternsTests
{
  private class StrategyClient0
  {
    public IStrategy<int> Strategy { get; set; }

    public int Execute() => Strategy?.OnExecute() ?? 0;
  }

  private class Strategy42 : IStrategy<int>
  {
    public int OnExecute() => 42;
  }

  private class Strategy69 : IStrategy<int>
  {
    public int OnExecute() => 69;
  }

  private class StrategyClient1
  {
    public IStrategy<int, int> Strategy { get; set; }

    public int Execute(int value) => Strategy?.OnExecute(value) ?? 0;
  }
  
  private class StrategyX2 : IStrategy<int, int>
  {
    public int OnExecute(int value) => value * 2;
  }

  private class StrategyX4 : IStrategy<int, int>
  {
    public int OnExecute(int value) => value * 4;
  }

  private class StrategyClient2
  {
    public IStrategy<int, int, int> Strategy { get; set; }

    public int Execute(int value0, int value1) => Strategy?.OnExecute(value0, value1) ?? 0;
  }
  
  private class StrategyAdd : IStrategy<int, int, int>
  {
    public int OnExecute(int value0, int value1) => value0 + value1;
  }

  private class StrategySub : IStrategy<int, int, int>
  {
    public int OnExecute(int value0, int value1) => value0 - value1;
  }

  private class StrategyClient3
  {
    public IStrategy<int, int, int, int> Strategy { get; set; }

    public int Execute(int value0, int value1, int value2) => Strategy?.OnExecute(value0, value1, value2) ?? 0;
  }
  
  private class StrategyMax : IStrategy<int, int, int, int>
  {
    public int OnExecute(int value0, int value1, int value2) => Math.Max(value0, Math.Max(value1, value2));
  }

  private class StrategyMin : IStrategy<int, int, int, int>
  {
    public int OnExecute(int value0, int value1, int value2) =>  Math.Min(value0, Math.Min(value1, value2));
  }
  
  /// <summary> Strategy test. </summary>
  [UnityTest]
  public IEnumerator Strategy()
  {
    StrategyClient0 client0 = new();
    
    client0.Strategy = new Strategy42();
    Assert.AreEqual(42, client0.Execute());
    
    client0.Strategy = new Strategy69();
    Assert.AreEqual(69, client0.Execute());

    StrategyClient1 client1 = new();
    
    client1.Strategy = new StrategyX2();
    Assert.AreEqual(4, client1.Execute(2));
    
    client1.Strategy = new StrategyX4();
    Assert.AreEqual(8, client1.Execute(2));

    StrategyClient2 client2 = new();
    
    client2.Strategy = new StrategyAdd();
    Assert.AreEqual(2, client2.Execute(1, 1));
    
    client2.Strategy = new StrategySub();
    Assert.AreEqual(2, client2.Execute(4, 2));

    StrategyClient3 client3 = new();
    
    client3.Strategy = new StrategyMax();
    Assert.AreEqual(8, client3.Execute(1, 4, 8));
    
    client3.Strategy = new StrategyMin();
    Assert.AreEqual(1, client3.Execute(1, 4, 8));
    
    yield return null;
  }
}
