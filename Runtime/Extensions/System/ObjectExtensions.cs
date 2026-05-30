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
  /// <summary> Object extensions. </summary>
  public static class ObjectExtensions
  {
    /// <summary> Returns true if the object is null (handles Unity Object destruction). </summary>
    /// <param name="obj">Object to check.</param>
    /// <returns>True if the object is null or a destroyed Unity Object.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNull(this object obj) => obj == null;

    /// <summary> Safely casts to type T, returning default(T) if cast fails. </summary>
    /// <param name="obj">Object to cast.</param>
    /// <typeparam name="T">Target type.</typeparam>
    /// <returns>The casted value or default(T).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Cast<T>(this object obj) => obj is T result ? result : default;

    /// <summary> Safely casts to type T with a custom default value. </summary>
    /// <param name="obj">Object to cast.</param>
    /// <param name="defaultValue">Value returned if the cast fails.</param>
    /// <typeparam name="T">Target type.</typeparam>
    /// <returns>The casted value or the specified default.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Cast<T>(this object obj, T defaultValue) => obj is T result ? result : defaultValue;

    /// <summary> Attempts to cast to type T. Returns true if successful. </summary>
    /// <param name="obj">Object to cast.</param>
    /// <param name="result">The casted value if successful, otherwise default(T).</param>
    /// <typeparam name="T">Target type.</typeparam>
    /// <returns>True if the cast was successful.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryCast<T>(this object obj, out T result)
    {
      if (obj is T casted)
      {
        result = casted;
        return true;
      }

      result = default;
      return false;
    }

    /// <summary> Converts an object to type T using Convert.ChangeType. Throws on failure. </summary>
    /// <param name="obj">Object to convert.</param>
    /// <typeparam name="T">Target type.</typeparam>
    /// <returns>The converted value.</returns>
    /// <exception cref="InvalidCastException">Thrown when the conversion is not supported.</exception>
    /// <exception cref="FormatException">Thrown when the object format is not compatible.</exception>
    public static T ConvertTo<T>(this object obj)
    {
      return (T)Convert.ChangeType(obj, typeof(T));
    }

    /// <summary> Safely converts to type T, returning default(T) on failure. </summary>
    /// <param name="obj">Object to convert.</param>
    /// <typeparam name="T">Target type.</typeparam>
    /// <returns>The converted value or default(T) if conversion fails.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SafeConvertTo<T>(this object obj)
    {
      try
      {
        return (T)Convert.ChangeType(obj, typeof(T));
      }
      catch
      {
        return default;
      }
    }
  }
}
