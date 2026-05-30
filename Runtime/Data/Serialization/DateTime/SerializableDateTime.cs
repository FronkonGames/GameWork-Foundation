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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Serializable DateTime wrapper for Unity serialization. </summary>
  [Serializable]
  public class SerializableDateTime
  {
    [SerializeField] private int year;
    [SerializeField] private int month;
    [SerializeField] private int day;
    [SerializeField] private int hour;
    [SerializeField] private int minute;
    [SerializeField] private int second;
    [SerializeField] private int millisecond;
    [SerializeField, HideInInspector] private DateTimeKind kind;

    /// <summary> Year component. </summary>
    public int Year => year;

    /// <summary> Month component. </summary>
    public int Month => month;

    /// <summary> Day component. </summary>
    public int Day => day;

    /// <summary> Hour component. </summary>
    public int Hour => hour;

    /// <summary> Minute component. </summary>
    public int Minute => minute;

    /// <summary> Second component. </summary>
    public int Second => second;

    /// <summary> Millisecond component. </summary>
    public int Millisecond => millisecond;

    /// <summary> DateTimeKind component. </summary>
    public DateTimeKind Kind => kind;

    /// <summary> Create from a DateTime. </summary>
    public SerializableDateTime(DateTime dateTime)
    {
      year = dateTime.Year;
      month = dateTime.Month;
      day = dateTime.Day;
      hour = dateTime.Hour;
      minute = dateTime.Minute;
      second = dateTime.Second;
      millisecond = dateTime.Millisecond;
      kind = dateTime.Kind;
    }

    /// <summary> Create from components. </summary>
    public SerializableDateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
    {
      this.year = year;
      this.month = month;
      this.day = day;
      this.hour = hour;
      this.minute = minute;
      this.second = second;
      millisecond = 0;
      this.kind = kind;
    }

    /// <summary> Create from components with milliseconds. </summary>
    public SerializableDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
    {
      this.year = year;
      this.month = month;
      this.day = day;
      this.hour = hour;
      this.minute = minute;
      this.second = second;
      this.millisecond = millisecond;
      this.kind = kind;
    }

    /// <summary> Convert to DateTime. </summary>
    public static implicit operator DateTime(SerializableDateTime self) =>
      new(self.year, self.month, self.day, self.hour, self.minute, self.second, self.millisecond, self.kind);

    /// <summary> Convert from DateTime. </summary>
    public static implicit operator SerializableDateTime(DateTime dateTime) => new(dateTime);
  }
}
