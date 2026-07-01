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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Implement this interface on a manager or bootstrap MonoBehaviour to register
  /// all <see cref="ScriptableSignal"/> assets into a <see cref="SignalRegistry"/> at startup.
  ///
  /// <example>
  /// <code>
  /// public class SignalManager : MonoBehaviour, ISignalSource
  /// {
  ///   [SerializeField] private PlayerJumpedSignal playerJumped;
  ///   [SerializeField] private HealthChangedSignal healthChanged;
  ///
  ///   public void RegisterSignals(SignalRegistry registry)
  ///   {
  ///     registry.Register(playerJumped);
  ///     registry.Register(healthChanged);
  ///   }
  /// }
  /// </code>
  /// </example>
  /// </summary>
  /// <seealso cref="SignalRegistry"/>
  /// <seealso cref="SignalBinding"/>
  public interface ISignalSource
  {
    /// <summary>
    /// Called once during initialization to register all signal assets this owner manages.
    /// </summary>
    /// <param name="registry">The registry to register signal assets into.</param>
    void RegisterSignals(SignalRegistry registry);
  }
}
