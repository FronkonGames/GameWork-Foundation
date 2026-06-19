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
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using FronkonGames.GameWork.Foundation;

/// <summary> Type extensions test. </summary>
[TestFixture]
public class TypeExtensionsNewTests
{
  private class GenericBase<T>
  {
    public int baseField;
  }

  private class IntDerivative : GenericBase<int>
  {
    public string childField;
  }

  private class SerializedSample : ScriptableObject
  {
    public int publicField;
    [SerializeField] private string privateField;
  }

  [Test]
  public void GetFieldAtPath_FindsNestedField()
  {
    FieldInfo field = typeof(IntDerivative).GetFieldAtPath("childField");

    Assert.IsNotNull(field);
    Assert.AreEqual("childField", field.Name);
  }

  [Test]
  public void GetFieldRecursive_FindsBaseField()
  {
    FieldInfo field = typeof(IntDerivative).GetFieldRecursive("baseField", BindingFlags.Public | BindingFlags.Instance);

    Assert.IsNotNull(field);
    Assert.AreEqual("baseField", field.Name);
  }

  [Test]
  public void GetSerializedFields_ReturnsPublicAndSerializeFieldMembers()
  {
    FieldInfo[] fields = typeof(SerializedSample).GetSerializedFields().ToArray();

    Assert.AreEqual(2, fields.Length);
    Assert.IsTrue(fields.Any(field => field.Name == "publicField"));
    Assert.IsTrue(fields.Any(field => field.Name == "privateField"));
  }

  [Test]
  public void IsNullable_DetectsReferenceAndNullableValueTypes()
  {
    Assert.IsTrue(typeof(string).IsNullable());
    Assert.IsTrue(typeof(int?).IsNullable());
    Assert.IsFalse(typeof(int).IsNullable());
  }

  [Test]
  public void IsSubclassOfRawGeneric_DetectsOpenGenericBase()
  {
    Assert.IsTrue(typeof(IntDerivative).IsSubclassOfRawGeneric(typeof(GenericBase<>)));
    Assert.IsFalse(typeof(string).IsSubclassOfRawGeneric(typeof(GenericBase<>)));
  }

  [Test]
  public void InheritsFrom_SupportsOpenGenericBase()
  {
    Assert.IsTrue(typeof(IntDerivative).InheritsFrom(typeof(GenericBase<>)));
    Assert.IsTrue(typeof(IntDerivative).InheritsFrom(typeof(object)));
  }

  [Test]
  public void IsUnitySerializable_DetectsCommonTypes()
  {
    Assert.IsTrue(typeof(int).IsUnitySerializable());
    Assert.IsTrue(typeof(Vector3).IsUnitySerializable());
    Assert.IsTrue(typeof(SerializedSample).IsUnitySerializable());
    Assert.IsFalse(typeof(System.Action).IsUnitySerializable());
  }

  [Test]
  public void IsEmpty_DetectsMarkerTypes()
  {
    Assert.IsTrue(typeof(EmptyMarker).IsEmpty());
    Assert.IsFalse(typeof(IntDerivative).IsEmpty());
  }

  [Test]
  public void GetShortAssemblyName_ReturnsAssemblyNameWithoutMetadata()
  {
    string assemblyName = typeof(string).GetShortAssemblyName();

    Assert.IsFalse(assemblyName.Contains(","));
    Assert.IsFalse(string.IsNullOrEmpty(assemblyName));
  }

  private class EmptyMarker
  {
  }
}
