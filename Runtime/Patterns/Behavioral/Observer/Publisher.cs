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
  /// <summary>
  /// .
  /// </summary>
  public abstract class Publisher
  {
    private readonly List<IObserver> observers = new();

    public void Notify()
    {
      for (int i = 0; i < observers.Count; ++i)
        observers[i].OnNotify();
    }

    public void AddObserver(IObserver observer) => observers.Add(observer);

    public void AddObservers(IEnumerable<IObserver> observers) => this.observers.AddRange(observers);

    public void RemoveObserver(IObserver observer) => observers.Remove(observer);

    public void RemoveObservers(IEnumerable<IObserver> observers) => this.observers.RemoveRange(observers);
  }

  /// <summary>
  /// .
  /// </summary>
  public abstract class Publisher<T>
  {
    private readonly List<IObserver<T>> observers = new();

    public void Notify(T value)
    {
      for (int i = 0; i < observers.Count; ++i)
        observers[i].OnNotify(value);
    }

    public void AddObserver(IObserver<T> observer) => observers.Add(observer);

    public void AddObservers(IEnumerable<IObserver<T>> observers) => this.observers.AddRange(observers);

    public void RemoveObserver(IObserver<T> observer) => observers.Remove(observer);

    public void RemoveObservers(IEnumerable<IObserver<T>> observers) => this.observers.RemoveRange(observers);
  }

  /// <summary>
  /// .
  /// </summary>
  public abstract class Publisher<T0, T1>
  {
    private readonly List<IObserver<T0, T1>> observers = new();

    public void Notify(T0 value0, T1 value1)
    {
      for (int i = 0; i < observers.Count; ++i)
        observers[i].OnNotify(value0, value1);
    }

    public void AddObserver(IObserver<T0, T1> observer) => observers.Add(observer);

    public void AddObservers(IEnumerable<IObserver<T0, T1>> observers) => this.observers.AddRange(observers);

    public void RemoveObserver(IObserver<T0, T1> observer) => observers.Remove(observer);

    public void RemoveObservers(IEnumerable<IObserver<T0, T1>> observers) => this.observers.RemoveRange(observers);
  }

  /// <summary>
  /// .
  /// </summary>
  public abstract class Publisher<T0, T1, T2>
  {
    private readonly List<IObserver<T0, T1, T2>> observers = new();

    public void Notify(T0 value0, T1 value1, T2 value2)
    {
      for (int i = 0; i < observers.Count; ++i)
        observers[i].OnNotify(value0, value1, value2);
    }

    public void AddObserver(IObserver<T0, T1, T2> observer) => observers.Add(observer);

    public void AddObservers(IEnumerable<IObserver<T0, T1, T2>> observers) => this.observers.AddRange(observers);

    public void RemoveObserver(IObserver<T0, T1, T2> observer) => observers.Remove(observer);

    public void RemoveObservers(IEnumerable<IObserver<T0, T1, T2>> observers) => this.observers.RemoveRange(observers);
  }
}
