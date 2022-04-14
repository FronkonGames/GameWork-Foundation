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
  /// GameObject extensions.
  /// </summary>
  public static class GameObjectExtension
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    static public T GetOrAddComponent<T>(this Component self) where T : Component
    {
      T result = self.GetComponent<T>();
      if (result == null)
        result = self.gameObject.AddComponent<T>();

      return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    static public T GetOrAddComponent<T>(this GameObject self) where T : Component => GetOrAddComponent<T>(self.transform);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetComponentInParentIgnoreSelf<T>(this GameObject self) => self.transform.parent.GetComponentInParent<T>();

    /// <summary>
    /// Return a list of all child objects.
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static List<GameObject> GetAllChildren(this GameObject self)
    {
      Transform[] childTransforms = self.GetComponentsInChildren<Transform>();
      List<GameObject> allChildren = new List<GameObject>(childTransforms.Length);

      foreach (Transform child in childTransforms)
      {
        if (child.gameObject != self) allChildren.Add(child.gameObject);
      }

      return allChildren;
    }

    /// <summary>
    /// Return a list of all child objects including itself.
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static List<GameObject> GetAllChildrenAndSelf(this GameObject self)
    {
      Transform[] childTransforms = self.GetComponentsInChildren<Transform>();
      List<GameObject> allChildren = new List<GameObject>(childTransforms.Length);

      for (int transformIndex = 0; transformIndex < childTransforms.Length; ++transformIndex)
        allChildren.Add(childTransforms[transformIndex].gameObject);

      return allChildren;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="go"></param>
    /// <param name="newMat"></param>
    public static void ChangeMaterial(this GameObject go, Material newMat)
    {
      Renderer[] children = go.GetComponentsInChildren<Renderer>(true);
      for (int i = 0; i < children.Length; ++i)
      {
        Renderer rend = children[i];

        Material[] mats = new Material[rend.materials.Length];
        for (int j = 0; j < rend.materials.Length; j++)
          mats[j] = newMat;

        rend.materials = mats;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    public static void DestroyAllChildrens(this Transform self)
    {
      for (var i = self.childCount - 1; i > -1; i--)
        SafeDestroy(self.GetChild(i).gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <typeparam name="T"></typeparam>
    public static void SafeDestroy<T>(T obj) where T : UnityEngine.Object
    {
#if UNITY_EDITOR
      if (Application.isEditor == true)
        UnityEngine.Object.DestroyImmediate(obj);
      else
#endif
      UnityEngine.Object.Destroy(obj);

      obj = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="component"></param>
    /// <typeparam name="T"></typeparam>
    public static void SafeDestroyComponent<T>(T component) where T : Component
    {
      if (component != null)
        SafeDestroy(component.gameObject);
    }

    /// <summary>
    /// .
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject FindAllByName(string name)
    {
      GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
      for (int i = 0; i < allObjects.Length; ++i)
      {
        if (name.Equals(allObjects[i].name) == true)
          return allObjects[i];
      }

      return null;
    }    
  }
}
