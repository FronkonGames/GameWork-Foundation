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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Receive the action of the command.
  /// </summary>
  public interface ICommandReceiver
  {
    /// <summary>
    /// Perform the action of the command.
    /// </summary>
    /// <returns>True if the execution was successful.</returns>
    bool DoAction();

    /// <summary>
    /// Undoe the changes of OnExecute.
    /// </summary>
    void UndoAction();
  }

  /// <summary>
  /// Receive the action of the command.
  /// </summary>
  public interface ICommandReceiver<T>
  {
    /// <summary>
    /// Perform the action of the command.
    /// </summary>
    /// <param name="value">Command' parameter.</param>
    /// <returns>True if the execution was successful.</returns>
    bool DoAction(T value);

    /// <summary>
    /// Undoe the changes of OnExecute.
    /// </summary>
    void UndoAction();
  }

  /// <summary>
  /// Receive the action of the command.
  /// </summary>
  public interface ICommandReceiver<T0, T1>
  {
    /// <summary>
    /// Perform the action of the command.
    /// </summary>
    /// <param name="value0">First parameter</param>
    /// <param name="value1">Second parameter</param>
    /// <returns>True if the execution was successful.</returns>
    bool DoAction(T0 value0, T1 value1);

    /// <summary>
    /// Undoe the changes of OnExecute.
    /// </summary>
    void UndoAction();
  }

  /// <summary>
  /// Receive the action of the command.
  /// </summary>
  public interface ICommandReceiver<T0, T1, T2>
  {
    /// <summary>
    /// Perform the action of the command.
    /// </summary>
    /// <param name="value0">First parameter</param>
    /// <param name="value1">Second parameter</param>
    /// <param name="value3">Third parameter</param>
    /// <returns>True if the execution was successful.</returns>
    bool DoAction(T0 value0, T1 value1, T2 value2);

    /// <summary>
    /// Undoe the changes of OnExecute.
    /// </summary>
    void UndoAction();
  }
}
