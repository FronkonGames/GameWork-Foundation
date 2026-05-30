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
using System.Globalization;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Serializable time duration for Unity serialization. </summary>
  [Serializable]
  public struct SerializableTime
  {
    private const long TicksPerMillisecond = 10000;
    private const long TicksPerSecond = 10000000;
    private const long TicksPerMinute = 600000000;
    private const long TicksPerHour = 36000000000;
    private const long TicksPerDay = 864000000000;

    [SerializeField] private int days;
    [SerializeField] private int hours;
    [SerializeField] private int minutes;
    [SerializeField] private int seconds;
    [SerializeField] private int milliseconds;

    /// <summary> Days component. </summary>
    public int Days => days;

    /// <summary> Hours component. </summary>
    public int Hours => hours;

    /// <summary> Minutes component. </summary>
    public int Minutes => minutes;

    /// <summary> Seconds component. </summary>
    public int Seconds => seconds;

    /// <summary> Milliseconds component. </summary>
    public int Milliseconds => milliseconds;

    /// <summary> Total ticks. </summary>
    public long Ticks => GetTicks();

    /// <summary> Total days. </summary>
    public double TotalDays => Math.Floor(GetTicks() / (double)TicksPerDay);

    /// <summary> Total hours. </summary>
    public double TotalHours => Math.Floor(GetTicks() / (double)TicksPerHour);

    /// <summary> Total minutes. </summary>
    public double TotalMinutes => Math.Floor(GetTicks() / (double)TicksPerMinute);

    /// <summary> Total seconds. </summary>
    public double TotalSeconds => Math.Floor(GetTicks() / (double)TicksPerSecond);

    /// <summary> Total milliseconds. </summary>
    public double TotalMilliSeconds => GetTicks() / (double)TicksPerMillisecond;

    /// <summary> Create from seconds. </summary>
    public static SerializableTime FromSeconds(double seconds)
    {
      TimeSpan span = TimeSpan.FromSeconds(seconds);
      return FromTimeSpan(span);
    }

    /// <summary> Create from ticks. </summary>
    public static SerializableTime FromTicks(long ticks)
    {
      TimeSpan span = new(ticks);
      return FromTimeSpan(span);
    }

    /// <summary> Create from TimeSpan. </summary>
    public static SerializableTime FromTimeSpan(TimeSpan timeSpan) => new()
    {
      days = timeSpan.Days,
      hours = timeSpan.Hours,
      minutes = timeSpan.Minutes,
      seconds = timeSpan.Seconds,
      milliseconds = timeSpan.Milliseconds
    };

    /// <summary> Create from DateTime (extracts time of day). </summary>
    public static SerializableTime FromDateTime(DateTime dateTime) => FromTimeSpan(dateTime.TimeOfDay);

    /// <summary> Convert to TimeSpan. </summary>
    public static implicit operator TimeSpan(SerializableTime self) => new(self.GetTicks());

    /// <summary> Convert to DateTime. </summary>
    public static implicit operator DateTime(SerializableTime self) => new(self.GetTicks());

    private long GetTicks()
    {
      long total = days * TicksPerDay;
      total += hours * TicksPerHour;
      total += minutes * TicksPerMinute;
      total += seconds * TicksPerSecond;
      total += milliseconds * TicksPerMillisecond;
      return total;
    }

    /// <summary> String representation. </summary>
    public override string ToString() => $"{days}:{hours:D2}:{minutes:D2}:{seconds:D2}.{milliseconds:D3}";

    /// <summary> String representation with format. </summary>
    public string ToString(string format) => ToString(format, CultureInfo.InvariantCulture);

    /// <summary> String representation with provider. </summary>
    public string ToString(IFormatProvider provider) => new TimeSpan(GetTicks()).ToString(null, provider);

    /// <summary> String representation with format and provider. </summary>
    public string ToString(string format, IFormatProvider provider) => new TimeSpan(GetTicks()).ToString(format, provider);
  }
}
