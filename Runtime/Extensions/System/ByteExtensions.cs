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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Byte extensions. </summary>
  public static class ByteExtensions
  {
    /// <summary> Is the bit set? </summary>
    /// <param name="self">Value</param>
    /// <param name="pos">Bit to check</param>
    /// <returns>True if bit is set</returns>
    public static bool IsBitSet(this byte self, int pos)
    {
      Check.IsWithin(pos, 0, 7);

      return (self & (1 << pos)) != 0;
    }

    /// <summary> Sets a bit to 1. </summary>
    /// <param name="self">Value</param>
    /// <param name="pos">Bit to change</param>
    /// <returns>The new byte.</returns>
    public static byte SetBit(this byte self, int pos)
    {
      Check.IsWithin(pos, 0, 7);

      return (byte)(self | (1 << pos));
    }

    /// <summary> Sets a bit to 0. </summary>
    /// <param name="self">Value</param>
    /// <param name="pos">Bit to change</param>
    /// <returns>The new byte.</returns>
    public static byte UnsetBit(this byte self, int pos)
    {
      Check.IsWithin(pos, 0, 7);

      return (byte)(self & ~(1 << pos));
    }

    /// <summary> Change one bit. </summary>
    /// <param name="self">Value</param>
    /// <param name="pos">Bit to change</param>
    /// <returns>The new byte.</returns>
    public static byte ToggleBit(this byte self, int pos)
    {
      Check.IsWithin(pos, 0, 7);

      return (byte)(self ^ (1 << pos));
    }

    /// <summary> Byte a string. </summary>
    /// <param name="b">Value</param>
    /// <returns>String.</returns>
    public static string ToBinaryString(this byte b) => Convert.ToString(b, 2).PadLeft(8, '0');    
  }
}