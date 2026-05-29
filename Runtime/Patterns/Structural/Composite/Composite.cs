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
  /// <summary> Base composite class that manages child components. </summary>
  /// <typeparam name="T">The component type.</typeparam>
  public abstract class Composite<T> : IComposite<T>
  {
    /// <summary> The list of child components. </summary>
    protected readonly List<T> children = new();

    /// <summary> Number of children. </summary>
    public int Count => children.Count;

    /// <summary> Whether this component has children. </summary>
    public bool HasChildren => children.Count > 0;

    /// <summary> Add a child component. </summary>
    public virtual void Add(T child) => children.Add(child);

    /// <summary> Remove a child component. </summary>
    public virtual bool Remove(T child) => children.Remove(child);

    /// <summary> Get child at index. </summary>
    public virtual T GetChild(int index) => children[index];

    /// <summary> Get all children. </summary>
    public IReadOnlyList<T> GetChildren() => children.AsReadOnly();
  }
}
