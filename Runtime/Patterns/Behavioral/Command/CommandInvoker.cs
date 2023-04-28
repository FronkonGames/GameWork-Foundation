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
using System.Collections.Generic;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Asks the command to carry out the request. </summary>
  public class CommandInvoker
  {
    private readonly Stack<ICommand> undo = new();
    private readonly Stack<ICommand> redo = new();

    /// <summary> Execute a command and add it to the undo redo stack if success. </summary>
    /// <param name="command"></param>
    /// <returns>True if the execution was successful.</returns>
    public bool Execute(ICommand command)
    {
      if (command.OnExecute() == true)
      {
        undo.Push(command);
        redo.Clear();

        return true;
      }

      return false;
    }

    /// <summary> Reverse the last command executed. </summary>
    public void Undo()
    {
      if (undo.Count > 0)
      {
        ICommand executed = undo.Pop();
        executed.OnUndo();
        redo.Push(executed);
      }
      else
        Log.Warning("Undo stack empty");
    }

    /// <summary> Replay the last undone command. </summary>
    public void Redo()
    {
      if (redo.Count > 0)
      {
        ICommand undone = redo.Pop();
        undone.OnExecute();
        undo.Push(undone);
      }
      else
        Log.Warning("Redo stack empty");
    }
  }
}
