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
using System.Collections.Generic;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Transform extenions.
  /// </summary>
  public static class TransformExt
  {
    /// <summary>
    /// Full name, separating the parents' names with '/'.
    /// </summary>
    /// <param name="self">Transform</param>
    /// <returns>Path</returns>
    public static string GetPath(this Transform self)
    {
      if (self.parent == null)
        return self.name;

      return $"{self.parent.GetPath()}/{self.name}";
    }

    /// <summary>
    /// Find a child by name, recursively.
    /// </summary>
    /// <param name="self">Transform</param>
    /// <param name="name">Name</param>
    /// <returns>Child or null.</returns>    
    public static Transform FindChildRecursive(this Transform self, string name)
    {
      Transform result = self.Find(name);
      if (result != null)
        return result;

      for (int i = 0; i < self.childCount; ++i)
      {
        Transform child = self.GetChild(i);
        result = child.FindChildRecursive(name);
        if (result != null)
          return result;
      }

      return null;
    }

    /// <summary>
    /// Destroy all children.
    /// </summary>
    /// <param name="self"></param>
    public static void DestroyChildren(this Transform self)
    {
      while (self.childCount > 0)
        Object.Destroy(self.GetChild(0));
    }

    /// <summary>
    /// Destroy all children immediately, only advisable in the Editor.
    /// </summary>
    /// <param name="self"></param>
    public static void DestroyChildrenImmediate(this Transform self)
    {
      while (self.childCount > 0)
        Object.DestroyImmediate(self.GetChild(0));
    }

    /// <summary>
    /// Destroy all children immediately, using DestroyImmediate in the Editor and Destroy outside it.
    /// </summary>
    /// <param name="self"></param>
    public static void DestroyChildrenUniversal(this Transform self)
    {
      if (Application.isPlaying == true)
        self.DestroyChildren();
      else
        self.DestroyChildrenImmediate();
    }

    /// <summary>
    /// All their children.
    /// </summary>
    /// <param name="self">Transform</param>
    /// <returns>List with children.</returns>
    public static List<Transform> GetChildren(this Transform self)
    {
      List<Transform> children = new();

      for (var i = 0; i < self.childCount; ++i)
        children.Add(self.GetChild(i));

      return children;
    }

    /// <summary>
    /// Sets the X-axis.
    /// </summary>
    /// <param name="self">Transform</param>
    /// <param name="x">X axis</param>
    public static void SetX(this Transform self, float x) => self.position = new Vector3(x, self.position.y, self.position.z);

    /// <summary>
    /// Sets the Y-axis.
    /// </summary>
    /// <param name="self">Transform</param>
    /// <param name="y">Y axis.</param>
    public static void SetY(this Transform self, float y) => self.position = new Vector3(self.position.x, y, self.position.z);

    /// <summary>
    /// Sets the Z-axis.
    /// </summary>
    /// <param name="self">Transform</param>
    /// <param name="z">Z axis</param>
    public static void SetZ(this Transform self, float z) => self.position = new Vector3(self.position.x, self.position.y, z);

    /// <summary>
    /// Sets the XY-axis.
    /// </summary>
    /// <param name="self">Transform</param>
    /// <param name="x">X axis</param>
    /// <param name="y">Y axis</param>
    public static void SetXY(this Transform self, float x, float y) => self.position = new Vector3(x, y, self.position.z);

    /// <summary>
    /// Sets the XZ-axis.
    /// </summary>
    /// <param name="self">Transform</param>
    /// <param name="x">X axis</param>
    /// <param name="y">Z axis</param>
    public static void SetXZ(this Transform self, float x, float z) => self.position = new Vector3(x, self.position.y, z);

    /// <summary>
    /// Sets the YZ-axis.
    /// </summary>
    /// <param name="self">Transform</param>
    /// <param name="x">Y axis</param>
    /// <param name="y">Z axis</param>
    public static void SetYZ(this Transform self, float y, float z) => self.position = new Vector3(self.position.x, y, z);

    /// <summary>
    /// Sets the XYZ-axis.
    /// </summary>
    /// <param name="self">Transform</param>
    /// <param name="x">X axis</param>
    /// <param name="y">Y axis</param>
    /// <param name="z">Z axis</param>
    public static void SetXYZ(this Transform self, float x, float y, float z) => self.position = new Vector3(x, y, z);

    /// <summary>
    /// Translates this transform along the X axis.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="local"></param>
    public static void TranslateX(this Transform self, float x, bool local = false) => self.TranslateXYZ(x, 0, 0, local);

    /// <summary>
    /// Translates this transform along the Y axis.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="y"></param>
    /// <param name="local"></param>
    public static void TranslateY(this Transform self, float y, bool local = false) => self.TranslateXYZ(0, y, 0, local);

    /// <summary>
    /// Translates this transform along the Z axis.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="z"></param>
    /// <param name="local"></param>
    public static void TranslateZ(this Transform self, float z, bool local = false) => self.TranslateXYZ(0, 0, z, local);

    /// <summary>
    /// Translates this transform along the X and Y axes.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="local"></param>
    public static void TranslateXY(this Transform self, float x, float y, bool local = false) => self.TranslateXYZ(x, y, 0, local);

    /// <summary>
    /// Translates this transform along the X and Z axes.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="local"></param>
    public static void TranslateXZ(this Transform self, float x, float z, bool local = false) => self.TranslateXYZ(x, 0, z, local);

    /// <summary>
    /// Translates this transform along the Y and Z axes.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="local"></param>
    public static void TranslateYZ(this Transform self, float y, float z, bool local = false) => self.TranslateXYZ(0, y, z, local);

    /// <summary>
    /// Translates this transform along the X, Y and Z axis.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="local"></param>
    public static void TranslateXYZ(this Transform self, float x, float y, float z, bool local = false)
    {
      Vector3 offset = new Vector3(x, y, z);

      if (local == true)
        self.localPosition += offset;
      else
        self.position += offset;
    }

    /// <summary>
    /// Sets the local X position of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    public static void SetLocalX(this Transform self, float x) => self.localPosition = new Vector3(x, self.localPosition.y, self.localPosition.z);

    /// <summary>
    /// Sets the local Y position of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="y"></param>
    public static void SetLocalY(this Transform self, float y) => self.localPosition = new Vector3(self.localPosition.x, y, self.localPosition.z);

    /// <summary>
    /// Sets the local Z position of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="z"></param>
    public static void SetLocalZ(this Transform self, float z) => self.localPosition = new Vector3(self.localPosition.x, self.localPosition.y, z);

    /// <summary>
    /// Sets the local X and Y position of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void SetLocalXY(this Transform self, float x, float y) => self.localPosition = new Vector3(x, y, self.localPosition.z);

    /// <summary>
    /// Sets the local X and Z position of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static void SetLocalXZ(this Transform self, float x, float z) => self.localPosition = new Vector3(x, self.localPosition.z, z);

    /// <summary>
    /// Sets the local Y and Z position of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void SetLocalYZ(this Transform self, float y, float z) => self.localPosition = new Vector3(self.localPosition.x, y, z);

    /// <summary>
    /// Sets the local X, Y and Z position of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void SetLocalXYZ(this Transform self, float x, float y, float z) => self.localPosition = new Vector3(x, y, z);

    /// <summary>
    /// Sets the local X scale of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    public static void SetScaleX(this Transform self, float x) => self.localScale = new Vector3(x, self.localScale.y, self.localScale.z);

    /// <summary>
    /// Sets the local Y scale of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="y"></param>
    public static void SetScaleY(this Transform self, float y) => self.localScale = new Vector3(self.localScale.x, y, self.localScale.z);

    /// <summary>
    /// Sets the local Z scale of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="z"></param>
    public static void SetScaleZ(this Transform self, float z) => self.localScale = new Vector3(self.localScale.x, self.localScale.y, z);

    /// <summary>
    /// Sets the local X and Y scale of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void SetScaleXY(this Transform self, float x, float y) => self.localScale = new Vector3(x, y, self.localScale.z);

    /// <summary>
    /// Sets the local X and Z scale of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static void SetScaleXZ(this Transform self, float x, float z) => self.localScale = new Vector3(x, self.localScale.y, z);

    /// <summary>
    /// Sets the local Y and Z scale of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void SetScaleYZ(this Transform self, float y, float z) => self.localScale = new Vector3(self.localScale.x, y, z);

    /// <summary>
    /// Sets the local X, Y and Z scale of this transform.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void SetScaleXYZ(this Transform self, float x, float y, float z) => self.localScale = new Vector3(x, y, z);

    /// <summary>
    /// Scale this transform in the X direction.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    public static void ScaleByX(this Transform self, float x) => self.localScale = new Vector3(self.localScale.x * x, self.localScale.y, self.localScale.z);

    /// <summary>
    /// Scale this transform in the Y direction.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="y"></param>
    public static void ScaleByY(this Transform self, float y) => self.localScale = new Vector3(self.localScale.x, self.localScale.y * y, self.localScale.z);

    /// <summary>
    /// Scale this transform in the Z direction.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="z"></param>
    public static void ScaleByZ(this Transform self, float z) => self.localScale = new Vector3(self.localScale.x, self.localScale.y, self.localScale.z * z);

    /// <summary>
    /// Scale this transform in the X, Y direction.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void ScaleByXY(this Transform self, float x, float y) => self.localScale = new Vector3(self.localScale.x * x, self.localScale.y * y, self.localScale.z);

    /// <summary>
    /// Scale this transform in the X, Z directions.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static void ScaleByXZ(this Transform self, float x, float z)
    {
      self.localScale = new Vector3(self.localScale.x * x, self.localScale.y, self.localScale.z * z);
    }

    /// <summary>
    /// Scale this transform in the Y and Z directions.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void ScaleByYZ(this Transform self, float y, float z) => self.localScale = new Vector3(self.localScale.x, self.localScale.y * y, self.localScale.z * z);

    /// <summary>
    /// Scale this transform in the X and Y directions.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="r"></param>
    public static void ScaleByXY(this Transform self, float r) => self.ScaleByXY(r, r);

    /// <summary>
    /// Scale this transform in the X and Z directions.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="r"></param>
    public static void ScaleByXZ(this Transform self, float r) => self.ScaleByXZ(r, r);

    /// <summary>
    /// Scale this transform in the Y and Z directions.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="r"></param>
    public static void ScaleByYZ(this Transform self, float r) => self.ScaleByYZ(r, r);

    /// <summary>
    /// Scale this transform in the X, Y and Z directions.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static void ScaleByXYZ(this Transform self, float x, float y, float z) => self.localScale = new Vector3(x, y, z);

    /// <summary>
    /// Scale this transform in the X, Y and Z directions.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="r"></param>
    public static void ScaleByXYZ(this Transform self, float r) => self.ScaleByXYZ(r, r, r);

    /// <summary>
    /// Negates the X scale.
    /// </summary>
    /// <param name="self"></param>
    public static void FlipX(this Transform self) => self.SetScaleX(-self.localScale.x);

    /// <summary>
    /// Negates the Y scale.
    /// </summary>
    /// <param name="self"></param>
    public static void FlipY(this Transform self) => self.SetScaleY(-self.localScale.y);

    /// <summary>
    /// Negates the Z scale.
    /// </summary>
    /// <param name="self"></param>
    public static void FlipZ(this Transform self) => self.SetScaleZ(-self.localScale.z);

    /// <summary>
    /// Negates the X and Y scale.
    /// </summary>
    /// <param name="self"></param>
    public static void FlipXY(this Transform self) => self.SetScaleXY(-self.localScale.x, -self.localScale.y);

    /// <summary>
    /// Negates the X and Z scale.
    /// </summary>
    /// <param name="self"></param>
    public static void FlipXZ(this Transform self) => self.SetScaleXZ(-self.localScale.x, -self.localScale.z);

    /// <summary>
    /// Negates the Y and Z scale.
    /// </summary>
    /// <param name="self"></param>
    public static void FlipYZ(this Transform self) => self.SetScaleYZ(-self.localScale.y, -self.localScale.z);

    /// <summary>
    /// Negates the X, Y and Z scale.
    /// </summary>
    /// <param name="self"></param>
    public static void FlipXYZ(this Transform self) => self.SetScaleXYZ(-self.localScale.z, -self.localScale.y, -self.localScale.z);

    /// <summary>
    /// Sets all scale values to the absolute values.
    /// </summary>
    /// <param name="self"></param>
    public static void FlipPostive(this Transform self) => self.localScale = new Vector3(Mathf.Abs(self.localScale.x),
                                                                                         Mathf.Abs(self.localScale.y),
                                                                                         Mathf.Abs(self.localScale.z));

    /// <summary>
    /// Resets the local scale of this transform in to 1 1 1.
    /// </summary>
    /// <param name="self"></param>
    public static void ResetScale(this Transform self) => self.localScale = Vector3.one;

    /// <summary>
    /// Rotates the transform around the X axis.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="angle"></param>
    public static void RotateAroundX(this Transform self, float angle) => self.Rotate(new Vector3(angle, 0, 0));

    /// <summary>
    /// Rotates the transform around the Y axis.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="angle"></param>
    public static void RotateAroundY(this Transform self, float angle) => self.Rotate(new Vector3(0, angle, 0));

    /// <summary>
    /// Rotates the transform around the Z axis.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="angle"></param>
    public static void RotateAroundZ(this Transform self, float angle) => self.Rotate(new Vector3(0, 0, angle));

    /// <summary>
    /// Sets the X rotation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="angle"></param>
    public static void SetRotationX(this Transform self, float angle) => self.eulerAngles = new Vector3(angle, 0, 0);

    /// <summary>
    /// Sets the Y rotation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="angle"></param>
    public static void SetRotationY(this Transform self, float angle) => self.eulerAngles = new Vector3(0, angle, 0);

    /// <summary>
    /// Adds a modifier to the current y rotation
    /// </summary>
    /// <param name="self"></param>
    /// <param name="y"></param>
    public static void AddYRotation(this Transform self, float y) => self.eulerAngles = new Vector3(self.eulerAngles.x,
                                                                                                  self.eulerAngles.y + y,
                                                                                                    self.eulerAngles.z);

    /// <summary>
    /// Sets the Z rotation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="angle"></param>
    public static void SetRotationZ(this Transform self, float angle) => self.eulerAngles = new Vector3(0, 0, angle);

    /// <summary>
    /// Sets the local X rotation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="angle"></param>
    public static void SetLocalRotationX(this Transform self, float angle) => self.localRotation = Quaternion.Euler(new Vector3(angle, 0, 0));

    /// <summary>
    /// Sets the local Y rotation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="angle"></param>
    public static void SetLocalRotationY(this Transform self, float angle) => self.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));

    /// <summary>
    /// Sets the local Z rotation.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="angle"></param>
    public static void SetLocalRotationZ(this Transform self, float angle) => self.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));

    /// <summary>
    /// Resets the rotation to 0, 0, 0.
    /// </summary>
    /// <param name="self"></param>
    public static void ResetRotation(this Transform self) => self.rotation = Quaternion.identity;

    /// <summary>
    /// Resets the local rotation to 0, 0, 0.
    /// </summary>
    /// <param name="self"></param>
    public static void ResetLocalRotation(this Transform self) => self.localRotation = Quaternion.identity;
    
    /// <summary>
    /// .
    /// </summary>
    /// <param name="trans"></param>
    public static void ResetWorld(this Transform self)
    {
      self.position = Vector3.zero;
      self.rotation = Quaternion.Euler(Vector3.zero);
      self.localScale = Vector3.one;
    }

    /// <summary>
    /// .
    /// </summary>
    /// <param name="self"></param>
    public static void ResetLocal(this Transform self)
    {
      self.localPosition = Vector3.zero;
      self.localRotation = Quaternion.Euler(Vector3.zero);
      self.localScale = Vector3.one;
    }

    /// <summary>
    /// .
    /// </summary>
    /// <param name="self"></param>
    public static void ResetRect(this RectTransform self)
    {
      self.localPosition = Vector3.zero;
      self.localRotation = Quaternion.Euler(Vector3.zero);
      self.localScale = Vector3.one;
      self.offsetMax = Vector2.zero;
      self.offsetMin = Vector2.zero;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Transform[] GetAllChildren(this Transform self)
    {
      Transform[] children = new Transform[self.childCount];
      for (int i = 0; i < self.childCount; ++i)
        children[i] = self.GetChild(i);

      return children;
    }    
  }
}
