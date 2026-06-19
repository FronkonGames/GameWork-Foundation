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
using System.Runtime.CompilerServices;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Enum extensions. </summary>
  public static class EnumExtensions
  {
    /// <summary> Gets all values of the specified enum type. </summary>
    /// <typeparam name="T"> The enum type. </typeparam>
    /// <returns> An array of all values of the enum type. </returns>
    public static T[] GetValues<T>() where T : struct, Enum
    {
      return Enum.GetValues(typeof(T)) as T[];
    }

    /// <summary> Converts enum to int. </summary>
    /// <typeparam name="T"> The enum type. </typeparam>
    /// <param name="self"> The enum value. </param>
    /// <returns> The integer representation of the enum value. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt<T>(this T self) where T : struct, Enum
    {
      return self.GetHashCode();
    }

    /// <summary> Converts enum to uint. </summary>
    /// <typeparam name="T"> The enum type. </typeparam>
    /// <param name="self"> The enum value. </param>
    /// <returns> The unsigned integer representation of the enum value. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint ToUInt<T>(this T self) where T : struct, Enum
    {
      return (uint)self.GetHashCode();
    }

    /// <summary> Gets the maximum value of the enum type. </summary>
    /// <typeparam name="T"> The enum type. </typeparam>
    /// <returns> The maximum value of the enum type. </returns>
    public static T GetMaxValue<T>() where T : struct, Enum
    {
      T[] values = GetValues<T>();
      T max = values[0];

      for (int i = 1; i < values.Length; ++i)
      {
        if (values[i].CompareTo(max) > 0)
          max = values[i];
      }

      return max;
    }

    /// <summary> Returns true if any of the flag bits are set. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasAnyFlag<T>(this T value, T flag) where T : struct, Enum
      => (Convert.ToUInt64(value) & Convert.ToUInt64(flag)) != 0;

    /// <summary> Returns true if none of the flag bits are set. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasNoneOfFlags<T>(this T value, T flag) where T : struct, Enum
      => (Convert.ToUInt64(value) & Convert.ToUInt64(flag)) == 0;

    /// <summary> Returns true if any of the given flags match. </summary>
    public static bool HasAnyFlag<T>(this T value, params T[] flags) where T : struct, Enum
    {
      ulong valueData = Convert.ToUInt64(value);

      for (int i = 0; i < flags.Length; i++)
      {
        if ((valueData & Convert.ToUInt64(flags[i])) != 0)
          return true;
      }

      return false;
    }

    /// <summary> Picks a random enum value. </summary>
    /// <typeparam name="T"> The enum type. </typeparam>
    /// <returns> Random enum value. </returns>
    public static T PickRandom<T>() where T : struct, Enum
    {
      Array values = Enum.GetValues(typeof(T));

      return (T)values.GetValue(Rand.Range(0, values.Length));
    }

    /// <summary> Picks a random enum value using the given weights. </summary>
    /// <typeparam name="T"> The enum type. </typeparam>
    /// <param name="weights"> One weight per enum value, in declaration order. </param>
    /// <returns> Weighted random enum value. </returns>
    public static T PickWeighted<T>(float[] weights) where T : struct, Enum
    {
      Array values = Enum.GetValues(typeof(T));

      if (weights == null || weights.Length != values.Length)
        throw new ArgumentException("weights.Length must equal the number of values in the enum.");

      int index = Rand.PickWeighted(weights);

      return (T)values.GetValue(index);
    }

    /// <summary> Picks a random enum value between two exclusive bounds. </summary>
    /// <typeparam name="T"> The enum type. </typeparam>
    /// <param name="minExclusive"> Lower bound (exclusive). </param>
    /// <param name="maxExclusive"> Upper bound (exclusive). </param>
    /// <returns> Random enum value in range. </returns>
    public static T PickBetween<T>(T minExclusive, T maxExclusive) where T : struct, Enum
    {
      int minIntVal = Convert.ToInt32(minExclusive) + 1;
      int maxIntVal = Convert.ToInt32(maxExclusive);

      if (minIntVal >= maxIntVal)
        throw new ArgumentException("minExclusive must be less than maxExclusive.");

      int enumVal = Rand.Range(minIntVal, maxIntVal);

      return (T)Enum.ToObject(typeof(T), enumVal);
    }

    /// <summary> Picks a random enum value from zero up to an exclusive upper bound. </summary>
    /// <typeparam name="T"> The enum type. </typeparam>
    /// <param name="maxExclusive"> Upper bound (exclusive). </param>
    /// <returns> Random enum value in range. </returns>
    public static T PickUpTo<T>(T maxExclusive) where T : struct, Enum
    {
      int maxIntVal = Convert.ToInt32(maxExclusive);
      int enumVal = Rand.Range(0, maxIntVal);

      return (T)Enum.ToObject(typeof(T), enumVal);
    }
  }
}
