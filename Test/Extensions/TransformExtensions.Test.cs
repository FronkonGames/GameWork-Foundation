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
using UnityEngine;
using UnityEngine.TestTools;
using FronkonGames.GameWork.Foundation;

/// <summary> Extensions test. </summary>
public partial class ExtensionsTests
{
  /// <summary> Transform extensions test. </summary>
  [UnityTest]
  public IEnumerator TransformExtensions()
  {
    GameObject go = new("TestTransform");
    Transform t = go.transform;

    Assert.AreEqual("TestTransform", t.GetPath());

    GameObject parent = new("Parent");
    go.transform.SetParent(parent.transform);
    Assert.AreEqual("Parent/TestTransform", t.GetPath());

    t.SetX(5.0f);
    Assert.AreEqual(5.0f, t.position.x);
    t.SetY(6.0f);
    Assert.AreEqual(6.0f, t.position.y);
    t.SetZ(7.0f);
    Assert.AreEqual(7.0f, t.position.z);

    t.SetXYZ(1.0f, 2.0f, 3.0f);
    Assert.AreEqual(new Vector3(1.0f, 2.0f, 3.0f), t.position);

    t.TranslateX(1.0f);
    Assert.AreEqual(2.0f, t.position.x);
    t.TranslateY(1.0f);
    Assert.AreEqual(3.0f, t.position.y);
    t.TranslateZ(1.0f);
    Assert.AreEqual(4.0f, t.position.z);

    t.SetLocalX(10.0f);
    Assert.AreEqual(10.0f, t.localPosition.x);
    t.SetLocalY(11.0f);
    Assert.AreEqual(11.0f, t.localPosition.y);
    t.SetLocalZ(12.0f);
    Assert.AreEqual(12.0f, t.localPosition.z);

    t.ScaleByXYZ(2.0f);
    Assert.AreEqual(new Vector3(2.0f, 2.0f, 2.0f), t.localScale);

    t.ResetScale();
    Assert.AreEqual(UnityEngine.Vector3.one, t.localScale);

    t.FlipX();
    Assert.AreEqual(-1.0f, t.localScale.x);
    t.FlipPositive();
    Assert.AreEqual(1.0f, t.localScale.x);

    t.ResetWorld();
    Assert.AreEqual(UnityEngine.Vector3.zero, t.position);
    Assert.AreEqual(UnityEngine.Vector3.one, t.localScale);

    t.ResetLocal();
    Assert.AreEqual(UnityEngine.Vector3.zero, t.localPosition);

    go.SafeDestroy();
    parent.SafeDestroy();

    yield return null;
  }
}
