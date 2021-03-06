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
using System.Runtime.CompilerServices;

using Debug = UnityEngine.Debug;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Log level.
  /// </summary>
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

  /// <summary>
  /// Text colors.
  /// </summary>
  public static class LogColors
  {
    public static string Info     => "#1E7CAA";
    public static string Warning  => "#FF8449";
    public static string Error    => "#FF3278";
  }

  /// <summary>
  /// Log messages.
  /// </summary>
  public static class Log
  {
    /// <summary>
    /// Log level.
    /// </summary>
    /// <returns>Log level.</returns>
    public static LogLevel Level { get; set; } = LogLevel.Info;

#if UNITY_EDITOR
    private static void LogInfo(string message)     => Debug.Log($"<color={LogColors.Info}>{message}</color>");
    private static void LogWarning(string message)  => Debug.LogWarning($"<color={LogColors.Warning}>{message}</color>");
    private static void LogError(string message)    => Debug.LogError($"<color={LogColors.Error}>{message}</color>");
#else
    private static void LogInfo(string message)     => Debug.Log(message);
    private static void LogWarning(string message)  => Debug.LogWarning(message);
    private static void LogError(string message)    => Debug.LogError(message);
#endif

    /// <summary>
    /// Information message.
    /// </summary>
    /// <param name="message">Message</param>
    [DebuggerStepThrough]
#if LOGS_DEVELOPER_BUILD
    [Conditional("DEVELOPMENT_BUILD")]
#endif
#if LOGS_EDITOR
    [Conditional("UNITY_EDITOR")]
#endif
    public static void Info(string message, [CallerMemberName]string member = "", [CallerFilePath]string sourceFile = "")
    {
      if (Level <= LogLevel.Info)
        LogInfo($"[{Path.GetFileNameWithoutExtension(sourceFile)}:{member}] {message}");
    }

    /// <summary>
    /// Warning message.
    /// </summary>
    /// <param name="message">Message</param>
    [DebuggerStepThrough]
#if LOGS_DEVELOPER_BUILD
    [Conditional("DEVELOPMENT_BUILD")]
#endif
#if LOGS_EDITOR
    [Conditional("UNITY_EDITOR")]
#endif
    public static void Warning(string message, [CallerMemberName]string member = "", [CallerFilePath]string sourceFile = "")
    {
      if (Level <= LogLevel.Warning)
        LogWarning($"[{Path.GetFileNameWithoutExtension(sourceFile)}:{member}] {message}");
    }

    /// <summary>
    /// Error message.
    /// </summary>
    /// <param name="message">Message</param>
    [DebuggerStepThrough]
#if LOGS_DEVELOPER_BUILD
    [Conditional("DEVELOPMENT_BUILD")]
#endif
#if LOGS_EDITOR
    [Conditional("UNITY_EDITOR")]
#endif
    public static void Error(string message, [CallerMemberName]string member = "", [CallerFilePath]string sourceFile = "")
    {
      if (Level <= LogLevel.Error)
        LogError($"[{Path.GetFileNameWithoutExtension(sourceFile)}:{member}] {message}");
    }

    /// <summary>
    /// Error message, exception and stack trace.
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="e">Exception</param>
    [DebuggerStepThrough]
#if LOGS_DEVELOPER_BUILD
    [Conditional("DEVELOPMENT_BUILD")]
#endif
#if LOGS_EDITOR
    [Conditional("UNITY_EDITOR")]
#endif
    public static void Exception(string message, Exception e = null, [CallerMemberName] string member = "", [CallerFilePath] string sourceFile = "")
    {
      LogError($"[{Path.GetFileNameWithoutExtension(sourceFile)}:{member}] {message}");

      if (e == null)
        e = new Exception(message);

      Debug.LogException(e);
      Debug.LogError(e.StackTrace);
    }
  }
}
