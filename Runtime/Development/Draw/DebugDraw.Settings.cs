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
    private const int Capacity = 1000;

    // It must be divisible by 360.
    private const int Segments = 360 / 4;

    private const float OcclusionColorFactor = 0.5f;
    
    private const float Transparency = 0.75f;

    private static readonly Color PointColor = "#8cc6eb".FromHex();
    private const float PointSize = 0.5f;

    private const int SphereRadialSegments = 4;

    private static readonly Color TriangleColor = "#8b9bf6".FromHex();

    private static readonly Color CubeColor = "#bee6e4".FromHex();

    private static readonly Color ArrowColor = "#b8ffce".FromHex();
    private const float ArrowSize = 2.0f;
    private const float ArrowHeadLength = 0.2f;
    private const float ArrowHeadWidth = 0.05f;
    
    private static readonly Color LineColor = "#f0cbc9".FromHex();
    private const float LineDashSize = 0.1f;

    private static readonly Color CircleColor = "#affff1".FromHex();

    private static readonly Color SphereColor = "#f5df8c".FromHex();

    private static readonly Color ArcColor = "#90d3be".FromHex();

    private static readonly Color DiamondColor = "#f5d59a".FromHex();
    private const float DiamondSize = 0.5f;
    
    private static readonly Color ColorX = Color.red;
    private static readonly Color ColorY = Color.green;
    private static readonly Color ColorZ = Color.blue;
  }
}
