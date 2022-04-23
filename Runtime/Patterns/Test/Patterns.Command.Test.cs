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
using Object = UnityEngine.Object;

/// <summary>
/// Patterns tests.
/// </summary>
public partial class PatternsTests
{
  private class AddCommand : ICommand
  {
    public int Value { get; set; }

    public AddCommand(int value) { Value = value; }

    public bool Execute() { Value++; return true; }

    public void Undo() => Value--;

    public void Redo() => Value++;
  }

  private CommandInvoker invoker = new CommandInvoker();

  /// <summary>
  /// Command test.
  /// </summary>
  [UnityTest]
  public IEnumerator Command()
  {
    AddCommand command = new AddCommand(0);

    invoker.Execute(command);
    Assert.AreEqual(command.Value, 1);

    invoker.Undo();
    Assert.AreEqual(command.Value, 0);

    invoker.Redo();
    Assert.AreEqual(command.Value, 1);

    yield return null;
  }
}
