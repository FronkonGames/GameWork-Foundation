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
using FronkonGames.GameWork.Foundation;
using UnityEngine;

/// <summary> Unity utils tests. </summary>
public class UnityTests
{
  private const string BoolKey = "PlayerPrefsUtilsTest.Bool";
  private const string IntKey = "PlayerPrefsUtilsTest.Int";
  private const string FloatKey = "PlayerPrefsUtilsTest.Float";
  private const string StringKey = "PlayerPrefsUtilsTest.String";
  private const string Vector2Key = "PlayerPrefsUtilsTest.Vector2";
  private const string Vector3Key = "PlayerPrefsUtilsTest.Vector3";
  private const string Vector4Key = "PlayerPrefsUtilsTest.Vector4";
  private const string DateTimeKey = "PlayerPrefsUtilsTest.DateTime";
  private const string QuaternionKey = "PlayerPrefsUtilsTest.Quaternion";

  /// <summary> PlayerPrefsUtils test. </summary>
  [UnityTest]
  public IEnumerator PlayerPrefs()
  {
    RemoveAllTestKeys();

    Assert.IsFalse(PlayerPrefsUtils.HasKey(BoolKey));
    Assert.IsFalse(PlayerPrefsUtils.HasKey(IntKey));
    Assert.IsFalse(PlayerPrefsUtils.HasKey(FloatKey));
    Assert.IsFalse(PlayerPrefsUtils.HasKey(StringKey));
    Assert.IsFalse(PlayerPrefsUtils.HasKey(Vector2Key));
    Assert.IsFalse(PlayerPrefsUtils.HasKey(Vector3Key));
    Assert.IsFalse(PlayerPrefsUtils.HasKey(Vector4Key));
    Assert.IsFalse(PlayerPrefsUtils.HasKey(DateTimeKey));
    Assert.IsFalse(PlayerPrefsUtils.HasKey(QuaternionKey));
    
    PlayerPrefsUtils.SetBool(BoolKey, true);
    Assert.IsTrue(PlayerPrefsUtils.GetBool(BoolKey));

    PlayerPrefsUtils.SetInt(IntKey, 42);
    Assert.AreEqual(PlayerPrefsUtils.GetInt(IntKey), 42);

    PlayerPrefsUtils.SetFloat(FloatKey, MathConstants.Pi);
    Assert.AreEqual(PlayerPrefsUtils.GetFloat(FloatKey), MathConstants.Pi);

    PlayerPrefsUtils.SetString(StringKey, "All your base are belong to us");
    Assert.AreEqual(PlayerPrefsUtils.GetString(StringKey), "All your base are belong to us");

    PlayerPrefsUtils.SetVector2(Vector2Key, Vector2.up);
    Assert.AreEqual(PlayerPrefsUtils.GetVector2(Vector2Key), Vector2.up);
    
    PlayerPrefsUtils.SetVector3(Vector3Key, Vector3.down);
    Assert.AreEqual(PlayerPrefsUtils.GetVector3(Vector3Key), Vector3.down);

    PlayerPrefsUtils.SetVector4(Vector4Key, Vector4.one);
    Assert.AreEqual(PlayerPrefsUtils.GetVector4(Vector4Key), Vector4.one);

    DateTime dateTime = DateTime.Today;
    PlayerPrefsUtils.SetDateTime(DateTimeKey, dateTime);
    Assert.AreEqual(PlayerPrefsUtils.GetDateTime(DateTimeKey), dateTime);

    PlayerPrefsUtils.SetQuaternion(QuaternionKey, Quaternion.identity);
    Assert.AreEqual(PlayerPrefsUtils.GetQuaternion(QuaternionKey), Quaternion.identity);
    
    RemoveAllTestKeys();
    
    yield return null;
  }

  /// <summary> CommandLineUtils test. </summary>
  [UnityTest]
  public IEnumerator CommandLine()
  {
    Assert.AreEqual(CommandLineUtils.GetArguments().Length, 7);
    Assert.IsTrue(CommandLineUtils.HasArgument("noparams"));
    Assert.IsFalse(CommandLineUtils.HasArgument("unknown"));
    Assert.AreEqual(CommandLineUtils.GetValue("string"), "text");
    Assert.AreEqual(CommandLineUtils.GetValue("unknown", "default"), "default");

    yield return null;
  }

  private static void RemoveAllTestKeys()
  {
    PlayerPrefsUtils.DeleteKey(BoolKey);
    PlayerPrefsUtils.DeleteKey(IntKey);
    PlayerPrefsUtils.DeleteKey(FloatKey);
    PlayerPrefsUtils.DeleteKey(StringKey);
    PlayerPrefsUtils.DeleteKey(Vector2Key);
    PlayerPrefsUtils.DeleteKey(Vector3Key);
    PlayerPrefsUtils.DeleteKey(Vector4Key);
    PlayerPrefsUtils.DeleteKey(DateTimeKey);
    PlayerPrefsUtils.DeleteKey(QuaternionKey);
  }
}
