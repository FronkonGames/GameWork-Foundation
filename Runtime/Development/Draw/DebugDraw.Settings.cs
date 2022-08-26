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
    private const int Capacity = 100;
    
    private const int Segments = 32;

    private const float OcclusionColorFactor = 0.5f;
    
    private const float Transparency = 0.75f;

    private const string PointColor = "#64b5f6";
    private const float PointSize = 0.5f;

    private const int SphereVerticalSegments = 3;
    private const int SphereRadialSegments = 3;
    
    private const string ArrowColor = "#8e24aa";
    private const float ArrowSize = 2.0f;
    private const float ArrowHeadLength = 0.2f;
    private const float ArrowHeadWidth = 0.05f;
    
    private const string LineColor = "#fff8e1";
    private const float DashSize = 1.0f;

    private const string DiscColor = "#ffeb3b";

    private const string SphereColor = "#fb8c00";

    private const string ColorX = "#FF0000";
    private const string ColorY = "#00FF00";
    private const string ColorZ = "#0000FF";
  }
}