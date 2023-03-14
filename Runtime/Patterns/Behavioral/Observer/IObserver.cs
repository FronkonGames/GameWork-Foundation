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
  /// <summary> Observer Interface. </summary>
  public interface IObserver
  {
    /// <summary> When the Observer is notified  </summary>
    void OnNotify();
  }

  /// <summary> Observer Interface, one parameter. </summary>
  public interface IObserver<in T>
  {
    /// <summary> When the Observer is notified  </summary>
    /// <param name="value">Parameter</param>
    void OnNotify(T value);
  }

  /// <summary> Observer Interface, two parameters. </summary>
  public interface IObserver<in T0, in T1>
  {
    /// <summary> When the Observer is notified  </summary>
    /// <param name="value0">Parameter</param>
    /// <param name="value1">Parameter</param>
    void OnNotify(T0 value0, T1 value1);
  }

  /// <summary> Observer Interface, three parameters. </summary>
  public interface IObserver<in T0, in T1, in T2>
  {
    /// <summary> When the Observer is notified  </summary>
    /// <param name="value0">Parameter</param>
    /// <param name="value1">Parameter</param>
    /// <param name="value2">Parameter</param>
    void OnNotify(T0 value0, T1 value1, T2 value2);
  }
}
