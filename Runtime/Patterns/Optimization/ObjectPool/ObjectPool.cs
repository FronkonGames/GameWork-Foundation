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
using System.Collections.Generic;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Generic object pool that reuses objects to reduce allocation overhead. </summary>
  /// <typeparam name="T">The object type to pool.</typeparam>
  public class ObjectPool<T> where T : class
  {
    private readonly Stack<T> pool;
    private readonly Func<T> createFunc;
    private readonly Action<T> onGet;
    private readonly Action<T> onRelease;

    /// <summary> Number of available objects in the pool. </summary>
    public int CountAvailable => pool.Count;

    /// <summary> Create a new object pool. </summary>
    /// <param name="createFunc">Function to create a new object.</param>
    /// <param name="onGet">Called when an object is retrieved from the pool.</param>
    /// <param name="onRelease">Called when an object is returned to the pool.</param>
    /// <param name="initialSize">Initial number of objects to pre-create.</param>
    public ObjectPool(Func<T> createFunc, Action<T> onGet = null, Action<T> onRelease = null, int initialSize = 0)
    {
      this.createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
      this.onGet = onGet;
      this.onRelease = onRelease;
      pool = new Stack<T>(initialSize);

      for (int i = 0; i < initialSize; i++)
        pool.Push(createFunc());
    }

    /// <summary> Get an object from the pool. Creates a new one if the pool is empty. </summary>
    public T Get()
    {
      T item = pool.Count > 0 ? pool.Pop() : createFunc();
      onGet?.Invoke(item);
      return item;
    }

    /// <summary> Return an object to the pool. </summary>
    public void Release(T item)
    {
      onRelease?.Invoke(item);
      pool.Push(item);
    }

    /// <summary> Clear all objects from the pool. </summary>
    public void Clear() => pool.Clear();
  }
}
