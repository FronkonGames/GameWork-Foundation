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
using UnityEngine;
using UnityEngine.UI;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Calculate FPS. </summary>
  public sealed class FPSCounter : BaseMonoBehaviour
  {
    /// <summary> Current FPS. </summary>
    /// <value>FPS.</value>
    public int CurrentFPS { get; private set; }

    /// <summary> Average FPS. </summary>
    /// <value>FPS.</value>
    public int AverageFPS { get; private set; }

    /// <summary> Time the last frame lasted, in milliseconds. </summary>
    /// <value>ms.</value>
    public int FrameTime { get; private set; }

    [SerializeField]
    private Text label;

    private int frames;
    private float deltaTime;

    private float waitToStart;

    private readonly int[] history = new int[Settings.FPS.HistoryFrames];
    private int historyIndex;

    /// <summary> Reset the counters. </summary>
    public void ResetCounters()
    {
      CurrentFPS = 0;
      AverageFPS = 0;
      FrameTime = 0;
      frames = 0;
      deltaTime = 0.0f;
      waitToStart = 0.0f;

      history.Fill(-1);
      historyIndex = 0;
    }

    private void OnEnable()
    {
      ResetCounters();

      waitToStart = Settings.FPS.WaitingToStartCounting;
    }

    private void Update()
    {
      if (waitToStart > 0.0f)
      {
        waitToStart -= Time.unscaledDeltaTime;

        return;
      }

      ++frames;
      deltaTime += Time.unscaledDeltaTime;

      float lapse = 1.0f / Settings.FPS.UpdatePerSecond;
      if (deltaTime > lapse)
      {
        CurrentFPS = Mathf.CeilToInt(frames / deltaTime);
        frames = 0;
        deltaTime -= lapse;

        if (historyIndex < Settings.FPS.HistoryFrames)
          history[historyIndex++] = CurrentFPS;
        else
        {
          historyIndex = 0;
          history[historyIndex] = CurrentFPS;
        }

        int total = 0, count = 0;
        for (int i = 0; i < Settings.FPS.HistoryFrames; ++i)
        {
          if (history[i] > 0)
          {
            total += history[i];
            count++;
          }
          else
            break;
        }

        AverageFPS = total / count;
        FrameTime = Mathf.CeilToInt(1000.0f * Time.deltaTime);

        if (label != null)
        {
          label.color = CurrentFPS < Settings.FPS.BadFPS ? Settings.FPS.BadColor : CurrentFPS < Settings.FPS.WarningFPS ? Settings.FPS.WarningColor : Settings.FPS.GoodColor;
          label.text = $"{CurrentFPS:000}FPS {AverageFPS:000}AVG {FrameTime:00}ms";
        }
      }
    }
  }
}
