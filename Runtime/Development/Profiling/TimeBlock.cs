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
using UnityEngine;
using System.Diagnostics;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Time profiling block. </summary>
  public class TimeBlock : IDisposable
  {
    private readonly string title;
    private readonly Stopwatch stopwatch;
    private readonly int frameStart;

    public float Duration => (float)stopwatch.ElapsedTicks / Stopwatch.Frequency * 1000;

    public TimeBlock()
    {
      title = string.Empty;
      stopwatch = Stopwatch.StartNew();
      frameStart = Time.frameCount;
    }

    public TimeBlock(string title)
    {
      this.title = title ?? "Unknown";
      stopwatch = Stopwatch.StartNew();
      frameStart = Time.frameCount;
    }

    public void Dispose()
    {
      stopwatch.Stop();
      double duration = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency * 1000;

      if (string.IsNullOrEmpty(title) == false)
        Log.Info($"Task '{title}' took {duration:0.00}ms ({Time.frameCount - frameStart} frames)");
    }
  }
}
