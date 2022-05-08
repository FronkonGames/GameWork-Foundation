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
  /// Int attribute.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public sealed class IntAttribute : BaseAttribute
  {
    public readonly int defaultValue;
    public readonly int min;
    public readonly int max;

    public readonly string tooltip;

    public IntAttribute(string tooltip = "")
    {
      this.defaultValue = 0;
      this.min = this.max = 0;
      this.tooltip = tooltip;
    }

    public IntAttribute(int min, int max, int defaultValue = 0, string tooltip = "")
    {
      this.defaultValue = defaultValue;
      this.min = min;
      this.max = max;
      this.tooltip = tooltip;
    }

    public IntAttribute(int defaultValue = 0, string tooltip = "")
    {
      this.defaultValue = defaultValue;
      this.min = this.max = 0;
      this.tooltip = tooltip;
    }
  }
}
