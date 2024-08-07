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
  /// <summary> Strategy interface. </summary>
  public interface IStrategy
  {
    /// <summary> Execute the strategy. </summary>
    void OnExecute();
  }

  /// <summary> Strategy interface. </summary>
  public interface IStrategy<out R>
  {
    /// <summary> Execute the strategy. </summary>
    R OnExecute();
  }

  /// <summary> Strategy interface with one parameter. </summary>
  public interface IStrategy<out R, in T>
  {
    /// <summary> Execute the strategy. </summary>
    R OnExecute(T value);
  }

  /// <summary> Strategy interface with two parameters. </summary>
  public interface IStrategy<out R, in T0, in T1>
  {
    /// <summary> Execute the strategy. </summary>
    R OnExecute(T0 value0, T1 value1);
  }

  /// <summary> Strategy interface with three parameters. </summary>
  public interface IStrategy<out R, in T0, in T1, in T2>
  {
    /// <summary> Execute the strategy. </summary>
    R OnExecute(T0 value0, T1 value1, T2 value2);
  }
}
