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
using System.Collections.Concurrent;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Thread-safe Service Locator implementation. </summary>
  public class ServiceLocator : IServiceLocator
  {
    // Thread-safe dictionary.
    private readonly ConcurrentDictionary<Type, IService> services = new();

    internal static readonly List<Type> NoDependencies = new();

    /// </inheritdoc>
    public void Register<T>(T service) where T : IService => Register(service.GetType(), service);

    /// </inheritdoc>
    public void Register(Type type, IService service)
    {
      Check.IsNotNull(service);
      Check.IsAssignableFrom(type, service);

      bool requirements = true;
      for (int i = 0; i < service.GetDependencies().Count && requirements == true; ++i)
      {
        Type dependence = service.GetDependencies()[i];

        // It must be registered and correctly initialized.
        if (IsRegistered(dependence) == false || Get(dependence).Status != ServiceStatus.Initialized)
        {
          Log.Warning($"Dependency '{dependence}' on service '{type}' is not fulfilled");

          requirements = false;
        }
      }

      if (requirements == true && service.Status != ServiceStatus.NotInitialized)
      {
        Log.Warning($"Service {type} status incorrect");

        requirements = false;
      }

      if (requirements == true)
      {
        if (services.TryAdd(type, service) == true)
          service.OnRegister();
        else
          Log.Warning($"Service of type '{type}' is already registered");
      }
    }

    /// </inheritdoc>
    public bool Unregister<T>() where T : IService => Unregister(typeof(T));

    /// </inheritdoc>
    public bool Unregister(Type type)
    {
      Check.IsNotNull(type);

      bool removed = services.TryRemove(type, out var service);
      if (service != null && service.Status == ServiceStatus.Initialized)
        service.OnUnregister();

      return removed;
    }

    /// </inheritdoc>
    public void UnregisterAll()
    {
      foreach (var service in services)
      {
        if (service.Value.Status == ServiceStatus.Initialized)
          service.Value.OnUnregister();
      }

      services.Clear();
    }

    /// </inheritdoc>
    public IService Get<T>() where T : IService => (T)Get(typeof(T));

    /// </inheritdoc>
    public IService Get(Type type)
    {
      Check.IsNotNull(type);

      if (services.TryGetValue(type, out var value) == false)
        Log.ExceptionKeyNotFound($"Service of type '{type}' is not registered");

      return value;
    }

    /// </inheritdoc>
    public bool IsRegistered<T>() where T : IService => IsRegistered(typeof(T));

    /// </inheritdoc>
    public bool IsRegistered(Type type)
    {
      Check.IsNotNull(type);

      return services.ContainsKey(type);
    }
  }
}
