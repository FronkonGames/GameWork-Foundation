
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
using System.Collections.Generic;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Generic 2D grid.
  /// </summary>
  [Serializable]
  public class Grid2D<T> : IEnumerable<T>
  {
    /// <summary> </summary>
    public int Width { get => width; private set => width = value; }

    /// <summary> </summary>
    public int Height { get => height; private set => height = value; }

    /// <summary> </summary>
    public int Count => width * height;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public ref T this[int x, int y]
    {
      get
      {
        Check.True(IsValid(x, y));

        return ref data[x + y * width];
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public ref T this[int index]
    {
      get
      {
        Check.GreaterOrEqual(index, 0);
        Check.Less(index, data.Length);

        return ref data[index];
      }
    }

    [SerializeField]
    protected T[] data;
    
    [SerializeField]
    protected int width;
    
    [SerializeField]
    protected int height;

    public Grid2D() : this(1, 1) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public Grid2D(int width, int height)
    {
      this.width = width;
      this.height = height;

      data = new T[width * height];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="value"></param>
    public Grid2D(int width, int height, T value) : this(width, height)
    {
      for (int i = 0; i < data.Length; ++i)
        data[i] = value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="grid"></param>
    public Grid2D(Grid2D<T> grid)
    {
      width = grid.width;
      height = grid.height;

      data = (T[])grid.data.Clone();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsValid(int x, int y) => x > 0 && y > 0 && x < width && y < height;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public int TryGetValue(int x, int y, out T data)
    {
      if (IsValid(x, y) == false)
      {
        data = default(T);
        return -1;
      }

      int index = x + y * width;
      data = this[x, y];
      return index;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetX(int index) => index % width;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetY(int index) => index / width;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void GetXY(int index, out int x, out int y)
    {
      Check.GreaterOrEqual(index, 0);
      Check.Less(index, data.Length);
            
      x = index % width;
      y = index / width;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int GetIndex(int x, int y)
    {
      Check.True(IsValid(x, y));

      return x + y * width;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public T[] GetArray() => data;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newWidth"></param>
    /// <param name="newHeight"></param>
    public void Resize(int newWidth, int newHeight)
    {
      if (width == newWidth)
      {
        if (height == newHeight)
          return;

        height = newHeight;
        Array.Resize<T>(ref data, width * height);
        return;
      }

      T[] newData = new T[newWidth * newHeight];
      int copyWidth = newWidth < width ? newWidth : width;
      int copyHeight = newHeight < height ? newHeight : height;
      for (int y = 0; y < copyHeight; ++y)
      {
        for (int x = 0; x < copyWidth; ++x)
        {
          T oldValue = this[x, y];
          newData[x + y * newWidth] = oldValue;
        }
      }

      data = newData;
      width = newWidth;
      height = newHeight;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void Clear() => Clear(default(T));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void Clear(T value)
    {
      for (int i = 0; i < data.Length; ++i)
        data[i] = value;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerator<T> GetEnumerator()
    {
      if (IsClass == true)
      {
        for (int i = 0; i < data.Length; ++i)
        {
          if (data[i] != null)
            yield return data[i];
        }
      }
      else
      {
        for (int i = 0; i < data.Length; ++i)
          yield return data[i];
      }
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    private static readonly bool IsClass = typeof(T).IsClass;
  }
}
