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

/// <summary> Visitor tests. </summary>
public partial class PatternsTests
{
  public class Hero : IVisitable
  {
    public int Live { get; set; } = 100;

    public bool IsAlive => Live > 0;

    public void Accept(Potion potion) => potion.Visit(this);
  }

  public abstract class Potion : IVisitor<Hero>
  {
    protected int strength;
    
    public abstract void Visit(Hero hero);
  }
  
  public class HealthPotion : Potion
  {
    public override void Visit(Hero hero) => hero.Live += strength;
    
    public HealthPotion(int strength) => this.strength = strength;
  }

  public class PoisonPotion : Potion
  {
    public override void Visit(Hero hero) => hero.Live -= strength;

    public PoisonPotion(int strength) => this.strength = strength;
  }
  
  /// <summary> Visitor test. </summary>
  [UnityTest]
  public IEnumerator Visitor()
  {
    Hero hero = new();

    HealthPotion healthPotion = new(10);
    PoisonPotion poisonPotion = new(50);
    
    Assert.AreEqual(100, hero.Live);
    Assert.IsTrue(hero.IsAlive);
    
    hero.Accept(healthPotion);
    Assert.AreEqual(110, hero.Live);
    
    hero.Accept(poisonPotion);
    Assert.AreEqual(60, hero.Live);

    hero.Accept(poisonPotion);
    hero.Accept(poisonPotion);
    Assert.AreEqual(-40, hero.Live);
    Assert.IsFalse(hero.IsAlive);

    yield return null;
  }
}
