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
  /// <summary> Command line utilities. </summary>
  /// <remarks>
  /// Arguments must begin with '-' and may or may not have a value associated with them. The name of the arguments
  /// are case-insensitive. Argument values do not support spaces.
  /// 
  /// For example: -god on -name Unknown -enableRTX
  /// </remarks>
  public static class CommandLineUtils
  {
    /// <summary>
    /// Returns a text array with the command line. The first one, which is the program name, is always eliminated.
    /// </summary>
    /// <returns> Text array. </returns>
    public static string[] GetArguments()
    {
      try
      {
        List<string> args = new(Environment.GetCommandLineArgs());
#if UNITY_EDITOR
        // @HACK: Only in the editor when a unit test is launched.
        if (Environment.StackTrace.Contains("UnityEngine.TestRunner") == true)
          args = new List<string> { "program_name", "-noparams", "-string", "text", "-int", "42", "-god", "on" };
#endif
        if (args != null && args.Count > 1)
        {
          // The first argument is the name of the program.
          args.RemoveAt(0);
          
          return args.ToArray();
        }
      }
      catch (Exception e)
      {
        Log.Error(e.Message);
      }

      return new string[0];
    }

    /// <summary> Does the parameter exist? </summary>
    /// <param name="argument"> Parameter name (case-insensitive). </param>
    /// <returns> True / false. </returns>
    public static bool HasArgument(string argument)
    {
      bool found = false;

      string[] args = GetArguments();
      for (int i = 0; i < args.Length && found == false; ++i)
        found = args[i].ToLower() == $"-{argument.ToLower()}";

      return found;
    }

    /// <summary> Returns the value of an argument or a default value if it does not exist. </summary>
    /// <param name="argument"> Parameter name (case-insensitive). </param>
    /// <param name="defaultValue"> Default value. </param>
    /// <returns> Value of the argument. </returns>
    public static string GetValue(string argument, string defaultValue = "")
    {
      string[] args = GetArguments();
      for (int i = 0; i < args.Length; ++i)
      {
        if (args[i].ToLower() == $"-{argument.ToLower()}")
        {
          if (i + 1 < args.Length && args[i + 1].StartsWith("-") == false)
            return args[i + 1];
        }
      }

      return defaultValue;
    }
  }
}
