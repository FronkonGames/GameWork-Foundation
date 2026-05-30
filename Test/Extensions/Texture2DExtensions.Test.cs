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

/// <summary> Texture2DExtensions tests. </summary>
public class Texture2DExtensionsTests
{
  private Texture2D texture;

  [SetUp]
  public void SetUp()
  {
    texture = new Texture2D(4, 4, TextureFormat.RGBA32, false);
  }

  [TearDown]
  public void TearDown()
  {
    Object.DestroyImmediate(texture);
  }

  [Test]
  public void Fill_SetsAllPixelsToColor()
  {
    Color fill = Color.red;

    texture.Fill(fill);

    for (int y = 0; y < texture.height; y++)
    {
      for (int x = 0; x < texture.width; x++)
      {
        Assert.AreEqual(fill, texture.GetPixel(x, y));
      }
    }
  }

  [Test]
  public void GetPixelClamped_InBounds_ReturnsExactPixel()
  {
    Color expected = Color.blue;
    texture.SetPixel(2, 2, expected);
    texture.Apply();

    Color result = texture.GetPixelClamped(2, 2);

    Assert.AreEqual(expected, result);
  }

  [Test]
  public void GetPixelClamped_OutOfBounds_ClampsToEdge()
  {
    Color edge = Color.green;
    texture.SetPixel(3, 3, edge);
    texture.Apply();

    Color result = texture.GetPixelClamped(100, 100);

    Assert.AreEqual(edge, result);
  }

  [Test]
  public void GetPixelClamped_NegativeCoords_ClampsToEdge()
  {
    Color edge = Color.yellow;
    texture.SetPixel(0, 0, edge);
    texture.Apply();

    Color result = texture.GetPixelClamped(-5, -5);

    Assert.AreEqual(edge, result);
  }

  [Test]
  public void SetPixelSafe_InBounds_SetsPixel()
  {
    Color expected = Color.cyan;

    texture.SetPixelSafe(1, 1, expected);
    texture.Apply();

    Assert.AreEqual(expected, texture.GetPixel(1, 1));
  }

  [Test]
  public void SetPixelSafe_OutOfBounds_DoesNotThrow()
  {
    Assert.DoesNotThrow(() =>
    {
      texture.SetPixelSafe(100, 100, Color.red);
      texture.Apply();
    });
  }

  [Test]
  public void SetPixelSafe_NegativeCoords_DoesNotThrow()
  {
    Assert.DoesNotThrow(() =>
    {
      texture.SetPixelSafe(-1, -1, Color.red);
      texture.Apply();
    });
  }

  [Test]
  public void Resize_ReturnsCorrectDimensions()
  {
    Texture2D result = texture.Resized(8, 6);

    Assert.AreEqual(8, result.width);
    Assert.AreEqual(6, result.height);

    Object.DestroyImmediate(result);
  }

  [Test]
  public void Resize_PreservesColor()
  {
    Color color = Color.magenta;
    texture.Fill(color);

    Texture2D result = texture.Resized(2, 2);

    for (int y = 0; y < result.height; y++)
    {
      for (int x = 0; x < result.width; x++)
      {
        Assert.AreEqual(color, result.GetPixel(x, y));
      }
    }

    Object.DestroyImmediate(result);
  }

  [Test]
  public void Crop_ReturnsCorrectDimensions()
  {
    Texture2D result = texture.Crop(0, 0, 2, 2);

    Assert.AreEqual(2, result.width);
    Assert.AreEqual(2, result.height);

    Object.DestroyImmediate(result);
  }

  [Test]
  public void Crop_ReturnsCorrectRegion()
  {
    Color expected = Color.red;
    texture.SetPixel(1, 1, expected);
    texture.Apply();

    Texture2D result = texture.Crop(1, 1, 1, 1);

    Assert.AreEqual(expected, result.GetPixel(0, 0));

    Object.DestroyImmediate(result);
  }

  [Test]
  public void FlipHorizontal_ReversesColumns()
  {
    Color left = Color.red;
    Color right = Color.blue;
    texture.SetPixel(0, 0, left);
    texture.SetPixel(3, 0, right);
    texture.Apply();

    texture.FlipHorizontal();

    Assert.AreEqual(right, texture.GetPixel(0, 0));
    Assert.AreEqual(left, texture.GetPixel(3, 0));
  }

  [Test]
  public void FlipVertical_ReversesRows()
  {
    Color bottom = Color.red;
    Color top = Color.blue;
    texture.SetPixel(0, 0, bottom);
    texture.SetPixel(0, 3, top);
    texture.Apply();

    texture.FlipVertical();

    Assert.AreEqual(top, texture.GetPixel(0, 0));
    Assert.AreEqual(bottom, texture.GetPixel(0, 3));
  }

  [Test]
  public void Blurred_ReturnsSameSize()
  {
    Texture2D result = texture.Blurred(1, 1);

    Assert.AreEqual(texture.width, result.width);
    Assert.AreEqual(texture.height, result.height);

    Object.DestroyImmediate(result);
  }

  [Test]
  public void Blurred_DoesNotModifyOriginal()
  {
    Color original = Color.green;
    texture.Fill(original);

    texture.Blurred(1, 1);

    Color center = texture.GetPixel(1, 1);
    Assert.AreEqual(original, center);
  }

  [Test]
  public void CropAndResizeToSquare_ReturnsTargetSize()
  {
    texture.Reinitialize(8, 4);
    texture.Apply();

    Texture2D result = texture.CropAndResizeToSquare(16);

    Assert.AreEqual(16, result.width);
    Assert.AreEqual(16, result.height);

    Object.DestroyImmediate(result);
  }
}
