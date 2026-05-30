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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Generic option that can use a local value or reference a global holder. </summary>
  /// <typeparam name="TValue">The value type.</typeparam>
  /// <typeparam name="THolder">The holder type.</typeparam>
  [System.Serializable]
  public class Option<TValue, THolder> where THolder : ScriptableObject, IValueHolder<TValue>
  {
    [SerializeField]
    private ValueHolderMode mode = ValueHolderMode.Disabled;

    [SerializeField]
    private TValue localValue = default;

    [SerializeField]
    private THolder globalHolder = null;

    /// <summary> Whether this option is enabled. </summary>
    public bool Enabled => mode != ValueHolderMode.Disabled;

    /// <summary> The current mode. </summary>
    public ValueHolderMode Mode => mode;

    /// <summary> The local value. </summary>
    public TValue LocalValue => localValue;

    /// <summary> The global holder reference. </summary>
    public THolder GlobalHolder => globalHolder;

    /// <summary> Gets the effective value based on the current mode. </summary>
    public TValue Value
    {
      get
      {
        switch (mode)
        {
          case ValueHolderMode.LocalValue:
            return localValue;
          case ValueHolderMode.GlobalValue:
            return globalHolder != null ? globalHolder.GetValue() : default;
          case ValueHolderMode.Disabled:
          default:
            return default;
        }
      }
    }

    /// <summary> Constructor with disabled mode. </summary>
    public Option()
    {
      mode = ValueHolderMode.Disabled;
      localValue = default;
      globalHolder = null;
    }

    /// <summary> Constructor with local value. </summary>
    public Option(TValue value)
    {
      mode = ValueHolderMode.LocalValue;
      localValue = value;
      globalHolder = null;
    }

    /// <summary> Constructor with global holder. </summary>
    public Option(THolder holder)
    {
      mode = ValueHolderMode.GlobalValue;
      localValue = default;
      globalHolder = holder;
    }

    /// <summary> Sets the mode to disabled. </summary>
    public void SetDisabled() => mode = ValueHolderMode.Disabled;

    /// <summary> Sets a local value. </summary>
    public void SetLocalValue(TValue value)
    {
      mode = ValueHolderMode.LocalValue;
      localValue = value;
    }

    /// <summary> Sets a global holder reference. </summary>
    public void SetGlobalHolder(THolder holder)
    {
      mode = ValueHolderMode.GlobalValue;
      globalHolder = holder;
    }

    /// <summary> Implicit conversion to TValue. </summary>
    public static implicit operator TValue(Option<TValue, THolder> option) => option.Value;

    /// <summary> Tries to get the value. Returns true if enabled. </summary>
    public bool TryGetValue(out TValue value)
    {
      value = Value;
      return Enabled && (mode != ValueHolderMode.GlobalValue || globalHolder != null);
    }

    /// <summary> Returns a string representation. </summary>
    public override string ToString()
    {
      if (!Enabled)
        return "Disabled";

      return mode == ValueHolderMode.LocalValue
        ? $"Local: {localValue}"
        : $"Global: {(globalHolder != null ? globalHolder.GetValue().ToString() : "null")}";
    }
  }
}