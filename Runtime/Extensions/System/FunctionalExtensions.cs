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
using System.Runtime.CompilerServices;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Functional-style extension methods. </summary>
  public static class FunctionalExtensions
  {
    /// <summary> Executes an action with the current value and returns the same value (fluent side effect). </summary>
    /// <param name="value">The value.</param>
    /// <param name="action">The action to execute.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>The same value that was passed in.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Do<T>(this T value, Action<T> action)
    {
      action?.Invoke(value);

      return value;
    }

    /// <summary> Applies a transformation function to the value and returns the result. </summary>
    /// <param name="value">The value.</param>
    /// <param name="func">The transformation function.</param>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <returns>The transformed value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Map<TIn, TOut>(this TIn value, Func<TIn, TOut> func)
    {
      return func(value);
    }
  }
}
