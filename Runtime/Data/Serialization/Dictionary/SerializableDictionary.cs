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
  /// <summary> Serializable dictionary for Unity serialization. </summary>
  [Serializable]
  public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
  {
    [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> list = new();

    /// <summary> On before serialize. </summary>
    public void OnBeforeSerialize()
    {
      list.Clear();
      foreach (KeyValuePair<TKey, TValue> pair in this)
        list.Add(new SerializableKeyValuePair<TKey, TValue>(pair.Key, pair.Value));
    }

    /// <summary> On after deserialize. </summary>
    public void OnAfterDeserialize()
    {
      Clear();
      for (int i = 0; i < list.Count; i++)
      {
        if (ContainsKey(list[i].Key) == false)
          TryAdd(list[i].Key, list[i].Value);
      }
    }

    /// <summary> Create from a standard dictionary. </summary>
    public static SerializableDictionary<TKey, TValue> FromDictionary(Dictionary<TKey, TValue> data)
    {
      SerializableDictionary<TKey, TValue> dict = new();
      foreach (KeyValuePair<TKey, TValue> pair in data)
        dict.Add(pair.Key, pair.Value);
      return dict;
    }

    /// <summary> Create from a list of key-value pairs. </summary>
    public static SerializableDictionary<TKey, TValue> FromKeyPairValueList(List<KeyValuePair<TKey, TValue>> data)
    {
      SerializableDictionary<TKey, TValue> dict = new();
      foreach (KeyValuePair<TKey, TValue> pair in data)
        dict.Add(pair.Key, pair.Value);
      return dict;
    }

    /// <summary> Create from a list of serializable key-value pairs. </summary>
    public static SerializableDictionary<TKey, TValue> FromKeyPairValueList(List<SerializableKeyValuePair<TKey, TValue>> data)
    {
      SerializableDictionary<TKey, TValue> dict = new();
      foreach (SerializableKeyValuePair<TKey, TValue> pair in data)
        dict.Add(pair.key, pair.value);
      return dict;
    }
  }
}
