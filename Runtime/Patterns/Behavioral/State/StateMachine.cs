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
using System.Collections.Generic;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> State machine base class. </summary>
  public class StateMachine : IStateMachine, IDisposable
  {
    /// <inheritdoc/>
    public IState CurrentState { get; protected set; }

    /// <inheritdoc/>
    public bool Running { get; set; }

    protected readonly List<ITransition> transitions = new();

    /// <summary> Constructor. </summary>
    /// <param name="running"> Update the state machine. </param>
    public StateMachine(bool running = true) => Running = running;

    /// <summary> IDisposable. </summary>
    public void Dispose() => ChangeState(null);

    /// <summary> Destructor. </summary>
    ~StateMachine() => Dispose();

    /// <inheritdoc/>
    public void Update()
    {
      if (Running == true && CurrentState != null)
      {
        CurrentState.OnUpdate();

        for (int i = 0; i < transitions.Count; ++i)
        {
          if (CurrentState == transitions[i].From && transitions[i].Evaluate() == true)
          {
            ChangeState(transitions[i].To);

            break;
          }
        }
      }
    }

    /// <inheritdoc/>
    public void ChangeState(IState state)
    {
      if (state != CurrentState)
      {
        CurrentState?.OnExit();

        CurrentState = state;

        CurrentState?.OnEnter();
      }
    }

    /// <inheritdoc/>
    public void AddTransition(IState from, IState to, Func<bool> condition) => AddTransition(from, to, new Condition() { Function = condition });

    /// <inheritdoc/>
    public void AddTransition(IState from, IState to, ICondition condition) => transitions.Add(new Transition() { From = from, To = to, Condition = condition,  });
  }
}
