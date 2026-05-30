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
using System.Globalization;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Long extensions. </summary>
  public static class LongExtensions
  {
    private static readonly string[] SizeSuffixes =
      { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

    private static readonly string[] CurrencySuffixes =
    {
      "", "K", "M", "B", "T", "Qa", "Qi", "Si", "Sp", "Oc", "N", "Dc", "Un", "Duo", "Tre", "Qua", "Qui", "SE", "SP",
      "OC", "NV", "VIG", "CE", "TRV", "QTU", "SPZ", "CJX", "XQR", "VNU", "YZQ", "KVZ", "JZW", "QZX", "ZKL", "HTZ",
      "RXV", "WZX", "XVC", "ZOL", "LXS", "YXZU", "JXKZ", "RTVX", "ZHGX", "QZQZ", "VZVZ"
    };

    /// <summary> Formats the value as a byte size suffix (e.g. KB, MB). </summary>
    public static string SizeSuffix(this long value, int decimalPlaces = 2)
    {
      if (decimalPlaces < 0)
        throw new ArgumentOutOfRangeException(nameof(decimalPlaces));

      if (value < 0)
        return $"-{SizeSuffix(-value, decimalPlaces)}";

      if (value == 0)
        return "0 bytes";

      int mag = (int)Math.Log(value, 1024);
      decimal adjustedSize = (decimal)value / (1L << (mag * 10));

      if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
      {
        mag += 1;
        adjustedSize /= 1024;
      }

      return string.Format(
        CultureInfo.InvariantCulture,
        "{0:n" + decimalPlaces + "} {1}",
        adjustedSize,
        SizeSuffixes[Math.Min(mag, SizeSuffixes.Length - 1)]);
    }

    /// <summary> Formats the value as a currency suffix (e.g. K, M, B). </summary>
    public static string CurrencySuffix(this long value, int decimalPlaces = 0)
    {
      if (decimalPlaces < 0)
        throw new ArgumentOutOfRangeException(nameof(decimalPlaces));

      if (value < 0)
        return $"-{CurrencySuffix(-value, decimalPlaces)}";

      if (value < 1000)
        return string.Format(CultureInfo.InvariantCulture, "{0:n0}", value);

      int mag = (int)Math.Log(value, 1000);
      decimal adjustedSize = (decimal)value / (int)Math.Pow(1000, mag);

      return string.Format(
        CultureInfo.InvariantCulture,
        "{0:n" + decimalPlaces + "} {1}",
        adjustedSize,
        CurrencySuffixes[Math.Min(mag, CurrencySuffixes.Length - 1)]);
    }
  }
}
