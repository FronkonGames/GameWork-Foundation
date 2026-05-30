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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Collider extensions. </summary>
  public static class ColliderExtensions
  {
    /// <summary> Clones a collider as a child GameObject with the specified layer. </summary>
    /// <param name="col">Source collider.</param>
    /// <param name="newLayer">Layer for the cloned GameObject.</param>
    public static void CloneColliderAsChild(this Collider col, LayerMask newLayer)
    {
      GameObject clone = new GameObject($"Clone_{col.name}");
      clone.transform.SetParent(col.transform, false);
      clone.layer = newLayer;

      if (col is BoxCollider box)
      {
        BoxCollider cloneBox = clone.AddComponent<BoxCollider>();
        cloneBox.center = box.center;
        cloneBox.size = box.size;
      }
      else if (col is SphereCollider sphere)
      {
        SphereCollider cloneSphere = clone.AddComponent<SphereCollider>();
        cloneSphere.center = sphere.center;
        cloneSphere.radius = sphere.radius;
      }
      else if (col is CapsuleCollider capsule)
      {
        CapsuleCollider cloneCapsule = clone.AddComponent<CapsuleCollider>();
        cloneCapsule.center = capsule.center;
        cloneCapsule.radius = capsule.radius;
        cloneCapsule.height = capsule.height;
        cloneCapsule.direction = capsule.direction;
      }
      else if (col is MeshCollider mesh)
      {
        MeshCollider cloneMesh = clone.AddComponent<MeshCollider>();
        cloneMesh.sharedMesh = mesh.sharedMesh;
        cloneMesh.convex = mesh.convex;
      }
    }

    /// <summary> Gets the closest point on the collider to a given position. </summary>
    /// <param name="collider">Collider.</param>
    /// <param name="position">World position.</param>
    /// <returns>Closest point on the collider surface.</returns>
    public static Vector3 ClosestPointTo(this Collider collider, Vector3 position) =>
      collider.ClosestPoint(position);

    /// <summary> Returns true if the collider contains a world point. </summary>
    /// <param name="collider">Collider.</param>
    /// <param name="point">World point.</param>
    /// <returns>True if the point is inside the collider.</returns>
    public static bool ContainsPoint(this Collider collider, Vector3 point) =>
      collider.ClosestPoint(point) == point;
  }
}
