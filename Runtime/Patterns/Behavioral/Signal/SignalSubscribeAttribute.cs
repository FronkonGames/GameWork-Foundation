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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Marks a field for automatic signal subscription via <see cref="SignalBinding"/>.
  ///
  /// <para>
  /// For <see cref="ScriptableSignal"/> (parameterless): the field must be <see cref="bool"/>
  /// and will be set to <c>true</c> when the signal is emitted.
  /// </para>
  /// <para>
  /// For <see cref="ScriptableSignal{T}"/> (1 parameter): the field type must match <c>T</c>
  /// and will be set to the emitted value.
  /// </para>
  /// </summary>
  /// <example>
  /// <code>
  /// [SignalSubscribe(typeof(PlayerJumpedSignal))]
  /// private bool jumped;
  ///
  /// [SignalSubscribe(typeof(HealthChangedSignal))]
  /// private float health;
  /// </code>
  /// </example>
  /// <seealso cref="SignalBinding"/>
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public sealed class SignalSubscribeAttribute : Attribute
  {
    /// <summary> The runtime Type of the ScriptableSignal to subscribe to. </summary>
    public Type SignalType { get; }

    /// <summary> Creates a new attribute targeting the given signal type. </summary>
    /// <param name="signalType">The concrete signal type (e.g. <c>typeof(PlayerJumpedSignal)</c>).</param>
    public SignalSubscribeAttribute(Type signalType) => SignalType = signalType;
  }
}
