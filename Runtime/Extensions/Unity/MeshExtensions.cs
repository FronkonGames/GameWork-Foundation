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
  /// <summary> Mesh extensions. </summary>
  public static class MeshExtensions
  {
    /// <summary> Returns the local-space center of the mesh bounds. </summary>
    public static Vector3 CalculateMeshCenter(this Mesh mesh)
    {
      if (mesh == null)
        return Vector3.zero;

      return mesh.bounds.center;
    }

    /// <summary> Returns the combined center of multiple meshes. </summary>
    public static Vector3 CalculateMeshCenter<T>(this T meshes) where T : IEnumerable<Mesh>
    {
      bool hasMesh = false;
      Bounds bounds = default;

      foreach (Mesh mesh in meshes)
      {
        if (mesh == null)
          continue;

        if (hasMesh == false)
        {
          bounds = mesh.bounds;
          hasMesh = true;
        }
        else
        {
          bounds.Encapsulate(mesh.bounds);
        }
      }

      return hasMesh ? bounds.center : Vector3.zero;
    }
  }
}
