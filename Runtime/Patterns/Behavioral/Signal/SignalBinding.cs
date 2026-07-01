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
using System.Reflection;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Scans a target object for fields marked with <see cref="SignalSubscribeAttribute"/>
  /// and automatically subscribes/unsubscribes them to the corresponding <see cref="ScriptableSignal"/>
  /// assets via a <see cref="SignalRegistry"/>.
  ///
  /// <para>
  /// For <see cref="ScriptableSignal"/> (parameterless): the field must be <see cref="bool"/>.
  /// It is set to <c>true</c> when the signal is emitted.
  /// </para>
  /// <para>
  /// For <see cref="ScriptableSignal{T}"/> (1 parameter): the field type must match <c>T</c>.
  /// It is set to the emitted value.
  /// </para>
  /// </summary>
  /// <example>
  /// <code>
  /// public class Player : MonoBehaviour
  /// {
  ///   [SignalSubscribe(typeof(PlayerJumpedSignal))]
  ///   private bool jumped;
  ///
  ///   [SignalSubscribe(typeof(HealthChangedSignal))]
  ///   private float health;
  ///
  ///   void OnEnable()  => SignalBinding.Instance.Bind(this);
  ///   void OnDisable() => SignalBinding.Instance.Unbind(this);
  /// }
  /// </code>
  /// </example>
  public sealed class SignalBinding
  {
    private sealed class Subscription
    {
      public ScriptableObject Signal;
      public Delegate Callback;
    }

    private readonly SignalRegistry registry;
    private readonly Dictionary<object, List<Subscription>> subscriptions = new();

    /// <summary> Creates a new <see cref="SignalBinding"/> backed by the given registry. </summary>
    /// <param name="registry">The registry used to resolve signal types to their ScriptableObject assets.</param>
    public SignalBinding(SignalRegistry registry) => this.registry = registry;

    /// <summary>
    /// Scans <paramref name="target"/> for <see cref="SignalSubscribeAttribute"/>-decorated fields
    /// and subscribes each to its corresponding signal asset.
    /// </summary>
    /// <param name="target">The object whose fields will be scanned (typically a MonoBehaviour).</param>
    public void Bind(object target)
    {
      FieldInfo[] fields = target.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      List<Subscription> targetSubscriptions = null;

      for (int i = 0; i < fields.Length; ++i)
      {
        SignalSubscribeAttribute attribute = fields[i].GetCustomAttribute<SignalSubscribeAttribute>();
        if (attribute == null)
          continue;

        if (registry.TryGet(attribute.SignalType, out ScriptableObject signal) == false || signal == null)
        {
          Debug.LogError($"Signal '{attribute.SignalType.Name}' is not registered for '{target}'.", signal);
          continue;
        }

        if (TryCreateCallback(target, fields[i], signal, out Delegate callback) == false)
          continue;

        MethodInfo subscribeMethod = signal.GetType().GetMethod("Subscribe", BindingFlags.Instance | BindingFlags.Public);
        subscribeMethod?.Invoke(signal, new object[] { callback });

        targetSubscriptions ??= new List<Subscription>();
        targetSubscriptions.Add(new Subscription { Signal = signal, Callback = callback });
      }

      if (targetSubscriptions != null)
        subscriptions[target] = targetSubscriptions;
    }

    /// <summary>
    /// Unsubscribes all signal callbacks previously bound to <paramref name="target"/>.
    /// </summary>
    /// <param name="target">The object whose subscriptions should be removed.</param>
    public void Unbind(object target)
    {
      if (subscriptions.TryGetValue(target, out List<Subscription> targetSubscriptions) == false)
        return;

      for (int i = 0; i < targetSubscriptions.Count; ++i)
      {
        Subscription subscription = targetSubscriptions[i];
        MethodInfo unsubscribeMethod = subscription.Signal.GetType().GetMethod("Unsubscribe", BindingFlags.Instance | BindingFlags.Public);
        unsubscribeMethod?.Invoke(subscription.Signal, new object[] { subscription.Callback });
      }

      subscriptions.Remove(target);
    }

    /// <summary> Unsubscribes all callbacks for all bound targets. </summary>
    public void UnbindAll()
    {
      foreach (KeyValuePair<object, List<Subscription>> entry in subscriptions)
      {
        List<Subscription> targetSubscriptions = entry.Value;
        for (int i = 0; i < targetSubscriptions.Count; ++i)
        {
          Subscription subscription = targetSubscriptions[i];
          MethodInfo unsubscribeMethod = subscription.Signal.GetType().GetMethod("Unsubscribe", BindingFlags.Instance | BindingFlags.Public);
          unsubscribeMethod?.Invoke(subscription.Signal, new object[] { subscription.Callback });
        }
      }

      subscriptions.Clear();
    }

    private static bool TryCreateCallback(object target, FieldInfo field, ScriptableObject signal, out Delegate callback)
    {
      callback = null;
      Type signalType = signal.GetType();

      if (signalType.BaseType != null && signalType.BaseType.IsGenericType == true)
      {
        Type valueType = signalType.BaseType.GetGenericArguments()[0];
        if (field.FieldType != valueType)
        {
          Debug.LogError($"Field '{field.Name}' on '{target}' expects '{valueType.Name}' but is '{field.FieldType.Name}'.", signal);
          return false;
        }

        Type binderType = typeof(FieldBinder<>).MakeGenericType(valueType);
        object binder = Activator.CreateInstance(binderType, target, field);
        callback = Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(valueType), binder, binderType.GetMethod("Receive"));
        return true;
      }

      if (typeof(ScriptableSignal).IsAssignableFrom(signalType) == true)
      {
        if (field.FieldType != typeof(bool))
        {
          Debug.LogError($"Field '{field.Name}' on '{target}' must be bool for parameterless signals.", signal);
          return false;
        }

        VoidFieldBinder binder = new(target, field);
        callback = Delegate.CreateDelegate(typeof(Action), binder, typeof(VoidFieldBinder).GetMethod("Receive"));
        return true;
      }

      Debug.LogError($"Signal '{signalType.Name}' is not supported by SignalBinding.", signal);
      return false;
    }

    private sealed class FieldBinder<T>
    {
      private readonly object target;
      private readonly FieldInfo field;

      public FieldBinder(object target, FieldInfo field)
      {
        this.target = target;
        this.field = field;
      }

      public void Receive(T value) => field.SetValue(target, value);
    }

    private sealed class VoidFieldBinder
    {
      private readonly object target;
      private readonly FieldInfo field;

      public VoidFieldBinder(object target, FieldInfo field)
      {
        this.target = target;
        this.field = field;
      }

      public void Receive() => field.SetValue(target, true);
    }
  }
}
