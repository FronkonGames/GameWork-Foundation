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
using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Checks values and throws an exception if the condition is not met. </summary>
  public static partial class Check
  {
    /// <summary> Check that the string is not null or empty. </summary>
    /// <param name="value">Value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void IsNotNullOrEmpty(string value, [CallerMemberName]string member = "",
                                                    [CallerFilePath]string sourceFile = "",
                                                    [CallerLineNumber]int line = 0) =>
      Assert(string.IsNullOrEmpty(value) == false, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(value)}' is null or empty.");

    /// <summary> Check that the chain has an exact length. </summary>
    /// <param name="value">Value</param>
    /// <param name="len">Length</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void Length(string value, int len, [CallerMemberName]string member = "",
                                                     [CallerFilePath]string sourceFile = "",
                                                     [CallerLineNumber]int line = 0) =>
      Assert(value.Length == len, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} Expected string '{nameof(value)}' length must be {len.ToString()}.");

    /// <summary> Check that the chain has a maximum length. </summary>
    /// <param name="value">Value</param>
    /// <param name="max">Max length</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void MaxLength(string value, int max, [CallerMemberName]string member = "",
                                                        [CallerFilePath]string sourceFile = "",
                                                        [CallerLineNumber]int line = 0) =>
      Assert(value.Length <= max, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} Expected string '{nameof(value)}' max length must be {max.ToString()}.");

    /// <summary> Check that the chain has a minimum length. </summary>
    /// <param name="value">Value</param>
    /// <param name="min">Min length</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void MinLength(string value, int min, [CallerMemberName]string member = "",
                                                        [CallerFilePath]string sourceFile = "",
                                                        [CallerLineNumber]int line = 0) =>
      Assert(value.Length >= min, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} Expected string '{nameof(value)}' min length must be {min.ToString()}.");
  }
}
