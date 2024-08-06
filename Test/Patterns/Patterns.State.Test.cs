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
  class StartState : State { }
  class IntroState : State { }
  class MenuState  : State { }
  class GameState  : State { }
  class PauseState : State { }
  class QuitState  : State { }

  class TrueCondition : ICondition { public bool Check() => true; }

  class FalseCondition : ICondition { public bool Check() => false; }

  /// <summary> State tests. </summary>
  [UnityTest]
  public IEnumerator State()
  {
    StartState startState = new();
    IntroState introState = new();
    MenuState menuState = new();
    GameState gameState = new();
    QuitState quitState = new();

    TrueCondition trueCondition = new();
    FalseCondition falseCondition = new();

    StateMachine stateMachine = new();

    stateMachine.AddTransition(startState, introState, trueCondition);
    stateMachine.AddTransition(introState, menuState, trueCondition);
    stateMachine.AddTransition(menuState, quitState, falseCondition);
    stateMachine.AddTransition(menuState, gameState, trueCondition);
    stateMachine.AddTransition(gameState, quitState, trueCondition);

    Assert.IsNull(stateMachine.CurrentState);

    stateMachine.ChangeState(startState);

    Assert.AreEqual(startState, stateMachine.CurrentState);

    stateMachine.Update();

    Assert.AreEqual(introState, stateMachine.CurrentState);

    stateMachine.Update();

    Assert.AreEqual(menuState, stateMachine.CurrentState);

    stateMachine.Update();

    Assert.AreEqual(gameState, stateMachine.CurrentState);

    stateMachine.Update();

    Assert.AreEqual(quitState, stateMachine.CurrentState);

    stateMachine.ChangeState(null);

    Assert.IsNull(stateMachine.CurrentState);

    yield return null;
  }
}
