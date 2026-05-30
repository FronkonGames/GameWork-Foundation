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
  /// <summary> RectTransform extensions. </summary>
  public static class RectTransformExtensions
  {
    /// <summary> Sets the pivot while keeping the RectTransform in the same visual position. </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void SetPivot(this RectTransform rectTransform, Vector2 pivot)
    {
      Vector3 deltaPosition = rectTransform.pivot - pivot;
      deltaPosition.Scale(rectTransform.rect.size);
      deltaPosition.Scale(rectTransform.localScale);
      deltaPosition = rectTransform.rotation * deltaPosition;

      rectTransform.pivot = pivot;
      rectTransform.localPosition -= deltaPosition;
    }

    /// <summary> Sets the left margin (offsetMin.x). </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void SetLeftMargin(this RectTransform rt, float value)
    {
      rt.offsetMin = new Vector2(value, rt.offsetMin.y);
    }

    /// <summary> Sets the right margin (-offsetMax.x). </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void SetRightMargin(this RectTransform rt, float value)
    {
      rt.offsetMax = new Vector2(-value, rt.offsetMax.y);
    }

    /// <summary> Sets the top margin (-offsetMax.y). </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void SetTopMargin(this RectTransform rt, float value)
    {
      rt.offsetMax = new Vector2(rt.offsetMax.x, -value);
    }

    /// <summary> Sets the bottom margin (offsetMin.y). </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void SetBottomMargin(this RectTransform rt, float value)
    {
      rt.offsetMin = new Vector2(rt.offsetMin.x, value);
    }

    /// <summary> Sets all margins. </summary>
    public static void SetAllMargins(this RectTransform rt, float left, float right, float top, float bottom)
    {
      rt.offsetMin = new Vector2(left, bottom);
      rt.offsetMax = new Vector2(-right, -top);
    }

    /// <summary> Sets the width. </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void SetWidth(this RectTransform rt, float width)
    {
      rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    /// <summary> Sets the height. </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void SetHeight(this RectTransform rt, float height)
    {
      rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    /// <summary> Sets both width and height. </summary>
    public static void SetSize(this RectTransform rt, float width, float height)
    {
      rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
      rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    /// <summary> Gets the left margin. </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static float GetLeftMargin(this RectTransform rt) => rt.offsetMin.x;

    /// <summary> Gets the right margin. </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static float GetRightMargin(this RectTransform rt) => -rt.offsetMax.x;

    /// <summary> Gets the top margin. </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static float GetTopMargin(this RectTransform rt) => -rt.offsetMax.y;

    /// <summary> Gets the bottom margin. </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static float GetBottomMargin(this RectTransform rt) => rt.offsetMin.y;

    /// <summary> Gets the width. </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static float GetWidth(this RectTransform rt) => rt.rect.width;

    /// <summary> Gets the height. </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static float GetHeight(this RectTransform rt) => rt.rect.height;

    /// <summary> Returns local-space bounds from the rect corners. </summary>
    public static Bounds ToBounds(this RectTransform rectTransform)
    {
      Vector3[] corners = new Vector3[4];
      rectTransform.GetLocalCorners(corners);

      Bounds bounds = new(corners[0], Vector3.zero);
      for (int i = 1; i < corners.Length; i++)
        bounds.Encapsulate(corners[i]);

      return bounds;
    }

    /// <summary> Returns world-space bounds from the rect corners. </summary>
    public static Bounds ToWorldBounds(this RectTransform rectTransform)
    {
      Vector3[] corners = new Vector3[4];
      rectTransform.GetWorldCorners(corners);

      Bounds bounds = new(corners[0], Vector3.zero);
      for (int i = 1; i < corners.Length; i++)
        bounds.Encapsulate(corners[i]);

      bounds.center = rectTransform.position;
      return bounds;
    }

    /// <summary> Stretches the rect transform to fill its parent. </summary>
    public static void ExpandFullscreen(this RectTransform rectTransform)
    {
      rectTransform.localScale = Vector3.one;
      rectTransform.anchorMin = Vector2.zero;
      rectTransform.anchorMax = Vector2.one;
      rectTransform.offsetMin = Vector2.zero;
      rectTransform.offsetMax = Vector2.zero;
      rectTransform.ForceUpdateRectTransforms();
    }
  }
}
