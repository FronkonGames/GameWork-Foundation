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

/// <summary> RaycastHitExtensions tests. </summary>
public class RaycastHitExtensionsTests
{
  private GameObject planeObject;
  private MeshFilter meshFilter;
  private MeshCollider meshCollider;

  [SetUp]
  public void SetUp()
  {
    planeObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
    planeObject.name = "TestPlane";
    planeObject.transform.position = Vector3.zero;
    planeObject.transform.localScale = Vector3.one;

    meshFilter = planeObject.GetComponent<MeshFilter>();
    meshCollider = planeObject.GetComponent<MeshCollider>();
    meshCollider.sharedMesh = meshFilter.sharedMesh;
  }

  [TearDown]
  public void TearDown()
  {
    Object.DestroyImmediate(planeObject);
  }

  private bool CastToPlane(out RaycastHit hit)
  {
    Vector3 origin = new Vector3(0.0f, 5.0f, 0.0f);
    Ray ray = new Ray(origin, Vector3.down);
    return Physics.Raycast(ray, out hit);
  }

  [Test]
  public void GetTextureCoordAtHit_ReturnsValidCoordinate()
  {
    Assert.IsTrue(CastToPlane(out RaycastHit hit));

    Vector2 texCoord = hit.GetTextureCoordAtHit();

    Assert.IsTrue(texCoord.x >= 0.0f && texCoord.x <= 1.0f);
    Assert.IsTrue(texCoord.y >= 0.0f && texCoord.y <= 1.0f);
  }

  [Test]
  public void GetTextureCoordAtHit_CenterHit_ReturnsMiddleCoordinate()
  {
    Ray ray = new Ray(new Vector3(0.0f, 5.0f, 0.0f), Vector3.down);
    Assert.IsTrue(Physics.Raycast(ray, out RaycastHit hit));

    Vector2 texCoord = hit.GetTextureCoordAtHit();

    Assert.AreEqual(0.5f, texCoord.x, 0.01f);
    Assert.AreEqual(0.5f, texCoord.y, 0.01f);
  }

  [Test]
  public void GetWorldNormal_ReturnsUpwardNormal()
  {
    Assert.IsTrue(CastToPlane(out RaycastHit hit));

    Vector3 worldNormal = hit.GetWorldNormal();

    Assert.AreEqual(Vector3.up.x, worldNormal.x, 0.001f);
    Assert.AreEqual(Vector3.up.y, worldNormal.y, 0.001f);
    Assert.AreEqual(Vector3.up.z, worldNormal.z, 0.001f);
  }

  [Test]
  public void GetWorldNormal_MatchesHitNormal()
  {
    Assert.IsTrue(CastToPlane(out RaycastHit hit));

    Vector3 worldNormal = hit.GetWorldNormal();

    Assert.AreEqual(hit.normal.x, worldNormal.x, 0.001f);
    Assert.AreEqual(hit.normal.y, worldNormal.y, 0.001f);
    Assert.AreEqual(hit.normal.z, worldNormal.z, 0.001f);
  }
}
