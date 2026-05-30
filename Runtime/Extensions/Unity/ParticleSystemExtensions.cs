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
using System.Collections.Generic;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> ParticleSystem extensions. </summary>
  public static class ParticleSystemExtensions
  {
    /// <summary> Instantiates, plays and returns a particle system at the given position. </summary>
    public static ParticleSystem PlayInstance(this ParticleSystem particle, Vector3 position, Transform parent = null)
    {
      Transform particleTransform = particle.transform;
      ParticleSystem instance = Object.Instantiate(particle, position + particleTransform.position, particleTransform.rotation, parent);
      instance.Play();
      return instance;
    }

    /// <summary> Scales the particle transform and returns the system. </summary>
    public static ParticleSystem ScaleTo(this ParticleSystem particle, Vector3 localScale)
    {
      particle.transform.ScaleTo(localScale);
      return particle;
    }

    /// <summary> Destroys the particle game object after a delay. </summary>
    public static ParticleSystem DestroyAfter(this ParticleSystem particle, float seconds)
    {
      particle.gameObject.DestroyAfter(seconds);
      return particle;
    }

    /// <summary> Plays all particle systems in the collection. </summary>
    public static void Play<T>(this T particles, bool withChildren = true) where T : IEnumerable<ParticleSystem>
    {
      foreach (ParticleSystem particle in particles)
        particle.Play(withChildren);
    }

    /// <summary> Stops all particle systems in the collection. </summary>
    public static void Stop<T>(this T particles, bool withChildren = true) where T : IEnumerable<ParticleSystem>
    {
      foreach (ParticleSystem particle in particles)
        particle.Stop(withChildren);
    }

    /// <summary> Clears all particle systems in the collection. </summary>
    public static void Clear<T>(this T particles, bool withChildren = true) where T : IEnumerable<ParticleSystem>
    {
      foreach (ParticleSystem particle in particles)
        particle.Clear(withChildren);
    }

    /// <summary> Plays the particle system when it is not already playing. </summary>
    public static void PlayWhenStop(this ParticleSystem particle, bool withChildren = false)
    {
      if (particle.isPlaying == false)
        particle.Play(withChildren);
    }

    /// <summary> Stops the particle system when it is playing. </summary>
    public static void StopWhenActive(this ParticleSystem particle, bool withChildren = false)
    {
      if (particle.isPlaying)
        particle.Stop(withChildren);
    }
  }
}
