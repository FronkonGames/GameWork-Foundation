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
  /// <summary> Action and Func invocation extensions. </summary>
  public static class ActionExtensions
  {
    /// <summary> Invokes all actions in the list. </summary>
    public static void InvokeAll(this List<Action> actions)
    {
      for (int i = 0; i < actions.Count; i++)
        actions[i]?.Invoke();
    }

    /// <summary> Invokes all actions in the array. </summary>
    public static void InvokeAll(this Action[] actions)
    {
      for (int i = 0; i < actions.Length; i++)
        actions[i]?.Invoke();
    }

    /// <summary> Invokes a copy of the list so handlers can modify the original safely. </summary>
    public static void InvokeAllSafe(this List<Action> actions)
    {
      if (actions == null || actions.Count == 0)
        return;

      Action[] copy = new Action[actions.Count];
      actions.CopyTo(copy);
      copy.InvokeAll();
    }

    /// <summary> Invokes a copy of the array so handlers can modify the original safely. </summary>
    public static void InvokeAllSafe(this Action[] actions)
    {
      if (actions == null || actions.Length == 0)
        return;

      Action[] copy = new Action[actions.Length];
      Array.Copy(actions, copy, actions.Length);
      copy.InvokeAll();
    }

    /// <summary> Invokes actions until one returns the target value. </summary>
    public static void InvokeAny(this List<Func<bool>> actions, bool target = true)
    {
      for (int i = 0; i < actions.Count; i++)
      {
        if (actions[i]?.Invoke() == target)
          break;
      }
    }

    /// <summary> Invokes actions until one returns the target value. </summary>
    public static void InvokeAny(this Func<bool>[] actions, bool target = true)
    {
      for (int i = 0; i < actions.Length; i++)
      {
        if (actions[i]?.Invoke() == target)
          break;
      }
    }

    /// <summary> Invokes a copy of the list until one returns the target value. </summary>
    public static void InvokeAnySafe(this List<Func<bool>> actions, bool target = true)
    {
      if (actions == null || actions.Count == 0)
        return;

      Func<bool>[] copy = new Func<bool>[actions.Count];
      actions.CopyTo(copy);
      copy.InvokeAny(target);
    }

    /// <summary> Invokes a copy of the array until one returns the target value. </summary>
    public static void InvokeAnySafe(this Func<bool>[] actions, bool target = true)
    {
      if (actions == null || actions.Length == 0)
        return;

      Func<bool>[] copy = new Func<bool>[actions.Length];
      Array.Copy(actions, copy, actions.Length);
      copy.InvokeAny(target);
    }
  }
}
