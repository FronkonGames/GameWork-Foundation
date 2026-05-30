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
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> Serialization tests. </summary>
public class SerializationTests
{
  /// <summary> SerializableDateTime test. </summary>
  [UnityTest]
  public IEnumerator SerializableDateTimeTest()
  {
    DateTime now = new(2024, 6, 15, 14, 30, 45, 500, DateTimeKind.Local);
    SerializableDateTime serializable = new(now);

    Assert.AreEqual(2024, serializable.Year);
    Assert.AreEqual(6, serializable.Month);
    Assert.AreEqual(15, serializable.Day);
    Assert.AreEqual(14, serializable.Hour);
    Assert.AreEqual(30, serializable.Minute);
    Assert.AreEqual(45, serializable.Second);
    Assert.AreEqual(500, serializable.Millisecond);
    Assert.AreEqual(DateTimeKind.Local, serializable.Kind);

    DateTime converted = serializable;
    Assert.AreEqual(now, converted);

    SerializableDateTime fromComponents = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    Assert.AreEqual(2024, fromComponents.Year);
    Assert.AreEqual(1, fromComponents.Month);

    DateTime implicitConvert = new(2023, 12, 25, 10, 0, 0, DateTimeKind.Utc);
    SerializableDateTime implicitSerializable = implicitConvert;
    Assert.AreEqual(2023, implicitSerializable.Year);
    Assert.AreEqual(12, implicitSerializable.Month);
    Assert.AreEqual(25, implicitSerializable.Day);

    yield return null;
  }

  /// <summary> SerializableKeyValuePair test. </summary>
  [UnityTest]
  public IEnumerator SerializableKeyValuePairTest()
  {
    SerializableKeyValuePair<string, int> pair = new("key", 42);
    Assert.AreEqual("key", pair.Key);
    Assert.AreEqual(42, pair.Value);

    pair.Value = 100;
    Assert.AreEqual(100, pair.Value);

    SerializableKeyValuePair<string, int> defaultPair = new();
    Assert.IsNull(defaultPair.Key);
    Assert.AreEqual(0, defaultPair.Value);

    KeyValuePair<string, int> standard = new("test", 99);
    SerializableKeyValuePair<string, int> fromStandard = standard;
    Assert.AreEqual("test", fromStandard.Key);
    Assert.AreEqual(99, fromStandard.Value);

    KeyValuePair<string, int> toStandard = fromStandard;
    Assert.AreEqual("test", toStandard.Key);
    Assert.AreEqual(99, toStandard.Value);

    SerializableKeyValuePair<string, int> equal = new("key", 100);
    Assert.IsTrue(pair.Equals(equal));

    SerializableKeyValuePair<string, int> notEqual = new("other", 100);
    Assert.IsFalse(pair.Equals(notEqual));

    Assert.IsFalse(pair.Equals(null));

    yield return null;
  }

  /// <summary> SerializableDictionary test. </summary>
  [UnityTest]
  public IEnumerator SerializableDictionaryTest()
  {
    SerializableDictionary<string, int> dict = new();
    dict.Add("one", 1);
    dict.Add("two", 2);
    dict.Add("three", 3);

    Assert.AreEqual(3, dict.Count);
    Assert.AreEqual(1, dict["one"]);
    Assert.AreEqual(2, dict["two"]);
    Assert.AreEqual(3, dict["three"]);

    dict.OnBeforeSerialize();
    dict.OnAfterDeserialize();

    Assert.AreEqual(3, dict.Count);
    Assert.AreEqual(1, dict["one"]);

    Dictionary<string, int> standard = new() { { "a", 1 }, { "b", 2 } };
    SerializableDictionary<string, int> fromStandard = SerializableDictionary<string, int>.FromDictionary(standard);
    Assert.AreEqual(2, fromStandard.Count);
    Assert.AreEqual(1, fromStandard["a"]);

    yield return null;
  }

  /// <summary> SerializableTime test. </summary>
  [UnityTest]
  public IEnumerator SerializableTimeTest()
  {
    SerializableTime time = SerializableTime.FromSeconds(3661.5);
    Assert.AreEqual(1, time.Hours);
    Assert.AreEqual(1, time.Minutes);
    Assert.AreEqual(1, time.Seconds);

    SerializableTime fromSeconds = SerializableTime.FromSeconds(0);
    Assert.AreEqual(0, fromSeconds.Days);
    Assert.AreEqual(0, fromSeconds.Hours);

    SerializableTime fromTimeSpan = SerializableTime.FromTimeSpan(new TimeSpan(1, 2, 3, 4, 500));
    Assert.AreEqual(1, fromTimeSpan.Days);
    Assert.AreEqual(2, fromTimeSpan.Hours);
    Assert.AreEqual(3, fromTimeSpan.Minutes);
    Assert.AreEqual(4, fromTimeSpan.Seconds);
    Assert.AreEqual(500, fromTimeSpan.Milliseconds);

    TimeSpan span = fromTimeSpan;
    Assert.AreEqual(1, span.Days);
    Assert.AreEqual(2, span.Hours);

    SerializableTime fromTicks = SerializableTime.FromTicks(TimeSpan.TicksPerHour * 5);
    Assert.AreEqual(5, fromTicks.TotalHours);

    yield return null;
  }
}
