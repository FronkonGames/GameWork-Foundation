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
using System.Runtime.CompilerServices;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> TimeSpan extensions. </summary>
  public static class TimeSpanExtensions
  {
    /// <summary> Returns the total whole hours including days. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int TotalWholeHours(this TimeSpan timeSpan) => timeSpan.Days * 24 + timeSpan.Hours;

    /// <summary> Returns the total whole minutes including days and hours. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int TotalWholeMinutes(this TimeSpan timeSpan) => (timeSpan.Days * 24 + timeSpan.Hours) * 60 + timeSpan.Minutes;

    /// <summary> Returns a new TimeSpan with seconds subtracted. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan SubtractSeconds(this TimeSpan timeSpan, int seconds) => timeSpan - TimeSpan.FromSeconds(seconds);
  }
}
