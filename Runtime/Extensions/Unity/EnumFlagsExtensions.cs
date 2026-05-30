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
using System.Collections.Generic;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Enum flags extensions. </summary>
  public static class EnumFlagsExtensions
  {
    /// <summary> Count the number of flags set. </summary>
    public static int EnumFlagsCount<T>(this T self) where T : Enum
    {
      int count = 0;
      int value = Convert.ToInt32(self);

      while (value != 0)
      {
        count += value & 1;
        value >>= 1;
      }

      return count;
    }

    /// <summary> Convert flags to an array of individual flags. </summary>
    public static T[] EnumFlagsToArray<T>(this T self) where T : Enum
    {
      List<T> flags = new();
      Array values = Enum.GetValues(typeof(T));
      int intValue = Convert.ToInt32(self);

      foreach (T value in values)
      {
        int intValue2 = Convert.ToInt32(value);
        if (intValue2 != 0 && (intValue & intValue2) == intValue2)
          flags.Add(value);
      }

      return flags.ToArray();
    }
  }
}
