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
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using FronkonGames.GameWork.Foundation;

/// <summary>
/// Extensions test.
/// </summary>
public partial class ExtensionsTests
{
  /// <summary>
  /// Int extensions test.
  /// </summary>
  [UnityTest]
  public IEnumerator Int()
  {
    Assert.AreEqual(0.Sign(), 1);
    Assert.AreEqual(1.Sign(), 1);
    Assert.AreEqual(-1.Sign(), -1);
    
    Assert.AreEqual(0.Max(1), 1);
    Assert.AreEqual(1.Max(0), 1);
    Assert.AreEqual(1.Max(-1), 1);
    Assert.AreEqual(0.Min(1), 0);
    Assert.AreEqual(1.Min(0), 0);
    Assert.AreEqual(1.Min(-1), -1);

    Assert.AreEqual(0.Abs(), 0);
    Assert.AreEqual(1.Abs(), 1);
    Assert.AreEqual((-1).Abs(), 1);

    Assert.AreEqual(0.Snap(5), 0);
    Assert.AreEqual(4.Snap(5), 5);
    Assert.AreEqual(8.Snap(5), 10);
    
    Assert.AreEqual(1.Clamp(0, 2), 1);
    Assert.AreEqual((-1).Clamp(0, 2), 0);
    Assert.AreEqual(3.Clamp(0, 2), 2);

    Assert.AreEqual(0.Pow(1), 0);
    Assert.AreEqual(1.Pow(0), 1);
    Assert.AreEqual(2.Pow(1), 2);
    Assert.AreEqual(2.Pow(2), 4);

    Assert.IsTrue(0.IsBetween(-1, 1));
    Assert.IsTrue(1.IsBetween(-1, 1));
    Assert.IsTrue((-1).IsBetween(-1, 1));
    Assert.IsFalse(2.IsBetween(-1, 1));
    Assert.IsFalse((-2).IsBetween(-1, 1));

    Assert.IsTrue(0.IsBetweenExclusive(-1, 1));
    Assert.IsFalse(1.IsBetweenExclusive(-1, 1));
    Assert.IsFalse((-1).IsBetweenExclusive(-1, 1));
    Assert.IsFalse(2.IsBetweenExclusive(-1, 1));
    Assert.IsFalse((-2).IsBetweenExclusive(-1, 1));

    Assert.IsTrue(0.IsEven());
    Assert.IsFalse(1.IsEven());
    Assert.IsTrue(2.IsEven());
    Assert.IsFalse(0.IsOdd());
    Assert.IsTrue(1.IsOdd());
    Assert.IsFalse(2.IsOdd());

    Assert.AreEqual(0.NextPowerOfTwo(), 0);
    Assert.AreEqual(1.NextPowerOfTwo(), 1);
    Assert.AreEqual(2.NextPowerOfTwo(), 2);
    Assert.AreEqual(3.NextPowerOfTwo(), 4);
    Assert.AreEqual(4.NextPowerOfTwo(), 4);

    int maskTest = LayerMask.NameToLayer("Test");
    Assert.IsTrue(maskTest.IsInLayerMask(Physics.AllLayers));

    Assert.AreEqual(16.BytesToHumanReadable(), "16 bytes");
    Assert.AreEqual(1024.BytesToHumanReadable(), "1 KB");
    Assert.AreEqual((1024 * 1024).BytesToHumanReadable(), "1 MB");

    Assert.AreEqual(60.SecondsToHumanReadable(), "00:01:00");
    Assert.AreEqual(3600.SecondsToHumanReadable(), "01:00:00");

    Assert.AreEqual(1.NumDigits(), 1);
    Assert.AreEqual((-1).NumDigits(), 1);
    Assert.AreEqual(10.NumDigits(), 2);
    Assert.AreEqual(100.NumDigits(), 3);

    Assert.AreEqual(15, 13.SnapToCeil(5));
    Assert.AreEqual(10, 13.SnapToFloor(5));
    Assert.AreEqual(50.0f, 5.Remap(0.0f, 10.0f, 0.0f, 100.0f), 0.001f);
    Assert.AreEqual(0.5f, 5.Remap01(0.0f, 10.0f), 0.001f);
    Assert.AreEqual(50, 5.RemapInt(0.0f, 10.0f, 0.0f, 100.0f));
    Assert.IsTrue(5.IsAny(1, 5, 9));
    Assert.IsFalse(5.IsAny(1, 2, 3));
    Assert.AreEqual(1.5f, 1500.ToSeconds(), 0.001f);
    Assert.AreEqual("0 bytes", 0.SizeSuffix());
    StringAssert.Contains("K", 1500.CurrencySuffix());

    Assert.AreEqual("0 bytes", 0L.SizeSuffix());
    StringAssert.Contains("KB", 2048L.SizeSuffix());
    Assert.AreEqual("999", 999L.CurrencySuffix());

    yield return null;
  }

  /// <summary> DateTime extensions test. </summary>
  [UnityTest]
  public IEnumerator DateTimeExtensions()
  {
    DateTime epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    DateTime dateTime = epoch.AddSeconds(100.0);

    Assert.AreEqual(100, dateTime.ToUnixEpoch());

    yield return null;
  }
}
