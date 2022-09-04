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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Console Command.
  /// </summary>
  public abstract class DevelopmentCommand : ScriptableObject, IDevelopmentCommand
  {
    /// <summary>
    /// Command Id.
    /// </summary>
    public string Id { get => id; set => id = value; }

    /// <summary>
    /// Use.
    /// </summary>
    public string Usage { get => usage; set => usage = value; }

    /// <summary>
    /// Description of use.
    /// </summary>
    public string Description { get => description; set => description = value; }

    [SerializeField]
    private string id;

    [SerializeField]
    private string usage;

    [SerializeField]
    private string description;

    /// <summary>
    /// Execute the command.
    /// </summary>
    /// <param name="args">Arguments</param>
    /// <returns>Success</returns>
    public abstract bool Execute(string[] args);
  }
}
