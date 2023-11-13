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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Current status of the service. </summary>
  public enum ServiceStatus
  {
    /// <summary> Not initialized, has not yet registered. </summary>
    NotInitialized,

    /// <summary> Initialized, already registered. </summary>
    Initialized,

    /// <summary> Error. </summary>
    Failed
  }

  /// <summary> Service interface. </summary>
  public interface IService
  {
    /// <summary> Current status. </summary>
    ServiceStatus Status { get; }

    /// <summary>
    /// Called when registering the service. The status must be NotInitialized.
    /// When registering the status becomes Initialized.
    /// </summary>
    void OnRegister();

    /// <summary>
    /// Call to unregister. The status must be Initialized.
    /// When unregistering the status becomes NotInitialized.
    /// </summary>
    void OnUnregister();

    /// <summary>
    /// List of dependencies. They are consulted when registering the service,
    /// if the requirements are not met, it is not registered and the status becomes Failed.
    /// </summary>
    /// <returns> List of types on which it depends. </returns>
    List<Type> GetDependencies();
  }
}
