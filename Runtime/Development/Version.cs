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
  /// Version format: X.Y-Z: X major version.
  ///                        Y minor version.
  ///                        Z commits count.
  /// </summary>
#if UNITY_EDITOR
  [UnityEditor.InitializeOnLoad]
#endif
  public static class Version
  {
    /// <summary> X.Y (major.minor)</summary>
    /// <value>Version.</value>
    public static string MajorMinor => $"{major}.{minor}";

    /// <summary> X.Y-Z (major.minor-commit hash) </summary>
    /// <value>Version.</value>
    public static string MajorMinorCommits => $"{major}.{minor}-{commits} ({commit})";

    /// <summary> X.Y-Z (H-B) (major.minor-commits hash:branch) </summary>
    /// <value>Version.</value>
    public static string MajorMinorCommitsBranch => $"{major}.{minor}-{commits} ({commit}:{branch})";

    private static int major;
    private static int minor;

    private static string commit;
    private static int commits;
    private static string branch;

#if UNITY_EDITOR
    static Version()
    {
      Refresh();
    }

    public static void Refresh()
    {
      commit = GetVersionHash();
      commits = GetTotalNumberOfCommits();
      branch = GetCurrentBranchName();

      string version = UnityEditor.PlayerSettings.bundleVersion;
      if (version.IndexOf(" ") >= 0)
        version = version[..version.IndexOf(" ")];

      string[] parts = string.IsNullOrEmpty(UnityEditor.PlayerSettings.bundleVersion) == false ? version.Split('.', '-') : null;
      if (parts != null && parts.Length == 3)
      {
        major = Convert.ToInt32(parts[0]);
        minor = Convert.ToInt32(parts[1]);
      }
      else
        major = minor = 0;

      UnityEditor.PlayerSettings.bundleVersion = MajorMinorCommits;
    }

    private static int GetTotalNumberOfCommits() => int.Parse(Execute("rev-list --count HEAD"));

    private static string GetVersionHash() => Execute("rev-parse --short HEAD");

    private static string GetCurrentBranchName() => Execute("branch --show-current");

    private static string Execute(string arguments)
    {
      using var process = new System.Diagnostics.Process();

      process.StartInfo.FileName = "git.exe";
      process.StartInfo.Arguments = arguments;
      process.StartInfo.CreateNoWindow = true;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardInput = true;
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.RedirectStandardError = false;

      process.Start();
      process.WaitForExit();

      return process.StandardOutput.ReadLine();
    }
#endif    
  }
}
