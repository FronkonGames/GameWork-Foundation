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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> MonoBehaviour extensions. </summary>
  public static class MonoBehaviourExtension
  {
    /// <summary> Safely starts a coroutine with null checks. </summary>
    /// <param name="monoBehaviour">MonoBehaviour</param>
    /// <param name="routine">Coroutine routine</param>
    /// <returns>Coroutine or null</returns>
    public static Coroutine SafeStartCoroutine(this MonoBehaviour monoBehaviour, IEnumerator routine)
    {
      if (monoBehaviour != null && monoBehaviour.isActiveAndEnabled && routine != null)
        return monoBehaviour.StartCoroutine(routine);

      return null;
    }

    /// <summary> Safely stops a coroutine with null checks. </summary>
    /// <param name="monoBehaviour">MonoBehaviour</param>
    /// <param name="coroutine">Coroutine to stop</param>
    public static void SafeStopCoroutine(this MonoBehaviour monoBehaviour, Coroutine coroutine)
    {
      if (monoBehaviour != null && coroutine != null)
        monoBehaviour.StopCoroutine(coroutine);
    }

    /// <summary> Sets the GameObject to DontDestroyOnLoad. </summary>
    /// <param name="monoBehaviour">MonoBehaviour</param>
    public static void DontDestroyOnLoad(this MonoBehaviour monoBehaviour)
    {
      if (monoBehaviour != null)
        UnityEngine.Object.DontDestroyOnLoad(monoBehaviour.gameObject);
    }

    /// <summary> Resolves a component from children if the reference is null. </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <param name="monoBehaviour">MonoBehaviour</param>
    /// <param name="component">Component reference</param>
    public static void ResolveComponentFromChildrenIfNull<T>(this MonoBehaviour monoBehaviour, ref T component) where T : Component
    {
      if (monoBehaviour != null && component == null)
        component = monoBehaviour.GetComponentInChildren<T>();
    }

    /// <summary> Resolves a component from children or parent if the reference is null. </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <param name="monoBehaviour">MonoBehaviour</param>
    /// <param name="component">Component reference</param>
    public static void ResolveComponentFromChildrenOrParentIfNull<T>(this MonoBehaviour monoBehaviour, ref T component) where T : Component
    {
      if (monoBehaviour != null && component == null)
      {
        component = monoBehaviour.GetComponentInChildren<T>();

        if (component == null)
          component = monoBehaviour.GetComponentInParent<T>();
      }
    }

    /// <summary> Stops the coroutine and returns null. </summary>
    public static Coroutine StopCoroutineResult(this MonoBehaviour monoBehaviour, Coroutine coroutine)
    {
      if (monoBehaviour != null && coroutine != null)
        monoBehaviour.StopCoroutine(coroutine);

      return null;
    }

    /// <summary> Restarts a coroutine, stopping the previous one if running. </summary>
    public static Coroutine RestartCoroutine(this MonoBehaviour monoBehaviour, Coroutine coroutine, IEnumerator enumerator)
    {
      if (monoBehaviour == null || enumerator == null)
        return null;

      if (coroutine != null)
        monoBehaviour.StopCoroutine(coroutine);

      return monoBehaviour.StartCoroutine(enumerator);
    }

    /// <summary> Gets components from a collection of MonoBehaviours, skipping null results. </summary>
    public static T[] GetComponents<T>(this ICollection<MonoBehaviour> objects) where T : UnityEngine.Object
    {
      List<T> result = new(objects.Count);

      foreach (MonoBehaviour obj in objects)
      {
        if (obj == null)
          continue;

        T component = obj.GetComponent<T>();

        if (component != null)
          result.Add(component);
      }

      return result.ToArray();
    }
  }
}
