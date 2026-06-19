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
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Rect extensions. </summary>
  public static class RectExtensions
  {
    /// <summary> Returns true if the point is inside the rect. </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <returns>True if the point is inside the rect.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(this Rect rect, float x, float y) =>
      x >= rect.xMin && x <= rect.xMax && y >= rect.yMin && y <= rect.yMax;

    /// <summary> Returns true if the point is inside the rect (supports negative width/height). </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="allowInverse">If true, allows rects with negative width/height.</param>
    /// <returns>True if the point is inside the rect.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains(this Rect rect, float x, float y, bool allowInverse)
    {
      if (!allowInverse)
        return x >= rect.xMin && x <= rect.xMax && y >= rect.yMin && y <= rect.yMax;

      float minX = Mathf.Min(rect.x, rect.xMax);
      float maxX = Mathf.Max(rect.x, rect.xMax);
      float minY = Mathf.Min(rect.y, rect.yMax);
      float maxY = Mathf.Max(rect.y, rect.yMax);

      return x >= minX && x <= maxX && y >= minY && y <= maxY;
    }

    /// <summary> Clamps a point to stay inside the rect. </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="point">The point to clamp.</param>
    /// <returns>The clamped point.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ClampPoint(this Rect rect, Vector2 point) =>
      new(Mathf.Clamp(point.x, rect.xMin, rect.xMax), Mathf.Clamp(point.y, rect.yMin, rect.yMax));

    /// <summary> Returns the bottom-left corner. </summary>
    /// <param name="rect">The rect.</param>
    /// <returns>The bottom-left corner.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 BottomLeft(this Rect rect) => new(rect.xMin, rect.yMin);

    /// <summary> Returns the bottom-right corner. </summary>
    /// <param name="rect">The rect.</param>
    /// <returns>The bottom-right corner.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 BottomRight(this Rect rect) => new(rect.xMax, rect.yMin);

    /// <summary> Returns the top-left corner. </summary>
    /// <param name="rect">The rect.</param>
    /// <returns>The top-left corner.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 TopLeft(this Rect rect) => new(rect.xMin, rect.yMax);

    /// <summary> Returns the top-right corner. </summary>
    /// <param name="rect">The rect.</param>
    /// <returns>The top-right corner.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 TopRight(this Rect rect) => new(rect.xMax, rect.yMax);

    /// <summary> Returns the center of the rect. </summary>
    /// <param name="rect">The rect.</param>
    /// <returns>The center.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Center(this Rect rect) => rect.center;

    /// <summary> Returns a new rect scaled from the center. </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="scaleX">The x scale factor.</param>
    /// <param name="scaleY">The y scale factor.</param>
    /// <returns>A new scaled rect.</returns>
    public static Rect Scaled(this Rect rect, float scaleX, float scaleY)
    {
      Vector2 center = rect.center;
      float halfWidth = rect.width * 0.5f * scaleX;
      float halfHeight = rect.height * 0.5f * scaleY;

      return new Rect(center.x - halfWidth, center.y - halfHeight, halfWidth * 2f, halfHeight * 2f);
    }

    /// <summary> Returns a new rect expanded by the given amount on all sides. </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="amount">The amount to expand on each side.</param>
    /// <returns>A new expanded rect.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rect Expanded(this Rect rect, float amount) =>
      new(rect.x - amount, rect.y - amount, rect.width + amount * 2f, rect.height + amount * 2f);

    /// <summary> Returns a copy with x, y, width, and height rounded to the nearest integer. </summary>
    /// <param name="rect"> The source rect. </param>
    /// <returns> A rect with rounded coordinates. </returns>
    public static Rect WithRoundedCoordinates(this Rect rect) =>
      new(Mathf.Round(rect.x), Mathf.Round(rect.y), Mathf.Round(rect.width), Mathf.Round(rect.height));

    /// <summary> Splits the rect vertically into left and right parts. </summary>
    /// <param name="originalRect"> The rect to split. </param>
    /// <param name="cutDistance"> Width of the cut slice. </param>
    /// <param name="fromRightSide"> When true, the cut is taken from the right edge. </param>
    /// <returns> Left and right rects after the cut. </returns>
    public static (Rect leftRect, Rect rightRect) CutVertically(this Rect originalRect, float cutDistance, bool fromRightSide = false)
    {
      Vector2 leftRectPos = originalRect.position;
      Vector2 cutDistanceSize = new(cutDistance, originalRect.height);
      Vector2 leftoverSize = new(originalRect.width - cutDistance, originalRect.height);
      Rect leftRect;
      Rect rightRect;

      if (fromRightSide == true)
      {
        leftRect = new Rect(leftRectPos, leftoverSize);
        rightRect = new Rect(new Vector2(originalRect.x + leftRect.width, originalRect.y), cutDistanceSize);
      }
      else
      {
        leftRect = new Rect(leftRectPos, cutDistanceSize);
        rightRect = new Rect(new Vector2(originalRect.x + leftRect.width, originalRect.y), leftoverSize);
      }

      return (leftRect, rightRect);
    }

    /// <summary> Returns a copy narrowed by horizontal padding on both sides. </summary>
    /// <param name="rect"> The source rect. </param>
    /// <param name="leftPadding"> Padding removed from the left edge. </param>
    /// <param name="rightPadding"> Padding removed from the right edge. </param>
    /// <returns> A padded rect. </returns>
    public static Rect AddHorizontalPadding(this Rect rect, float leftPadding, float rightPadding)
    {
      rect.xMin += leftPadding;
      rect.xMax -= rightPadding;

      return rect;
    }

    /// <summary> Returns a copy vertically centered with the given height. </summary>
    /// <param name="rect"> The source rect. </param>
    /// <param name="height"> Target height. </param>
    /// <returns> A vertically centered rect. </returns>
    public static Rect AlignMiddleVertically(this Rect rect, float height)
    {
      rect.y = rect.y + rect.height * 0.5f - height * 0.5f;
      rect.height = height;

      return rect;
    }
  }
}
