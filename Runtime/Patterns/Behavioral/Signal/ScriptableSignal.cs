////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Martin Bustos @FronkonGames <fronkongames@gmail.com>
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Interface for all ScriptableObject signals to support clearing on domain reload. </summary>
  public interface IScriptableSignal
  {
    /// <summary> Clears all registered callbacks. Called on domain reload and asset reload. </summary>
    void Clear();
  }

  /// <summary>
  /// A parameter-less ScriptableObject event that subscribers can listen to.
  ///
  /// Usage:
  ///   1. Create a ScriptableObject asset via <c>Create → FronkonGames → Signal</c>.
  ///   2. Reference it from MonoBehaviours that need to emit or subscribe.
  ///   3. Subscribe in OnEnable, unsubscribe in OnDisable.
  /// </summary>
  /// <remarks>
  /// The subscriber list is cleared automatically on domain reload (Play Mode enter/exit)
  /// to prevent stale references. Back-iteration is used during Emit to safely handle
  /// callbacks that unsubscribe during invocation.
  /// </remarks>
  public class ScriptableSignal : ScriptableObject, IScriptableSignal
  {
    private readonly List<Action> callbacks = new();

    /// <summary> Registers a callback to be invoked when the signal is emitted. </summary>
    /// <param name="callback">The action to invoke on emit. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is null.</exception>
    public void Subscribe(Action callback)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof(callback));

      if (callbacks.Contains(callback) == false)
        callbacks.Add(callback);
    }

    /// <summary> Removes a previously registered callback from this signal. </summary>
    /// <param name="callback">The action to remove. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is null.</exception>
    public void Unsubscribe(Action callback)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof(callback));

      callbacks.Remove(callback);
    }

    /// <summary> Invokes all registered callbacks. </summary>
    /// <remarks>
    /// Iterates backwards so callbacks may safely unsubscribe during invocation.
    /// Null-conditional invocation protects against destroyed objects.
    /// </remarks>
    public void Emit()
    {
      for (int i = callbacks.Count - 1; i >= 0; --i)
        callbacks[i]?.Invoke();
    }

    /// <summary> Removes all registered callbacks. </summary>
    public void Clear() => callbacks.Clear();

    private void OnEnable() => Clear();
  }

  /// <summary>
  /// A single-parameter ScriptableObject event. Subscribers receive a value of type <typeparamref name="T"/>.
  ///
  /// Usage:
  ///   Create a concrete subclass (e.g. <c>class FloatSignal : ScriptableSignal&lt;float&gt;</c>),
  ///   then create an asset and reference it.
  /// </summary>
  /// <typeparam name="T">The type of value emitted with this signal.</typeparam>
  public class ScriptableSignal<T> : ScriptableObject, IScriptableSignal
  {
    private readonly List<Action<T>> callbacks = new();

    /// <summary> Registers a callback that will receive the emitted value. </summary>
    /// <param name="callback">The action to invoke on emit. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is null.</exception>
    public void Subscribe(Action<T> callback)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof(callback));

      if (callbacks.Contains(callback) == false)
        callbacks.Add(callback);
    }

    /// <summary> Removes a previously registered callback from this signal. </summary>
    /// <param name="callback">The action to remove. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is null.</exception>
    public void Unsubscribe(Action<T> callback)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof(callback));

      callbacks.Remove(callback);
    }

    /// <summary> Invokes all registered callbacks with the given value. </summary>
    /// <param name="value">The value to pass to all subscribers.</param>
    /// <remarks>Iterates backwards so callbacks may safely unsubscribe during invocation.</remarks>
    public void Emit(T value)
    {
      for (int i = callbacks.Count - 1; i >= 0; --i)
        callbacks[i]?.Invoke(value);
    }

    /// <summary> Removes all registered callbacks. </summary>
    public void Clear() => callbacks.Clear();

    private void OnEnable() => Clear();
  }

  /// <summary>
  /// A two-parameter ScriptableObject event. Subscribers receive values of types
  /// <typeparamref name="T0"/> and <typeparamref name="T1"/>.
  /// </summary>
  /// <typeparam name="T0">The type of the first emitted value.</typeparam>
  /// <typeparam name="T1">The type of the second emitted value.</typeparam>
  public class ScriptableSignal<T0, T1> : ScriptableObject, IScriptableSignal
  {
    private readonly List<Action<T0, T1>> callbacks = new();

    /// <summary> Registers a callback that will receive the emitted values. </summary>
    /// <param name="callback">The action to invoke on emit. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is null.</exception>
    public void Subscribe(Action<T0, T1> callback)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof(callback));

      if (callbacks.Contains(callback) == false)
        callbacks.Add(callback);
    }

    /// <summary> Removes a previously registered callback from this signal. </summary>
    /// <param name="callback">The action to remove. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is null.</exception>
    public void Unsubscribe(Action<T0, T1> callback)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof(callback));

      callbacks.Remove(callback);
    }

    /// <summary> Invokes all registered callbacks with the given values. </summary>
    /// <param name="value0">The first value to pass to all subscribers.</param>
    /// <param name="value1">The second value to pass to all subscribers.</param>
    /// <remarks>Iterates backwards so callbacks may safely unsubscribe during invocation.</remarks>
    public void Emit(T0 value0, T1 value1)
    {
      for (int i = callbacks.Count - 1; i >= 0; --i)
        callbacks[i]?.Invoke(value0, value1);
    }

    /// <summary> Removes all registered callbacks. </summary>
    public void Clear() => callbacks.Clear();

    private void OnEnable() => Clear();
  }

  /// <summary>
  /// A three-parameter ScriptableObject event. Subscribers receive values of types
  /// <typeparamref name="T0"/>, <typeparamref name="T1"/>, and <typeparamref name="T2"/>.
  /// </summary>
  /// <typeparam name="T0">The type of the first emitted value.</typeparam>
  /// <typeparam name="T1">The type of the second emitted value.</typeparam>
  /// <typeparam name="T2">The type of the third emitted value.</typeparam>
  public class ScriptableSignal<T0, T1, T2> : ScriptableObject, IScriptableSignal
  {
    private readonly List<Action<T0, T1, T2>> callbacks = new();

    /// <summary> Registers a callback that will receive the emitted values. </summary>
    /// <param name="callback">The action to invoke on emit. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is null.</exception>
    public void Subscribe(Action<T0, T1, T2> callback)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof(callback));

      if (callbacks.Contains(callback) == false)
        callbacks.Add(callback);
    }

    /// <summary> Removes a previously registered callback from this signal. </summary>
    /// <param name="callback">The action to remove. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="callback"/> is null.</exception>
    public void Unsubscribe(Action<T0, T1, T2> callback)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof(callback));

      callbacks.Remove(callback);
    }

    /// <summary> Invokes all registered callbacks with the given values. </summary>
    /// <param name="value0">The first value to pass to all subscribers.</param>
    /// <param name="value1">The second value to pass to all subscribers.</param>
    /// <param name="value2">The third value to pass to all subscribers.</param>
    /// <remarks>Iterates backwards so callbacks may safely unsubscribe during invocation.</remarks>
    public void Emit(T0 value0, T1 value1, T2 value2)
    {
      for (int i = callbacks.Count - 1; i >= 0; --i)
        callbacks[i]?.Invoke(value0, value1, value2);
    }

    /// <summary> Removes all registered callbacks. </summary>
    public void Clear() => callbacks.Clear();

    private void OnEnable() => Clear();
  }
}
