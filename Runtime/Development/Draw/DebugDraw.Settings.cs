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

    private const bool DepthTest = true;
    
    private const float Transparency = 0.75f;

    private const float PointSize = 0.5f;
    private static readonly Color AxisX = Color.red;
    private static readonly Color AxisY = Color.green;
    private static readonly Color AxisZ = Color.blue;

    private static readonly Color LineColor = "#f0cbc9".FromHex();
    private const float LineGapSize = 4.0f;
    private const float LineThickness = 0.0f;
    
    private static readonly Color ArrowColor = "#b8ffce".FromHex();
    private const float ArrowTipSize = 0.25f;
    private const float ArrowWidth = 0.5f;

    private static readonly Color CircleColor = "#affff1".FromHex();
    
    private static readonly Color CubeColor = "#bee6e4".FromHex();

    private static readonly Color SphereColor = "#f5df8c".FromHex();

    private static readonly Color ArcColor = "#90d3be".FromHex();

    private static readonly Color DiamondColor = "#f5d59a".FromHex();
    private const float DiamondSize = 0.5f;

    private static readonly Color ConeColor = "#f3ed80".FromHex();

    private static readonly Color TextColor = "#f5f5f5".FromHex();
    private const int TextSize = 24;
  }
}
