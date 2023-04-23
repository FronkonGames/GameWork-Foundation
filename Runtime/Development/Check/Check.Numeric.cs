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
using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Checks values and throws an exception if the condition is not met. </summary>
  public static partial class Check
  {
    /// <summary> The values must be equal. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Equal<T>(T a, T b, [CallerMemberName]string member = "",
                                          [CallerFilePath]string sourceFile = "",
                                          [CallerLineNumber]int line = 0) where T : struct, IComparable<T> =>
      Assert(a.Equals(b), $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(b)}'.");

    /// <summary> The values must be equal. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Equal(Vector2 a, Vector2 b, [CallerMemberName]string member = "",
                                                   [CallerFilePath]string sourceFile = "",
                                                   [CallerLineNumber]int line = 0) =>
      Assert(a.Equals(b), $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(b)}'.");

    /// <summary> The values must be equal. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Equal(Vector2 a, float magnitude, [CallerMemberName]string member = "",
                                                         [CallerFilePath]string sourceFile = "",
                                                         [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude.NearlyEquals(magnitude), $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(magnitude)}'.");

    /// <summary> The values must be equal. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Equal(Vector3 a, Vector3 b, [CallerMemberName]string member = "",
                                                   [CallerFilePath]string sourceFile = "",
                                                   [CallerLineNumber]int line = 0) =>
      Assert(a.Equals(b), $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(b)}'.");

    /// <summary> The values must be equal. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Equal(Vector3 a, float magnitude, [CallerMemberName]string member = "",
                                                         [CallerFilePath]string sourceFile = "",
                                                         [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude.NearlyEquals(magnitude), $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(magnitude)}'.");

    /// <summary> The values must be equal. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Equal(Vector4 a, Vector4 b, [CallerMemberName]string member = "",
                                                   [CallerFilePath]string sourceFile = "",
                                                   [CallerLineNumber]int line = 0) =>
      Assert(a.Equals(b), $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(b)}'.");

    /// <summary> The values must be equal. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Equal(Vector4 a, float magnitude, [CallerMemberName]string member = "",
                                                         [CallerFilePath]string sourceFile = "",
                                                         [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude.NearlyEquals(magnitude), $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(magnitude)}'.");

    /// <summary> The values must be equal. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Equal(Quaternion a, Quaternion b, [CallerMemberName]string member = "",
                                                         [CallerFilePath]string sourceFile = "",
                                                         [CallerLineNumber]int line = 0) =>
      Assert(a.Equals(b), $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(b)}'.");

    /// <summary> Values should not be equal. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void NotEqual<T>(T a, T b, [CallerMemberName]string member = "",
                                             [CallerFilePath]string sourceFile = "",
                                             [CallerLineNumber]int line = 0) =>
      Assert(a.Equals(b) == false, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(b)}'.");

    /// <summary> Values should not be equal. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void NotEqual(Vector2 a, Vector2 b, [CallerMemberName]string member = "",
                                                      [CallerFilePath]string sourceFile = "",
                                                      [CallerLineNumber]int line = 0) =>
      Assert(a.Equals(b) == false, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(b)}'.");

    /// <summary> Values should not be equal. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void NotEqual(Vector2 a, float magnitude, [CallerMemberName]string member = "",
                                                            [CallerFilePath]string sourceFile = "",
                                                            [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude.NearlyEquals(magnitude) == false, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(magnitude)}'.");

    /// <summary> Values should not be equal. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void NotEqual(Vector3 a, Vector3 b, [CallerMemberName]string member = "",
                                                      [CallerFilePath]string sourceFile = "",
                                                      [CallerLineNumber]int line = 0) =>
      Assert(a.Equals(b) == false, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(b)}'.");

    /// <summary> Values should not be equal. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void NotEqual(Vector3 a, float magnitude, [CallerMemberName]string member = "",
                                                            [CallerFilePath]string sourceFile = "",
                                                            [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude.NearlyEquals(magnitude) == false, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(magnitude)}'.");

    /// <summary> Values should not be equal. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void NotEqual(Vector4 a, Vector4 b, [CallerMemberName]string member = "",
                                                      [CallerFilePath]string sourceFile = "",
                                                      [CallerLineNumber]int line = 0) =>
      Assert(a.Equals(b) == false, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(b)}'.");

    /// <summary> Values should not be equal. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void NotEqual(Vector4 a, float magnitude, [CallerMemberName]string member = "",
                                                            [CallerFilePath]string sourceFile = "",
                                                            [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude.NearlyEquals(magnitude) == false, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(magnitude)}'.");

    /// <summary> Values should not be equal. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void NotEqual(Quaternion a, Quaternion b, [CallerMemberName]string member = "",
                                                            [CallerFilePath]string sourceFile = "",
                                                            [CallerLineNumber]int line = 0) =>
      Assert(a.Equals(b) == false, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be equal to '{nameof(b)}'.");

    /// <summary> The first value must be less than the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Less<T>(T a, T b, [CallerMemberName]string member = "",
                                         [CallerFilePath]string sourceFile = "",
                                         [CallerLineNumber]int line = 0) where T : struct, IComparable<T> =>
      Assert(a.CompareTo(b) < 0, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less than '{nameof(b)}'.");

    /// <summary> The first value must be less than the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Less(Vector2 a, Vector2 b, [CallerMemberName]string member = "",
                                                  [CallerFilePath]string sourceFile = "",
                                                  [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude < b.magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less than '{nameof(b)}'.");

    /// <summary> The first value must be less than the second. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Less(Vector2 a, float magnitude, [CallerMemberName]string member = "",
                                                        [CallerFilePath]string sourceFile = "",
                                                        [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude < magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less than '{nameof(magnitude)}'.");

    /// <summary> The first value must be less than the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Less(Vector3 a, Vector3 b, [CallerMemberName]string member = "",
                                                  [CallerFilePath]string sourceFile = "",
                                                  [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude < b.magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less than '{nameof(b)}'.");

    /// <summary> The first value must be less than the second. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Less(Vector3 a, float magnitude, [CallerMemberName]string member = "",
                                                        [CallerFilePath]string sourceFile = "",
                                                        [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude < magnitude, $"{System.IO.Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less than '{nameof(magnitude)}'.");

    /// <summary> The first value must be less than the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Less(Vector4 a, Vector4 b, [CallerMemberName]string member = "",
                                                  [CallerFilePath]string sourceFile = "",
                                                  [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude < b.magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less than '{nameof(b)}'.");

    /// <summary> The first value must be less than the second. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Less(Vector4 a, float magnitude, [CallerMemberName]string member = "",
                                                        [CallerFilePath]string sourceFile = "",
                                                        [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude < magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less than '{nameof(magnitude)}'.");

    /// <summary> The first value must be less than or equal to the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void LessOrEqual<T>(T a, T b, [CallerMemberName]string member = "",
                                                [CallerFilePath]string sourceFile = "",
                                                [CallerLineNumber]int line = 0) where T : struct, IComparable<T> =>
      Assert(a.CompareTo(b) <= 0, $"{System.IO.Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less or equal to '{nameof(b)}'.");

    /// <summary> The first value must be less than or equal to the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void LessOrEqual(Vector2 a, Vector2 b, [CallerMemberName]string member = "",
                                                         [CallerFilePath]string sourceFile = "",
                                                         [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude <= b.magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less or equal to '{nameof(b)}'.");

    /// <summary> The first value must be less than or equal to the second. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void LessOrEqual(Vector2 a, float magnitude, [CallerMemberName]string member = "",
                                                               [CallerFilePath]string sourceFile = "",
                                                               [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude <= magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less or equal to '{nameof(magnitude)}'.");

    /// <summary> The first value must be less than or equal to the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void LessOrEqual(Vector3 a, Vector3 b, [CallerMemberName]string member = "",
                                                         [CallerFilePath]string sourceFile = "",
                                                         [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude <= b.magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less or equal to '{nameof(b)}'.");

    /// <summary> The first value must be less than or equal to the second. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void LessOrEqual(Vector3 a, float magnitude, [CallerMemberName]string member = "",
                                                               [CallerFilePath]string sourceFile = "",
                                                               [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude <= magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less or equal to '{nameof(magnitude)}'.");

    /// <summary> The first value must be less than or equal to the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void LessOrEqual(Vector4 a, Vector4 b, [CallerMemberName]string member = "",
                                                         [CallerFilePath]string sourceFile = "",
                                                         [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude <= b.magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less or equal to '{nameof(b)}'.");

    /// <summary> The first value must be less than or equal to the second. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void LessOrEqual(Vector4 a, float magnitude, [CallerMemberName]string member = "",
                                                               [CallerFilePath]string sourceFile = "",
                                                               [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude <= magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be less or equal to '{nameof(magnitude)}'.");

    /// <summary> The first value must be greater than the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Greater<T>(T a, T b, [CallerMemberName]string member = "",
                                            [CallerFilePath]string sourceFile = "",
                                            [CallerLineNumber]int line = 0) where T : struct, IComparable<T> =>
      Assert(b.CompareTo(a) < 0, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater than '{nameof(b)}'.");

    /// <summary> The first value must be greater than the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Greater(Vector2 a, Vector2 b, [CallerMemberName]string member = "",
                                                     [CallerFilePath]string sourceFile = "",
                                                     [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude > b.magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater than '{nameof(b)}'.");

    /// <summary> The first value must be greater than the second. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Greater(Vector2 a, float magnitude, [CallerMemberName]string member = "",
                                                           [CallerFilePath]string sourceFile = "",
                                                           [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude > magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater than '{nameof(magnitude)}'.");

    /// <summary> The first value must be greater than the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Greater(Vector3 a, Vector3 b, [CallerMemberName]string member = "",
                                                     [CallerFilePath]string sourceFile = "",
                                                     [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude > b.magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater than '{nameof(b)}'.");

    /// <summary> The first value must be greater than the second. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Greater(Vector3 a, float magnitude, [CallerMemberName]string member = "",
                                                           [CallerFilePath]string sourceFile = "",
                                                           [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude > magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater than '{nameof(magnitude)}'.");

    /// <summary> The first value must be greater than the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Greater(Vector4 a, Vector4 b, [CallerMemberName]string member = "",
                                                     [CallerFilePath]string sourceFile = "",
                                                     [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude > b.magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater than '{nameof(b)}'.");

    /// <summary> The first value must be greater than the second. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Greater(Vector4 a, float magnitude, [CallerMemberName]string member = "",
                                                           [CallerFilePath]string sourceFile = "",
                                                           [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude > magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater than '{nameof(magnitude)}'.");

    /// <summary> The first value must be greater than or equal to the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void GreaterOrEqual<T>(T a, T b, [CallerMemberName]string member = "",
                                                   [CallerFilePath]string sourceFile = "",
                                                   [CallerLineNumber]int line = 0) where T : struct, IComparable<T> =>
      Assert(b.CompareTo(a) <= 0, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater or equal to '{nameof(b)}'.");

    /// <summary> The first value must be greater than or equal to the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void GreaterOrEqual(Vector2 a, Vector2 b, [CallerMemberName]string member = "",
                                                            [CallerFilePath]string sourceFile = "",
                                                            [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude >= b.magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater or equal to '{nameof(b)}'.");

    /// <summary> The first value must be greater than or equal to the second. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void GreaterOrEqual(Vector2 a, float magnitude, [CallerMemberName]string member = "",
                                                                  [CallerFilePath]string sourceFile = "",
                                                                  [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude >= magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater or equal to '{nameof(magnitude)}'.");

    /// <summary> The first value must be greater than or equal to the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void GreaterOrEqual(Vector3 a, Vector3 b, [CallerMemberName]string member = "",
                                                            [CallerFilePath]string sourceFile = "",
                                                            [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude >= b.magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater or equal to '{nameof(b)}'.");

    /// <summary> The first value must be greater than or equal to the second. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void GreaterOrEqual(Vector3 a, float magnitude, [CallerMemberName]string member = "",
                                                                  [CallerFilePath]string sourceFile = "",
                                                                  [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude >= magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater or equal to '{nameof(magnitude)}'.");

    /// <summary> The first value must be greater than or equal to the second. </summary>
    /// <param name="a">First value</param>
    /// <param name="b">Second value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void GreaterOrEqual(Vector4 a, Vector4 b, [CallerMemberName]string member = "",
                                                            [CallerFilePath]string sourceFile = "",
                                                            [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude >= b.magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater or equal to '{nameof(b)}'.");

    /// <summary> The first value must be greater than or equal to the second. </summary>
    /// <param name="a">Value</param>
    /// <param name="magnitude">Magnitude</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void GreaterOrEqual(Vector4 a, float magnitude, [CallerMemberName]string member = "",
                                                                  [CallerFilePath]string sourceFile = "",
                                                                  [CallerLineNumber]int line = 0) =>
      Assert(a.magnitude >= magnitude, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(a)}' must be greater or equal to '{nameof(magnitude)}'.");

    /// <summary> The value must be less than zero. </summary>
    /// <param name="value">Value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Negative<T>(T value, [CallerMemberName]string member = "",
                                            [CallerFilePath]string sourceFile = "",
                                            [CallerLineNumber]int line = 0) where T : struct, IComparable<T> =>
      Assert(value.CompareTo(default) < 0, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(value)}' must be negative.");

    /// <summary> The value must be greater than or equal to zero. </summary>
    /// <param name="value">Value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void NotNegative<T>(T value, [CallerMemberName]string member = "",
                                               [CallerFilePath]string sourceFile = "",
                                               [CallerLineNumber]int line = 0) where T : struct, IComparable<T> =>
      Assert(value.CompareTo(default) >= 0, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(value)}' must not be negative.");

    /// <summary> The value must be within a range (including limits). </summary>
    /// <param name="value">Value</param>
    /// <param name="min">Lower limit</param>
    /// <param name="max">Upper limit</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void IsWithin<T>(T value, T min, T max, [CallerMemberName]string member = "",
                                                          [CallerFilePath]string sourceFile = "",
                                                          [CallerLineNumber]int line = 0) where T : struct, IComparable<T> =>
      Assert(value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(value)}' must be within '{nameof(min)}' and '{nameof(max)}'.");
    
    /// <summary> The value must be within a range (limits not included). </summary>
    /// <param name="value">Value</param>
    /// <param name="min">Lower limit</param>
    /// <param name="max">Upper limit</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void IsBetween<T>(T value, T min, T max, [CallerMemberName]string member = "",
                                                           [CallerFilePath]string sourceFile = "",
                                                           [CallerLineNumber]int line = 0) where T : struct, IComparable<T> =>
      Assert(value.CompareTo(min) > 0 && value.CompareTo(max) < 0, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(value)}' must be between '{nameof(min)}' and '{nameof(max)}'.");
  }
}
