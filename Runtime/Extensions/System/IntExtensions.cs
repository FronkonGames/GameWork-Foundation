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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Int extensions. </summary>
  public static class IntExtensions
  {
    private static readonly string[] SizeSuffixes = { @"bytes", @"KB", @"MB", @"GB", @"TB", @"PB", @"EB", @"ZB", @"YB" };

    private const long ByteConversion = 1000;

    /// <summary> Value sign. </summary>
    /// <param name="self">Value</param>
    /// <returns>1 if greater than or equal to 0, -1 if less than 0.</returns>
    public static int Sign(this int self) => self >= 0 ? 1 : -1;

    /// <summary> Returns the maximum value. </summary>
    /// <param name="a">Value</param>
    /// <param name="b">Value</param>
    /// <returns>Int</returns>
    public static int Max(this int a, int b) => a < b ? b : a;

    /// <summary> Returns the minimum value. </summary>
    /// <param name="a">Value</param>
    /// <param name="b">Value</param>
    /// <returns>Int</returns>
    public static int Min(this int a, int b) => a < b ? a : b;

    /// <summary> Returns the absolute value. </summary>
    /// <param name="self">Value</param>
    /// <returns>Int</returns>
    public static int Abs(this int self) => Math.Abs(self);

    /// <summary> Returns the rounded value. </summary>
    /// <param name="snap"> Rounding distance </param>
    /// <returns>Int</returns>
    public static int Snap(this int self, int snap) => snap > 0 ? Mathf.RoundToInt((float)self / snap) * snap : self;

    /// <summary> Constrain the value to a range. </summary>
    /// <param name="self">Value</param>
    /// <param name="min">Lower range</param>
    /// <param name="max">Upper range</param>
    /// <returns>Int</returns>
    public static int Clamp(this int self, int min, int max)
    {
      if (self < min)
        return min;

      return self > max ? max : self;
    }

    /// <summary> Pow. </summary>
    /// <param name="self">Value</param>
    /// <param name="exp">Exponent</param>
    /// <returns>Int</returns>
    public static int Pow(this int self, int exp)
    {
      int result = 1;

      for (int i = 0; i < exp; ++i)
        result *= self;

      return result;
    }

    /// <summary> Value is even. </summary>
    /// <param name="self">Value</param>
    /// <returns>True/false</returns>
    public static bool IsEven(this int self) => self % 2 == 0;

    /// <summary> Value is odd. </summary>
    /// <param name="self">Value</param>
    /// <returns>True/false</returns>
    public static bool IsOdd(this int self) => self % 2 != 0;

    /// <summary> Next power of two. </summary>
    /// <param name="self">Value</param>
    /// <returns>Int</returns>
    public static int NextPowerOfTwo(this int self)
    {
      self = Math.Abs(self);
      self--;
      self |= self >> 1;
      self |= self >> 2;
      self |= self >> 4;
      self |= self >> 8;
      self |= self >> 16;
      self++;

      return self;
    }

    /// <summary> Calculate the mask of a layer. </summary>
    /// <param name="self">Value</param>
    /// <returns>Int</returns>
    public static int GetMask(this int self) => 1 << self;

    /// <summary>Calculates the mask of a set of layers. </summary>
    /// <param name="self">Value</param>
    /// <returns>Int</returns>
    public static int GetMask(this int[] self)
    {
      int layerMask = 0;
      for (int i = 0; i < self.Length; ++i)
        layerMask |= self[i].GetMask();

      return layerMask;
    }

    /// <summary> Layer included? </summary>
    /// <param name="self">Value</param>
    /// <param name="layermask">Layer mask</param>
    /// <returns>Layer included</returns>
    public static bool IsInLayerMask(this int self, LayerMask layermask) => layermask == (layermask | (1 << self));

    /// <summary> Layer included? </summary>
    /// <param name="self">Value</param>
    /// <param name="layerName">Layer name</param>
    /// <returns>Layer included</returns>
    public static bool IsInLayerMask(this int self, string layerName)
    {
      int layermask = LayerMask.NameToLayer(layerName);

      return layermask == (layermask | (1 << self));
    }

    /// <summary> Bytes to a text string. </summary>
    /// <param name="self">Value</param>
    /// <returns>String</returns>
    public static string BytesToHumanReadable(this int self)
    {
      if (self < 0)
        return $"-{BytesToHumanReadable(-self)}";

      if (self == 0)
        return "0 bytes";

      int mag = (int)Math.Log(self, ByteConversion);
      float adjustedSize = self / Mathf.Pow(1024, mag);

      return $"{adjustedSize.ToInvariantCulture()} {SizeSuffixes[mag]}";
    }

    /// <summary> Seconds to a text string. </summary>
    /// <param name="self">Value</param>
    /// <returns>String</returns>
    public static string SecondsToHumanReadable(this int self) => $"{self / 3600:00}:{(self / 60) % 60:00}:{self % 60:00}";

    /// <summary> Returns the number of digits in the number. </summary>
    /// <param name="self">Value.</param>
    /// <returns>The number of digits in the number.</returns>
    public static int NumDigits(this int self) => Mathf.FloorToInt(Mathf.Log10((float)self.Abs()) + 1.0f);
  }
}
