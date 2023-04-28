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
using System.Diagnostics;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Min-max Int/Float slider attribute. </summary>
  [Conditional("UNITY_EDITOR")]
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class MinMaxSliderAttribute : PropertyAttribute
  {
    public readonly float min;

    public readonly float max;

    public readonly float snap;

    public readonly float resetMin;

    public readonly float resetMax;

    public MinMaxSliderAttribute(float min, float max, float resetMin = 0.0f, float resetMax = 1.0f, float snap = 0.0f)
    {
      this.min = Mathf.Min(min, max);
      this.max = Mathf.Max(min, max);
      this.resetMin = Mathf.Max(this.min, resetMin);
      this.resetMax = Mathf.Min(this.max, resetMax);
      this.snap = snap;
    }
  }
}