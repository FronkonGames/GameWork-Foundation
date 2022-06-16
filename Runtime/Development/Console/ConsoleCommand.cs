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
  /// <summary>
  /// 
  /// </summary>
  public class ConsoleCommand : ConsoleCommandBase
  {
    private Action action;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="description"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public ConsoleCommand(string id, string description, Action action) : base(id, description) => this.action = action;

    /// <summary>
    /// Execute the commnand.
    /// </summary>
    public void Execute() => action?.Invoke();
  }

  /// <summary>
  /// 
  /// </summary>
  public class ConsoleCommand<T> : ConsoleCommandBase
  {
    private Action<T> action;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="description"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public ConsoleCommand(string id, string description, Action<T> action) : base(id, description) => this.action = action;

    /// <summary>
    /// Execute the commnand.
    /// </summary>
    public void Execute(T value) => action?.Invoke(value);
  }

  /// <summary>
  /// 
  /// </summary>
  public class ConsoleCommand<T0, T1> : ConsoleCommandBase
  {
    private Action<T0, T1> action;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="description"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public ConsoleCommand(string id, string description, Action<T0, T1> action) : base(id, description) => this.action = action;

    /// <summary>
    /// Execute the commnand.
    /// </summary>
    public void Execute(T0 value0, T1 value1) => action?.Invoke(value0, value1);
  }

  /// <summary>
  /// 
  /// </summary>
  public class ConsoleCommand<T0, T1, T2> : ConsoleCommandBase
  {
    private Action<T0, T1, T2> action;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="description"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public ConsoleCommand(string id, string description, Action<T0, T1, T2> action) : base(id, description) => this.action = action;

    /// <summary>
    /// Execute the commnand.
    /// </summary>
    public void Execute(T0 value0, T1 value1, T2 value2) => action?.Invoke(value0, value1, value2);
  }
}
