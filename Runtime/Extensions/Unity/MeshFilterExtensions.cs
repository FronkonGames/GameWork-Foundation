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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> MeshFilter extensions. </summary>
  public static class MeshFilterExtensions
  {
    /// <summary> Optimizes and uploads mesh data for all filters in the collection. </summary>
    public static T UploadMeshData<T>(this T meshFilters, bool markNoLongerReadable = true) where T : IEnumerable<MeshFilter>
    {
      foreach (MeshFilter meshFilter in meshFilters)
      {
        Mesh mesh = meshFilter.sharedMesh;

        if (mesh == null || mesh.isReadable == false)
          continue;

        mesh.Optimize();
        mesh.UploadMeshData(markNoLongerReadable);
      }

      return meshFilters;
    }

    /// <summary> Yields shared meshes from mesh filters. </summary>
    public static IEnumerable<Mesh> SharedMeshes<T>(this T meshFilters) where T : IEnumerable<MeshFilter>
    {
      foreach (MeshFilter meshFilter in meshFilters)
        yield return meshFilter.sharedMesh;
    }

    /// <summary> Returns the center of the shared mesh bounds. </summary>
    public static Vector3 CalculateMeshCenter(this MeshFilter meshFilter)
      => meshFilter.sharedMesh.CalculateMeshCenter();

    /// <summary> Returns the combined center of shared meshes. </summary>
    public static Vector3 CalculateMeshCenter<T>(this T meshFilters) where T : IEnumerable<MeshFilter>
      => meshFilters.SharedMeshes().CalculateMeshCenter();
  }
}
