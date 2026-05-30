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
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation.Tests
{
  /// <summary> Slothsoft extensions tests. </summary>
  [TestFixture]
  public class SlothsoftExtensionsTests
  {
    /// <summary> BoundsIntExtensions.Contains2D with Vector2Int inside returns true. </summary>
    [Test]
    public void BoundsIntExtensions_Contains2D_Vector2Int_Inside_ReturnsTrue()
    {
      BoundsInt bounds = new BoundsInt(0, 0, 0, 5, 5, 5);

      Assert.IsTrue(bounds.Contains2D(new Vector2Int(2, 3)));
    }

    /// <summary> BoundsIntExtensions.Contains2D with Vector2Int outside returns false. </summary>
    [Test]
    public void BoundsIntExtensions_Contains2D_Vector2Int_Outside_ReturnsFalse()
    {
      BoundsInt bounds = new BoundsInt(0, 0, 0, 5, 5, 5);

      Assert.IsFalse(bounds.Contains2D(new Vector2Int(6, 3)));
    }

    /// <summary> BoundsIntExtensions.Contains2D with Vector3Int inside returns true. </summary>
    [Test]
    public void BoundsIntExtensions_Contains2D_Vector3Int_Inside_ReturnsTrue()
    {
      BoundsInt bounds = new BoundsInt(0, 0, 0, 5, 5, 5);

      Assert.IsTrue(bounds.Contains2D(new Vector3Int(1, 2, 3)));
    }

    /// <summary> CollectionExtensions.GetXY returns correct element from 2D list. </summary>
    [Test]
    public void CollectionExtensions_GetXY_ReturnsCorrectElement()
    {
      List<int> list = new List<int> { 1, 2, 3, 4, 5, 6 };

      Assert.AreEqual(4, list.GetXY(0, 1, 3));
      Assert.AreEqual(6, list.GetXY(2, 1, 3));
    }

    /// <summary> CollectionExtensions.SetXY sets correct element in 2D list. </summary>
    [Test]
    public void CollectionExtensions_SetXY_SetsCorrectElement()
    {
      List<int> list = new List<int> { 0, 0, 0, 0, 0, 0 };

      list.SetXY(1, 1, 3, 42);

      Assert.AreEqual(42, list[4]);
    }

    /// <summary> Color32Extensions.IsEqualTo on equal colors returns true. </summary>
    [Test]
    public void Color32Extensions_IsEqualTo_EqualColors_ReturnsTrue()
    {
      Color32 a = new Color32(255, 128, 64, 32);
      Color32 b = new Color32(255, 128, 64, 32);

      Assert.IsTrue(a.IsEqualTo(b));
    }

    /// <summary> Color32Extensions.IsEqualTo on different colors returns false. </summary>
    [Test]
    public void Color32Extensions_IsEqualTo_DifferentColors_ReturnsFalse()
    {
      Color32 a = new Color32(255, 128, 64, 32);
      Color32 b = new Color32(255, 128, 64, 64);

      Assert.IsFalse(a.IsEqualTo(b));
    }

    /// <summary> Color32Extensions.CreateTexture creates texture with correct dimensions. </summary>
    [Test]
    public void Color32Extensions_CreateTexture_CreatesCorrectDimensions()
    {
      int width = 4;
      int height = 3;
      Color32[] pixels = new Color32[width * height];

      for (int i = 0; i < pixels.Length; i++)
        pixels[i] = new Color32(255, 0, 0, 255);

      Texture2D texture = pixels.CreateTexture(width, height);

      Assert.AreEqual(width, texture.width);
      Assert.AreEqual(height, texture.height);

      UnityEngine.Object.DestroyImmediate(texture);
    }

    /// <summary> SpriteExtensions.GetRect returns correct rect. </summary>
    [Test]
    public void SpriteExtensions_GetRect_ReturnsCorrectRect()
    {
      Texture2D texture = new Texture2D(32, 32);
      Sprite sprite = Sprite.Create(texture, new Rect(4, 8, 16, 24), new Vector2(0.5f, 0.5f));

      RectInt rect = sprite.GetRect();

      Assert.AreEqual(4, rect.x);
      Assert.AreEqual(8, rect.y);
      Assert.AreEqual(16, rect.width);
      Assert.AreEqual(24, rect.height);

      UnityEngine.Object.DestroyImmediate(sprite);
      UnityEngine.Object.DestroyImmediate(texture);
    }

    /// <summary> SpriteExtensions.IsFullyTransparent on transparent sprite returns true. </summary>
    [Test]
    public void SpriteExtensions_IsFullyTransparent_TransparentSprite_ReturnsTrue()
    {
      Texture2D texture = new Texture2D(2, 2);
      Color32[] pixels = new Color32[4];
      for (int i = 0; i < 4; i++)
        pixels[i] = new Color32(0, 0, 0, 0);

      texture.SetPixels32(pixels);
      texture.Apply();
      Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 2, 2), Vector2.zero);

      Assert.IsTrue(sprite.IsFullyTransparent());

      UnityEngine.Object.DestroyImmediate(sprite);
      UnityEngine.Object.DestroyImmediate(texture);
    }

    /// <summary> TypeExtensions.FindImplementations finds concrete types. </summary>
    [Test]
    public void TypeExtensions_FindImplementations_FindsConcreteTypes()
    {
      var implementations = typeof(TestBase).FindImplementations();

      CollectionAssert.Contains(implementations, typeof(TestDerivedA));
      CollectionAssert.Contains(implementations, typeof(TestDerivedB));
    }

    /// <summary> KeyValuePairExtensions.Deconstruct extracts key and value. </summary>
    [Test]
    public void KeyValuePairExtensions_Deconstruct_ExtractsKeyAndValue()
    {
      KeyValuePair<int, string> kvp = new KeyValuePair<int, string>(42, "hello");

      var (key, value) = kvp;

      Assert.AreEqual(42, key);
      Assert.AreEqual("hello", value);
    }

    /// <summary> UnityObjectExtensions.IsValid on valid object returns true. </summary>
    [Test]
    public void UnityObjectExtensions_IsValid_ValidObject_ReturnsTrue()
    {
      GameObject go = new GameObject("TestObject");

      Assert.IsTrue(go.IsValid());

      UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary> UnityObjectExtensions.IsValid on null returns false. </summary>
    [Test]
    public void UnityObjectExtensions_IsValid_Null_ReturnsFalse()
    {
      GameObject go = null;

      Assert.IsFalse(go.IsValid());
    }

    /// <summary> Vector2IntExtensions.Deconstruct extracts x and y. </summary>
    [Test]
    public void Vector2IntExtensions_Deconstruct_ExtractsXY()
    {
      Vector2Int vector = new Vector2Int(3, 7);

      var (x, y) = vector;

      Assert.AreEqual(3, x);
      Assert.AreEqual(7, y);
    }

    /// <summary> Vector2IntExtensions.WithX returns new vector with replaced X. </summary>
    [Test]
    public void Vector2IntExtensions_WithX_ReturnsNewVector()
    {
      Vector2Int vector = new Vector2Int(1, 2);

      Vector2Int result = vector.WithX(99);

      Assert.AreEqual(99, result.x);
      Assert.AreEqual(2, result.y);
    }

    /// <summary> Vector2IntExtensions.WithY returns new vector with replaced Y. </summary>
    [Test]
    public void Vector2IntExtensions_WithY_ReturnsNewVector()
    {
      Vector2Int vector = new Vector2Int(1, 2);

      Vector2Int result = vector.WithY(99);

      Assert.AreEqual(1, result.x);
      Assert.AreEqual(99, result.y);
    }

    /// <summary> Vector2IntExtensions.ManhattanDistance calculates correct distance. </summary>
    [Test]
    public void Vector2IntExtensions_ManhattanDistance_CalculatesCorrectDistance()
    {
      Vector2Int a = new Vector2Int(1, 2);
      Vector2Int b = new Vector2Int(4, 6);

      Assert.AreEqual(7, a.ManhattanDistance(b));
    }

    /// <summary> Vector2IntExtensions.ToVector2 converts correctly. </summary>
    [Test]
    public void Vector2IntExtensions_ToVector2_ConvertsCorrectly()
    {
      Vector2Int vector = new Vector2Int(3, 5);

      Vector2 result = vector.ToVector2();

      Assert.AreEqual(3f, result.x);
      Assert.AreEqual(5f, result.y);
    }

    /// <summary> Vector3IntExtensions.Deconstruct extracts x, y, and z. </summary>
    [Test]
    public void Vector3IntExtensions_Deconstruct_ExtractsXYZ()
    {
      Vector3Int vector = new Vector3Int(1, 2, 3);

      var (x, y, z) = vector;

      Assert.AreEqual(1, x);
      Assert.AreEqual(2, y);
      Assert.AreEqual(3, z);
    }

    /// <summary> Vector3IntExtensions.WithX returns new vector with replaced X. </summary>
    [Test]
    public void Vector3IntExtensions_WithX_ReturnsNewVector()
    {
      Vector3Int vector = new Vector3Int(1, 2, 3);

      Vector3Int result = vector.WithX(99);

      Assert.AreEqual(99, result.x);
      Assert.AreEqual(2, result.y);
      Assert.AreEqual(3, result.z);
    }

    /// <summary> Vector3IntExtensions.WithY returns new vector with replaced Y. </summary>
    [Test]
    public void Vector3IntExtensions_WithY_ReturnsNewVector()
    {
      Vector3Int vector = new Vector3Int(1, 2, 3);

      Vector3Int result = vector.WithY(99);

      Assert.AreEqual(1, result.x);
      Assert.AreEqual(99, result.y);
      Assert.AreEqual(3, result.z);
    }

    /// <summary> Vector3IntExtensions.WithZ returns new vector with replaced Z. </summary>
    [Test]
    public void Vector3IntExtensions_WithZ_ReturnsNewVector()
    {
      Vector3Int vector = new Vector3Int(1, 2, 3);

      Vector3Int result = vector.WithZ(99);

      Assert.AreEqual(1, result.x);
      Assert.AreEqual(2, result.y);
      Assert.AreEqual(99, result.z);
    }

    /// <summary> Vector3IntExtensions.ManhattanDistance calculates correct distance. </summary>
    [Test]
    public void Vector3IntExtensions_ManhattanDistance_CalculatesCorrectDistance()
    {
      Vector3Int a = new Vector3Int(1, 2, 3);
      Vector3Int b = new Vector3Int(4, 6, 8);

      Assert.AreEqual(12, a.ManhattanDistance(b));
    }

    /// <summary> Vector3IntExtensions.ToVector3 converts correctly. </summary>
    [Test]
    public void Vector3IntExtensions_ToVector3_ConvertsCorrectly()
    {
      Vector3Int vector = new Vector3Int(1, 4, 9);

      Vector3 result = vector.ToVector3();

      Assert.AreEqual(1f, result.x);
      Assert.AreEqual(4f, result.y);
      Assert.AreEqual(9f, result.z);
    }

    private class TestBase { }
    private class TestDerivedA : TestBase { }
    private class TestDerivedB : TestBase { }
  }
}
