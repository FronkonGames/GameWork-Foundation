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
using System.Collections.Generic;
using FronkonGames.GameWork.Foundation;
using UnityEngine;

namespace FronkonGames.GameWork.Core
{
  /// <summary> Calculate FPS. </summary>
  public sealed class CalculateFPS : BaseMonoBehaviour
  {
    /// <summary> Last FPS. </summary>
    /// <value>FPS.</value>
    public float CurrentFPS { get; private set; }

    /// <summary> Average FPS. </summary>
    /// <value>FPS.</value>
    public float AverageFPS { get; private set; }

    private int frames;
    private float deltaTime;

    private const int UpdatePerSecond = 2;
    private const int HistoryFrames = 100;

    private readonly Queue<float> history = new Queue<float>(HistoryFrames);
    private IEnumerator<float> historyEnumerator;

    /// <summary> Reset the counters. </summary>
    public void Reset()
    {
      CurrentFPS = 0.0f;
      AverageFPS = 0.0f;
      frames = 0;
      deltaTime = 0.0f;

      history.Clear();
      historyEnumerator = history.GetEnumerator();
    }

    private void OnEnable()
    {
      Reset();
    }

    private void Update()
    {
      ++frames;
      deltaTime += Time.unscaledDeltaTime;
      
      float lapse = 1.0f / UpdatePerSecond;
      if (deltaTime > lapse)
      {
        CurrentFPS = frames / deltaTime;
        frames = 0;
        deltaTime -= lapse;

        int count = history.Count;
        if (count >= HistoryFrames)
          history.Dequeue();

        history.Enqueue(CurrentFPS);

        float total = 0.0f;
        while (historyEnumerator.MoveNext() == true)
          total += historyEnumerator.Current;

        AverageFPS = total / count;
      }
    }
  }
}
