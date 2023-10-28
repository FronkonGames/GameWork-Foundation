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
  /// <summary> Configurable Foundation parameters. </summary>
  public static class Settings
  {
    /// <summary> Console messages. </summary>
    public static class Log
    {
      public static readonly LogLevel DefaultLevel = LogLevel.Info;
      public static readonly bool ShowStackTrace = true;

      public static readonly Color InfoColor = new(0.12f, 0.49f, 0.67f);
      public static readonly Color WarningColor = new(1.0f, 0.52f, 0.29f);
      public static readonly Color ErrorColor = new(1.0f, 0.2f, 0.47f);
    }

    /// <summary> FPS calculation. </summary>
    public static class FPS
    {
      public static readonly float WaitingToStartCounting = 2.0f;
      public static readonly int UpdatePerSecond = 2;
      public static readonly int HistoryFrames = 100;

      public static readonly int WarningFPS = 30;
      public static readonly int BadFPS = 20;

      public static readonly Color GoodColor = new(0.2f, 0.8f, 0.2f);
      public static readonly Color WarningColor = new(0.4f, 0.4f, 0.2f);
      public static readonly Color BadColor = new(0.8f, 0.2f, 0.2f);
    }

    /// <summary> Debug Draw. </summary>
    public static class Draw
    {
      public static readonly bool DrawInSceneView = true;

      public static readonly int Capacity = 500;
      public static readonly float Transparency = 0.7f;
      public static readonly float PointSize = 0.5f;
      public static readonly Color AxisXColor = Color.red;
      public static readonly Color AxisYColor = Color.green;
      public static readonly Color AxisZColor = Color.blue;
      public static readonly Color LineColor = new(0.941f, 0.796f, 0.788f);

      public static readonly float LineGapSize = 4.0f;
      public static readonly float LineThickness = 0.0f;
      public static readonly Color ArrowColor = new(0.043f, 0.561f, 0.988f);
      public static readonly float ArrowTipSize = 0.25f;
      public static readonly float ArrowWidth = 0.5f;
      public static readonly Color RayColor = "#a94241".FromHex();
      public static readonly float RayLength = 1000.0f;
      public static readonly Color HitColor = "#C91211".FromHex();
      public static readonly float HitRadius = 0.1f;
      public static readonly float HitLength = 0.25f;
      public static readonly Color CircleColor = "#affff1".FromHex();
      public static readonly Color CubeColor = "#bee6e4".FromHex();
      public static readonly Color SphereColor = "#f5df8c".FromHex();
      public static readonly Color ArcColor = "#90d3be".FromHex();
      public static readonly Color DiamondColor = "#f5d59a".FromHex();
      public static readonly float DiamondSize = 0.5f;
      public static readonly Color ConeColor = "#f3ed80".FromHex();
      public static readonly Color BoundsColor = "#f1c59c".FromHex();
      public static readonly Color TextColor = "#f5f5f5".FromHex();
      public static readonly int TextSize = 24;
    }
#if UNITY_EDITOR
    /// <summary> Editor styles. </summary>
    public static class Editor
    {
      public static readonly Color ErrorColor = Color.red;
      public static readonly float FileButtonWidth = 20.0f;
      public static readonly float FileButtonPadding = 4.0f;
      public static readonly float MinMaxFieldWidth = 30.0f;
      public static readonly string MessageBoxInfoIcon = "console.infoicon";
      public static readonly string MessageBoxWarningIcon = "console.warnicon";
      public static readonly string MessageBoxErrorIcon = "console.erroricon";
      public static readonly int TitleSpaceBeforeTitle = 8;
      public static readonly int TitleSpaceBeforeLine = 2;
      public static readonly int TitleLineHeight = 2;
      public static readonly int TitleSpaceBeforeContent = 3;
      public static readonly string RefreshIcon = "d_Refresh";
      
      public const float SpaceSeparation = 5.0f;
    }
#endif
  }
}
