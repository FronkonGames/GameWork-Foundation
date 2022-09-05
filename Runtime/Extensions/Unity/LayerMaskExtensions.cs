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
  /// <summary>
  /// LayerMask extensions.
  /// </summary>
  public static class LayerMaskExtensions
  {
    /// <summary>
    /// GameObject in the layer?
    /// </summary>
    /// <param name="self">LayerMask</param>
    /// <param name="other">GameObject</param>
    /// <returns>True/false</returns>
    public static bool CheckLayermask(this LayerMask self, GameObject other) => CheckLayermask(self, other.layer);

    /// <summary>
    /// In the layer?
    /// </summary>
    /// <param name="self">LayerMask</param>
    /// <param name="layer">Value</param>
    /// <returns>True/false</returns>
    public static bool CheckLayermask(this LayerMask self, int layer) => ((1 << layer) & self) != 0;
  }
}
