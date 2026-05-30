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
  /// <summary>
  /// Extension methods for <see cref="RaycastHit"/>.
  /// </summary>
  public static class RaycastHitExtensions
  {
    /// <summary>
    /// Gets the material at the hit point by determining which sub-mesh was hit.
    /// </summary>
    /// <param name="hit">The raycast hit result.</param>
    /// <returns>The <see cref="Material"/> at the hit point, or <c>null</c> if the renderer has no materials.</returns>
    public static Material GetMaterialAtHit(this RaycastHit hit)
    {
      MeshRenderer meshRenderer = hit.collider.GetComponent<MeshRenderer>();
      if (meshRenderer == null)
        return null;

      MeshFilter meshFilter = hit.collider.GetComponent<MeshFilter>();
      if (meshFilter == null || meshFilter.sharedMesh == null)
        return meshRenderer.sharedMaterial;

      Mesh mesh = meshFilter.sharedMesh;
      int subMeshCount = mesh.subMeshCount;
      int[] triangles = mesh.triangles;

      int triangleIndex = hit.triangleIndex * 3;

      for (int subMesh = 0; subMesh < subMeshCount; subMesh++)
      {
        int start = mesh.GetSubMesh(subMesh).indexStart;
        int end = start + mesh.GetSubMesh(subMesh).indexCount;

        if (triangleIndex >= start && triangleIndex < end)
        {
          if (subMesh < meshRenderer.sharedMaterials.Length)
            return meshRenderer.sharedMaterials[subMesh];

          return null;
        }
      }

      return meshRenderer.sharedMaterial;
    }

    /// <summary>
    /// Gets the texture coordinate at the hit point.
    /// </summary>
    /// <param name="hit">The raycast hit result.</param>
    /// <returns>The texture coordinate (<see cref="Vector2"/>) at the hit point.</returns>
    public static Vector2 GetTextureCoordAtHit(this RaycastHit hit)
    {
      return hit.textureCoord;
    }

    /// <summary>
    /// Gets the normal in world space at the hit point.
    /// </summary>
    /// <param name="hit">The raycast hit result.</param>
    /// <returns>The world-space normal (<see cref="Vector3"/>) at the hit point.</returns>
    public static Vector3 GetWorldNormal(this RaycastHit hit)
    {
      return hit.normal;
    }
  }
}
