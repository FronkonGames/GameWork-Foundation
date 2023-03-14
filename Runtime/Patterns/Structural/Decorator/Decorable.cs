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
  /// <summary> It allows alter dynamically behaviors. </summary>
  public abstract class Decorable
  {
    /// <summary> Change the behavior. </summary>
    /// <value>Decorator component</value>
    public IDecorator Decorator { get; set; }

    /// <summary> Perform the operation if there is an associated behavior. </summary>
    public void Operation() => Decorator?.Operation();
  }

  /// <summary> It allows alter dynamically behaviors. </summary>
  public abstract class Decorable<R>
  {
    /// <summary> Change the behavior. </summary>
    /// <value>Decorator component</value>
    public IDecorator<R> Decorator { get; set; }

    /// <summary> Perform the operation if there is an associated behavior. </summary>
    /// <value>Value</value>
    public R Operation() => Decorator != null ? Decorator.Operation() : default(R);
  }

  /// <summary> It allows alter dynamically behaviors. </summary>
  public abstract class Decorable<R, T>
  {
    /// <summary> Change the behavior. </summary>
    /// <value>Decorator component</value>
    public IDecorator<R, T> Decorator { get; set; }

    /// <summary> Perform the operation if there is an associated behavior. </summary>
    /// <param name="value">Value</param>
    /// <returns>Value</returns>    
    public R Operation(T value) => Decorator != null ? Decorator.Operation(value) : default(R);
  }

  /// <summary> It allows alter dynamically behaviors. </summary>
  public abstract class Decorable<R, T0, T1>
  {
    /// <summary> Change the behavior. </summary>
    /// <value>Decorator component</value>
    public IDecorator<R, T0, T1> Decorator { get; set; }

    /// <summary> Perform the operation if there is an associated behavior. </summary>
    /// <param name="value0">First value</param>
    /// <param name="value1">Second value</param>
    /// <returns>Value</returns>    
    public R Operation(T0 value0, T1 value1) => Decorator != null ? Decorator.Operation(value0, value1) : default(R);
  }
}
