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
using System;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Drawing of objects for development.
  /// </summary>
  /// <remarks>Only available in the Editor</remarks>
  public static partial class Draw
  {
    internal readonly struct DrawBoxStructure : IEquatable<DrawBoxStructure>
    {
      public readonly Vector3 UFL, UFR, UBL, UBR, DFL, DFR, DBL, DBR;

      public DrawBoxStructure(Vector3 halfExtents, Quaternion orientation)
      {
        Vector3 up = orientation * new Vector3(0, halfExtents.y, 0.0f),
                right = orientation * new Vector3(halfExtents.x, 0.0f, 0.0f),
                forward = orientation * new Vector3(0.0f, 0.0f, halfExtents.z);

        UFL = up + forward - right;
        UFR = up + forward + right;
        UBL = up - forward - right;
        UBR = up - forward + right;
        DFL = -up + forward - right;
        DFR = -up + forward + right;
        DBL = -up - forward - right;
        DBR = -up - forward + right;
      }

      public bool Equals(DrawBoxStructure other)
      {
        return UFL != other.UFL || UFR != other.UFR || UBL != other.UBL || UFR != other.UFR ||
               DFL != other.DFL || DFR != other.DFR || DBL != other.DBL;
      }
    }    
  }
}
