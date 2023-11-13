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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Service Locator interface. </summary>
  public interface IServiceLocator
  {
    /// <summary> Register an instance with the given type T. </summary>
    /// <param name="service">The instance to register.</param>
    void Register<T>(T service) where T : IService;

    /// <summary> Register an instance with the given type T. </summary>
    /// <param name="type">The type that this service will be registered with.</param>
    /// <param name="service">The instance to register.</param>
    void Register(Type type, IService service);

    /// <summary> Unregister the service of type T. </summary>
    /// <typeparam name="T">T is the type of the service to be unregistered</typeparam>
    /// <returns>True if the type is successfully unregistered.</returns>
    bool Unregister<T>() where T : IService;

    /// <summary> Unregister the service of type T. </summary>
    /// <returns>True if the type is successfully unregistered.</returns>
    bool Unregister(Type type);

    /// <summary> Unregister all services. </summary>
    void UnregisterAll();

    /// <summary> Get the service of type T. </summary>
    /// <typeparam name="T">The type of the service to be retrieved.</typeparam>
    /// <returns>The instance that is registered with the given interface type T. </returns>
    IService Get<T>() where T : IService;

    /// <summary> Get the service of type. </summary>
    /// <param name="type">The type of the service to be retrieved.</param>
    /// <returns>The instance that is registered with the given interface type T. </returns>
    IService Get(Type type);
    
    /// <summary> Check if the service of type T is registered. </summary>
    /// <typeparam name="T">The type of the service to be checked.</typeparam>
    /// <returns>True if the service is registered, false otherwise.</returns>
    bool IsRegistered<T>() where T : IService;

    /// <summary> Check if the service of type T is registered. </summary>
    /// <param name="type">The type of the service to be checked.</param>
    /// <returns>True if the service is registered, false otherwise.</returns>
    bool IsRegistered(Type type);
  }
}
