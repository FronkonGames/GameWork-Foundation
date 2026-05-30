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
  /// <summary> Color32 extensions. </summary>
  public static class Color32Extensions
  {
    /// <summary> Returns true if all 4 channels match. </summary>
    /// <param name="self">First color.</param>
    /// <param name="second">Second color.</param>
    /// <returns>True if all channels are equal.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo(this Color32 self, Color32 second)
      => self.r == second.r && self.g == second.g && self.b == second.b && self.a == second.a;

    /// <summary> Creates a Texture2D from a Color32 array. </summary>
    /// <param name="self">Color32 array (bitmap data).</param>
    /// <param name="width">Texture width.</param>
    /// <param name="height">Texture height.</param>
    /// <returns>A new Texture2D with the provided pixel data.</returns>
    public static Texture2D CreateTexture(this Color32[] self, int width, int height)
    {
      Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
      texture.SetPixels32(self);
      texture.Apply();

      return texture;
    }
  }
}
