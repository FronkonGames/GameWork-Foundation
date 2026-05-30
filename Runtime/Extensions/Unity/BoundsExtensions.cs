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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Bounds extensions. </summary>
  public static class BoundsExtensions
  {
    /// <summary> Returns a random point inside the bounds. </summary>
    /// <param name="self">Bounds.</param>
    /// <returns>A random point inside the bounds.</returns>
    public static Vector3 RandomPoint(this Bounds self)
      => new Vector3(
        Random.Range(self.min.x, self.max.x),
        Random.Range(self.min.y, self.max.y),
        Random.Range(self.min.z, self.max.z));

    /// <summary> Returns a random point inside the bounds using the given Random instance. </summary>
    /// <param name="self">Bounds.</param>
    /// <param name="rng">The random instance to use.</param>
    /// <returns>A random point inside the bounds.</returns>
    public static Vector3 RandomPoint(this Bounds self, System.Random rng)
      => new Vector3(
        (float)(self.min.x + rng.NextDouble() * self.size.x),
        (float)(self.min.y + rng.NextDouble() * self.size.y),
        (float)(self.min.z + rng.NextDouble() * self.size.z));

    /// <summary> Returns the closest point on the bounds to the given point. </summary>
    /// <param name="self">Bounds.</param>
    /// <param name="point">The point to find the closest position to.</param>
    /// <returns>The closest point on the bounds surface or inside.</returns>
    public static Vector3 ClosestPoint(this Bounds self, Vector3 point)
      => self.ClosestPoint(point);

    /// <summary> Returns a new bounds expanded by the given amount on all sides. </summary>
    /// <param name="self">Bounds.</param>
    /// <param name="amount">The amount to expand on each side.</param>
    /// <returns>A new expanded bounds.</returns>
    public static Bounds Expanded(this Bounds self, float amount)
      => new Bounds(self.center, self.size + Vector3.one * (amount * 2f));

    /// <summary> Returns a new bounds scaled from the center. </summary>
    /// <param name="self">Bounds.</param>
    /// <param name="scale">The scale factor.</param>
    /// <returns>A new scaled bounds.</returns>
    public static Bounds Scaled(this Bounds self, Vector3 scale)
      => new Bounds(self.center, Vector3.Scale(self.size, scale));

    /// <summary> Returns true if the bounds intersect with another bounds. </summary>
    /// <param name="self">Bounds.</param>
    /// <param name="other">The other bounds to test against.</param>
    /// <returns>True if the bounds intersect.</returns>
    public static bool Intersects(this Bounds self, Bounds other)
      => self.Intersects(other);
  }
}
