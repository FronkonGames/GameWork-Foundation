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
    private static void EnsureNormalized(this ref Vector3 vector3)
    {
      float sqrMag = vector3.sqrMagnitude;
      if (Mathf.Approximately(sqrMag, 1.0f))
        return;

      vector3 /= Mathf.Sqrt(sqrMag);
    }

    private static void EnsureNormalized(this ref Vector2 vector2)
    {
      float sqrMag = vector2.sqrMagnitude;
      if (Mathf.Approximately(sqrMag, 1.0f))
        return;

      vector2 /= Mathf.Sqrt(sqrMag);
    }

    private static Vector3 GetAxisAlignedAlternateWhereRequired(Vector3 normal, Vector3 alternate)
    {
      if (Mathf.Abs(Vector3.Dot(normal, alternate)) > 0.999f)
        alternate = GetAxisAlignedAlternate(normal);

      return alternate;
    }

    private static Vector3 GetAxisAlignedAlternate(Vector3 normal)
    {
      Vector3 alternate = new Vector3(0.0f, 0.0f, 1.0f);
      if (Mathf.Abs(Vector3.Dot(normal, alternate)) > 0.707f)
        alternate = new Vector3(0.0f, 1.0f, 0.0f);

      return alternate;
    }

    private static Vector3 GetAxisAlignedPerpendicular(Vector3 normal)
    {
      Vector3 cross = Vector3.Cross(normal, GetAxisAlignedAlternate(normal));
      cross.EnsureNormalized();

      return cross;
    }
  }
}
