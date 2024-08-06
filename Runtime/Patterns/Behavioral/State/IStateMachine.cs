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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Interface for state machine. </summary>
  public interface IStateMachine
  {
    /// <summary> Current status (can be null). </summary>
    IState CurrentState { get; }

    /// <summary> Activates / deactivates the status machine. </summary>
    bool Running { get; set; }

    /// <summary> Must be executed in each frame. </summary>
    void Update();

    /// <summary> Changes the current state to other or null. </summary>
    /// <param name="state"> Other state or null. </param>
    void ChangeState(IState state);

    /// <summary> Adds a new transition between states. </summary>
    /// <param name="from"> Origin state. </param>
    /// <param name="to"> Destination state. </param>
    /// <param name="condition"> Function to make the transition. </param>
    void AddTransition(IState from, IState to, Func<bool> condition);

    /// <summary> Adds a new transition between states. </summary>
    /// <param name="from"> Origin state. </param>
    /// <param name="to"> Destination state. </param>
    /// <param name="condition"> Condition to make the transition. </param>
    void AddTransition(IState from, IState to, ICondition condition);
  }
}
