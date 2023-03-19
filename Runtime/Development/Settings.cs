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
  /// <summary> Foundation settings (Edit > Preferences > Game:Work > Foundation) </summary>
  public static class Settings
  {
    // Log messages.
    public static readonly SettingColor LogInfoColor = new Color(0.12f, 0.49f, 0.67f);
    public static readonly SettingColor LogWarningColor = new Color(1.0f, 0.52f, 0.29f);
    public static readonly SettingColor LogErrorColor = new Color(1.0f, 0.2f, 0.47f);

    // Debug draw.
    public static readonly SettingInt DebugDrawCapacity = 1000;
    public static readonly SettingFloat DebugDrawTransparency = 0.7f;
    public static readonly SettingFloat DebugDrawPointSize = 0.5f;
    public static readonly SettingColor DebugDrawAxisXColor = Color.red;
    public static readonly SettingColor DebugDrawAxisYColor = Color.green;
    public static readonly SettingColor DebugDrawAxisZColor = Color.blue;
    public static readonly SettingColor DebugDrawLineColor = "#f0cbc9".FromHex();
    public static readonly SettingFloat DebugDrawLineGapSize = 4.0f;
    public static readonly SettingFloat DebugDrawLineThickness = 0.0f;
    public static readonly SettingColor DebugDrawArrowColor = "#b8ffce".FromHex();
    public static readonly SettingFloat DebugDrawArrowTipSize = 0.25f;
    public static readonly SettingFloat DebugDrawArrowWidth = 0.5f;
    public static readonly SettingColor DebugDrawRayColor = "#a94241".FromHex();
    public static readonly SettingFloat DebugDrawRayLength = 1000.0f;
    public static readonly SettingColor DebugDrawHitColor = "#C91211".FromHex();
    public static readonly SettingFloat DebugDrawHitRadius = 0.1f;
    public static readonly SettingFloat DebugDrawHitLength = 0.25f;
    public static readonly SettingColor DebugDrawCircleColor = "#affff1".FromHex();
    public static readonly SettingColor DebugDrawCubeColor = "#bee6e4".FromHex();
    public static readonly SettingColor DebugDrawSphereColor = "#f5df8c".FromHex();
    public static readonly SettingColor DebugDrawArcColor = "#90d3be".FromHex();
    public static readonly SettingColor DebugDrawDiamondColor = "#f5d59a".FromHex();
    public static readonly SettingFloat DebugDrawDiamondSize = 0.5f;
    public static readonly SettingColor DebugDrawConeColor = "#f3ed80".FromHex();
    public static readonly SettingColor DebugDrawBoundsColor = "#f1c59c".FromHex();
    public static readonly SettingColor DebugDrawTextColor = "#f5f5f5".FromHex();
    public static readonly SettingInt DebugDrawTextSize = 24;
  }
  
  /// <summary> Int setting. </summary>
  public class SettingInt : SettingValue<int>
  {
    public SettingInt(int value) : base(value) { }

    public static implicit operator SettingInt(int value) => new(value);
  }

  /// <summary> Float setting. </summary>
  public class SettingFloat : SettingValue<float>
  {
    public SettingFloat(float value) : base(value) { }
    
    public static implicit operator SettingFloat(float value) => new(value);
  }
      
  /// <summary> String setting. </summary>
  public class SettingString : SettingValue<string>
  {
    public SettingString(string value) : base(value) { }
    
    public static implicit operator SettingString(string value) => new(value);
  }

  /// <summary> Color setting. </summary>
  public class SettingColor : SettingValue<Color>
  {
    public SettingColor(Color value) : base(value) { }
    
    public static implicit operator SettingColor(Color value) => new(value);
  }
      
  /// <summary> Generic setting. </summary>
  public abstract class SettingValue<T>
  {
    public string Key { get; private set; }

    public T Value { get; set; }

    public void Reset() => Value = defaultValue;

    private readonly T defaultValue;

    protected SettingValue(T value)
    {
      Key = this.GetType().Name;
      Value = defaultValue = value;
    }

    protected SettingValue() { }
  }  
}
