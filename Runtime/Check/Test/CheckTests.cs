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
using UnityEngine;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary>
/// Check test.
/// </summary>
public class CheckTests
{
  /// <summary>
  /// Check boolean test.
  /// </summary>
  [UnityTest]
  public IEnumerator Boolean()
  {
    Assert.DoesNotThrow(() => Check.True(true));
    Assert.Throws<Exception>(() => Check.True(false));

    Assert.DoesNotThrow(() => Check.False(false));
    Assert.Throws<Exception>(() => Check.False(true));

    yield return null;
  }

  /// <summary>
  /// Check type test.
  /// </summary>
  [UnityTest]
  public IEnumerator Type()
  {
    object objectNotNull = new object();
    object objectNull = null;

    Assert.DoesNotThrow(() => Check.IsNull(objectNull));
    Assert.Throws<Exception>(() => Check.IsNull(objectNotNull));

    Assert.DoesNotThrow(() => Check.IsNotNull(objectNotNull));
    Assert.Throws<Exception>(() => Check.IsNotNull(objectNull));

    List<int> listNull = null;
    List<int> listIntEmpty = new List<int>();
    List<int> listInt = new List<int> { 1, 2, 3, 4 };

    Assert.Throws<Exception>(() => Check.IsNotNullOrEmpty(listNull));
    Assert.Throws<Exception>(() => Check.IsNotNullOrEmpty(listIntEmpty));
    Assert.DoesNotThrow(() => Check.IsNotNullOrEmpty(listInt));

    Assert.Throws<Exception>(() => Check.OfType(listInt, typeof(int)));
    Assert.DoesNotThrow(() => Check.OfType(listInt, typeof(List<int>)));

    yield return null;
  }

  /// <summary>
  /// Check string test.
  /// </summary>
  [UnityTest]
  public IEnumerator String()
  {
    const string stringValid = "All your base are belong to us";
    const string stringNull = null;

    Assert.DoesNotThrow(() => Check.IsNotNullOrEmpty(stringValid));
    Assert.Throws<Exception>(() => Check.IsNotNullOrEmpty(string.Empty));
    Assert.Throws<Exception>(() => Check.IsNotNullOrEmpty(stringNull));

    Assert.DoesNotThrow(() => Check.Length(stringValid, stringValid.Length));
    Assert.Throws<Exception>(() => Check.Length(stringValid, stringValid.Length * 2));

    Assert.DoesNotThrow(() => Check.MaxLength(stringValid, stringValid.Length * 2));
    Assert.Throws<Exception>(() => Check.MaxLength(stringValid, stringValid.Length / 2));

    Assert.DoesNotThrow(() => Check.MinLength(stringValid, stringValid.Length / 2));
    Assert.Throws<Exception>(() => Check.MinLength(stringValid, stringValid.Length * 2));

    yield return null;
  }

  /// <summary>
  /// Check numeric test.
  /// </summary>
  [UnityTest]
  public IEnumerator Numeric()
  {
    // Int.
    Assert.DoesNotThrow(() => Check.Equal(1, 1));
    Assert.Throws<Exception>(() => Check.Equal(1, 0));
    Assert.DoesNotThrow(() => Check.NotEqual(1, 0));
    Assert.Throws<Exception>(() => Check.NotEqual(1, 1));

    Assert.DoesNotThrow(() => Check.Less(0, 1));
    Assert.Throws<Exception>(() => Check.Less(1, 0));
    Assert.DoesNotThrow(() => Check.LessOrEqual(0, 1));
    Assert.DoesNotThrow(() => Check.LessOrEqual(1, 1));
    Assert.Throws<Exception>(() => Check.LessOrEqual(1, 0));

    Assert.DoesNotThrow(() => Check.Greater(1, 0));
    Assert.Throws<Exception>(() => Check.Greater(0, 1));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(1, 0));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(1, 1));
    Assert.Throws<Exception>(() => Check.GreaterOrEqual(0, 1));

    Assert.DoesNotThrow(() => Check.Negative(-1));
    Assert.Throws<Exception>(() => Check.Negative(1));
    Assert.Throws<Exception>(() => Check.Negative(0));

    Assert.DoesNotThrow(() => Check.NotNegative(1));
    Assert.DoesNotThrow(() => Check.NotNegative(0));
    Assert.Throws<Exception>(() => Check.NotNegative(-1));

    Assert.DoesNotThrow(() => Check.IsWithin(0, -1, 1));
    Assert.Throws<Exception>(() => Check.IsWithin(2, -1, 1));
    Assert.Throws<Exception>(() => Check.IsWithin(-2, -1, 1));

    // Float.
    Assert.DoesNotThrow(() => Check.Equal(1.0f, 1.0f));
    Assert.Throws<Exception>(() => Check.Equal(1.0f, 0.0f));
    Assert.DoesNotThrow(() => Check.NotEqual(1.0f, 0.0f));
    Assert.Throws<Exception>(() => Check.NotEqual(1.0f, 1.0f));

    Assert.DoesNotThrow(() => Check.Less(0.0f, 1.0f));
    Assert.Throws<Exception>(() => Check.Less(1.0f, 0.0f));
    Assert.DoesNotThrow(() => Check.LessOrEqual(0.0f, 1.0f));
    Assert.DoesNotThrow(() => Check.LessOrEqual(1.0f, 1.0f));
    Assert.Throws<Exception>(() => Check.LessOrEqual(1.0f, 0.0f));

    Assert.DoesNotThrow(() => Check.Greater(1.0f, 0.0f));
    Assert.Throws<Exception>(() => Check.Greater(0.0f, 1.0f));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(1.0f, 0.0f));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(1.0f, 1.0f));
    Assert.Throws<Exception>(() => Check.GreaterOrEqual(0.0f, 1.0f));

    Assert.DoesNotThrow(() => Check.Negative(-1.0f));
    Assert.Throws<Exception>(() => Check.Negative(1.0f));
    Assert.Throws<Exception>(() => Check.Negative(0.0f));

    Assert.DoesNotThrow(() => Check.NotNegative(1.0f));
    Assert.DoesNotThrow(() => Check.NotNegative(0.0f));
    Assert.Throws<Exception>(() => Check.NotNegative(-1.0f));

    Assert.DoesNotThrow(() => Check.IsWithin(0.0f, -1.0f, 1.0f));
    Assert.Throws<Exception>(() => Check.IsWithin(2.0f, -1.0f, 1.0f));
    Assert.Throws<Exception>(() => Check.IsWithin(-2.0f, -1.0f, 1.0f));

    // Double.
    Assert.DoesNotThrow(() => Check.Equal(1.0d, 1.0d));
    Assert.Throws<Exception>(() => Check.Equal(1.0d, 0.0d));
    Assert.DoesNotThrow(() => Check.NotEqual(1.0d, 0.0d));
    Assert.Throws<Exception>(() => Check.NotEqual(1.0d, 1.0d));

    Assert.DoesNotThrow(() => Check.Less(0.0d, 1.0d));
    Assert.Throws<Exception>(() => Check.Less(1.0d, 0.0d));
    Assert.DoesNotThrow(() => Check.LessOrEqual(0.0d, 1.0d));
    Assert.DoesNotThrow(() => Check.LessOrEqual(1.0d, 1.0d));
    Assert.Throws<Exception>(() => Check.LessOrEqual(1.0d, 0.0d));

    Assert.DoesNotThrow(() => Check.Greater(1.0d, 0.0d));
    Assert.Throws<Exception>(() => Check.Greater(0.0d, 1.0d));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(1.0d, 0.0d));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(1.0d, 1.0d));
    Assert.Throws<Exception>(() => Check.GreaterOrEqual(0.0d, 1.0d));

    Assert.DoesNotThrow(() => Check.Negative(-1.0d));
    Assert.Throws<Exception>(() => Check.Negative(1.0d));
    Assert.Throws<Exception>(() => Check.Negative(0.0d));

    Assert.DoesNotThrow(() => Check.NotNegative(1.0d));
    Assert.DoesNotThrow(() => Check.NotNegative(0.0d));
    Assert.Throws<Exception>(() => Check.NotNegative(-1.0d));

    // Vector2.
    Assert.DoesNotThrow(() => Check.Equal(Vector2.one, Vector2.one));
    Assert.Throws<Exception>(() => Check.Equal(Vector2.one, Vector2.zero));
    Assert.DoesNotThrow(() => Check.NotEqual(Vector2.one, Vector2.zero));
    Assert.Throws<Exception>(() => Check.NotEqual(Vector2.one, Vector2.one));

    Assert.DoesNotThrow(() => Check.Equal(Vector2.one, Vector2.one.magnitude));
    Assert.Throws<Exception>(() => Check.Equal(Vector2.one, 0.0f));
    Assert.DoesNotThrow(() => Check.NotEqual(Vector2.one, 0.0f));
    Assert.Throws<Exception>(() => Check.NotEqual(Vector2.one, Vector2.one.magnitude));

    Assert.DoesNotThrow(() => Check.Less(Vector2.zero, Vector2.one));
    Assert.Throws<Exception>(() => Check.Less(Vector2.one, Vector2.zero));
    Assert.DoesNotThrow(() => Check.LessOrEqual(Vector2.zero, Vector2.one));
    Assert.DoesNotThrow(() => Check.LessOrEqual(Vector2.one, Vector2.one));
    Assert.Throws<Exception>(() => Check.LessOrEqual(Vector2.one, Vector2.zero));

    Assert.DoesNotThrow(() => Check.Less(Vector2.zero, Vector2.one.magnitude));
    Assert.Throws<Exception>(() => Check.Less(Vector2.one, 0.0f));
    Assert.DoesNotThrow(() => Check.LessOrEqual(Vector2.zero, Vector2.one.magnitude));
    Assert.DoesNotThrow(() => Check.LessOrEqual(Vector2.one, Vector2.one.magnitude));
    Assert.Throws<Exception>(() => Check.LessOrEqual(Vector2.one, 0.0f));

    Assert.DoesNotThrow(() => Check.Greater(Vector2.one, Vector2.zero));
    Assert.Throws<Exception>(() => Check.Greater(Vector2.zero, Vector2.one));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(Vector2.one, Vector2.zero));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(Vector2.one, Vector2.one));
    Assert.Throws<Exception>(() => Check.GreaterOrEqual(Vector2.zero, Vector2.one));

    Assert.DoesNotThrow(() => Check.Greater(Vector2.one, 0.0f));
    Assert.Throws<Exception>(() => Check.Greater(Vector2.zero, Vector2.one.magnitude));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(Vector2.one, 0.0f));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(Vector2.one, Vector2.one.magnitude));
    Assert.Throws<Exception>(() => Check.GreaterOrEqual(Vector2.zero, Vector2.one.magnitude));

    // Vector3.
    Assert.DoesNotThrow(() => Check.Equal(Vector3.one, Vector3.one));
    Assert.Throws<Exception>(() => Check.Equal(Vector3.one, Vector3.zero));
    Assert.DoesNotThrow(() => Check.NotEqual(Vector3.one, Vector3.zero));
    Assert.Throws<Exception>(() => Check.NotEqual(Vector3.one, Vector3.one));

    Assert.DoesNotThrow(() => Check.Equal(Vector3.one, Vector3.one.magnitude));
    Assert.Throws<Exception>(() => Check.Equal(Vector3.one, 0.0f));
    Assert.DoesNotThrow(() => Check.NotEqual(Vector3.one, 0.0f));
    Assert.Throws<Exception>(() => Check.NotEqual(Vector3.one, Vector3.one.magnitude));

    Assert.DoesNotThrow(() => Check.Less(Vector3.zero, Vector3.one.magnitude));
    Assert.Throws<Exception>(() => Check.Less(Vector3.one, 0.0f));
    Assert.DoesNotThrow(() => Check.LessOrEqual(Vector3.zero, Vector3.one.magnitude));
    Assert.DoesNotThrow(() => Check.LessOrEqual(Vector3.one, Vector3.one.magnitude));
    Assert.Throws<Exception>(() => Check.LessOrEqual(Vector3.one, 0.0f));

    Assert.DoesNotThrow(() => Check.Greater(Vector3.one, Vector3.zero));
    Assert.Throws<Exception>(() => Check.Greater(Vector3.zero, Vector3.one));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(Vector3.one, Vector3.zero));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(Vector3.one, Vector3.one));
    Assert.Throws<Exception>(() => Check.GreaterOrEqual(Vector3.zero, Vector3.one));

    Assert.DoesNotThrow(() => Check.Greater(Vector3.one, 0.0f));
    Assert.Throws<Exception>(() => Check.Greater(Vector3.zero, Vector3.one.magnitude));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(Vector3.one, 0.0f));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(Vector3.one, Vector3.one.magnitude));
    Assert.Throws<Exception>(() => Check.GreaterOrEqual(Vector3.zero, Vector3.one.magnitude));

    // Vector4.
    Assert.DoesNotThrow(() => Check.Equal(Vector4.one, Vector4.one));
    Assert.Throws<Exception>(() => Check.Equal(Vector4.one, Vector4.zero));
    Assert.DoesNotThrow(() => Check.NotEqual(Vector4.one, Vector4.zero));
    Assert.Throws<Exception>(() => Check.NotEqual(Vector4.one, Vector4.one));

    Assert.DoesNotThrow(() => Check.Equal(Vector4.one, Vector4.one.magnitude));
    Assert.Throws<Exception>(() => Check.Equal(Vector4.one, 0.0f));
    Assert.DoesNotThrow(() => Check.NotEqual(Vector4.one, 0.0f));
    Assert.Throws<Exception>(() => Check.NotEqual(Vector4.one, Vector4.one.magnitude));

    Assert.DoesNotThrow(() => Check.Less(Vector4.zero, Vector4.one));
    Assert.Throws<Exception>(() => Check.Less(Vector4.one, Vector4.zero));
    Assert.DoesNotThrow(() => Check.LessOrEqual(Vector4.zero, Vector4.one));
    Assert.DoesNotThrow(() => Check.LessOrEqual(Vector4.one, Vector4.one));
    Assert.Throws<Exception>(() => Check.LessOrEqual(Vector4.one, Vector4.zero));

    Assert.DoesNotThrow(() => Check.Less(Vector4.zero, Vector4.one.magnitude));
    Assert.Throws<Exception>(() => Check.Less(Vector4.one, 0.0f));
    Assert.DoesNotThrow(() => Check.LessOrEqual(Vector4.zero, Vector4.one.magnitude));
    Assert.DoesNotThrow(() => Check.LessOrEqual(Vector4.one, Vector4.one.magnitude));
    Assert.Throws<Exception>(() => Check.LessOrEqual(Vector4.one, 0.0f));

    Assert.DoesNotThrow(() => Check.Greater(Vector4.one, Vector4.zero));
    Assert.Throws<Exception>(() => Check.Greater(Vector4.zero, Vector4.one));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(Vector4.one, Vector4.zero));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(Vector4.one, Vector4.one));
    Assert.Throws<Exception>(() => Check.GreaterOrEqual(Vector4.zero, Vector4.one));

    Assert.DoesNotThrow(() => Check.Greater(Vector4.one, 0.0f));
    Assert.Throws<Exception>(() => Check.Greater(Vector4.zero, Vector4.one.magnitude));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(Vector4.one, 0.0f));
    Assert.DoesNotThrow(() => Check.GreaterOrEqual(Vector4.one, Vector4.one.magnitude));
    Assert.Throws<Exception>(() => Check.GreaterOrEqual(Vector4.zero, Vector4.one.magnitude));

    yield return null;
  }
}
