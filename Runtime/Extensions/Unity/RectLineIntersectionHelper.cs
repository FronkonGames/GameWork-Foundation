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
  /// Finds intersections between a line segment and an axis-aligned rect.
  /// Based on JohannesMP's public-domain implementation.
  /// </summary>
  public static class RectLineIntersectionHelper
  {
    private enum Sector
    {
      __,
      S0, S1, S2,
      S3, S4, S5,
      S6, S7, S8
    }

    private static readonly Sector[,,] RaycastLookup =
    {
      {
        { Sector.__, Sector.__ }, { Sector.__, Sector.__ }, { Sector.__, Sector.__ },
        { Sector.__, Sector.__ }, { Sector.S0, Sector.S4 }, { Sector.S0, Sector.S5 },
        { Sector.__, Sector.__ }, { Sector.S0, Sector.S7 }, { Sector.S0, Sector.S8 },
      },
      {
        { Sector.__, Sector.__ }, { Sector.__, Sector.__ }, { Sector.__, Sector.__ },
        { Sector.S1, Sector.S3 }, { Sector.S1, Sector.S4 }, { Sector.S1, Sector.S5 },
        { Sector.S1, Sector.S6 }, { Sector.S1, Sector.S7 }, { Sector.S1, Sector.S8 },
      },
      {
        { Sector.__, Sector.__ }, { Sector.__, Sector.__ }, { Sector.__, Sector.__ },
        { Sector.S2, Sector.S3 }, { Sector.S2, Sector.S4 }, { Sector.__, Sector.__ },
        { Sector.S2, Sector.S6 }, { Sector.S2, Sector.S7 }, { Sector.__, Sector.__ },
      },
      {
        { Sector.__, Sector.__ }, { Sector.S3, Sector.S1 }, { Sector.S3, Sector.S2 },
        { Sector.__, Sector.__ }, { Sector.S3, Sector.S4 }, { Sector.S3, Sector.S5 },
        { Sector.__, Sector.__ }, { Sector.S3, Sector.S7 }, { Sector.S3, Sector.S8 },
      },
      {
        { Sector.S4, Sector.S0 }, { Sector.S4, Sector.S1 }, { Sector.S4, Sector.S2 },
        { Sector.S4, Sector.S3 }, { Sector.S4, Sector.S4 }, { Sector.S4, Sector.S5 },
        { Sector.S4, Sector.S6 }, { Sector.S4, Sector.S7 }, { Sector.S4, Sector.S8 },
      },
      {
        { Sector.S5, Sector.S0 }, { Sector.S5, Sector.S1 }, { Sector.__, Sector.__ },
        { Sector.S5, Sector.S3 }, { Sector.S5, Sector.S4 }, { Sector.__, Sector.__ },
        { Sector.S5, Sector.S6 }, { Sector.S5, Sector.S7 }, { Sector.__, Sector.__ },
      },
      {
        { Sector.__, Sector.__ }, { Sector.S6, Sector.S1 }, { Sector.S6, Sector.S2 },
        { Sector.__, Sector.__ }, { Sector.S6, Sector.S4 }, { Sector.S6, Sector.S5 },
        { Sector.__, Sector.__ }, { Sector.__, Sector.__ }, { Sector.__, Sector.__ },
      },
      {
        { Sector.S7, Sector.S0 }, { Sector.S7, Sector.S1 }, { Sector.S7, Sector.S2 },
        { Sector.S7, Sector.S3 }, { Sector.S7, Sector.S4 }, { Sector.S7, Sector.S5 },
        { Sector.__, Sector.__ }, { Sector.__, Sector.__ }, { Sector.__, Sector.__ },
      },
      {
        { Sector.S8, Sector.S0 }, { Sector.S8, Sector.S1 }, { Sector.__, Sector.__ },
        { Sector.S8, Sector.S3 }, { Sector.S8, Sector.S4 }, { Sector.__, Sector.__ },
        { Sector.__, Sector.__ }, { Sector.__, Sector.__ }, { Sector.__, Sector.__ },
      },
    };

    /// <summary>
    /// Finds where a line segment enters and exits an axis-aligned rect.
    /// </summary>
    /// <param name="rect"> Target rect. </param>
    /// <param name="begin"> Line start point. </param>
    /// <param name="end"> Line end point. </param>
    /// <returns>
    /// Entry and exit fractions along the segment (0 to 1).
    /// Null means no intersection on that side.
    /// </returns>
    public static (float? entry, float? exit) GetLineIntersections(this Rect rect, Vector2 begin, Vector2 end)
    {
      Vector2 direction = end - begin;
      int sectorBegin = GetRectPointSector(rect, begin);
      int sectorEnd = GetRectPointSector(rect, end);
      float? entry = GetRayToRectSide(begin, direction, RaycastLookup[sectorBegin, sectorEnd, 0], rect, 0.0f);
      float? exit = GetRayToRectSide(begin, direction, RaycastLookup[sectorBegin, sectorEnd, 1], rect, 1.0f);

      return (entry, exit);
    }

    private static int GetRectPointSector(Rect rect, Vector2 point)
    {
      if (point.y > rect.yMax)
      {
        if (point.x < rect.xMin)
          return 0;

        return point.x > rect.xMax ? 2 : 1;
      }

      if (point.y < rect.yMin)
      {
        if (point.x < rect.xMin)
          return 6;

        return point.x > rect.xMax ? 8 : 7;
      }

      if (point.x < rect.xMin)
        return 3;

      return point.x > rect.xMax ? 5 : 4;
    }

    private static float? GetRayToRectSide(Vector2 begin, Vector2 direction, Sector side, Rect rect, float insideValue)
    {
      return side switch
      {
        Sector.S0 => GetRayToRectSide(begin, direction, Sector.S1, rect, insideValue)
          ?? GetRayToRectSide(begin, direction, Sector.S3, rect, insideValue),
        Sector.S1 => RayToHorizontal(begin, direction, rect.xMin, rect.yMax, rect.width),
        Sector.S2 => GetRayToRectSide(begin, direction, Sector.S1, rect, insideValue)
          ?? GetRayToRectSide(begin, direction, Sector.S5, rect, insideValue),
        Sector.S3 => RayToVertical(begin, direction, rect.xMin, rect.yMin, rect.height),
        Sector.S4 => insideValue,
        Sector.S5 => RayToVertical(begin, direction, rect.xMax, rect.yMin, rect.height),
        Sector.S6 => GetRayToRectSide(begin, direction, Sector.S3, rect, insideValue)
          ?? GetRayToRectSide(begin, direction, Sector.S7, rect, insideValue),
        Sector.S7 => RayToHorizontal(begin, direction, rect.xMin, rect.yMin, rect.width),
        Sector.S8 => GetRayToRectSide(begin, direction, Sector.S5, rect, insideValue)
          ?? GetRayToRectSide(begin, direction, Sector.S7, rect, insideValue),
        _ => null
      };
    }

    private static float? RayToHorizontal(Vector2 fromPoint, Vector2 fromDirection, float x, float y, float width)
    {
      if (fromDirection.y == 0.0f)
        return null;

      float fromParam = (y - fromPoint.y) / fromDirection.y;

      if (fromParam < 0.0f || fromParam > 1.0f)
        return null;

      if (fromDirection.x == 0.0f)
        return fromPoint.x >= x && fromPoint.x <= x + width ? fromParam : null;

      float lineParam = (fromPoint.x + fromDirection.x * fromParam - x) / width;

      if (lineParam < 0.0f || lineParam > 1.0f)
        return null;

      return fromParam;
    }

    private static float? RayToVertical(Vector2 fromPoint, Vector2 fromDirection, float x, float y, float height)
    {
      if (fromDirection.x == 0.0f)
        return null;

      float fromParam = (x - fromPoint.x) / fromDirection.x;

      if (fromParam < 0.0f || fromParam > 1.0f)
        return null;

      if (fromDirection.y == 0.0f)
        return fromPoint.y >= y && fromPoint.y <= y + height ? fromParam : null;

      float lineParam = (fromPoint.y + fromDirection.y * fromParam - y) / height;

      if (lineParam < 0.0f || lineParam > 1.0f)
        return null;

      return fromParam;
    }
  }
}
