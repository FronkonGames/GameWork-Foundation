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
  /// Drawing of objects for development.
  /// </summary>
  /// <remarks>Only available in the Editor</remarks>
  public static partial class DebugDraw
  {
    private static Vector3[] circle;
    private static Vector3[] solidCircle;
    private static Vector3[] cube;

    private static void CreateMeshes()
    {
      circle = new Vector3[Segments + 2];

      int deg = 360 / Segments;
      for (int i = 0; i <= 360; i += 2 * deg)
      {
        float x = Mathf.Sin(Mathf.Deg2Rad * i);
        float z = Mathf.Cos(Mathf.Deg2Rad * i);

        float x2 = Mathf.Sin(Mathf.Deg2Rad * (i + deg));
        float z2 = Mathf.Cos(Mathf.Deg2Rad * (i + deg));

        circle[i / deg] = new Vector3(x, 0.0f, z);
        circle[i / deg + 1] = new Vector3(x2, 0.0f, z2);
      }

      solidCircle = new Vector3[Segments + 2];
      for (int i = 0; i <= 360; i += 2 * deg)
      {
        float x = Mathf.Sin(Mathf.Deg2Rad * i);
        float z = Mathf.Cos(Mathf.Deg2Rad * i);

        solidCircle[i / deg] = new Vector3(x, 0.0f, z);
      }

      cube = new[]
      {
        new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0.5f, -0.5f, -0.5f),
        new Vector3(0.5f, -0.5f, 0.5f), new Vector3(-0.5f, -0.5f, 0.5f),
        new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, -0.5f, -0.5f),
        
        new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, -0.5f),
        new Vector3(0.5f, 0.5f, 0.5f), new Vector3(-0.5f, 0.5f, 0.5f),
        new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(-0.5f, 0.5f, -0.5f),
        new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(0.5f, 0.5f, -0.5f),
        
        new Vector3(0.5f, -0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f),
        new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0.5f, 0.5f, -0.5f),
        new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-0.5f, 0.5f, 0.5f),
        new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-0.5f, 0.5f, -0.5f)
      };
    }
  }
}
