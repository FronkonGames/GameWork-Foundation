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
using NUnit.Framework;
using UnityEngine;
using FronkonGames.GameWork.Foundation;

namespace FronkonGames.GameWork.Foundation.Tests
{
  /// <summary> Matrix4x4 extensions tests. </summary>
  [TestFixture]
  public class Matrix4x4ExtensionsTests
  {
    private const float Tolerance = 0.01f;

    /// <summary> DecomposeTRS recovers TRS components from a TRS matrix. </summary>
    [Test]
    public void DecomposeTRS_RecoversOriginalTransform()
    {
      Vector3 position = new(1.0f, 2.0f, 3.0f);
      Quaternion rotation = Quaternion.Euler(15.0f, 30.0f, 45.0f);
      Vector3 scale = new(2.0f, 3.0f, 4.0f);
      Matrix4x4 matrix = Matrix4x4.TRS(position, rotation, scale);

      (Vector3 decomposedPosition, Quaternion decomposedRotation, Vector3 decomposedScale) = matrix.DecomposeTRS();

      Assert.AreEqual(position.x, decomposedPosition.x, Tolerance);
      Assert.AreEqual(position.y, decomposedPosition.y, Tolerance);
      Assert.AreEqual(position.z, decomposedPosition.z, Tolerance);
      Assert.AreEqual(scale.x, decomposedScale.x, Tolerance);
      Assert.AreEqual(scale.y, decomposedScale.y, Tolerance);
      Assert.AreEqual(scale.z, decomposedScale.z, Tolerance);
      Assert.AreEqual(rotation.x, decomposedRotation.x, Tolerance);
      Assert.AreEqual(rotation.y, decomposedRotation.y, Tolerance);
      Assert.AreEqual(rotation.z, decomposedRotation.z, Tolerance);
      Assert.AreEqual(rotation.w, decomposedRotation.w, Tolerance);
    }
  }
}
