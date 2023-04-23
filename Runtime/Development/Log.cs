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

// Define to have Logs ONLY in Developer builds.
#define LOGS_DEVELOPER_BUILD

// Define to have Logs in the Editor.
#define LOGS_EDITOR

using System;
using System.IO;
using System.Diagnostics;
using CallerName = System.Runtime.CompilerServices.CallerMemberNameAttribute;
using CallerPath = System.Runtime.CompilerServices.CallerFilePathAttribute;
using CallerLine  = System.Runtime.CompilerServices.CallerLineNumberAttribute;
using UnityEngine;

using Debug = UnityEngine.Debug;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Log level. </summary>
  public enum LogLevel : byte
  {
    // Info.
    Info,

    // Warnings.
    Warning,

    // Errors.
    Error,

    // No message.
    Off
  }

  /// <summary> Log messages. </summary>
  public static class Log
  {
    /// <summary> Log level. </summary>
    public static LogLevel Level { get; set; } = Settings.Log.DefaultLevel;

#if UNITY_EDITOR
    private static void LogInfo(string source, string message)     => Debug.Log($"{source} <color={Settings.Log.InfoColor.ToHex()}>{message}</color>");
    private static void LogWarning(string source, string message)  => Debug.LogWarning($"{source} <color={(Settings.Log.WarningColor).ToHex()}>{message}</color>");
    private static void LogError(string source, string message)    => Debug.LogError($"{source} <color={(Settings.Log.ErrorColor).ToHex()}>{message}</color>");
#else
    private static void LogInfo(string source, string message)     => Debug.Log($"{source} {message}");
    private static void LogWarning(string source, string message)  => Debug.LogWarning($"{source} {message}");
    private static void LogError(string source, string message)    => Debug.LogError($"{source} {message}");
#endif

    /// <summary> Information message. </summary>
    /// <param name="message">Message</param>
    [DebuggerStepThrough]
#if LOGS_DEVELOPER_BUILD
    [Conditional("DEVELOPMENT_BUILD")]
#endif
#if LOGS_EDITOR
    [Conditional("UNITY_EDITOR")]
#endif
    public static void Info(string message, [CallerName] string member = "",
                                            [CallerPath] string sourceFile = "",
                                            [CallerLine] int line = 0)
    {
      if (Level <= LogLevel.Info)
        LogInfo($"[{Path.GetFileNameWithoutExtension(sourceFile)}:{member}:{line}]", message);
    }

    /// <summary> Warning message. </summary>
    /// <param name="message">Message</param>
    [DebuggerStepThrough]
#if LOGS_DEVELOPER_BUILD
    [Conditional("DEVELOPMENT_BUILD")]
#endif
#if LOGS_EDITOR
    [Conditional("UNITY_EDITOR")]
#endif
    public static void Warning(string message, [CallerName] string member = "",
                                               [CallerPath] string sourceFile = "",
                                               [CallerLine] int line = 0)
    {
      if (Level <= LogLevel.Warning)
        LogWarning($"[{Path.GetFileNameWithoutExtension(sourceFile)}:{member}:{line}]", message);
    }

    /// <summary> Error message. </summary>
    /// <param name="message">Message</param>
    [DebuggerStepThrough]
#if LOGS_DEVELOPER_BUILD
    [Conditional("DEVELOPMENT_BUILD")]
#endif
#if LOGS_EDITOR
    [Conditional("UNITY_EDITOR")]
#endif
    public static void Error(string message, [CallerName] string member = "",
                                             [CallerPath] string sourceFile = "",
                                             [CallerLine] int line = 0)
    {
      if (Level <= LogLevel.Error)
        LogError($"[{Path.GetFileNameWithoutExtension(sourceFile)}:{member}:{line}]", message);
    }

    /// <summary> Error message, exception and stack trace. </summary>
    /// <param name="message">Message</param>
    /// <param name="e">Exception</param>
    [DebuggerStepThrough]
#if LOGS_DEVELOPER_BUILD
    [Conditional("DEVELOPMENT_BUILD")]
#endif
#if LOGS_EDITOR
    [Conditional("UNITY_EDITOR")]
#endif
    public static void Exception(string message, Exception e = null, [CallerName] string member = "",
                                                                     [CallerPath] string sourceFile = "",
                                                                     [CallerLine] int line = 0)
    {
      LogError($"[{Path.GetFileNameWithoutExtension(sourceFile)}:{member}:{line}]", message);

      if (e == null)
        e = new Exception(message);

      Debug.LogException(e);

      if (Settings.Log.ShowStackTrace == true)
        Debug.LogError(e.StackTrace);
    }
  }
}
