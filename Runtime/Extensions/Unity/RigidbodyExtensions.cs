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
  /// <summary> Rigidbody extensions. </summary>
  public static class RigidbodyExtensions
  {
    /// <summary> Changes the velocity direction while maintaining speed. </summary>
    /// <param name="rigidbody">Rigidbody.</param>
    /// <param name="direction">New direction (will be normalized).</param>
    /// <returns>The Rigidbody for chaining.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rigidbody ChangeDirection(this Rigidbody rigidbody, Vector3 direction)
    {
      rigidbody.linearVelocity = direction.normalized * rigidbody.linearVelocity.magnitude;
      return rigidbody;
    }

    /// <summary> Stops the Rigidbody by zeroing velocities. </summary>
    /// <param name="rigidbody">Rigidbody.</param>
    /// <returns>The Rigidbody for chaining.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rigidbody Stop(this Rigidbody rigidbody)
    {
      rigidbody.linearVelocity = Vector3.zero;
      rigidbody.angularVelocity = Vector3.zero;
      return rigidbody;
    }

    /// <summary> Adds force towards a target position. </summary>
    /// <param name="rigidbody">Rigidbody.</param>
    /// <param name="target">Target world position.</param>
    /// <param name="force">Force magnitude.</param>
    /// <param name="mode">Force mode.</param>
    /// <returns>The Rigidbody for chaining.</returns>
    public static Rigidbody AddForceTowards(this Rigidbody rigidbody, Vector3 target, float force, ForceMode mode = ForceMode.Force)
    {
      Vector3 direction = (target - rigidbody.position).normalized;
      rigidbody.AddForce(direction * force, mode);
      return rigidbody;
    }

    /// <summary> Returns true if the Rigidbody is moving (velocity magnitude > threshold). </summary>
    /// <param name="rigidbody">Rigidbody.</param>
    /// <param name="threshold">Minimum velocity magnitude to be considered moving.</param>
    /// <returns>True if the Rigidbody is moving.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoving(this Rigidbody rigidbody, float threshold = 0.01f) =>
      rigidbody.linearVelocity.sqrMagnitude > threshold * threshold;
  }
}
