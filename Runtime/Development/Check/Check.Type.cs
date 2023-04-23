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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Checks values and throws an exception if the condition is not met. </summary>
  public static partial class Check
  {
    /// <summary> Check if the value is null. </summary>
    /// <param name="value">Value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void IsNull(object value, [CallerMemberName]string member = "",
                                            [CallerFilePath]string sourceFile = "",
                                            [CallerLineNumber]int line = 0) =>
      Assert(value == null, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(value)}' must be null.");

    /// <summary> Check that the value is not null. </summary>
    /// <param name="value">Value</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void IsNotNull(object value, [CallerMemberName]string member = "",
                                               [CallerFilePath]string sourceFile = "",
                                               [CallerLineNumber]int line = 0) =>
      Assert(value != null, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(value)}' must not be null.");

    /// <summary> Check that the collection is not null or empty. </summary>
    /// <param name="collection">ICollection</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void IsNotNullOrEmpty<T>(ICollection<T> collection, [CallerMemberName]string member = "",
                                                                      [CallerFilePath]string sourceFile = "",
                                                                      [CallerLineNumber]int line = 0) =>
      Assert(collection != null && collection.Count > 0, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(collection)}' cant be null or empty.");

    /// <summary> The object is of type Type. </summary>
    /// <param name="value">Value</param>
    /// <param name="type">Type</param>
    /// <remarks>Only executed if UNITY_ASSERTIONS is defined.</remarks>
    [DebuggerStepThrough, Conditional("UNITY_ASSERTIONS")]
    public static void OfType(object value, Type type, [CallerMemberName]string member = "",
                                                       [CallerFilePath]string sourceFile = "",
                                                       [CallerLineNumber]int line = 0) =>
      Assert(type != null && value != null && type.IsInstanceOfType(value) == true, $"{Path.GetFileName(sourceFile)}:{member}:{line.ToString()} '{nameof(value)}' must be of type '{type?.Name}'.");
  }
}
