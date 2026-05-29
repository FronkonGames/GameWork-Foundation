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
  /// <summary> Generic factory base class using a dictionary of creator functions. </summary>
  /// <typeparam name="TKey">The key type.</typeparam>
  /// <typeparam name="TProduct">The base product type.</typeparam>
  public abstract class Factory<TKey, TProduct> : IFactory<TKey, TProduct>
  {
    private readonly Dictionary<TKey, Func<TProduct>> creators = new();

    /// <summary> Register a creator function for a key. </summary>
    protected void Register(TKey key, Func<TProduct> creator) => creators[key] = creator;

    /// <summary> Create a product by key. </summary>
    public TProduct Create(TKey key)
    {
      if (creators.TryGetValue(key, out Func<TProduct> creator))
        return creator();

      throw new KeyNotFoundException($"No creator registered for key '{key}'.");
    }
  }

  /// <summary> Generic factory base class with parameters. </summary>
  /// <typeparam name="TKey">The key type.</typeparam>
  /// <typeparam name="TProduct">The base product type.</typeparam>
  /// <typeparam name="TParam">The parameter type.</typeparam>
  public abstract class Factory<TKey, TProduct, TParam> : IFactory<TKey, TProduct, TParam>
  {
    private readonly Dictionary<TKey, Func<TParam, TProduct>> creators = new();

    /// <summary> Register a creator function for a key. </summary>
    protected void Register(TKey key, Func<TParam, TProduct> creator) => creators[key] = creator;

    /// <summary> Create a product by key with a parameter. </summary>
    public TProduct Create(TKey key, TParam param)
    {
      if (creators.TryGetValue(key, out Func<TParam, TProduct> creator))
        return creator(param);

      throw new KeyNotFoundException($"No creator registered for key '{key}'.");
    }
  }
}
