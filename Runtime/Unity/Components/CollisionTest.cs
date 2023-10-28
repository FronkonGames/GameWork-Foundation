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
using UnityEngine.Events;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Collision tester. </summary>
  public class CollisionTest : BaseMonoBehaviour
  {
    [SerializeField]
    private LayerMask layerFilter = -1;

    [SerializeField]
    private string nameFilter;

    [SerializeField]
    public float velocityFilter = 0.0f;

    [Space, SerializeField]
    private UnityEvent<GameObject, Collision> onCollisionEnter;

    [Space, SerializeField]
    private UnityEvent<GameObject, Collision> onCollisionStay;

    [SerializeField]
    private UnityEvent<GameObject, Collision> onCollisionExit;

    private bool PassFilter(GameObject gameObject)
    {
      if (string.IsNullOrEmpty(nameFilter) == false && string.Compare(nameFilter, gameObject.name) != 0)
        return false;

      return gameObject.layer.IsInLayerMask(layerFilter);
    }

    private void OnCollisionEnter(Collision collision)
    {
      if (PassFilter(collision.gameObject) == true && collision.relativeVelocity.magnitude > velocityFilter)
        onCollisionEnter?.Invoke(gameObject, collision);
    }

    private void OnCollisionStay(Collision collision)
    {
      if (PassFilter(collision.gameObject) == true)
        onCollisionStay?.Invoke(gameObject, collision);
    }

    private void OnCollisionExit(Collision collision)
    {
      if (PassFilter(collision.gameObject) == true)
        onCollisionExit?.Invoke(gameObject, collision);
    }
  }
}
