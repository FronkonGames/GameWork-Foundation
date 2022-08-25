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
  public static partial class Draw
  {
    private static int Capacity = 100;
    
    private static int Segments = 32;
    
    private static float ArrowHeadLength = 0.2f;
    private static float ArrowHeadWidth = 0.05f;
    
    private static float DashSize = 1.0f;

    private static float OcclusionColorFactor = 0.5f;
    
    private static float Transparency = 0.75f;

    private static Color TextColor => new Color(0.22f, 0.22f, 0.22f);

    private static Color PointColor => new Color(1.0f, 0.8f, 0.0f, Transparency);
    
    private static Color LineColor => new Color(1.0f, 0.4f, 0.3f, Transparency);

    private static Color DiscColor => new Color(0.8f, 0.4f, 0.8f, Transparency);

    private static Color ArrowColor => new Color(0.4f, 0.7f, 0.8f, Transparency);
    
    private static Color StartColor => new Color(1.0f, 0.4f, 0.3f);
    private static Color EndColor => new Color(0.4f, 1.0f, 0.3f);

    private static Color HitColor => new Color(1.0f, 0.1f, 0.2f);
    private static Color RayColor => new Color(0.4f, 1.0f, 0.3f);

    private static Color ColorX => new Color(1.0f, 0.1f, 0.2f);
    private static Color ColorY => new Color(0.3f, 1.0f, 0.1f);
    private static Color ColorZ => new Color(0.1f, 0.4f, 1.0f);
  }
}
