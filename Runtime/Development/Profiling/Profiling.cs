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
using Unity.Profiling;
using UnityEngine.Profiling;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Profiling. </summary>
  public static class Profiling
  {
    /// <summary> Gets the allocated managed memory for live objects and non-collected objects. </summary>
    /// <remarks>It needs the Profiler to be enabled.</remarks>
    public static long MonoUsed => Profiler.supported == true ? Profiler.GetMonoUsedSizeLong() : 0L;

    /// <summary> Returns the size in bytes of the reserved space for managed-memory. </summary>
    public static long MonoHeap => Profiler.supported == true ? Profiler.GetMonoHeapSizeLong() : GC.GetTotalMemory(false);

    /// <summary>
    /// The total memory allocated by the internal allocators in Unity. Unity reserves large pools of memory from
    /// the system; this includes double the required memory for textures because Unity keeps a copy of each texture
    /// on both the CPU and GPU. This function returns the amount of used memory in those pools.
    /// </summary>
    /// <remarks>It needs the Profiler to be enabled.</remarks>
    public static long Allocated => Profiler.supported == true ? Profiler.GetTotalAllocatedMemoryLong() : 0L;

    /// <summary> The total memory Unity has reserved. </summary>
    /// <remarks>It needs the Profiler to be enabled.</remarks>
    public static long Reserved => Profiler.supported == true ? Profiler.GetTotalReservedMemoryLong() : 0L;

    /// <summary> Returns the size of the temp allocator. </summary>
    /// <remarks>It needs the Profiler to be enabled.</remarks>
    public static long Stack => Profiler.supported == true ? Profiler.GetTempAllocatorSize() : 0L;

    /// <summary> Used and available memory. </summary>
    /// <remarks>It needs the Profiler to be enabled.</remarks>
    public static string MonoMemory => $"{((int)MonoUsed).BytesToHumanReadable()}/{((int)MonoHeap).BytesToHumanReadable()}";

    /// <summary> Total used and available memory. </summary>
    /// <remarks>It needs the Profiler to be enabled.</remarks>
    public static string TotalMemory => $"{((int)Allocated).BytesToHumanReadable()}/{((int)Reserved).BytesToHumanReadable()}";

    /// <summary> Returns the size of the temp allocator. </summary>
    /// <remarks>It needs the Profiler to be enabled.</remarks>
    public static string StackMemory => ((int)Stack).BytesToHumanReadable();

    /// <summary> It measures the elapsed time. </summary>
    /// <remarks>Only available in the Editor or Development Builds.</remarks>
    /// <param name="title">Label</param>
    /// <returns>TimeBlock</returns>
    public static TimeBlock Time(string title) =>
#if UNITY_EDITOR || DEVELOPMENT_BUILD
      new(title);
#else
      null;
#endif

    /// <summary> It measures the memory consumed. </summary>
    /// <remarks>Only available in the Editor or Development Builds.</remarks>
    /// <param name="title">Label</param>
    /// <returns>MemoryBlock</returns>
    public static MemoryBlock Memory(string title) =>
#if UNITY_EDITOR || DEVELOPMENT_BUILD
      new(title);
#else
      null;
#endif

    /// <summary> Profiling CPU block of code with a custom label. </summary>
    /// <remarks>Only available in the Editor or Development Builds.</remarks>
    /// <param name="title">Label</param>
    /// <returns>SampleBlock</returns>
    public static SampleBlock Sample(string title) =>
#if UNITY_EDITOR || DEVELOPMENT_BUILD
      new(title);
#else
      null;
#endif

    /// <summary> Profiling marker block of code. </summary>
    /// <remarks>Only available in the Editor or Development Builds.</remarks>
    /// <param name="category">Profiler category</param>
    /// <param name="title">Label</param>
    /// <returns>MarkerBlock</returns>
    public static MarkerBlock Marker(ProfilerCategory category, string title) =>
#if UNITY_EDITOR || DEVELOPMENT_BUILD
      new(category, title);
#else
      null;
#endif
  }
}
