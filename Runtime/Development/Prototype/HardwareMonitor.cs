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
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace FronkonGames.GameWork.Foundation.Prototype
{
  /// <summary> Hardware monitor. </summary>
  public sealed class HardwareMonitor : CachedMonoBehaviour
  {
    /// <summary> Current FPS. </summary>
    public int FPS { get; private set; }

    /// <summary> Max FPS. </summary>
    public int MaxFPS { get; private set; }

    /// <summary> Average FPS. </summary>
    public int AverageFPS { get; private set; }

    /// <summary> Time the last frame lasted, in milliseconds. </summary>
    /// <value>ms.</value>
    public int FrameTime { get; private set; }

    /// <summary> Current reserved memory (bytes). </summary>
    public long ReservedMemory { get; private set; }

    /// <summary> Current allocated memory (bytes). </summary>
    public long AllocatedMemory { get; private set; }

    /// <summary> Free memory (bytes). </summary>
    public long FreeMemory { get; private set; }

    /// <summary> Total mono memory size (bytes). </summary>
    public long MonoHeap { get; private set; }

    /// <summary> Used mono memory size (bytes). </summary>
    public long MonoUsed { get; private set; }

    [SerializeField]
    private Text processorLabel;

    [SerializeField]
    private Text gpuLabel;

    [SerializeField]
    private Text resolutionLabel;

    [SerializeField]
    private Text fpsLabel;

    [SerializeField]
    private Text memory1Label;

    [SerializeField]
    private Text memory2Label;

    [SerializeField]
    private Text drawCall1Label;

    [SerializeField]
    private Text drawCall2Label;

    private Vector2Int lastResolution;

    private int frames;
    private float deltaTime;

    private float waitToStart;

    private readonly int[] history = new int[Settings.FPS.HistoryFrames];
    private int historyIndex;

    /// <summary> Unload unused resources. </summary>
    public void UnloadUnusedAssets() => Resources.UnloadUnusedAssets();

    /// <summary> Garbage collection. </summary>
    public void RunGarbageCollection()
    {
      GC.Collect();
      GC.WaitForPendingFinalizers();
    }

    /// <summary> Reset the counters. </summary>
    public void ResetCounters()
    {
      FPS = 0;
      AverageFPS = 0;
      FrameTime = 0;
      frames = 0;
      deltaTime = 0.0f;
      waitToStart = 0.0f;

      history.Fill(-1);
      historyIndex = 0;
    }

    private void UpdateProcessor()
    {
      if (processorLabel != null)
      {
        string processorType = SystemInfo.processorType;
        if (processorType.Contains(" CPU") == true)
        {
          int cpuIndex = -1;

          string[] splits = processorType.Split(" ");
          for (int i = 0; i < splits.Length && cpuIndex == -1; ++i)
            cpuIndex = splits[i] == "CPU" ? i : -1;

          if (cpuIndex > 0)
            processorType = splits[cpuIndex - 1];
        }

        processorLabel.text = processorType;
      }
    }

    private void UpdateGPU()
    {
      if (gpuLabel != null)
        gpuLabel.text = SystemInfo.graphicsDeviceName;
    }

    private void UpdateResolution()
    {
      if (resolutionLabel != null)
        resolutionLabel.text = $"{Screen.width} x {Screen.height}";

      lastResolution = new Vector2Int(Screen.width, Screen.height);
    }

    private void UpdateMemory()
    {
      ReservedMemory = Profiler.GetTotalReservedMemoryLong();
      AllocatedMemory = Profiler.GetTotalAllocatedMemoryLong();
      FreeMemory = Profiler.GetTotalUnusedReservedMemoryLong();
      MonoHeap = Profiler.GetMonoHeapSizeLong();
      MonoUsed = Profiler.GetMonoUsedSizeLong();

      if (memory1Label != null)
        memory1Label.text = $"RESERVED {ReservedMemory / 1_000_000:0000}MB  ALLOC {AllocatedMemory / 1_000_000:0000}MB";

      if (memory2Label != null)
        memory2Label.text = $"FREE {FreeMemory / 1_000_000:0000}MB  USED {MonoUsed / 1_000_000:0000}MB";
    }

    private void UpdateDrawCall()
    {
#if UNITY_EDITOR
      if (drawCall1Label != null)
        drawCall1Label.text = $"{UnityEditor.UnityStats.drawCalls:0000} DRAW CALLS  {UnityEditor.UnityStats.batches:000} BATCHES";
      
      if (drawCall2Label != null)
        drawCall2Label.text = $"{UnityEditor.UnityStats.triangles:0000} TRIANGLES  {UnityEditor.UnityStats.vertices:000} VERTICES";
#endif
    }

    private void OnEnable()
    {
      ResetCounters();

      UpdateProcessor();
      UpdateGPU();
      UpdateResolution();

      if (fpsLabel != null)
      {
        fpsLabel.color = Settings.FPS.GoodColor;
        fpsLabel.text = "FPS 000  MAX 000  AVG 000  00ms";
      }

      if (memory1Label != null)
        memory1Label.text = $"RESERVED 0000MB  ALLOC 0000MB";

#if !UNITY_EDITOR
      if (drawCallLabel != null)
        drawCallLabel.text = "Draw Calls is only available in the Editor";
#endif

      waitToStart = Settings.FPS.WaitingToStartCounting;
    }

    private void Update()
    {
      if (Screen.width != lastResolution.x || Screen.height != lastResolution.y)
        UpdateResolution();

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
        FPS = Mathf.CeilToInt(frames / deltaTime);
        frames = 0;
        deltaTime -= lapse;

        if (historyIndex < Settings.FPS.HistoryFrames)
          history[historyIndex++] = FPS;
        else
        {
          historyIndex = 0;
          history[historyIndex] = FPS;
        }

        MaxFPS = 0;
        int total = 0, count = 0;
        for (int i = 0; i < Settings.FPS.HistoryFrames; ++i)
        {
          if (history[i] > 0)
          {
            if (MaxFPS < history[i])
              MaxFPS = history[i];

            total += history[i];
            count++;
          }
          else
            break;
        }

        AverageFPS = total / count;
        FrameTime = Mathf.CeilToInt(1000.0f * Time.deltaTime);

        if (fpsLabel != null)
        {
          fpsLabel.color = FPS < Settings.FPS.BadFPS ? Settings.FPS.BadColor : FPS < Settings.FPS.WarningFPS ? Settings.FPS.WarningColor : Settings.FPS.GoodColor;
          fpsLabel.text = $"FPS {FPS:000}  MAX {MaxFPS:000}  AVG {AverageFPS:000}  {FrameTime:00}ms";
        }
      }
    }

    private void LateUpdate()
    {
      UpdateMemory();
#if UNITY_EDITOR
      UpdateDrawCall();
#endif
    }
  }
}
