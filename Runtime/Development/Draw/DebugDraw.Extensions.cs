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

using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Drawing of objects for development.
  /// </summary>
  /// <remarks>Only available in the Editor</remarks>
  public static partial class DebugDraw
  {
    public static void Draw(this Vector3 self, float size = PointSize, Color? color = null)
      => Point(self, size, color);
    
    public static void Draw(this Vector3[] self, float size = PointSize, Color? color = null)
      => Points(self, size, color);

    public static void Draw(this Vector3[] self, Color? color = null)
      => Lines(self, color);

    public static void DrawDotted(this Vector3[] self, Color? color = null)
      => DottedLines(self, color);
    
    public static void Draw(this Transform self, float length = 1.0f, Color? color = null)
      => Arrow(self.position, self.rotation, length, ArrowTipSize, ArrowWidth, color);
    
    public static void Draw(this Bounds self, Color? color = null) => Bounds(self, color);

    public static void Draw(this BoundsInt self, Color? color = null) => Bounds(new Bounds(self.center, self.size), color);

    public static void Draw(this Ray self, Color? color = null) => Ray(self.origin, Quaternion.LookRotation(self.direction), color);

    public static void Draw(this Ray self, RaycastHit[] hits, int maxHits = 0, Color? color = null)
    {
      if (hits.Length > 0)
      {
        if (maxHits <= 0)
          maxHits = hits.Length;

        self.Draw(color);

        for (int i = 0; i < maxHits; ++i)
        {
          Circle(hits[i].point, HitRadius * 0.5f, color ?? HitColor, hits[i].normal);
          Circle(hits[i].point, HitRadius, color ?? HitColor, hits[i].normal);
          Line(hits[i].point, hits[i].point + (hits[i].normal * HitLength), color ?? HitColor);
        }
      }
    }
    
    public static void DrawName(this GameObject self, Color? color = null) => Text(self.transform.position, self.name, color);
  }
}
