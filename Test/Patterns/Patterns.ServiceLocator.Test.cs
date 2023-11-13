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
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;
using UnityEngine;

/// <summary> Patterns tests. </summary>
public partial class PatternsTests
{
  private static readonly Regex AnyString = new("");

  public class ServiceA : Service { }

  public class ServiceB : Service
  {
    private readonly List<Type> dependencies = new() { typeof(ServiceA) };

    public override List<Type> GetDependencies() => dependencies;
  }

  public class ServiceC : Service
  {
    private readonly List<Type> dependencies = new() { typeof(ServiceA), typeof(ServiceB) };

    public override List<Type> GetDependencies() => dependencies;
  }

  /// <summary> Service Locator test. </summary>
  [UnityTest]
  public IEnumerator ServiceLocator()
  {
    ServiceLocator serviceLocatorSimple = new();
    ServiceA serviceA = new();
    ServiceB serviceB = new();
    ServiceC serviceC = new();

    LogAssert.ignoreFailingMessages = true;

    Assert.IsFalse(serviceLocatorSimple.IsRegistered<ServiceA>());
    Assert.IsFalse(serviceLocatorSimple.IsRegistered(typeof(ServiceA)));

    LogAssert.Expect(LogType.Exception, AnyString);
    Assert.That(() => serviceLocatorSimple.Get<ServiceB>(), Throws.TypeOf<KeyNotFoundException>());

    Assert.AreEqual(serviceA.Status, ServiceStatus.NotInitialized);

    serviceLocatorSimple.Register(serviceA);
    Assert.IsTrue(serviceLocatorSimple.IsRegistered<ServiceA>());
    LogAssert.Expect(LogType.Warning, AnyString);
    serviceLocatorSimple.Register(serviceA);

    Assert.IsNotNull(serviceLocatorSimple.Get<ServiceA>());
    Assert.AreEqual(serviceA.Status, ServiceStatus.Initialized);

    Assert.IsTrue(serviceLocatorSimple.Unregister<ServiceA>());
    Assert.IsFalse(serviceLocatorSimple.Unregister<ServiceA>());
    Assert.AreEqual(serviceA.Status, ServiceStatus.NotInitialized);
    LogAssert.Expect(LogType.Exception, AnyString);
    Assert.That(() => serviceLocatorSimple.Get<ServiceB>(), Throws.TypeOf<KeyNotFoundException>());

    serviceLocatorSimple.Register(serviceA);
    Assert.IsTrue(serviceLocatorSimple.IsRegistered<ServiceA>());
    Assert.AreEqual(serviceA.Status, ServiceStatus.Initialized);
    serviceLocatorSimple.UnregisterAll();
    Assert.IsFalse(serviceLocatorSimple.IsRegistered<ServiceA>());
    Assert.AreEqual(serviceA.Status, ServiceStatus.NotInitialized);

    LogAssert.Expect(LogType.Warning, AnyString);
    serviceLocatorSimple.Register(serviceB);
    Assert.IsFalse(serviceLocatorSimple.IsRegistered<ServiceB>());
    Assert.That(() => serviceLocatorSimple.Get<ServiceB>(), Throws.TypeOf<KeyNotFoundException>());

    serviceLocatorSimple.Register(serviceA);
    serviceLocatorSimple.Register(serviceB);
    Assert.IsTrue(serviceLocatorSimple.IsRegistered<ServiceA>());
    Assert.IsTrue(serviceLocatorSimple.IsRegistered<ServiceB>());
    Assert.IsNotNull(serviceLocatorSimple.Get<ServiceA>());
    Assert.IsNotNull(serviceLocatorSimple.Get<ServiceB>());

    serviceLocatorSimple.Register(serviceC);
    Assert.IsTrue(serviceLocatorSimple.IsRegistered<ServiceC>());

    serviceLocatorSimple.UnregisterAll();
    Assert.IsFalse(serviceLocatorSimple.IsRegistered<ServiceA>());
    Assert.IsFalse(serviceLocatorSimple.IsRegistered<ServiceB>());
    Assert.IsFalse(serviceLocatorSimple.IsRegistered<ServiceC>());

    LogAssert.ignoreFailingMessages = false;

    yield return null;
  }
}

