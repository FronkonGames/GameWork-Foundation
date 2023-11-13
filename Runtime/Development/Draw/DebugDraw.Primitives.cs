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
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Drawing of objects for development. </summary>
  /// <remarks>Only available in the Editor</remarks>
  public partial class DebugDraw : CachedMonoBehaviour
  {
    private static List<Vector3> SphereWire;
    private static List<Vector3> CubeWire;

    private static void CreateSphereWireMesh()
    {
      SphereWire = new List<Vector3>();

      float step = MathConstants.Tau / Settings.DebugDraw.Divisions;
      for (float theta = 0.0f; theta < MathConstants.Tau; theta += step)
      {
        float cos0 = Mathf.Cos(theta);
        float cos1 = Mathf.Cos(theta + step);
        float sin0 = Mathf.Sin(theta);
        float sin1 = Mathf.Sin(theta + step);

        SphereWire.Add(0.5f * new Vector3(0.0f, cos0, -sin0));
        SphereWire.Add(0.5f * new Vector3(0.0f, cos1, -sin1));

        SphereWire.Add(0.5f * new Vector3(cos0, 0.0f, -sin0));
        SphereWire.Add(0.5f * new Vector3(cos1, 0.0f, -sin1));

        SphereWire.Add(0.5f * new Vector3(cos0, -sin0, 0.0f));
        SphereWire.Add(0.5f * new Vector3(cos1, -sin1, 0.0f));
      }
    }

    private static void CreateCubeWireMesh()
    {
      CubeWire = new List<Vector3>();

      float s = 1.0f * 0.5f;
      CubeWire.Add(new Vector3(-s, -s, -s));
      CubeWire.Add(new Vector3(-s, -s, +s));
      CubeWire.Add(new Vector3(-s, -s, +s));
      CubeWire.Add(new Vector3(+s, -s, +s));
      CubeWire.Add(new Vector3(+s, -s, +s));
      CubeWire.Add(new Vector3(+s, -s, -s));
      CubeWire.Add(new Vector3(+s, -s, -s));
      CubeWire.Add(new Vector3(-s, -s, -s));

      CubeWire.Add(new Vector3(-s, +s, -s));
      CubeWire.Add(new Vector3(-s, +s, +s));
      CubeWire.Add(new Vector3(-s, +s, +s));
      CubeWire.Add(new Vector3(+s, +s, +s));
      CubeWire.Add(new Vector3(+s, +s, +s));
      CubeWire.Add(new Vector3(+s, +s, -s));
      CubeWire.Add(new Vector3(+s, +s, -s));
      CubeWire.Add(new Vector3(-s, +s, -s));

      CubeWire.Add(new Vector3(+s, +s, +s));
      CubeWire.Add(new Vector3(+s, -s, +s));
      CubeWire.Add(new Vector3(+s, +s, -s));
      CubeWire.Add(new Vector3(+s, -s, -s));

      CubeWire.Add(new Vector3(-s, +s, +s));
      CubeWire.Add(new Vector3(-s, -s, +s));
      CubeWire.Add(new Vector3(-s, +s, -s));
      CubeWire.Add(new Vector3(-s, -s, -s));
    }
  }
}
#endif