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
  /// <summary>
  /// IComparable extensions.
  /// </summary>
  public static class ComparableExtensions
  {
    /// <summary>
    /// Checks if the value is between a min and max value.
    /// </summary>
    /// <typeparam name="T">The type of value to check.</typeparam>
    /// <param name="self">Value.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <param name="includeMin">The minimum value is inclusive if true, exclusive if false.</param>
    /// <param name="includeMax">The maximum value is inclusive if true, exclusive if false.</param>
    /// <returns>True if the value is between the min and max value.</returns>
    public static bool IsBetween<T>(this T self, T min, T max, bool includeMin = true, bool includeMax = true) where T : IComparable<T>
    {
      int minCompare = self.CompareTo(min);
      int maxCompare = self.CompareTo(max);

      if (minCompare < 0 || maxCompare > 0)
        return false;
      
      if (includeMin == false && minCompare == 0)
        return false;
      
      if (includeMax == false && maxCompare == 0)
        return false;

      return true;
    }

    /// <summary>
    /// Checks if the value is in the range (min..max).
    /// </summary>
    /// <typeparam name="T">The type of value to check.</typeparam>
    /// <param name="self">Value.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>True if the value is in the range (min..max).</returns>
    public static bool IsBetweenExclusive<T>(this T self, T min, T max) where T : IComparable<T> => self.IsBetween(min, max, false, false);
  }
}