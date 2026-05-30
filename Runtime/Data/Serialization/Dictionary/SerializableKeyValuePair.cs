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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Serializable key-value pair for Unity serialization. </summary>
  [Serializable]
  public class SerializableKeyValuePair<TKey, TValue> : IEquatable<SerializableKeyValuePair<TKey, TValue>>
  {
    [SerializeField] public TKey key;
    [SerializeField] public TValue value;

    /// <summary> The key. </summary>
    public TKey Key => key;

    /// <summary> The value. </summary>
    public TValue Value
    {
      get => value;
      set => this.value = value;
    }

    /// <summary> Default constructor. </summary>
    public SerializableKeyValuePair() { }

    /// <summary> Create with key and value. </summary>
    public SerializableKeyValuePair(TKey key, TValue value)
    {
      this.key = key;
      this.value = value;
    }

    /// <summary> Convert from standard KeyValuePair. </summary>
    public static implicit operator SerializableKeyValuePair<TKey, TValue>(KeyValuePair<TKey, TValue> other) =>
      new(other.Key, other.Value);

    /// <summary> Convert to standard KeyValuePair. </summary>
    public static implicit operator KeyValuePair<TKey, TValue>(SerializableKeyValuePair<TKey, TValue> other) =>
      new(other.key, other.value);

    /// <summary> Equality check. </summary>
    public bool Equals(SerializableKeyValuePair<TKey, TValue> other)
    {
      if (other is null)
        return false;

      return EqualityComparer<TKey>.Default.Equals(key, other.key) &&
             EqualityComparer<TValue>.Default.Equals(value, other.value);
    }

    /// <summary> Equality check. </summary>
    public override bool Equals(object obj) => obj is SerializableKeyValuePair<TKey, TValue> other && Equals(other);

    /// <summary> Get hash code. </summary>
    public override int GetHashCode() => HashCode.Combine(key, value);

    /// <summary> String representation. </summary>
    public override string ToString() => $"(Key: {key}, Value: {value})";
  }
}
