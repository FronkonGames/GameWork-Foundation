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
  /// <summary>
  /// A runtime registry that maps signal types to their ScriptableObject asset instances.
  ///
  /// Used by <see cref="SignalBinding"/> to resolve signal types at runtime and auto-wire
  /// attributed fields or methods to the correct signal instance.
  /// </summary>
  /// <remarks>
  /// The registry is typically owned by a manager or scene bootstrap that implements <see cref="ISignalSource"/>.
  /// Signals are registered once during initialization, then queried by <see cref="SignalBinding.Bind"/>.
  /// </remarks>
  public sealed class SignalRegistry
  {
    private readonly Dictionary<Type, ScriptableObject> signals = new();

    /// <summary> Removes all registered signals from the registry. </summary>
    public void Clear() => signals.Clear();

    /// <summary> Registers a signal asset so it can be resolved by type later. </summary>
    /// <typeparam name="TSignal">The concrete signal type (e.g. <c>PlayerJumpedSignal</c>).</typeparam>
    /// <param name="signal">The ScriptableObject asset instance. Ignored if null.</param>
    public void Register<TSignal>(TSignal signal) where TSignal : ScriptableObject, IScriptableSignal
    {
      if (signal == null)
        return;

      signals[typeof(TSignal)] = signal;
    }

    /// <summary> Attempts to find a registered signal by its runtime type. </summary>
    /// <param name="signalType">The Type of the signal to look up.</param>
    /// <param name="signal">When this method returns true, contains the registered ScriptableObject asset.</param>
    /// <returns><c>true</c> if a signal of the given type is registered; otherwise <c>false</c>.</returns>
    public bool TryGet(Type signalType, out ScriptableObject signal) => signals.TryGetValue(signalType, out signal);
  }
}
