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
using NUnit.Framework;
using UnityEngine;
using FronkonGames.GameWork.Foundation;

namespace FronkonGames.GameWork.Foundation.Tests
{
  /// <summary> RectTransform extensions new methods tests. </summary>
  [TestFixture]
  public class RectTransformExtensionsNewTests
  {
    private const float Tolerance = 0.01f;

    private GameObject canvasObject;
    private GameObject gameObject;
    private RectTransform rectTransform;

    [SetUp]
    public void SetUp()
    {
      canvasObject = new GameObject("TestCanvas");
      canvasObject.AddComponent<Canvas>();

      gameObject = new GameObject("TestRectTransform");
      gameObject.transform.SetParent(canvasObject.transform);
      rectTransform = gameObject.AddComponent<RectTransform>();
      rectTransform.anchorMin = Vector2.zero;
      rectTransform.anchorMax = Vector2.zero;
      rectTransform.sizeDelta = new Vector2(100.0f, 100.0f);
      rectTransform.anchoredPosition = Vector2.zero;
    }

    [TearDown]
    public void TearDown()
    {
      Object.DestroyImmediate(gameObject);
      Object.DestroyImmediate(canvasObject);
    }

    /// <summary> SetLeftMargin sets correct value. </summary>
    [Test]
    public void SetLeftMargin_SetsCorrectValue()
    {
      rectTransform.SetLeftMargin(25.0f);

      Assert.AreEqual(25.0f, rectTransform.offsetMin.x, Tolerance);
    }

    /// <summary> SetRightMargin sets correct value. </summary>
    [Test]
    public void SetRightMargin_SetsCorrectValue()
    {
      rectTransform.SetRightMargin(30.0f);

      Assert.AreEqual(-30.0f, rectTransform.offsetMax.x, Tolerance);
    }

    /// <summary> SetTopMargin sets correct value. </summary>
    [Test]
    public void SetTopMargin_SetsCorrectValue()
    {
      rectTransform.SetTopMargin(20.0f);

      Assert.AreEqual(-20.0f, rectTransform.offsetMax.y, Tolerance);
    }

    /// <summary> SetBottomMargin sets correct value. </summary>
    [Test]
    public void SetBottomMargin_SetsCorrectValue()
    {
      rectTransform.SetBottomMargin(15.0f);

      Assert.AreEqual(15.0f, rectTransform.offsetMin.y, Tolerance);
    }

    /// <summary> SetAllMargins sets all margins. </summary>
    [Test]
    public void SetAllMargins_SetsAllMargins()
    {
      rectTransform.SetAllMargins(10.0f, 20.0f, 30.0f, 40.0f);

      Assert.AreEqual(10.0f, rectTransform.offsetMin.x, Tolerance);
      Assert.AreEqual(40.0f, rectTransform.offsetMin.y, Tolerance);
      Assert.AreEqual(-20.0f, rectTransform.offsetMax.x, Tolerance);
      Assert.AreEqual(-30.0f, rectTransform.offsetMax.y, Tolerance);
    }

    /// <summary> SetWidth sets correct width. </summary>
    [Test]
    public void SetWidth_SetsCorrectWidth()
    {
      rectTransform.SetWidth(250.0f);

      Assert.AreEqual(250.0f, rectTransform.rect.width, Tolerance);
    }

    /// <summary> SetHeight sets correct height. </summary>
    [Test]
    public void SetHeight_SetsCorrectHeight()
    {
      rectTransform.SetHeight(350.0f);

      Assert.AreEqual(350.0f, rectTransform.rect.height, Tolerance);
    }

    /// <summary> SetSize sets both dimensions. </summary>
    [Test]
    public void SetSize_SetsBothDimensions()
    {
      rectTransform.SetSize(200.0f, 300.0f);

      Assert.AreEqual(200.0f, rectTransform.rect.width, Tolerance);
      Assert.AreEqual(300.0f, rectTransform.rect.height, Tolerance);
    }

    /// <summary> GetLeftMargin returns correct value. </summary>
    [Test]
    public void GetLeftMargin_ReturnsCorrectValue()
    {
      rectTransform.offsetMin = new Vector2(25.0f, rectTransform.offsetMin.y);

      Assert.AreEqual(25.0f, rectTransform.GetLeftMargin(), Tolerance);
    }

    /// <summary> GetRightMargin returns correct value. </summary>
    [Test]
    public void GetRightMargin_ReturnsCorrectValue()
    {
      rectTransform.offsetMax = new Vector2(-30.0f, rectTransform.offsetMax.y);

      Assert.AreEqual(30.0f, rectTransform.GetRightMargin(), Tolerance);
    }

    /// <summary> GetTopMargin returns correct value. </summary>
    [Test]
    public void GetTopMargin_ReturnsCorrectValue()
    {
      rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -20.0f);

      Assert.AreEqual(20.0f, rectTransform.GetTopMargin(), Tolerance);
    }

    /// <summary> GetBottomMargin returns correct value. </summary>
    [Test]
    public void GetBottomMargin_ReturnsCorrectValue()
    {
      rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 15.0f);

      Assert.AreEqual(15.0f, rectTransform.GetBottomMargin(), Tolerance);
    }

    /// <summary> GetWidth returns correct width. </summary>
    [Test]
    public void GetWidth_ReturnsCorrectWidth()
    {
      rectTransform.SetWidth(500.0f);

      Assert.AreEqual(500.0f, rectTransform.GetWidth(), Tolerance);
    }

    /// <summary> GetHeight returns correct height. </summary>
    [Test]
    public void GetHeight_ReturnsCorrectHeight()
    {
      rectTransform.SetHeight(600.0f);

      Assert.AreEqual(600.0f, rectTransform.GetHeight(), Tolerance);
    }

    [Test]
    public void ToBounds_ReturnsNonEmptyBounds()
    {
      Bounds bounds = rectTransform.ToBounds();

      Assert.Greater(bounds.size.x, 0.0f);
      Assert.Greater(bounds.size.y, 0.0f);
    }

    [Test]
    public void ToWorldBounds_CenterMatchesPosition()
    {
      rectTransform.position = new Vector3(10.0f, 20.0f, 0.0f);

      Bounds bounds = rectTransform.ToWorldBounds();

      Assert.AreEqual(rectTransform.position, bounds.center);
    }
  }
}
