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
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Animator extensions. </summary>
  public static class AnimatorExtension
  {
    /// <summary> Sets a float parameter with lerping toward the target value. </summary>
    /// <param name="animator">Target Animator.</param>
    /// <param name="id">Parameter hash id.</param>
    /// <param name="target">Target value.</param>
    /// <param name="time">Lerp time in seconds.</param>
    public static void SetFloatWithLerp(this Animator animator, int id, float target, float time)
    {
      if (animator == null || !animator.enabled || !animator.hasRootMotion)
        return;

      animator.SetFloat(id, Mathf.Lerp(animator.GetFloat(id), target, time * Time.deltaTime));
    }

    /// <summary> Safely sets a bool parameter only if it exists. </summary>
    /// <param name="animator">Target Animator.</param>
    /// <param name="id">Parameter hash id.</param>
    /// <param name="value">Value to set.</param>
    public static void SetBoolSafe(this Animator animator, int id, bool value)
    {
      if (animator == null || !animator.enabled || animator.runtimeAnimatorController == null)
        return;

      for (int i = 0; i < animator.parameterCount; i++)
      {
        if (animator.parameters[i].nameHash == id)
        {
          animator.SetBool(id, value);
          return;
        }
      }
    }

    /// <summary> Safely sets a float parameter only if it exists. </summary>
    /// <param name="animator">Target Animator.</param>
    /// <param name="id">Parameter hash id.</param>
    /// <param name="value">Value to set.</param>
    public static void SetFloatSafe(this Animator animator, int id, float value)
    {
      if (animator == null || !animator.enabled || animator.runtimeAnimatorController == null)
        return;

      for (int i = 0; i < animator.parameterCount; i++)
      {
        if (animator.parameters[i].nameHash == id)
        {
          animator.SetFloat(id, value);
          return;
        }
      }
    }

    /// <summary> Safely sets an integer parameter only if it exists. </summary>
    /// <param name="animator">Target Animator.</param>
    /// <param name="id">Parameter hash id.</param>
    /// <param name="value">Value to set.</param>
    public static void SetIntegerSafe(this Animator animator, int id, int value)
    {
      if (animator == null || !animator.enabled || animator.runtimeAnimatorController == null)
        return;

      for (int i = 0; i < animator.parameterCount; i++)
      {
        if (animator.parameters[i].nameHash == id)
        {
          animator.SetInteger(id, value);
          return;
        }
      }
    }

    /// <summary> Safely sets a trigger parameter only if it exists. </summary>
    /// <param name="animator">Target Animator.</param>
    /// <param name="id">Parameter hash id.</param>
    public static void SetTriggerSafe(this Animator animator, int id)
    {
      if (animator == null || !animator.enabled || animator.runtimeAnimatorController == null)
        return;

      for (int i = 0; i < animator.parameterCount; i++)
      {
        if (animator.parameters[i].nameHash == id)
        {
          animator.SetTrigger(id);
          return;
        }
      }
    }

    /// <summary> Safely sets a bool parameter by name. </summary>
    /// <param name="animator">Target Animator.</param>
    /// <param name="name">Parameter name.</param>
    /// <param name="value">Value to set.</param>
    public static void SetBoolSafe(this Animator animator, string name, bool value)
    {
      if (animator == null || !animator.enabled || animator.runtimeAnimatorController == null)
        return;

      for (int i = 0; i < animator.parameterCount; i++)
      {
        if (animator.parameters[i].name == name)
        {
          animator.SetBool(name, value);
          return;
        }
      }
    }

    /// <summary> Safely sets a float parameter by name. </summary>
    /// <param name="animator">Target Animator.</param>
    /// <param name="name">Parameter name.</param>
    /// <param name="value">Value to set.</param>
    public static void SetFloatSafe(this Animator animator, string name, float value)
    {
      if (animator == null || !animator.enabled || animator.runtimeAnimatorController == null)
        return;

      for (int i = 0; i < animator.parameterCount; i++)
      {
        if (animator.parameters[i].name == name)
        {
          animator.SetFloat(name, value);
          return;
        }
      }
    }

    /// <summary> Safely sets an integer parameter by name. </summary>
    /// <param name="animator">Target Animator.</param>
    /// <param name="name">Parameter name.</param>
    /// <param name="value">Value to set.</param>
    public static void SetIntegerSafe(this Animator animator, string name, int value)
    {
      if (animator == null || !animator.enabled || animator.runtimeAnimatorController == null)
        return;

      for (int i = 0; i < animator.parameterCount; i++)
      {
        if (animator.parameters[i].name == name)
        {
          animator.SetInteger(name, value);
          return;
        }
      }
    }

    /// <summary> Safely sets a trigger parameter by name. </summary>
    /// <param name="animator">Target Animator.</param>
    /// <param name="name">Parameter name.</param>
    public static void SetTriggerSafe(this Animator animator, string name)
    {
      if (animator == null || !animator.enabled || animator.runtimeAnimatorController == null)
        return;

      for (int i = 0; i < animator.parameterCount; i++)
      {
        if (animator.parameters[i].name == name)
        {
          animator.SetTrigger(name);
          return;
        }
      }
    }

    /// <summary> Returns true if the animator is available (not null and enabled). </summary>
    /// <param name="animator">Target Animator.</param>
    /// <returns>True if available.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAvailable(this Animator animator) => animator != null && animator.enabled;

    /// <summary> Returns true if the animator has a parameter with the given name. </summary>
    public static bool HasParameter(this Animator animator, string parameterName)
    {
      if (animator == null || animator.runtimeAnimatorController == null)
        return false;

#if UNITY_EDITOR
      animator.logWarnings = false;
#endif

      AnimatorControllerParameter[] parameters = animator.parameters;

      for (int i = 0; i < parameters.Length; i++)
      {
        if (parameters[i].name == parameterName)
          return true;
      }

      return false;
    }
  }
}
