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
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> Development test. </summary>
public class ProfilingTests
{
  /// <summary> TimeBlock test. </summary>
  [UnityTest]
  public IEnumerator TimeBlock()
  {
    using (TimeBlock block = new("Test"))
    {
      Assert.IsNotNull(block);
    }

    using (TimeBlock block = new())
    {
      Assert.IsNotNull(block);
    }

    using (TimeBlock block = new(null))
    {
      Assert.IsNotNull(block);
    }

    yield return null;
  }

  /// <summary> MemoryBlock test. </summary>
  [UnityTest]
  public IEnumerator MemoryBlock()
  {
    using (MemoryBlock block = new("Test Memory"))
    {
      Assert.IsNotNull(block);
    }

    using (MemoryBlock block = new(null))
    {
      Assert.IsNotNull(block);
    }

    yield return null;
  }

  /// <summary> Profiling static methods test. </summary>
  [UnityTest]
  public IEnumerator ProfilingMethods()
  {
    Assert.IsNotNull(Profiling.MonoMemory);
    Assert.IsNotNull(Profiling.TotalMemory);
    Assert.IsNotNull(Profiling.StackMemory);

    using (Profiling.Time("Test Profiling"))
    {
      Assert.IsNotNull(Profiling.Time("Test"));
    }

    using (Profiling.Memory("Test Memory Profiling"))
    {
      Assert.IsNotNull(Profiling.Memory("Test"));
    }

    yield return null;
  }

  /// <summary> SampleBlock test. </summary>
  [UnityTest]
  public IEnumerator SampleBlock()
  {
    using (SampleBlock block = new("TestSample"))
    {
      Assert.IsNotNull(block);
    }

    yield return null;
  }

  /// <summary> MarkerBlock test. </summary>
  [UnityTest]
  public IEnumerator MarkerBlock()
  {
    using (MarkerBlock block = new(Unity.Profiling.ProfilerCategory.Scripts, "TestMarker"))
    {
      Assert.IsNotNull(block);
    }

    yield return null;
  }
}
